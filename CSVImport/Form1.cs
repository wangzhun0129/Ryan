using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace CSVImport
{
    public partial class Form1 : Form
    {
        //sql连接字符串
        public string StrCon
        {
            get
            {
                return m_strCon;
            }
            set
            {
                m_strCon = value;
            }
        }

        //服务器名称
        public string StrServer
        {
            get
            {
                return m_strServer;
            }
            set
            {
                m_strServer = value;
            }
        }

        //数据库名称
        public string StrDbName
        {
            get
            {
                return m_strDbName;
            }
            set
            {
                m_strDbName = value;
            }
        }

        private string m_strServer = "";
        private string m_strDbName = "";
        private string m_strCon = "";
        private string m_strCsvFolder = "";        

        public Form1()
        {
            InitializeComponent();

            this.btnConnectSqlServer.Click += new EventHandler(btnConnectSqlServer_Click);
            this.btnSelFolder.Click += new EventHandler(btnSelFolder_Click);
            this.btnImport.Click += new EventHandler(btnImport_Click);
            this.richTextBox1.Visible = false;
            this.progressBar1.Visible = false;
        }


        //csv导入
        void btnImport_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(m_strCsvFolder))
            {
                MessageBox.Show("请选择csv目录！");
                return;
            }
            if(String.IsNullOrEmpty(m_strDbName) || String.IsNullOrEmpty(m_strServer))
            {
                MessageBox.Show("请连接数据库！");
                return;
            }
            SqlConnection sqlCon = null;
            try
            {                
                //获取目录下的csv文件
                this.richTextBox1.Visible = true;
                this.richTextBox1.Text = "正在搜索当前目录下包含的csv文件，可能需要较长时间，请耐心等待...";
                System.Windows.Forms.Application.DoEvents();
                string[] strCSVArray = Directory.GetFiles(m_strCsvFolder);
                if (strCSVArray.Length == 0)
                {
                    MessageBox.Show("当前目录下不包含任何csv文件！");
                    return;
                }

                sqlCon = new SqlConnection(m_strCon);
                sqlCon.Open();

                //清空数据库下所有的表
                string strSql = "select name from sysobjects WHERE xtype='U'";
                SqlCommand cmd = new SqlCommand(strSql, sqlCon);
                SqlDataReader oddr = cmd.ExecuteReader();
                List<string> lstTables = new List<string>();
                while (oddr.HasRows && oddr.Read())
                {
                    string strTale = oddr.GetString(0).ToLower();
                    lstTables.Add(strTale);
                }
                oddr.Close();

                foreach (string tb in lstTables)
                {
                    strSql = "drop table "+tb;
                    cmd = new SqlCommand(strSql, sqlCon);
                    cmd.ExecuteNonQuery();
                }
                
                string strCsvFileName = "";
                string strpattern = "";
                string strpath = "";
                string strDb = "";
                if (this.checkPOI.Checked || this.checkPOI_Tel.Checked)
                {
                    strDb = "MnSoft_POI";
                }
                if (this.checkText.Checked)
                {
                    strDb = "MnSoft_Text";
                }

                strpath = strCSVArray[0];
                strCsvFileName = Path.GetFileName(strpath);
                strpattern = strCsvFileName.Substring(strCsvFileName.Length - 3, 3);
                if (strpattern == "mdb")
                {
                    strSql = "exec sp_configure 'show advanced options',1";
                    strSql += " reconfigure";
                    strSql += " exec sp_configure 'Ad Hoc Distributed Queries',1";
                    strSql += " reconfigure";
                    cmd = new SqlCommand(strSql, sqlCon);
                    cmd.ExecuteNonQuery();
                }
                if(this.chkAll.Checked)
                {
                    if (strpattern == "csv")
                    {
                        //创建table
                        strSql = "CREATE TABLE MnSoft_POI(POI_ID nvarchar(50),CP_ID nvarchar(50),Entity_ID nvarchar(255),PARENT_ID nvarchar(255),POI_CODE nvarchar(50),";
                        strSql += "LARGE_CD nvarchar(255),MIDDLE_CD nvarchar(255),SMALL_CD nvarchar(255),FNAME nvarchar(255),";
                        strSql += "ENAME nvarchar(255),ANAME nvarchar(255),CNAME nvarchar(255),PNAME nvarchar(255),Zip_Code nvarchar(255),";
                        strSql += "ADDR nvarchar(255),ADDR_PYN nvarchar(255),ADDR2 nvarchar(255),ADDR2_PYN nvarchar(255),PRI_BUN nvarchar(255),";
                        strSql += "SEC_BUN nvarchar(255),TELE_A nvarchar(255),TELE_B nvarchar(255),TELE_C nvarchar(255),TELECODE nvarchar(255),Center_X nvarchar(100),Center_Y nvarchar(100),";
                        strSql += "Guide_X nvarchar(100),Guide_Y nvarchar(100),NT_CODE nvarchar(50),[NAICS/SUB_CODE] nvarchar(50),CHAIN_CODE nvarchar(50),CUISION_CODE nvarchar(50),REGIONAL_FOOD_CODE nvarchar(50),";
                        strSql += "PRI_BUN_TEMP nvarchar(255),GMAP_ID nvarchar(255),GMAP_Flag char(10),EDGE_ID nvarchar(255),DIRECTION nvarchar(255),";
                        strSql += "ABS_ID nvarchar(255),ABS_DIRECTION nvarchar(255),ADDR_Flag char(10),DIFFNAME_Flag char(10),AIRPORT_Flag char(10),STREET_TYPE_Flag char(10),";
                        strSql += "ZAGAT_Flag char(10),Z_Food char(10),Z_Decor char(10),Z_Service char(10),Z_Cost nvarchar(50),PA_NAME nvarchar(255),KA_NAME nvarchar(255),KD_NAME nvarchar(255),FILE_NAME nvarchar(255))";
                        cmd = new SqlCommand(strSql, sqlCon);
                        cmd.ExecuteNonQuery();
                    }                                      
                }

                //遍历所有csv文件 将其导入数据库
                this.progressBar1.Visible = true;
                this.progressBar1.Value = 0;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = strCSVArray.Length;
                foreach (string s in strCSVArray)
                {
                    this.richTextBox1.Text = "正在导入:" + s;
                    System.Windows.Forms.Application.DoEvents();
                    strCsvFileName = Path.GetFileName(s);
                    strpattern = strCsvFileName.Substring(strCsvFileName.Length - 3,3);
                    //创建table
                    if (!this.chkAll.Checked)
                    {
                        strDb = strCsvFileName.Substring(0, strCsvFileName.Length - 4);
                        if (strpattern == "csv")
                        {
                            strSql = "CREATE TABLE " + strDb + "(POI_ID nvarchar(50),CP_ID nvarchar(50),Entity_ID nvarchar(255),PARENT_ID nvarchar(255),POI_CODE nvarchar(50),";
                            strSql += "LARGE_CD nvarchar(255),MIDDLE_CD nvarchar(255),SMALL_CD nvarchar(255),FNAME nvarchar(255),";
                            strSql += "ENAME nvarchar(255),ANAME nvarchar(255),CNAME nvarchar(255),PNAME nvarchar(255),Zip_Code nvarchar(255),";
                            strSql += "ADDR nvarchar(255),ADDR_PYN nvarchar(255),ADDR2 nvarchar(255),ADDR2_PYN nvarchar(255),PRI_BUN nvarchar(255),";
                            strSql += "SEC_BUN nvarchar(255),TELE_A nvarchar(255),TELE_B nvarchar(255),TELE_C nvarchar(255),TELECODE nvarchar(255),Center_X nvarchar(100),Center_Y nvarchar(100),";
                            strSql += "Guide_X nvarchar(100),Guide_Y nvarchar(100),NT_CODE nvarchar(50),[NAICS/SUB_CODE] nvarchar(50),CHAIN_CODE nvarchar(50),CUISION_CODE nvarchar(50),REGIONAL_FOOD_CODE nvarchar(50),";
                            strSql += "PRI_BUN_TEMP nvarchar(255),GMAP_ID nvarchar(255),GMAP_Flag char(10),EDGE_ID nvarchar(255),DIRECTION nvarchar(255),";
                            strSql += "ABS_ID nvarchar(255),ABS_DIRECTION nvarchar(255),ADDR_Flag char(10),DIFFNAME_Flag char(10),AIRPORT_Flag char(10),STREET_TYPE_Flag char(10),";
                            strSql += "ZAGAT_Flag char(10),Z_Food char(10),Z_Decor char(10),Z_Service char(10),Z_Cost nvarchar(50),PA_NAME nvarchar(255),KA_NAME nvarchar(255),KD_NAME nvarchar(255),FILE_NAME nvarchar(255))";
                            cmd = new SqlCommand(strSql, sqlCon);
                            cmd.ExecuteNonQuery();
                        }                       
                        
                    }
                    //导入
                    try
                    {
                        if (strpattern == "csv")
                        {
                            strSql = "BULK INSERT " + strDb + " from '" + s + "' WITH ( FIELDTERMINATOR = ',',firstrow=2)";
                            cmd = new SqlCommand(strSql, sqlCon);
                            cmd.CommandTimeout = 0;
                            cmd.ExecuteNonQuery();
                        }
                        else if (strpattern == "mdb")
                        {
                            if (this.checkPOI.Checked)
                            {
                                if (!this.chkAll.Checked)
                                {
                                    strSql = "select * into " + strDb + " from OpenDataSource";
                                    strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...POI_I_COMMON";
                                    cmd = new SqlCommand(strSql, sqlCon);
                                    cmd.CommandTimeout = 0;
                                    cmd.ExecuteNonQuery();
                                }
                                if (this.chkAll.Checked)
                                {
                                    if (this.progressBar1.Value == 0)
                                    {
                                        strSql = "select * into " + strDb + " from OpenDataSource";
                                        strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...POI_I_COMMON";
                                        cmd = new SqlCommand(strSql, sqlCon);
                                        cmd.CommandTimeout = 0;
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (this.progressBar1.Value > 0)
                                    {
                                        strSql = "insert into " + strDb + " select * from OpenDataSource";
                                        strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...POI_I_COMMON";
                                        cmd = new SqlCommand(strSql, sqlCon);
                                        cmd.CommandTimeout = 0;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (this.checkPOI_Tel.Checked)
                            {
                                if (!this.chkAll.Checked)
                                {
                                    strSql = "select * into " + strDb + " from OpenDataSource";
                                    strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...POI_I_COMMON_TEL";
                                    cmd = new SqlCommand(strSql, sqlCon);
                                    cmd.CommandTimeout = 0;
                                    cmd.ExecuteNonQuery();
                                }
                                if (this.chkAll.Checked)
                                {
                                    if (this.progressBar1.Value == 0)
                                    {
                                        strSql = "select * into " + strDb + " from OpenDataSource";
                                        strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...POI_I_COMMON_TEL";
                                        cmd = new SqlCommand(strSql, sqlCon);
                                        cmd.CommandTimeout = 0;
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (this.progressBar1.Value > 0)
                                    {
                                        strSql = "insert into " + strDb + " select * from OpenDataSource";
                                        strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...POI_I_COMMON_TEL";
                                        cmd = new SqlCommand(strSql, sqlCon);
                                        cmd.CommandTimeout = 0;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (this.checkText.Checked)
                            {
                                if (!this.chkAll.Checked)
                                {
                                    strSql = "select * into " + strDb + " from OpenDataSource";
                                    strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...TXT_4";
                                    cmd = new SqlCommand(strSql, sqlCon);
                                    cmd.CommandTimeout = 0;
                                    cmd.ExecuteNonQuery();
                                }
                                if (this.chkAll.Checked)
                                {
                                    if (this.progressBar1.Value == 0)
                                    {
                                        strSql = "select * into " + strDb + " from OpenDataSource";
                                        strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...TXT_4";
                                        cmd = new SqlCommand(strSql, sqlCon);
                                        cmd.CommandTimeout = 0;
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (this.progressBar1.Value > 0)
                                    {
                                        strSql = "insert into " + strDb + " select * from OpenDataSource";
                                        strSql += " ('Microsoft.Jet.OLEDB.4.0','Data Source=" + s + ";')...TXT_4";
                                        cmd = new SqlCommand(strSql, sqlCon);
                                        cmd.CommandTimeout = 0;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        else
                        { }
                        
                    }
                    catch (Exception exx)
                    {
                        MessageBox.Show(exx.Message);
                    }

                    this.progressBar1.Value++;
                }
                //压缩数据库
                CompressDB(sqlCon, m_strDbName);
                MessageBox.Show("导入完毕");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                string strSql = "exec sp_configure 'Ad Hoc Distributed Queries',0";
                strSql += " reconfigure";
                strSql += " exec sp_configure 'show advanced options',0";
                strSql += " reconfigure";
                SqlCommand cmd = new SqlCommand(strSql, sqlCon);
                cmd = new SqlCommand(strSql, sqlCon);
                cmd.ExecuteNonQuery();
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
                this.progressBar1.Visible = false;
                this.richTextBox1.Visible = false;
            }
        }

        //压缩数据库
        private void CompressDB(SqlConnection sqlCon, string strDBName)
        {
            string strSql = "dbcc shrinkdatabase (\"" + strDBName + "\")";
            SqlCommand cmd = new SqlCommand(strSql, sqlCon);
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
        }

        //选择csv目录
        void btnSelFolder_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_strCsvFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBox1.Text = m_strCsvFolder;
            }
        }

        //连接SQLServer
        void btnConnectSqlServer_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(this);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txtSqlServer.Text = m_strServer;
                this.txtDbName.Text = m_strDbName;
            }
        }

    }
}

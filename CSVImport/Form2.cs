using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSVImport
{
    public partial class Form2 : Form
    {
        Form1 parent;
        private Dictionary<string, Dictionary<string, string>> m_dicTables = new Dictionary<string, Dictionary<string, string>>();
        public Form2()
        {
            InitializeComponent();
            this.btnConnect.Click += new EventHandler(btnConnect_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);            
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strCon = "Data Source=" + this.txtIp.Text + ";Initial Catalog=" + this.comboBox1.Text + ";user id=sa;password=sa";
        }

        public Form2(Form1 frm)
            : this()
        {
            parent = frm;
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            string strServerName = this.txtIp.Text;
            if (String.IsNullOrEmpty(strServerName))
            {
                MessageBox.Show("请输入数据库服务器名称！");
                return;
            }
            if(this.comboBox1.Items.Count == 0)
            {
                MessageBox.Show("当前数据库服务器不包含任何数据库！");
                return;
            }

            string strCon = "Data Source=" + strServerName + ";Initial Catalog=" + this.comboBox1.Text + ";user id=sa;password=sa";
            parent.StrCon = strCon;
            parent.StrDbName = this.comboBox1.Text;
            parent.StrServer = strServerName;            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void btnConnect_Click(object sender, EventArgs e)
        {
            this.comboBox1.Items.Clear();
            string strServerName = this.txtIp.Text;
            if (String.IsNullOrEmpty(strServerName))
            {
                MessageBox.Show("请输入数据库服务器名称！");
                return;
            }
            SqlConnection sqlCon = null;
            try
            {
                string strCon = "Data Source=" + strServerName + ";Initial Catalog=master;user id=sa;password=sa";
                sqlCon = new SqlConnection(strCon);
                sqlCon.Open();
                string strSql = "select name from master.sys.databases";
                SqlCommand cmd = new SqlCommand(strSql,sqlCon);
                SqlDataReader sqlReader = cmd.ExecuteReader();
                while (sqlReader.HasRows && sqlReader.Read())
                {
                    string strDbName = sqlReader.GetSqlString(0).ToString();
                    if (strDbName == "master" || strDbName == "tempdb" || strDbName == "model" || strDbName == "msdb")
                    {
                        continue;
                    }
                    this.comboBox1.Items.Add(strDbName);
                }
                this.comboBox1.SelectedIndex = 0;
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("连接当前数据库服务器失败！");
            }
            finally
            {
                if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
    }
}

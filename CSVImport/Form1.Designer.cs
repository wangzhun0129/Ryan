namespace CSVImport
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label3 = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConnectSqlServer = new System.Windows.Forms.Button();
            this.txtSqlServer = new System.Windows.Forms.TextBox();
            this.btnSelFolder = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnImport = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.checkPOI = new System.Windows.Forms.CheckBox();
            this.checkPOI_Tel = new System.Windows.Forms.CheckBox();
            this.checkText = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 49;
            this.label3.Text = "数据库名称:";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(287, 39);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.ReadOnly = true;
            this.txtDbName.Size = new System.Drawing.Size(100, 21);
            this.txtDbName.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 47;
            this.label2.Text = "服务器名称:";
            // 
            // btnConnectSqlServer
            // 
            this.btnConnectSqlServer.Location = new System.Drawing.Point(12, 12);
            this.btnConnectSqlServer.Name = "btnConnectSqlServer";
            this.btnConnectSqlServer.Size = new System.Drawing.Size(148, 23);
            this.btnConnectSqlServer.TabIndex = 46;
            this.btnConnectSqlServer.Text = "连接SqlServer数据库";
            this.btnConnectSqlServer.UseVisualStyleBackColor = true;
            // 
            // txtSqlServer
            // 
            this.txtSqlServer.Location = new System.Drawing.Point(84, 40);
            this.txtSqlServer.Name = "txtSqlServer";
            this.txtSqlServer.ReadOnly = true;
            this.txtSqlServer.Size = new System.Drawing.Size(113, 21);
            this.txtSqlServer.TabIndex = 45;
            // 
            // btnSelFolder
            // 
            this.btnSelFolder.Location = new System.Drawing.Point(347, 67);
            this.btnSelFolder.Name = "btnSelFolder";
            this.btnSelFolder.Size = new System.Drawing.Size(40, 23);
            this.btnSelFolder.TabIndex = 52;
            this.btnSelFolder.Text = "...";
            this.btnSelFolder.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(132, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(207, 21);
            this.textBox1.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 50;
            this.label1.Text = "请选择csv或mdb目录:";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 162);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(395, 10);
            this.progressBar1.TabIndex = 53;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(302, 91);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(85, 23);
            this.btnImport.TabIndex = 54;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(224, 95);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(72, 16);
            this.chkAll.TabIndex = 55;
            this.chkAll.Text = "合并导入";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(0, 118);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(395, 44);
            this.richTextBox1.TabIndex = 56;
            this.richTextBox1.Text = "";
            // 
            // checkPOI
            // 
            this.checkPOI.AutoSize = true;
            this.checkPOI.Location = new System.Drawing.Point(14, 95);
            this.checkPOI.Name = "checkPOI";
            this.checkPOI.Size = new System.Drawing.Size(42, 16);
            this.checkPOI.TabIndex = 57;
            this.checkPOI.Text = "POI";
            this.checkPOI.UseVisualStyleBackColor = true;
            // 
            // checkPOI_Tel
            // 
            this.checkPOI_Tel.AutoSize = true;
            this.checkPOI_Tel.Location = new System.Drawing.Point(62, 95);
            this.checkPOI_Tel.Name = "checkPOI_Tel";
            this.checkPOI_Tel.Size = new System.Drawing.Size(66, 16);
            this.checkPOI_Tel.TabIndex = 58;
            this.checkPOI_Tel.Text = "POI_TEL";
            this.checkPOI_Tel.UseVisualStyleBackColor = true;
            // 
            // checkText
            // 
            this.checkText.AutoSize = true;
            this.checkText.Location = new System.Drawing.Point(149, 95);
            this.checkText.Name = "checkText";
            this.checkText.Size = new System.Drawing.Size(48, 16);
            this.checkText.TabIndex = 59;
            this.checkText.Text = "Text";
            this.checkText.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 172);
            this.Controls.Add(this.checkText);
            this.Controls.Add(this.checkPOI_Tel);
            this.Controls.Add(this.checkPOI);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSelFolder);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConnectSqlServer);
            this.Controls.Add(this.txtSqlServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MnSoft-CSV导入SQLServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnectSqlServer;
        private System.Windows.Forms.TextBox txtSqlServer;
        private System.Windows.Forms.Button btnSelFolder;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox checkPOI;
        private System.Windows.Forms.CheckBox checkPOI_Tel;
        private System.Windows.Forms.CheckBox checkText;
    }
}


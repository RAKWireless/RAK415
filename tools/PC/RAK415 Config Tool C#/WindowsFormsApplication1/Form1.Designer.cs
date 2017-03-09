namespace WindowsFormsApplication1
{
    partial class RAKConfigure
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAKConfigure));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOpenserial = new System.Windows.Forms.Button();
            this.comboBaudrate = new System.Windows.Forms.ComboBox();
            this.comboPortName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SSID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RSSI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SECURITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_scan = new System.Windows.Forms.Button();
            this.textBox_Version = new System.Windows.Forms.TextBox();
            this.textBox_mac = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_web_username = new System.Windows.Forms.TextBox();
            this.textBox_web_password = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button_save_webcfg = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.linkLabelClear2 = new System.Windows.Forms.LinkLabel();
            this.button_save_factorycfg = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_read_factorycfg = new System.Windows.Forms.Button();
            this.button_export_cfg = new System.Windows.Forms.Button();
            this.button_readcfg = new System.Windows.Forms.Button();
            this.textBox_cfg_text = new System.Windows.Forms.TextBox();
            this.button_save_cfg = new System.Windows.Forms.Button();
            this.button_import_cfg = new System.Windows.Forms.Button();
            this.textBox_read_location = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.linkLabelClear = new System.Windows.Forms.LinkLabel();
            this.textBox_Serial_text = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.buttonOpenserial);
            this.groupBox1.Controls.Add(this.comboBaudrate);
            this.groupBox1.Controls.Add(this.comboPortName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonOpenserial
            // 
            resources.ApplyResources(this.buttonOpenserial, "buttonOpenserial");
            this.buttonOpenserial.Name = "buttonOpenserial";
            this.buttonOpenserial.UseVisualStyleBackColor = true;
            this.buttonOpenserial.Click += new System.EventHandler(this.buttonOpenserial_Click);
            // 
            // comboBaudrate
            // 
            this.comboBaudrate.FormattingEnabled = true;
            this.comboBaudrate.Items.AddRange(new object[] {
            resources.GetString("comboBaudrate.Items"),
            resources.GetString("comboBaudrate.Items1"),
            resources.GetString("comboBaudrate.Items2"),
            resources.GetString("comboBaudrate.Items3"),
            resources.GetString("comboBaudrate.Items4"),
            resources.GetString("comboBaudrate.Items5"),
            resources.GetString("comboBaudrate.Items6"),
            resources.GetString("comboBaudrate.Items7")});
            resources.ApplyResources(this.comboBaudrate, "comboBaudrate");
            this.comboBaudrate.Name = "comboBaudrate";
            // 
            // comboPortName
            // 
            this.comboPortName.FormattingEnabled = true;
            resources.ApplyResources(this.comboPortName, "comboPortName");
            this.comboPortName.Name = "comboPortName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.button_scan);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SSID,
            this.CH,
            this.RSSI,
            this.SECURITY});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // SSID
            // 
            this.SSID.FillWeight = 147.7865F;
            resources.ApplyResources(this.SSID, "SSID");
            this.SSID.Name = "SSID";
            this.SSID.ReadOnly = true;
            // 
            // CH
            // 
            this.CH.FillWeight = 81.21828F;
            resources.ApplyResources(this.CH, "CH");
            this.CH.Name = "CH";
            this.CH.ReadOnly = true;
            // 
            // RSSI
            // 
            this.RSSI.FillWeight = 81.61951F;
            resources.ApplyResources(this.RSSI, "RSSI");
            this.RSSI.Name = "RSSI";
            this.RSSI.ReadOnly = true;
            // 
            // SECURITY
            // 
            this.SECURITY.FillWeight = 89.37568F;
            resources.ApplyResources(this.SECURITY, "SECURITY");
            this.SECURITY.Name = "SECURITY";
            this.SECURITY.ReadOnly = true;
            // 
            // button_scan
            // 
            resources.ApplyResources(this.button_scan, "button_scan");
            this.button_scan.Name = "button_scan";
            this.button_scan.UseVisualStyleBackColor = true;
            this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
            // 
            // textBox_Version
            // 
            this.textBox_Version.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.textBox_Version, "textBox_Version");
            this.textBox_Version.Name = "textBox_Version";
            this.textBox_Version.ReadOnly = true;
            // 
            // textBox_mac
            // 
            resources.ApplyResources(this.textBox_mac, "textBox_mac");
            this.textBox_mac.Name = "textBox_mac";
            this.textBox_mac.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.textBox_mac);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBox_Version);
            this.groupBox3.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBox_web_username
            // 
            resources.ApplyResources(this.textBox_web_username, "textBox_web_username");
            this.textBox_web_username.Name = "textBox_web_username";
            // 
            // textBox_web_password
            // 
            resources.ApplyResources(this.textBox_web_password, "textBox_web_password");
            this.textBox_web_password.Name = "textBox_web_password";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.button_save_webcfg);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.textBox_web_password);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.textBox_web_username);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // button_save_webcfg
            // 
            resources.ApplyResources(this.button_save_webcfg, "button_save_webcfg");
            this.button_save_webcfg.Name = "button_save_webcfg";
            this.button_save_webcfg.UseVisualStyleBackColor = true;
            this.button_save_webcfg.Click += new System.EventHandler(this.button_save_webcfg_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.linkLabelClear2);
            this.groupBox5.Controls.Add(this.button_save_factorycfg);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.button_read_factorycfg);
            this.groupBox5.Controls.Add(this.button_export_cfg);
            this.groupBox5.Controls.Add(this.button_readcfg);
            this.groupBox5.Controls.Add(this.textBox_cfg_text);
            this.groupBox5.Controls.Add(this.button_save_cfg);
            this.groupBox5.Controls.Add(this.button_import_cfg);
            this.groupBox5.Controls.Add(this.textBox_read_location);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // linkLabelClear2
            // 
            resources.ApplyResources(this.linkLabelClear2, "linkLabelClear2");
            this.linkLabelClear2.Name = "linkLabelClear2";
            this.linkLabelClear2.TabStop = true;
            this.linkLabelClear2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClear2_LinkClicked);
            // 
            // button_save_factorycfg
            // 
            resources.ApplyResources(this.button_save_factorycfg, "button_save_factorycfg");
            this.button_save_factorycfg.Name = "button_save_factorycfg";
            this.button_save_factorycfg.UseVisualStyleBackColor = true;
            this.button_save_factorycfg.Click += new System.EventHandler(this.button_save_factorycfg_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // button_read_factorycfg
            // 
            resources.ApplyResources(this.button_read_factorycfg, "button_read_factorycfg");
            this.button_read_factorycfg.Name = "button_read_factorycfg";
            this.button_read_factorycfg.UseVisualStyleBackColor = true;
            this.button_read_factorycfg.Click += new System.EventHandler(this.button_read_factorycfg_Click);
            // 
            // button_export_cfg
            // 
            resources.ApplyResources(this.button_export_cfg, "button_export_cfg");
            this.button_export_cfg.Name = "button_export_cfg";
            this.button_export_cfg.UseVisualStyleBackColor = true;
            this.button_export_cfg.Click += new System.EventHandler(this.button_export_cfg_Click);
            // 
            // button_readcfg
            // 
            resources.ApplyResources(this.button_readcfg, "button_readcfg");
            this.button_readcfg.Name = "button_readcfg";
            this.button_readcfg.UseVisualStyleBackColor = true;
            this.button_readcfg.Click += new System.EventHandler(this.button_readcfg_Click);
            // 
            // textBox_cfg_text
            // 
            this.textBox_cfg_text.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.textBox_cfg_text, "textBox_cfg_text");
            this.textBox_cfg_text.Name = "textBox_cfg_text";
            // 
            // button_save_cfg
            // 
            resources.ApplyResources(this.button_save_cfg, "button_save_cfg");
            this.button_save_cfg.Name = "button_save_cfg";
            this.button_save_cfg.UseVisualStyleBackColor = true;
            this.button_save_cfg.Click += new System.EventHandler(this.button_save_cfg_Click);
            // 
            // button_import_cfg
            // 
            resources.ApplyResources(this.button_import_cfg, "button_import_cfg");
            this.button_import_cfg.Name = "button_import_cfg";
            this.button_import_cfg.UseVisualStyleBackColor = true;
            this.button_import_cfg.Click += new System.EventHandler(this.button_import_cfg_Click);
            // 
            // textBox_read_location
            // 
            resources.ApplyResources(this.textBox_read_location, "textBox_read_location");
            this.textBox_read_location.Name = "textBox_read_location";
            this.textBox_read_location.ReadOnly = true;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.linkLabelClear);
            this.groupBox6.Controls.Add(this.textBox_Serial_text);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // linkLabelClear
            // 
            resources.ApplyResources(this.linkLabelClear, "linkLabelClear");
            this.linkLabelClear.Name = "linkLabelClear";
            this.linkLabelClear.TabStop = true;
            this.linkLabelClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClear_LinkClicked);
            // 
            // textBox_Serial_text
            // 
            resources.ApplyResources(this.textBox_Serial_text, "textBox_Serial_text");
            this.textBox_Serial_text.Name = "textBox_Serial_text";
            this.textBox_Serial_text.ReadOnly = true;
            // 
            // RAKConfigure
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "RAKConfigure";
            this.Load += new System.EventHandler(this.RAKConfigure_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBaudrate;
        private System.Windows.Forms.ComboBox comboPortName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonOpenserial;
        private System.Windows.Forms.TextBox textBox_Version;
        private System.Windows.Forms.TextBox textBox_mac;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_scan;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CH;
        private System.Windows.Forms.DataGridViewTextBoxColumn RSSI;
        private System.Windows.Forms.DataGridViewTextBoxColumn SECURITY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_web_username;
        private System.Windows.Forms.TextBox textBox_web_password;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_save_webcfg;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_save_cfg;
        private System.Windows.Forms.Button button_import_cfg;
        private System.Windows.Forms.TextBox textBox_read_location;
        private System.Windows.Forms.TextBox textBox_cfg_text;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox_Serial_text;
        private System.Windows.Forms.Button button_export_cfg;
        private System.Windows.Forms.Button button_readcfg;
        private System.Windows.Forms.Button button_read_factorycfg;
        private System.Windows.Forms.Button button_save_factorycfg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabelClear;
        private System.Windows.Forms.LinkLabel linkLabelClear2;
    }
}


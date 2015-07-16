namespace MailPartition
{
    partial class FormMailPartition
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
            this.menuStripHeader = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelPLCIP = new System.Windows.Forms.Label();
            this.textBoxPLCIP = new System.Windows.Forms.TextBox();
            this.groupBoxMailInfo = new System.Windows.Forms.GroupBox();
            this.textBoxBar = new System.Windows.Forms.TextBox();
            this.textBoxWeight = new System.Windows.Forms.TextBox();
            this.textBoxVolume = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.labelWeight = new System.Windows.Forms.Label();
            this.labelBar = new System.Windows.Forms.Label();
            this.labelVolume = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.groupBoxPicture = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonRotateRight = new System.Windows.Forms.Button();
            this.buttonRotateLeft = new System.Windows.Forms.Button();
            this.pictureBoxPic = new System.Windows.Forms.PictureBox();
            this.buttonNormal = new System.Windows.Forms.Button();
            this.buttonError = new System.Windows.Forms.Button();
            this.timerNext = new System.Windows.Forms.Timer(this.components);
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxOBRIP = new System.Windows.Forms.TextBox();
            this.pictureBoxOBRConn = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxPLCConn = new System.Windows.Forms.PictureBox();
            this.menuStripHeader.SuspendLayout();
            this.groupBoxMailInfo.SuspendLayout();
            this.groupBoxPicture.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPic)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOBRConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPLCConn)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripHeader
            // 
            this.menuStripHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripHeader.Location = new System.Drawing.Point(0, 0);
            this.menuStripHeader.Name = "menuStripHeader";
            this.menuStripHeader.Size = new System.Drawing.Size(984, 25);
            this.menuStripHeader.TabIndex = 4;
            this.menuStripHeader.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.toolStripSeparator1,
            this.versionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.settingToolStripMenuItem.Text = "相关设置";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.versionToolStripMenuItem.Text = "版本信息";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.AutoToolTip = true;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.ToolTipText = "帮助信息";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // labelPLCIP
            // 
            this.labelPLCIP.AutoSize = true;
            this.labelPLCIP.Location = new System.Drawing.Point(74, 12);
            this.labelPLCIP.Name = "labelPLCIP";
            this.labelPLCIP.Size = new System.Drawing.Size(47, 12);
            this.labelPLCIP.TabIndex = 1;
            this.labelPLCIP.Text = "PLC IP:";
            // 
            // textBoxPLCIP
            // 
            this.textBoxPLCIP.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxPLCIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPLCIP.Enabled = false;
            this.textBoxPLCIP.Location = new System.Drawing.Point(121, 11);
            this.textBoxPLCIP.Name = "textBoxPLCIP";
            this.textBoxPLCIP.Size = new System.Drawing.Size(100, 14);
            this.textBoxPLCIP.TabIndex = 2;
            // 
            // groupBoxMailInfo
            // 
            this.groupBoxMailInfo.Controls.Add(this.textBoxBar);
            this.groupBoxMailInfo.Controls.Add(this.textBoxWeight);
            this.groupBoxMailInfo.Controls.Add(this.textBoxVolume);
            this.groupBoxMailInfo.Controls.Add(this.textBoxID);
            this.groupBoxMailInfo.Controls.Add(this.labelWeight);
            this.groupBoxMailInfo.Controls.Add(this.labelBar);
            this.groupBoxMailInfo.Controls.Add(this.labelVolume);
            this.groupBoxMailInfo.Controls.Add(this.labelID);
            this.groupBoxMailInfo.Location = new System.Drawing.Point(13, 65);
            this.groupBoxMailInfo.Name = "groupBoxMailInfo";
            this.groupBoxMailInfo.Size = new System.Drawing.Size(636, 84);
            this.groupBoxMailInfo.TabIndex = 3;
            this.groupBoxMailInfo.TabStop = false;
            this.groupBoxMailInfo.Text = "邮件信息";
            // 
            // textBoxBar
            // 
            this.textBoxBar.Location = new System.Drawing.Point(73, 56);
            this.textBoxBar.Name = "textBoxBar";
            this.textBoxBar.Size = new System.Drawing.Size(112, 21);
            this.textBoxBar.TabIndex = 1;
            // 
            // textBoxWeight
            // 
            this.textBoxWeight.Location = new System.Drawing.Point(449, 53);
            this.textBoxWeight.Name = "textBoxWeight";
            this.textBoxWeight.Size = new System.Drawing.Size(114, 21);
            this.textBoxWeight.TabIndex = 3;
            // 
            // textBoxVolume
            // 
            this.textBoxVolume.Location = new System.Drawing.Point(449, 22);
            this.textBoxVolume.Name = "textBoxVolume";
            this.textBoxVolume.Size = new System.Drawing.Size(114, 21);
            this.textBoxVolume.TabIndex = 2;
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(74, 24);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(111, 21);
            this.textBoxID.TabIndex = 0;
            // 
            // labelWeight
            // 
            this.labelWeight.AutoSize = true;
            this.labelWeight.Location = new System.Drawing.Point(370, 57);
            this.labelWeight.Name = "labelWeight";
            this.labelWeight.Size = new System.Drawing.Size(71, 12);
            this.labelWeight.TabIndex = 0;
            this.labelWeight.Text = "重量（10g）";
            // 
            // labelBar
            // 
            this.labelBar.AutoSize = true;
            this.labelBar.Location = new System.Drawing.Point(28, 60);
            this.labelBar.Name = "labelBar";
            this.labelBar.Size = new System.Drawing.Size(29, 12);
            this.labelBar.TabIndex = 0;
            this.labelBar.Text = "条码";
            // 
            // labelVolume
            // 
            this.labelVolume.AutoSize = true;
            this.labelVolume.Location = new System.Drawing.Point(370, 26);
            this.labelVolume.Name = "labelVolume";
            this.labelVolume.Size = new System.Drawing.Size(77, 12);
            this.labelVolume.TabIndex = 0;
            this.labelVolume.Text = "体积（cm^3）";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(27, 28);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(41, 12);
            this.labelID.TabIndex = 0;
            this.labelID.Text = "邮件ID";
            // 
            // groupBoxPicture
            // 
            this.groupBoxPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPicture.Controls.Add(this.statusStrip1);
            this.groupBoxPicture.Controls.Add(this.buttonRotateRight);
            this.groupBoxPicture.Controls.Add(this.buttonRotateLeft);
            this.groupBoxPicture.Controls.Add(this.pictureBoxPic);
            this.groupBoxPicture.Location = new System.Drawing.Point(13, 156);
            this.groupBoxPicture.Name = "groupBoxPicture";
            this.groupBoxPicture.Size = new System.Drawing.Size(959, 587);
            this.groupBoxPicture.TabIndex = 2;
            this.groupBoxPicture.TabStop = false;
            this.groupBoxPicture.Text = "图像";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(953, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.DimGray;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(633, 17);
            this.toolStripStatusLabel1.Text = "郑州国际邮件分拣决策程序   F1: 帮助    F5：正常确认    F6：异常确认    F9：图像左旋转90°    F10：图像右旋转90°";
            // 
            // buttonRotateRight
            // 
            this.buttonRotateRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRotateRight.BackgroundImage = global::MailPartition.Properties.Resources.rotateRight;
            this.buttonRotateRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRotateRight.Location = new System.Drawing.Point(598, 20);
            this.buttonRotateRight.Name = "buttonRotateRight";
            this.buttonRotateRight.Size = new System.Drawing.Size(24, 24);
            this.buttonRotateRight.TabIndex = 1;
            this.buttonRotateRight.UseVisualStyleBackColor = true;
            this.buttonRotateRight.Visible = false;
            this.buttonRotateRight.Click += new System.EventHandler(this.buttonRotateRight_Click);
            // 
            // buttonRotateLeft
            // 
            this.buttonRotateLeft.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRotateLeft.BackgroundImage = global::MailPartition.Properties.Resources.rotateLeft;
            this.buttonRotateLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRotateLeft.Location = new System.Drawing.Point(349, 20);
            this.buttonRotateLeft.Name = "buttonRotateLeft";
            this.buttonRotateLeft.Size = new System.Drawing.Size(24, 24);
            this.buttonRotateLeft.TabIndex = 0;
            this.buttonRotateLeft.UseVisualStyleBackColor = false;
            this.buttonRotateLeft.Visible = false;
            this.buttonRotateLeft.Click += new System.EventHandler(this.buttonRotateLeft_Click);
            // 
            // pictureBoxPic
            // 
            this.pictureBoxPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPic.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxPic.Location = new System.Drawing.Point(6, 14);
            this.pictureBoxPic.Name = "pictureBoxPic";
            this.pictureBoxPic.Size = new System.Drawing.Size(947, 545);
            this.pictureBoxPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPic.TabIndex = 0;
            this.pictureBoxPic.TabStop = false;
            // 
            // buttonNormal
            // 
            this.buttonNormal.Location = new System.Drawing.Point(703, 122);
            this.buttonNormal.Name = "buttonNormal";
            this.buttonNormal.Size = new System.Drawing.Size(75, 23);
            this.buttonNormal.TabIndex = 0;
            this.buttonNormal.Text = "正常";
            this.buttonNormal.UseVisualStyleBackColor = true;
            this.buttonNormal.Click += new System.EventHandler(this.buttonNormal_Click);
            // 
            // buttonError
            // 
            this.buttonError.Location = new System.Drawing.Point(861, 122);
            this.buttonError.Name = "buttonError";
            this.buttonError.Size = new System.Drawing.Size(75, 23);
            this.buttonError.TabIndex = 1;
            this.buttonError.Text = "异常";
            this.buttonError.UseVisualStyleBackColor = true;
            this.buttonError.Click += new System.EventHandler(this.buttonError_Click);
            // 
            // timerNext
            // 
            this.timerNext.Interval = 3000;
            this.timerNext.Tick += new System.EventHandler(this.timerNext_Tick);
            // 
            // timerStatus
            // 
            this.timerStatus.Interval = 2000;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.textBoxOBRIP);
            this.groupBox1.Controls.Add(this.pictureBoxOBRConn);
            this.groupBox1.Controls.Add(this.textBoxPLCIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBoxPLCConn);
            this.groupBox1.Controls.Add(this.labelPLCIP);
            this.groupBox1.Location = new System.Drawing.Point(13, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(636, 30);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // textBoxOBRIP
            // 
            this.textBoxOBRIP.BackColor = System.Drawing.SystemColors.Menu;
            this.textBoxOBRIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxOBRIP.Enabled = false;
            this.textBoxOBRIP.Location = new System.Drawing.Point(418, 11);
            this.textBoxOBRIP.Name = "textBoxOBRIP";
            this.textBoxOBRIP.Size = new System.Drawing.Size(100, 14);
            this.textBoxOBRIP.TabIndex = 2;
            // 
            // pictureBoxOBRConn
            // 
            this.pictureBoxOBRConn.InitialImage = null;
            this.pictureBoxOBRConn.Location = new System.Drawing.Point(530, 11);
            this.pictureBoxOBRConn.Name = "pictureBoxOBRConn";
            this.pictureBoxOBRConn.Size = new System.Drawing.Size(14, 14);
            this.pictureBoxOBRConn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOBRConn.TabIndex = 6;
            this.pictureBoxOBRConn.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "OBR IP:";
            // 
            // pictureBoxPLCConn
            // 
            this.pictureBoxPLCConn.InitialImage = null;
            this.pictureBoxPLCConn.Location = new System.Drawing.Point(233, 11);
            this.pictureBoxPLCConn.Name = "pictureBoxPLCConn";
            this.pictureBoxPLCConn.Size = new System.Drawing.Size(14, 14);
            this.pictureBoxPLCConn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPLCConn.TabIndex = 6;
            this.pictureBoxPLCConn.TabStop = false;
            // 
            // FormMailPartition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 750);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonError);
            this.Controls.Add(this.buttonNormal);
            this.Controls.Add(this.groupBoxPicture);
            this.Controls.Add(this.groupBoxMailInfo);
            this.Controls.Add(this.menuStripHeader);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStripHeader;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FormMailPartition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "邮件分拣";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMailPartition_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMailPartition_KeyDown);
            this.menuStripHeader.ResumeLayout(false);
            this.menuStripHeader.PerformLayout();
            this.groupBoxMailInfo.ResumeLayout(false);
            this.groupBoxMailInfo.PerformLayout();
            this.groupBoxPicture.ResumeLayout(false);
            this.groupBoxPicture.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPic)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOBRConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPLCConn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripHeader;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label labelPLCIP;
        private System.Windows.Forms.TextBox textBoxPLCIP;
        private System.Windows.Forms.GroupBox groupBoxMailInfo;
        private System.Windows.Forms.TextBox textBoxBar;
        private System.Windows.Forms.TextBox textBoxVolume;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label labelBar;
        private System.Windows.Forms.Label labelVolume;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.GroupBox groupBoxPicture;
        private System.Windows.Forms.PictureBox pictureBoxPic;
        private System.Windows.Forms.Button buttonNormal;
        private System.Windows.Forms.Button buttonError;
        private System.Windows.Forms.TextBox textBoxWeight;
        private System.Windows.Forms.Label labelWeight;
        private System.Windows.Forms.Timer timerNext;
        private System.Windows.Forms.Button buttonRotateRight;
        private System.Windows.Forms.Button buttonRotateLeft;
        private System.Windows.Forms.PictureBox pictureBoxPLCConn;
        private System.Windows.Forms.Timer timerStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxOBRIP;
        private System.Windows.Forms.PictureBox pictureBoxOBRConn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}


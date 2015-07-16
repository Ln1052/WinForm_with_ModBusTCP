namespace MailPartition.ChildrenForms
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxSetting = new System.Windows.Forms.GroupBox();
            this.labelIntervalSetting = new System.Windows.Forms.Label();
            this.comboBoxIntervalTime = new System.Windows.Forms.ComboBox();
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttoCancel = new System.Windows.Forms.Button();
            this.groupBoxSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSetting
            // 
            this.groupBoxSetting.Controls.Add(this.comboBoxIntervalTime);
            this.groupBoxSetting.Controls.Add(this.labelIntervalSetting);
            this.groupBoxSetting.Location = new System.Drawing.Point(26, 33);
            this.groupBoxSetting.Name = "groupBoxSetting";
            this.groupBoxSetting.Size = new System.Drawing.Size(321, 99);
            this.groupBoxSetting.TabIndex = 0;
            this.groupBoxSetting.TabStop = false;
            // 
            // labelIntervalSetting
            // 
            this.labelIntervalSetting.AutoSize = true;
            this.labelIntervalSetting.Location = new System.Drawing.Point(26, 44);
            this.labelIntervalSetting.Name = "labelIntervalSetting";
            this.labelIntervalSetting.Size = new System.Drawing.Size(173, 12);
            this.labelIntervalSetting.TabIndex = 0;
            this.labelIntervalSetting.Text = "默认处理等待间隔时间（秒）：";
            // 
            // comboBoxIntervalTime
            // 
            this.comboBoxIntervalTime.CausesValidation = false;
            this.comboBoxIntervalTime.FormattingEnabled = true;
            this.comboBoxIntervalTime.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "15",
            "20"});
            this.comboBoxIntervalTime.Location = new System.Drawing.Point(227, 41);
            this.comboBoxIntervalTime.Name = "comboBoxIntervalTime";
            this.comboBoxIntervalTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBoxIntervalTime.Size = new System.Drawing.Size(64, 20);
            this.comboBoxIntervalTime.TabIndex = 1;
            this.comboBoxIntervalTime.TabStop = false;
            this.comboBoxIntervalTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxIntervalTime_SelectedIndexChanged);
            // 
            // buttonYes
            // 
            this.buttonYes.Location = new System.Drawing.Point(93, 176);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(58, 23);
            this.buttonYes.TabIndex = 1;
            this.buttonYes.Text = "确定";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            // 
            // buttoCancel
            // 
            this.buttoCancel.Location = new System.Drawing.Point(217, 176);
            this.buttoCancel.Name = "buttoCancel";
            this.buttoCancel.Size = new System.Drawing.Size(58, 23);
            this.buttoCancel.TabIndex = 1;
            this.buttoCancel.Text = "取消";
            this.buttoCancel.UseVisualStyleBackColor = true;
            this.buttoCancel.Click += new System.EventHandler(this.buttoCancel_Click);
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 262);
            this.Controls.Add(this.buttoCancel);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.groupBoxSetting);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "相关参数设置";
            this.groupBoxSetting.ResumeLayout(false);
            this.groupBoxSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSetting;
        private System.Windows.Forms.Label labelIntervalSetting;
        private System.Windows.Forms.ComboBox comboBoxIntervalTime;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttoCancel;
    }
}
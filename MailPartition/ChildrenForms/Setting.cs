using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MailPartition.ChildrenForms
{
    public partial class FormSetting : Form
    {
        public int IntervalTime { get; private set; }

        public FormSetting()
        {
            InitializeComponent();

            //默认间隔时间5秒
            this.comboBoxIntervalTime.SelectedIndex = 0;         
        }

        private void comboBoxIntervalTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.IntervalTime = int.Parse(this.comboBoxIntervalTime.Text);
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            this.IntervalTime = int.Parse(this.comboBoxIntervalTime.Text);
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void buttoCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

    }
}

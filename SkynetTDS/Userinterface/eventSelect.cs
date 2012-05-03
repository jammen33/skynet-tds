using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkynetTDS.Userinterface
{
    public partial class eventSelect : Form
    {
        public eventSelect()
        {
            InitializeComponent();
        }

        private void friendFoeButton_Click(object sender, EventArgs e)
        {
            StateSelected = 1;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void foeButton_Click(object sender, EventArgs e)
        {
            StateSelected = 0;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        public int StateSelected
        {
            get;
            private set;
        }
    }
}

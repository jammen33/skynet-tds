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
        DialogResult result;
        public eventSelect()
        {
            InitializeComponent();
        }

        private void friendFoeButton_Click(object sender, EventArgs e)
        {
            StateSelected = 1;
            result = ok;
            Close();
        }

        private void foeButton_Click(object sender, EventArgs e)
        {
            StateSelected = 0;
            Close();
        }

        public int StateSelected
        {
            get;
            private set;
        }
    }
}

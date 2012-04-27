using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SkynetTDS.Vision;

namespace SkynetTDS.Userinterface
{
    public partial class UserInterface : Form
    {
        IVisionDevice vision;
        public UserInterface()
        {
            InitializeComponent();
            vision = new VisionDevice();
            vision.Start();

        }

        private void UserInterface_Load(object sender, EventArgs e)
        {

        }

        private void displayImage_Click(object sender, EventArgs e)
        {

        }

        public void EventHandler( IVisionDevice sender, ImageDeviceArgs e )
        {
            displayImage.Image = e.Frame;
        }
    }
}

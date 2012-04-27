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
            vision.CameraStarted += new EventHandler(captureStarted);
            vision.ImageCaptured += new EventHandler<ImageDeviceArgs>(updatIimage);
            vision.Start();
            vision.Stop();

        }

        private void UserInterface_Load(object sender, EventArgs e)
        {

        }

        private void displayImage_Click(object sender, EventArgs e)
        {

        }

        public void captureStarted( object sender, EventArgs e )
        {
           //displayImage.Image = vision.GetImage();
        }

        private void start_capture_Click(object sender, EventArgs e)
        {
            
        }
       
        public void updatIimage(object sender, ImageDeviceArgs e)
        {
            displayImage.Image = e.Frame;
            //displayImage.Image = vision.GetImage();
        }
    }
}

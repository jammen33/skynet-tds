using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SkynetTDS.Vision;
using SkynetTDS.Controller;

namespace SkynetTDS.Userinterface
{
    public partial class UserInterface : Form
    {
        IVisionDevice vision;
        IEventControllerCreator controllerCreator;
        IEventController controller;


        int eventType;

        public UserInterface()
        {
            InitializeComponent();
            vision = VisionDevice.getInstance();
            controllerCreator = new EventControllerCreator();
            vision.CameraStarted += new EventHandler(captureStarted);
            vision.ImageCaptured += new EventHandler<ImageDeviceArgs>(updatIimage);
            vision.CameraStopped += new EventHandler(captureStopped);
           
            
        }

        public void captureStarted( object sender, EventArgs e )
        {
           MessageBox.Show("capture started!");
        }

        private void start_capture_Click(object sender, EventArgs e)
        {
            vision.Start();
        }
       
        public void updatIimage(object sender, ImageDeviceArgs e)
        {
            displayImage.Image = e.Frame;
        }

        public void captureStopped(object sender, EventArgs e)
        {
            MessageBox.Show("Capture Stopped!");
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            vision.Stop();
            if (controller != null)
            {
                controller.stopEvent();
            }
        }

        private void eventTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventType = eventTypeComboBox.SelectedIndex;
        }

        private void StartEvent_Click(object sender, EventArgs e)
        {
            spawnEventController();
            controller.startEvent();
        }

        private void spawnEventController()
        {
            controller = controllerCreator.createEventController(eventType);           
        }
    }
}

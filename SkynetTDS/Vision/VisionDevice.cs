using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using Emgu.CV;

namespace SkynetTDS.Vision
{
    class VisionDevice : IVisionDevice
    {
        Capture capture;
        Image img;
        ThreadStart captureThreadStart;
        Thread captureThread;
        /// <summary>
        /// Object for synchronizing
        /// </summary>
        private object m_lockObject;

        public event EventHandler<ImageDeviceArgs> ImageCaptured;

        public event EventHandler CameraStarted;

        public event EventHandler CameraStopped;

        public VisionDevice()
        {
            Name = "Mario";
           
        }
        /// <summary>
        /// Puts the camera in continuous mode.
        /// </summary>
        public void Start()
        {
            capture = new Capture();
            m_lockObject = new object();
            captureThreadStart = new ThreadStart(captureLoop);
            captureThread = new Thread(captureThreadStart);
            captureThread.Start();
            EventArgs e = new EventArgs();
            EventHandler cameraStarted = CameraStarted;
            if (cameraStarted != null)
            {
                cameraStarted(this, e);
            }
        }

        /// <summary>
        /// Stops the camera if in continuous mode.
        /// </summary>
        public void Stop()
        {
            captureThread.Abort();
        }

        /// <summary>
        /// Gets the last image if in continuous mode, or polls the image device.
        /// </summary>
        /// <returns>The last image.</returns>
        public Image GetImage()
        {
            return capture.QueryFrame().ToBitmap();
        }
        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        private void captureLoop()
        {
            while (true)
            {
                lock (m_lockObject)
                {
                    img = capture.QueryFrame().ToBitmap();
                }
                ImageDeviceArgs e = new ImageDeviceArgs(img);
                EventHandler<ImageDeviceArgs> imageCaptured = ImageCaptured;
                if (imageCaptured != null)
                {
                    imageCaptured(this, e);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("not working");
                }
            }
        }

        private void onCaptureStart(EventArgs e)
        {
            CameraStarted(this, e);
        }
    }
}
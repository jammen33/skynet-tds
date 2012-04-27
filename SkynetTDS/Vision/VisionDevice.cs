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
            captureThreadStart = new ThreadStart(captureLoop);
            captureThread = new Thread(captureThreadStart);

            CameraStarted.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Stops the camera if in continuous mode.
        /// </summary>
        public void Stop()
        {
            captureThread.Abort();
            CameraStopped.Invoke(this, new EventArgs());
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
                ImageCaptured.Invoke(this, new ImageDeviceArgs(img));
            }
        }

        public event EventHandler<ImageDeviceArgs> ImageCaptured;

        public event EventHandler CameraStarted;

        public event EventHandler CameraStopped;
    }
}
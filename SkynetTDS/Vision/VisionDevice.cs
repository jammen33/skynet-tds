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
        static IVisionDevice instance = null;
        bool isCapturing;

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
            m_lockObject = new object();
            isCapturing = false;
            if (instance == null)
            {
                instance = this;
            }
        }

        public static IVisionDevice getInstance()
        {
            if (instance == null)
            {
                instance = new VisionDevice();
            }
            return instance;
        }
        /// <summary>
        /// Puts the camera in continuous mode.
        /// </summary>
        public void Start()
        {
            if (!isCapturing)
            {
                capture = new Capture();
                captureThreadStart = new ThreadStart(captureLoop);
                captureThread = new Thread(captureThreadStart);
                captureThread.Start();
                EventArgs e = new EventArgs();
                EventHandler cameraStarted = CameraStarted;
                if (cameraStarted != null)
                {
                    cameraStarted(this, e);
                }
                isCapturing = true;
            }
        }

        /// <summary>
        /// Stops the camera if in continuous mode.
        /// </summary>
        public void Stop()
        {
            if (isCapturing)
            {
                captureThread.Abort();
                EventArgs e = new EventArgs();
                EventHandler cameraStopped = CameraStopped;
                if (cameraStopped != null)
                {
                    cameraStopped(this, e);
                }
                isCapturing = false;
            }
        }

        /// <summary>
        /// Gets the last image if in continuous mode, or polls the image device.
        /// </summary>
        /// <returns>The last image.</returns>
        public Image GetImage()
        {
            lock (m_lockObject)
            {
                return img;
            }
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
            }
        }
    }
}
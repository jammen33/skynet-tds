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
        
        #region Events
        public event EventHandler<ImageDeviceArgs> ImageCaptured;
        public event EventHandler CameraStarted;
        public event EventHandler CameraStopped;

        private void onStart(EventArgs e)
        {
            EventHandler cameraStarted = CameraStarted;
            if (cameraStarted != null)
            {
                cameraStarted(this, e);
            }
        }
        private void onCapture(ImageDeviceArgs e)
        {
            EventHandler<ImageDeviceArgs> imageCaptured = ImageCaptured;
            if (imageCaptured != null)
            {
                imageCaptured(this, e);
            }
        }
        private void onStop(EventArgs e)
        {
            EventHandler cameraStopped = CameraStopped;
            if (cameraStopped != null)
            {
                cameraStopped(this, e);
            }
        }

        #endregion

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
        ~VisionDevice()
        {
            if (isCapturing)
            {
                captureThread.Abort();
                onStop(new EventArgs());
                isCapturing = false;
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
                onStart(new EventArgs());
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
                onStop(new EventArgs());
                isCapturing = false;
            }
        }

        /// <summary>
        /// Gets the last image if in continuous mode, or polls the image device.
        /// </summary>
        /// <returns>The last image.</returns>
        public Image GetImage()
        {
            lock (img)
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
            img = capture.QueryFrame().Copy().ToBitmap();   //so img isn'y null for the lock
            ImageDeviceArgs e;
            while (true)
            {
                
                lock (img)
                {
                    img = capture.QueryFrame().Copy().ToBitmap();
                    e = new ImageDeviceArgs(img);
                }    
            
                onCapture(e);
            }
        }
    }
}
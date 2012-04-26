using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Emgu.CV;

namespace SkynetTDS.Vision
{
    class VisionDevice : IVisionDevice
    {
        Capture capture;

        /// <summary>
        /// Puts the camera in continuous mode.
        /// </summary>
        public void Start()
        {
            capture = new Capture();
        }
        /// <summary>
        /// Stops the camera if in continuous mode.
        /// </summary>
        public void Stop()
        {
            capture = null;
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
    }
}
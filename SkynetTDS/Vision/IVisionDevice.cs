using System;
using System.Drawing;

namespace SkynetTDS.Vision
{
    /// <summary>
    /// Interface for a vision device.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVisionDevice
    {
        /// <summary>
        /// Puts the camera in continuous mode.
        /// </summary>
        void Start();
        /// <summary>
        /// Stops the camera if in continuous mode.
        /// </summary>
        void Stop();
        /// <summary>
        /// Gets the last image if in continuous mode, or polls the image device.
        /// </summary>
        /// <returns>The last image.</returns>
        Image GetImage();
        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        #region Events 
        /// <summary>
        /// Fired when an image is captured.
        /// </summary>
        event EventHandler<ImageDeviceArgs> ImageCaptured;
        /// <summary>
        /// Fired when the camera is put into continuous mode.
        /// </summary>
        event EventHandler CameraStarted;
        /// <summary>
        /// Fired when the camera is stopped.
        /// </summary>
        event EventHandler CameraStopped;
        #endregion
    }
 }

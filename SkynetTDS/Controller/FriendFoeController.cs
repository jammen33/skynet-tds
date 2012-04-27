using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections.ObjectModel;

using SkynetTDS.Vision;
using SkynetTDS.Targets;
using SkynetTDS.Launchers;

namespace SkynetTDS.Controller
{
    class FriendFoeController : IEventController
    {
        ThreadStart controllerThreadStart;
        Thread controllerThread;
        IVisionDevice vision;
        IImageProcessor processor;
        ILauncher launcher;
        Image img;
        Collection<Target> targets;
        bool isRunning;

        /// <summary>
        /// Object for synchronizing
        /// </summary>
        private object m_lockObject;

        public FriendFoeController()
        {
            launcher = new LauncherController();
            launcher.Calibrate();

            System.Windows.Forms.MessageBox.Show("Hello, World! This is FriendFoeController!");
            m_lockObject = new object();
            vision = VisionDevice.getInstance();
            processor = new FriendFoeImageProcessor();
            isRunning = false;

        }
        public void startEvent()
        {
            if (!isRunning)
            {
                controllerThreadStart = new ThreadStart(run);
                controllerThread = new Thread(controllerThreadStart);
                controllerThread.Start();
            }
        }


        public void stopEvent()
        {
            if (isRunning)
            {
                controllerThread.Abort();
            }
        }

        public void emergencyStop()
        {
            controllerThread.Abort();
        }

        private void run()
        {
            vision.Start();
            img = vision.GetImage();
            targets = processor.DetectTargets(img);
            while(true)
            {
                foreach( Target t in targets )
                {
                    if (!t.IsFriend)
                    {
                        //atk

                        lock (m_lockObject)
                        {
                            launcher.MoveAbsolute((int)t.Point.X, (int)t.Point.Y, 0);
                            launcher.Fire(1);
                        }
                    }
                }
            }
        }
    }
}

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
        bool shouldRun;
        int numberOfMissiles;
        bool isRunning;

        /// <summary>
        /// Object for synchronizing
        /// </summary>
        private object m_lockObject;

        public FriendFoeController()
        {
            launcher = new LauncherController();
            numberOfMissiles = 4;
            launcher.MissileFired += new EventHandler(launcherFired);

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
                isRunning = true;
                shouldRun = true;
                controllerThreadStart = new ThreadStart(run);
                controllerThread = new Thread(controllerThreadStart);
                controllerThread.Start();
            }
        }


        public void stopEvent()
        {
            if (isRunning)
            {
                shouldRun = false;
            }
        }

        public void emergencyStop()
        {
            controllerThread.Abort();
            shouldRun = false;
        }

        private void run()
        {
            int foeCount = 0;
            int friendCount = 0;
            vision.Start();
            while (shouldRun)
            {
                System.Windows.Forms.MessageBox.Show("loop");
                img = vision.GetImage();
                targets = processor.DetectTargets(img);
                foreach (Target t in targets)
                {
                    if(t.IsFriend)
                    {
                        friendCount++;
                    }
                    else
                    {
                        foeCount++;
                    }
                }
                FoundTatgetEventArgs e = new FoundTatgetEventArgs(foeCount,friendCount, targets);
                onFoundTargets(e);
                if(foeCount < 1)
                {
                    break;
                }
                foreach( Target t in targets )
                {
                    if (!t.IsFriend)
                    {
                        lock (this)
                        {
                           // System.Windows.Forms.MessageBox.Show(t.Point.X.ToString());
                            launcher.MoveAbsolute(((int)t.Point.X - (img.Width/2))*2, (int)t.Point.Y, 0);
                            if (numberOfMissiles > 0)
                            {
                                //check for stop before we fire
                                if (shouldRun != false)
                                {
                                    launcher.Fire(1);
                                    numberOfMissiles--;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                shouldRun = false;
                                break;
                            }
                        }
                    }
                }
                targets.Clear();
            }
            System.Windows.Forms.MessageBox.Show("Event Over");
        }

        private void launcherFired(object sender, EventArgs e)
        {
            onMissileFired(e);
        }

        public event EventHandler MissileFired;
        private void onMissileFired( EventArgs e )
        {
            EventHandler missileFired = MissileFired;
            if (missileFired != null)
            {
                missileFired.Invoke(this, e);
            }
        }


        public event EventHandler<FoundTatgetEventArgs> FoundTargets;
        private void onFoundTargets(FoundTatgetEventArgs e)
        {
            EventHandler<FoundTatgetEventArgs> foundTargets = FoundTargets;
            if (foundTargets != null)
            {
                foundTargets.Invoke(this, e);
            }
        }


        public void calibrateLauncher()
        {

            launcher.Calibrate();
        }
    }
}

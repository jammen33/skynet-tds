﻿using System;
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


        public int numberOfMissiles
        {
            get;
            private set;
        }
        bool shouldRun;

        bool isRunning;

        public FriendFoeController()
        {
            launcher = new LauncherController();
            numberOfMissiles = 8;
            launcher.MissileFired += new EventHandler(launcherFired);
            vision = VisionDevice.getInstance();
            processor = new FriendFoeImageProcessor();
            isRunning = false;
        }

        ~FriendFoeController()
        {

                vision = null;
                launcher = null;
                if (controllerThread.IsAlive)
                {
                    controllerThread.Abort();

                }
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                vision = null;
                launcher = null;
                if (controllerThread.IsAlive)
                {
                    controllerThread.Abort();

                }
            }
        }

        public void calibrateLauncher()
        {

            launcher.Calibrate();
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
                isRunning = false;
                onEventTerminated(new EventArgs());
            }
        }

        public void emergencyStop()
        {
            controllerThread.Abort();
            shouldRun = false;
            isRunning = false;
            onEventTerminated(new EventArgs());
        }

        #region run
        /// <summary>
        /// Loop to run as a thread
        /// </summary>
        private void run()
        {
            int foeCount = 0;
            int friendCount = 0;
            vision.Start();
            while (shouldRun)
            {
                img = vision.GetImage();
                targets = processor.DetectTargets(img);
                foeCount = 0;
                friendCount = 0;

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
                            launcher.MoveAbsolute((int)((t.Point.X - (img.Width / 2)) * 1.5), (int)((img.Height) - t.Point.Y), 0);
                            if (numberOfMissiles > 0)
                            {
                                //check for stop before we fire
                                if (shouldRun)
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
                                onOutOfMissiles(new EventArgs());
                                stopEvent();
                                break;
                            }
                        }
                    }
                }
                targets.Clear();
            }
            
        }
        #endregion
        #region events
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

        public event EventHandler OutOfMissiles;
        private void onOutOfMissiles(EventArgs e)
        {
            EventHandler empty = OutOfMissiles;
            if (empty != null)
            {
                empty.Invoke(this, e);
            }
        }
        public event EventHandler EventTerminated;
        private void onEventTerminated(EventArgs e)
        {
            EventHandler term = EventTerminated;
            if (term != null)
            {
                term.Invoke(this, e);
            }
        }
        #endregion
    }
}

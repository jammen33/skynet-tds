using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.ObjectModel;
using System.Timers;


using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using SkynetTDS.Vision;
using SkynetTDS.Controller;
using SkynetTDS.Targets;

namespace SkynetTDS.Userinterface
{
    public partial class UserInterface : Form
    {
        IVisionDevice vision;
        IEventControllerCreator controllerCreator;
        IEventController controller;
        Collection<Target> targets;

        delegate void SetTextCallback(string text);
        delegate void setCountsCallBack();
        delegate void changeButtons( bool start, bool stop, bool estop);
        delegate void displayTimeRemaining(string time);

        ThreadStart timerThreadStart;
        Thread timerThread;

        event EventHandler TimesAlmostUp;
        event EventHandler TimeIsUp;

        int numberOfFoes;
        int numberOfFriends;
        TimeSpan time;
        bool Counting;

        public UserInterface()
        {
            InitializeComponent();

            controllerCreator = new EventControllerCreator();

            //set default values
            stopEventButton.Enabled = false;
            estopButton.Enabled = false;

            startVideo();
            setUpController();
            this.TimesAlmostUp += new EventHandler(timeAlmostUp);
            this.TimeIsUp += new EventHandler(timeIsUp);
            eventTimeLimit.Text = "0";

        }

        /// <summary>
        /// displays a menu for the user to select an event type
        /// </summary>
        /// <returns>event number</returns>
        private int showEventSelect()
        {
            eventSelect dlg = new eventSelect();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                return dlg.StateSelected;
                
            }
            return -1;
        }

        /// <summary>
        /// Initailizes the carmera and starts continueus image capture
        /// </summary>
        private void startVideo()
        {
            vision = VisionDevice.getInstance();
            vision.Start();
            vision.ImageCaptured += new EventHandler<ImageDeviceArgs>(updatIimage);
            vision.CameraStopped += new EventHandler(captureStopped);
        }
            

        /// <summary>
        /// Ask the user to select an event and creates the controller object
        /// </summary>
        private void setUpController()
        {
            controller = controllerCreator.createEventController(showEventSelect());
            controller.calibrateLauncher();
            controller.FoundTargets += new EventHandler<FoundTatgetEventArgs>(targetsFound);
            controller.MissileFired += new EventHandler(missileFired);
            controller.EventTerminated += new EventHandler(eventTerminated);
            controller.OutOfMissiles += new EventHandler(outOfMissiles);
            setMissileCount(controller.numberOfMissiles.ToString());
        }


        public void captureStopped(object sender, EventArgs e)
        {
            vision.ImageCaptured -= new EventHandler<ImageDeviceArgs>(updatIimage);
            vision.CameraStopped -= new EventHandler(captureStopped);
        }
        /// <summary>
        /// Draws targets to the image and displays it to the screen.
        /// if there are no targets it simply displays the image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void updatIimage(object sender, ImageDeviceArgs e)
        {
            lock (e.Frame)
            {
                if (targets != null)
                {
                    Image<Bgr, byte> tmp = new Image<Bgr, byte>(new Bitmap((Image)e.Frame.Clone()));
                    foreach (Target t in targets)
                    {
                        if (t.IsFriend)
                        {
                            tmp.Draw(new Cross2DF(t.Point, 15, 15), new Bgr(Color.Blue), 5);
                        }
                        else
                        {
                            tmp.Draw(new Cross2DF(t.Point, 15, 15), new Bgr(Color.Orange), 5);
                        }
                    }
                    displayImage.Image = tmp.Copy().ToBitmap();
                }
                else
                {
                        displayImage.Image = (Image)e.Frame.Clone();
                }
            }
        }



        /// <summary>
        /// loop for timer thread
        /// </summary>
        private void timer()
        {
            bool warned = false;
            DateTime startTime = DateTime.Now;
            DateTime currentTime;
            TimeSpan t, timeLeft;
            
            //set time to 0 to not have a timed event
            if (time.Equals( new TimeSpan(0,0,0,0)))
            {
                timerThread.Abort();
            }
            while (Counting)
            {
                currentTime = DateTime.Now;
                t = currentTime.Subtract(startTime);
                timeLeft = time.Subtract(t);

                //3 seconds to fire missiles?
                if (timeLeft.Seconds < 4 && timeLeft.Seconds > 0 && !warned)
                {
                    TimesAlmostUp(this, new EventArgs());
                    warned = true;
                }
                else if (timeLeft.Seconds < 0)
                {
                    TimeIsUp(this, new EventArgs());
                }
                if (this.timeRemainingLable.InvokeRequired)
                {
                     displayTimeRemaining d = new displayTimeRemaining(setTimer);
                    this.Invoke(d, new object[] { timeLeft.Seconds.ToString() });

                }
                else
                {
                    timeRemainingLable.Text = time.ToString();
                }
            }
        }

        private void setTimer(string timeLeft)
        {
            lock (timeRemainingLable.Text)
            {
                timeRemainingLable.Text = timeLeft;
            }
        }

       
        private void setButtonEnabled( bool start, bool stop, bool estp )
        {
            stopEventButton.Enabled = stop;
            estopButton.Enabled = estp;
            StartEventButton.Enabled = start;
        }

        #region eventHandlers


        private void timeAlmostUp(object sender, EventArgs e)
        {
            controller.timesAlmostUp();
        }
        private void timeIsUp(object send, EventArgs e)
        {
            controller.emergencyStop();
            emergencyStop();
        }
        private void missileFired( object sender, EventArgs e)
        {
            lock (this)
            {

                if (this.missileCount.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setMissileCount);
                    this.Invoke(d, new object[] { controller.numberOfMissiles.ToString() });      

                } else 
                {
                    missileCount.Text = controller.numberOfMissiles.ToString();
                }
            }
        }

        private void outOfMissiles(object sender, EventArgs e)
        {
            
        }

        private void eventTerminated( object sender, EventArgs e)
        {
            lock (this)
            {
                if (timerThread != null && timerThread.IsAlive)
                {
                    timerThread.Abort();
                }
                if (controller != null)
                {
                    controller = null;
                }
                if (this.StartEventButton.InvokeRequired || stopEventButton.InvokeRequired )
                {
                    changeButtons d = new changeButtons(setButtonEnabled);
                    this.Invoke(d, new object[] { true, false, false });      

                } else 
                {
                    missileCount.Text = controller.numberOfMissiles.ToString();
                }
            }
        }

        private void targetsFound(object sender, FoundTatgetEventArgs e)
        {
            lock (this)
            {
                numberOfFoes = e.FoeCount;
                numberOfFriends = e.FriendCount;
                targets = e.targets;

                if (this.foeCount.InvokeRequired || this.friendCount.InvokeRequired)
                {
                    setCountsCallBack d = new setCountsCallBack(setFriendFoeCounts);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    setFriendFoeCounts();
                }
            }

        }

        void setMissileCount(string text)
        {
            lock (missileCount)
            {
                missileCount.Text = text;
            }
        }

        void setFriendFoeCounts()
        {
            friendCount.Text = numberOfFriends.ToString();
            foeCount.Text = numberOfFoes.ToString();
        }


        #endregion

        #region controller control

        private void StartEvent_Click(object sender, EventArgs e)
        {
            if (controller == null)
            {
                setUpController();
            }
            time = new TimeSpan(0,0,0, Convert.ToInt32(eventTimeLimit.Text));
            setTimer(eventTimeLimit.Text);
            Counting = true;
            timerThreadStart = new ThreadStart(timer);
            timerThread = new Thread(timerThreadStart);
            timerThread.Start();

            stopEventButton.Enabled = true;
            estopButton.Enabled = true;
            StartEventButton.Enabled = false;

            controller.startEvent();
        }

        private void stopEvent_Click(object sender, EventArgs e)
        {
            stopEvent();
        }

        private void stopEvent()
        {
            stopEventButton.Enabled = false;
            estopButton.Enabled = false;
            StartEventButton.Enabled = true;
            Counting = false;
            if (timerThread != null && timerThread.IsAlive)
            {
                timerThread.Abort();
            }

            if (controller != null)
            {
                controller.stopEvent();
                controller = null;
            }
        }

        private void estop_Click(object sender, EventArgs e)
        {
            emergencyStop();
        }

        private void emergencyStop()
        {
            if (this.StartEventButton.InvokeRequired || stopEventButton.InvokeRequired)
            {
                changeButtons d = new changeButtons(setButtonEnabled);
                this.Invoke(d, new object[] { true, false, false });

            }
            else
            {
                setButtonEnabled(true, false, false);
            }
            if (timerThread != null && timerThread.IsAlive)
            {
                timerThread.Abort();
                timerThread = null;
            }
            if (controller != null)
            {
                controller.emergencyStop();
                controller = null;
            }
        }

        #endregion

        private void UserInterface_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (vision != null)
            {
                vision.Stop();
                vision = null;
            }
            if (controller != null)
            {
                //force the thread to die
                controller.emergencyStop();
                controller = null;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            vision.Stop();
            if (timerThread != null && timerThread.IsAlive)
            {
                timerThread.Abort();
            }
            if (controller != null)
            {
                controller.emergencyStop();
                controller = null;
            }
            this.Close();

        }

    }
}

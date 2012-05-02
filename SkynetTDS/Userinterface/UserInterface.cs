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

        int eventType;
        int numberOfMissiles;
        int numberOfFoes;
        int numberOfFriends;
        bool isEvent;

        public UserInterface()
        {
            InitializeComponent();
            
            //set default values
            stopEvent.Enabled = false;
            estop.Enabled = false;
            numberOfMissiles = 4;
            isEvent = false;

            //set up controller
            controllerCreator = new EventControllerCreator();
            controller = controllerCreator.createEventController(showEventSelect());
            controller.calibrateLauncher();
            controller.FoundTargets += new EventHandler<FoundTatgetEventArgs>(targetsFound);
            controller.MissileFired += new EventHandler(missileFired);
            controller.EventTerminated += new EventHandler(eventTerminated);
            controller.OutOfMissiles += new EventHandler(outOfMissiles);

            //set up camera
            vision = VisionDevice.getInstance();
            vision.Start();
            vision.CameraStarted += new EventHandler(captureStarted);
            vision.ImageCaptured += new EventHandler<ImageDeviceArgs>(updatIimage);
            vision.CameraStopped += new EventHandler(captureStopped);

            //initalize missile count
            missileCount.Text = numberOfMissiles.ToString();
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


        public void captureStarted( object sender, EventArgs e )
        {
           
        }
       
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

        public void captureStopped(object sender, EventArgs e)
        {

        }

        private void eventTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventType = eventTypeComboBox.SelectedIndex;
        }

        private void StartEvent_Click(object sender, EventArgs e)
        {
            stopEvent.Enabled = true;
            estop.Enabled = true;
            StartEvent.Enabled = false;
           // spawnEventController();
            controller.startEvent();
        }

   /*     private void spawnEventController()
        {
            if (!isEvent)
            {
                controller = controllerCreator.createEventController(eventType);
                controller.FoundTargets += new EventHandler<FoundTatgetEventArgs>(targetsFound);
                controller.MissileFired += new EventHandler(missileFired);
                isEvent = true;
            }
        }*/
        private void targetsFound( object sender, FoundTatgetEventArgs e)
        {
            lock(this)
            {
                numberOfFoes = e.FoeCount;
                numberOfFriends = e.FriendCount;
                targets = e.targets;

            if(this.foeCount.InvokeRequired || this.friendCount.InvokeRequired)
            {
                setCountsCallBack d = new setCountsCallBack(setFriendFoeCounts);
                    this.Invoke(d, new object[] {});      

                } else 
                {
                    setFriendFoeCounts();
                }
            }

        }
        #region eventHandlers
        private void missileFired( object sender, EventArgs e)
        {
            lock (this)
            {
                numberOfMissiles--;
                if (this.missileCount.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setMissileCount);
                    this.Invoke(d, new object[] { numberOfMissiles.ToString() });      

                } else 
                {
                    missileCount.Text = numberOfMissiles.ToString();
                }
            }
        }

        private void outOfMissiles(object sender, EventArgs e)
        {
            //controller.stopEvent();
        }

        private void eventTerminated( object sender, EventArgs e)
        {
            StartEvent.Enabled = true;
            stopEvent.Enabled = false;
            estop.Enabled = false;
        }
        #endregion

        void setMissileCount(string text)
        {
            missileCount.Text = text;
        }
        void setFriendFoeCounts()
        {
            friendCount.Text = numberOfFriends.ToString();
            foeCount.Text = numberOfFoes.ToString();
        }

        private void stopEvent_Click(object sender, EventArgs e)
        {
            if (controller != null)
            {
                controller.stopEvent();
            }
        }

        private void estop_Click(object sender, EventArgs e)
        {
            if (controller != null)
            {
                controller.emergencyStop();
            }
        }

        private void UserInterface_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (vision != null)
            {
                vision.Stop();
            }
            if (controller != null)
            {
                //force the thread to die
                controller.emergencyStop();
            }
        }
    }
}

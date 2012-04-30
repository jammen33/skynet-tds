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

        /// <summary>
        /// Object for synchronizing
        /// </summary>
        private object m_lockObject;
        delegate void SetTextCallback(string text);
        delegate void setCountsCallBack();
        delegate void setHWCallBack(ImageDeviceArgs e);

        int eventType;
        int numberOfMissiles;
        int numberOfFoes;
        int numberOfFriends;
        bool isEvent;
        bool HWSet;

        public UserInterface()
        {
            InitializeComponent();
            m_lockObject = new object();
            numberOfMissiles = 4;
            isEvent = false;
            HWSet = false;

            vision = VisionDevice.getInstance();
            vision.Start();

            controllerCreator = new EventControllerCreator();
            vision.CameraStarted += new EventHandler(captureStarted);
            vision.ImageCaptured += new EventHandler<ImageDeviceArgs>(updatIimage);
            vision.CameraStopped += new EventHandler(captureStopped);

            missileCount.Text = numberOfMissiles.ToString();
        }

        public void captureStarted( object sender, EventArgs e )
        {
           
        }
       
        public void updatIimage(object sender, ImageDeviceArgs e)
        {
            lock (e.Frame)
            {
                if (!HWSet)
                {
                    setHWCallBack d = new setHWCallBack(setHW);
                    this.Invoke(d, new object[] { e });
                }
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
                            //tmp.Draw(circle, new Bgr(Color.Red), 2);
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
            MessageBox.Show("Capture Stopped!");
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            vision.Stop();
            if (controller != null)
            {
                controller.stopEvent();
            }
        }

        private void eventTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventType = eventTypeComboBox.SelectedIndex;
        }

        private void StartEvent_Click(object sender, EventArgs e)
        {
            spawnEventController();
            controller.startEvent();
        }

        private void spawnEventController()
        {
            if (!isEvent)
            {
                controller = controllerCreator.createEventController(eventType);
                controller.FoundTargets += new EventHandler<FoundTatgetEventArgs>(targetsFound);
                controller.MissileFired += new EventHandler(missileFired);
                isEvent = true;
            }
        }
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
            controller.stopEvent();
        }

        private void estop_Click(object sender, EventArgs e)
        {
            controller.emergencyStop();
        }
        private void setHW(ImageDeviceArgs e)
        {
            displayImage.Height = e.Frame.Height;
            displayImage.Width = e.Frame.Width;
            HWSet = true;
        }
    }
}

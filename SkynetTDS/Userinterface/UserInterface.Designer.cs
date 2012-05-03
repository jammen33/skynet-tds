namespace SkynetTDS.Userinterface
{
    partial class UserInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.displayImage = new System.Windows.Forms.PictureBox();
            this.StartEvent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.missileCount = new System.Windows.Forms.Label();
            this.stopEvent = new System.Windows.Forms.Button();
            this.estop = new System.Windows.Forms.Button();
            this.foeCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.friendCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).BeginInit();
            this.SuspendLayout();
            // 
            // displayImage
            // 
            this.displayImage.Location = new System.Drawing.Point(132, 12);
            this.displayImage.Name = "displayImage";
            this.displayImage.Size = new System.Drawing.Size(673, 486);
            this.displayImage.TabIndex = 0;
            this.displayImage.TabStop = false;
            // 
            // StartEvent
            // 
            this.StartEvent.Location = new System.Drawing.Point(12, 39);
            this.StartEvent.Name = "StartEvent";
            this.StartEvent.Size = new System.Drawing.Size(114, 70);
            this.StartEvent.TabIndex = 4;
            this.StartEvent.Text = "Start Event";
            this.StartEvent.UseVisualStyleBackColor = true;
            this.StartEvent.Click += new System.EventHandler(this.StartEvent_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(442, 501);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Missiles Remaining:";
            // 
            // missileCount
            // 
            this.missileCount.AutoSize = true;
            this.missileCount.Location = new System.Drawing.Point(563, 501);
            this.missileCount.Name = "missileCount";
            this.missileCount.Size = new System.Drawing.Size(0, 13);
            this.missileCount.TabIndex = 6;
            // 
            // stopEvent
            // 
            this.stopEvent.Location = new System.Drawing.Point(12, 115);
            this.stopEvent.Name = "stopEvent";
            this.stopEvent.Size = new System.Drawing.Size(114, 70);
            this.stopEvent.TabIndex = 7;
            this.stopEvent.Text = "Stop Event";
            this.stopEvent.UseVisualStyleBackColor = true;
            this.stopEvent.Click += new System.EventHandler(this.stopEvent_Click);
            // 
            // estop
            // 
            this.estop.Location = new System.Drawing.Point(811, 39);
            this.estop.Name = "estop";
            this.estop.Size = new System.Drawing.Size(114, 70);
            this.estop.TabIndex = 8;
            this.estop.Text = "Emergency Stop";
            this.estop.UseVisualStyleBackColor = true;
            this.estop.Click += new System.EventHandler(this.estop_Click);
            // 
            // foeCount
            // 
            this.foeCount.AutoSize = true;
            this.foeCount.Location = new System.Drawing.Point(205, 501);
            this.foeCount.Name = "foeCount";
            this.foeCount.Size = new System.Drawing.Size(0, 13);
            this.foeCount.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 501);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Foe Targets: ";
            // 
            // friendCount
            // 
            this.friendCount.AutoSize = true;
            this.friendCount.Location = new System.Drawing.Point(210, 514);
            this.friendCount.Name = "friendCount";
            this.friendCount.Size = new System.Drawing.Size(0, 13);
            this.friendCount.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 514);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Friend Targets: ";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 539);
            this.Controls.Add(this.friendCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.foeCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.estop);
            this.Controls.Add(this.stopEvent);
            this.Controls.Add(this.missileCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartEvent);
            this.Controls.Add(this.displayImage);
            this.Name = "UserInterface";
            this.Text = "UserInterface";
            this.Load += new System.EventHandler(this.UserInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox displayImage;
        private System.Windows.Forms.Button StartEvent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label missileCount;
        private System.Windows.Forms.Button stopEvent;
        private System.Windows.Forms.Button estop;
        private System.Windows.Forms.Label foeCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label friendCount;
        private System.Windows.Forms.Label label4;
    }
}
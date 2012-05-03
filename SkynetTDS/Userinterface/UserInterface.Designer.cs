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
            this.StartEventButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.missileCount = new System.Windows.Forms.Label();
            this.stopEventButton = new System.Windows.Forms.Button();
            this.estopButton = new System.Windows.Forms.Button();
            this.foeCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.friendCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.eventTimeLimit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timeRemainingLable = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
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
            // StartEventButton
            // 
            this.StartEventButton.Location = new System.Drawing.Point(12, 39);
            this.StartEventButton.Name = "StartEventButton";
            this.StartEventButton.Size = new System.Drawing.Size(114, 70);
            this.StartEventButton.TabIndex = 4;
            this.StartEventButton.Text = "Start Event";
            this.StartEventButton.UseVisualStyleBackColor = true;
            this.StartEventButton.Click += new System.EventHandler(this.StartEvent_Click);
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
            // stopEventButton
            // 
            this.stopEventButton.Location = new System.Drawing.Point(12, 115);
            this.stopEventButton.Name = "stopEventButton";
            this.stopEventButton.Size = new System.Drawing.Size(114, 70);
            this.stopEventButton.TabIndex = 7;
            this.stopEventButton.Text = "Stop Event";
            this.stopEventButton.UseVisualStyleBackColor = true;
            this.stopEventButton.Click += new System.EventHandler(this.stopEvent_Click);
            // 
            // estopButton
            // 
            this.estopButton.Location = new System.Drawing.Point(811, 39);
            this.estopButton.Name = "estopButton";
            this.estopButton.Size = new System.Drawing.Size(114, 70);
            this.estopButton.TabIndex = 8;
            this.estopButton.Text = "Emergency Stop";
            this.estopButton.UseVisualStyleBackColor = true;
            this.estopButton.Click += new System.EventHandler(this.estop_Click);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Time Limit: ";
            // 
            // eventTimeLimit
            // 
            this.eventTimeLimit.Location = new System.Drawing.Point(15, 245);
            this.eventTimeLimit.Name = "eventTimeLimit";
            this.eventTimeLimit.Size = new System.Drawing.Size(100, 20);
            this.eventTimeLimit.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 313);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Time Remaining: ";
            // 
            // timeRemainingLable
            // 
            this.timeRemainingLable.AutoSize = true;
            this.timeRemainingLable.Location = new System.Drawing.Point(12, 335);
            this.timeRemainingLable.Name = "timeRemainingLable";
            this.timeRemainingLable.Size = new System.Drawing.Size(0, 13);
            this.timeRemainingLable.TabIndex = 16;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(848, 504);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 17;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 539);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.timeRemainingLable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.eventTimeLimit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.friendCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.foeCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.estopButton);
            this.Controls.Add(this.stopEventButton);
            this.Controls.Add(this.missileCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StartEventButton);
            this.Controls.Add(this.displayImage);
            this.Name = "UserInterface";
            this.Text = "UserInterface";
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox displayImage;
        private System.Windows.Forms.Button StartEventButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label missileCount;
        private System.Windows.Forms.Button stopEventButton;
        private System.Windows.Forms.Button estopButton;
        private System.Windows.Forms.Label foeCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label friendCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox eventTimeLimit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label timeRemainingLable;
        private System.Windows.Forms.Button exitButton;
    }
}
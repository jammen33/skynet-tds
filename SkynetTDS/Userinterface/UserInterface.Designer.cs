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
            this.start_capture = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.eventTypeComboBox = new System.Windows.Forms.ComboBox();
            this.StartEvent = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).BeginInit();
            this.SuspendLayout();
            // 
            // displayImage
            // 
            this.displayImage.Location = new System.Drawing.Point(301, 12);
            this.displayImage.Name = "displayImage";
            this.displayImage.Size = new System.Drawing.Size(431, 261);
            this.displayImage.TabIndex = 0;
            this.displayImage.TabStop = false;
            // 
            // start_capture
            // 
            this.start_capture.Location = new System.Drawing.Point(159, 65);
            this.start_capture.Name = "start_capture";
            this.start_capture.Size = new System.Drawing.Size(75, 23);
            this.start_capture.TabIndex = 1;
            this.start_capture.Text = "StartCamera";
            this.start_capture.UseVisualStyleBackColor = true;
            this.start_capture.Click += new System.EventHandler(this.start_capture_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(132, 110);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // eventTypeComboBox
            // 
            this.eventTypeComboBox.FormattingEnabled = true;
            this.eventTypeComboBox.Items.AddRange(new object[] {
            "Foe Event",
            "Friend Foe Event"});
            this.eventTypeComboBox.Location = new System.Drawing.Point(12, 12);
            this.eventTypeComboBox.Name = "eventTypeComboBox";
            this.eventTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.eventTypeComboBox.TabIndex = 3;
            this.eventTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.eventTypeComboBox_SelectedIndexChanged);
            // 
            // StartEvent
            // 
            this.StartEvent.Location = new System.Drawing.Point(12, 50);
            this.StartEvent.Name = "StartEvent";
            this.StartEvent.Size = new System.Drawing.Size(75, 23);
            this.StartEvent.TabIndex = 4;
            this.StartEvent.Text = "Start Event";
            this.StartEvent.UseVisualStyleBackColor = true;
            this.StartEvent.Click += new System.EventHandler(this.StartEvent_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 348);
            this.Controls.Add(this.StartEvent);
            this.Controls.Add(this.eventTypeComboBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.start_capture);
            this.Controls.Add(this.displayImage);
            this.Name = "UserInterface";
            this.Text = "UserInterface";
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox displayImage;
        private System.Windows.Forms.Button start_capture;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.ComboBox eventTypeComboBox;
        private System.Windows.Forms.Button StartEvent;
    }
}
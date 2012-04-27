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
            this.displayImage.Click += new System.EventHandler(this.displayImage_Click);
            // 
            // start_capture
            // 
            this.start_capture.Location = new System.Drawing.Point(132, 81);
            this.start_capture.Name = "start_capture";
            this.start_capture.Size = new System.Drawing.Size(75, 23);
            this.start_capture.TabIndex = 1;
            this.start_capture.Text = "Start";
            this.start_capture.UseVisualStyleBackColor = true;
            this.start_capture.Click += new System.EventHandler(this.start_capture_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 348);
            this.Controls.Add(this.start_capture);
            this.Controls.Add(this.displayImage);
            this.Name = "UserInterface";
            this.Text = "UserInterface";
            this.Load += new System.EventHandler(this.UserInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.displayImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox displayImage;
        private System.Windows.Forms.Button start_capture;
    }
}
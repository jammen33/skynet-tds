namespace SkynetTDS.Userinterface
{
    partial class eventSelect
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
            this.friendFoeButton = new System.Windows.Forms.Button();
            this.foeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // friendFoeButton
            // 
            this.friendFoeButton.Location = new System.Drawing.Point(12, 12);
            this.friendFoeButton.Name = "friendFoeButton";
            this.friendFoeButton.Size = new System.Drawing.Size(201, 91);
            this.friendFoeButton.TabIndex = 0;
            this.friendFoeButton.Text = "Friend Foe";
            this.friendFoeButton.UseVisualStyleBackColor = true;
            this.friendFoeButton.Click += new System.EventHandler(this.friendFoeButton_Click);
            // 
            // foeButton
            // 
            this.foeButton.Location = new System.Drawing.Point(12, 109);
            this.foeButton.Name = "foeButton";
            this.foeButton.Size = new System.Drawing.Size(201, 91);
            this.foeButton.TabIndex = 1;
            this.foeButton.Text = "Foe";
            this.foeButton.UseVisualStyleBackColor = true;
            this.foeButton.Click += new System.EventHandler(this.foeButton_Click);
            // 
            // eventSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 210);
            this.Controls.Add(this.foeButton);
            this.Controls.Add(this.friendFoeButton);
            this.Name = "eventSelect";
            this.Text = "eventSelect";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button friendFoeButton;
        private System.Windows.Forms.Button foeButton;
    }
}
namespace Lockdown {
    partial class LockdownForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.title = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.Label();
            this.bottomPictureBox = new System.Windows.Forms.PictureBox();
            this.topPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.title.Dock = System.Windows.Forms.DockStyle.Top;
            this.title.Font = new System.Drawing.Font("Segoe UI Symbol", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(0, 50);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(600, 118);
            this.title.TabIndex = 8;
            this.title.Text = "Access Denied";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.text.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.text.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text.Location = new System.Drawing.Point(0, 106);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(600, 144);
            this.text.TabIndex = 9;
            this.text.Text = "This computer is locked due to the security issue.";
            this.text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bottomPictureBox
            // 
            this.bottomPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.bottomPictureBox.BackgroundImage = global::Lockdown.Properties.Resources.Line;
            this.bottomPictureBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPictureBox.Location = new System.Drawing.Point(0, 250);
            this.bottomPictureBox.Name = "bottomPictureBox";
            this.bottomPictureBox.Size = new System.Drawing.Size(600, 50);
            this.bottomPictureBox.TabIndex = 7;
            this.bottomPictureBox.TabStop = false;
            // 
            // topPictureBox
            // 
            this.topPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.topPictureBox.BackgroundImage = global::Lockdown.Properties.Resources.Line;
            this.topPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPictureBox.Location = new System.Drawing.Point(0, 0);
            this.topPictureBox.Name = "topPictureBox";
            this.topPictureBox.Size = new System.Drawing.Size(600, 50);
            this.topPictureBox.TabIndex = 6;
            this.topPictureBox.TabStop = false;
            // 
            // LockdownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.title);
            this.Controls.Add(this.text);
            this.Controls.Add(this.bottomPictureBox);
            this.Controls.Add(this.topPictureBox);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LockdownForm";
            this.Text = "Lock";
            ((System.ComponentModel.ISupportInitialize)(this.bottomPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.PictureBox bottomPictureBox;
        private System.Windows.Forms.PictureBox topPictureBox;
    }
}
namespace Lockdown {
    partial class Main {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.MainPanel = new System.Windows.Forms.Panel();
            this.PassBoxIndicator = new System.Windows.Forms.Label();
            this.PassIndicator = new System.Windows.Forms.Panel();
            this.PassPanel = new System.Windows.Forms.Panel();
            this.PassBox = new System.Windows.Forms.TextBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.UserText = new System.Windows.Forms.Label();
            this.ProfilePic = new System.Windows.Forms.PictureBox();
            this.PassText = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            this.PassPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 1000;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.MainPanel.Controls.Add(this.PassText);
            this.MainPanel.Controls.Add(this.PassBoxIndicator);
            this.MainPanel.Controls.Add(this.PassIndicator);
            this.MainPanel.Controls.Add(this.PassPanel);
            this.MainPanel.Controls.Add(this.StatusLabel);
            this.MainPanel.Controls.Add(this.UserText);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MainPanel.Location = new System.Drawing.Point(0, 50);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(600, 400);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            // 
            // PassBoxIndicator
            // 
            this.PassBoxIndicator.AutoSize = true;
            this.PassBoxIndicator.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
            this.PassBoxIndicator.Location = new System.Drawing.Point(13, 403);
            this.PassBoxIndicator.Name = "PassBoxIndicator";
            this.PassBoxIndicator.Size = new System.Drawing.Size(0, 25);
            this.PassBoxIndicator.TabIndex = 5;
            // 
            // PassIndicator
            // 
            this.PassIndicator.Location = new System.Drawing.Point(126, 275);
            this.PassIndicator.Name = "PassIndicator";
            this.PassIndicator.Size = new System.Drawing.Size(240, 10);
            this.PassIndicator.TabIndex = 4;
            this.PassIndicator.Paint += new System.Windows.Forms.PaintEventHandler(this.PassIndicator_Paint);
            this.PassIndicator.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            // 
            // PassPanel
            // 
            this.PassPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PassPanel.Controls.Add(this.PassBox);
            this.PassPanel.Location = new System.Drawing.Point(126, 229);
            this.PassPanel.Name = "PassPanel";
            this.PassPanel.Size = new System.Drawing.Size(240, 40);
            this.PassPanel.TabIndex = 3;
            this.PassPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            // 
            // PassBox
            // 
            this.PassBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PassBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PassBox.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassBox.ForeColor = System.Drawing.Color.White;
            this.PassBox.Location = new System.Drawing.Point(10, 7);
            this.PassBox.Name = "PassBox";
            this.PassBox.Size = new System.Drawing.Size(220, 26);
            this.PassBox.TabIndex = 2;
            this.PassBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PassBox.UseSystemPasswordChar = true;
            this.PassBox.TextChanged += new System.EventHandler(this.PassBox_TextChanged);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Segoe UI Variable Small", 12F);
            this.StatusLabel.Location = new System.Drawing.Point(40, 122);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(368, 21);
            this.StatusLabel.TabIndex = 1;
            this.StatusLabel.Text = "This computer is locked due to the security issue.";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StatusLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            // 
            // UserText
            // 
            this.UserText.AutoSize = true;
            this.UserText.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserText.Location = new System.Drawing.Point(164, 63);
            this.UserText.Name = "UserText";
            this.UserText.Size = new System.Drawing.Size(144, 43);
            this.UserText.TabIndex = 0;
            this.UserText.Text = "UserText";
            this.UserText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UserText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            // 
            // ProfilePic
            // 
            this.ProfilePic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ProfilePic.Location = new System.Drawing.Point(172, 0);
            this.ProfilePic.Name = "ProfilePic";
            this.ProfilePic.Size = new System.Drawing.Size(100, 100);
            this.ProfilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ProfilePic.TabIndex = 1;
            this.ProfilePic.TabStop = false;
            this.ProfilePic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            // 
            // PassText
            // 
            this.PassText.AutoSize = true;
            this.PassText.Font = new System.Drawing.Font("Segoe UI Variable Display Semib", 14F, System.Drawing.FontStyle.Bold);
            this.PassText.Location = new System.Drawing.Point(208, 183);
            this.PassText.Name = "PassText";
            this.PassText.Size = new System.Drawing.Size(93, 26);
            this.PassText.TabIndex = 6;
            this.PassText.Text = "Password";
            this.PassText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.ProfilePic);
            this.Controls.Add(this.MainPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lockdown";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Main_Load);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.PassPanel.ResumeLayout(false);
            this.PassPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProfilePic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox ProfilePic;
        private System.Windows.Forms.Label UserText;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.TextBox PassBox;
        private System.Windows.Forms.Panel PassPanel;
        private System.Windows.Forms.Panel PassIndicator;
        private System.Windows.Forms.Label PassBoxIndicator;
        private System.Windows.Forms.Label PassText;
    }
}


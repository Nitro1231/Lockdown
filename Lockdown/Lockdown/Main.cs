﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Lockdown {
    public partial class Main : Form {
        Lockdown lockdown;
        string pass = "1234";

        public Main() {
            InitializeComponent();
            lockdown = new Lockdown();

            UserText.Text = $"Hello, {Environment.UserName}!";

            string path = Directory.GetCurrentDirectory() + @"\ProfilePic.jpg";
            if (File.Exists(path))
                ProfilePic.Image = Image.FromFile(path);
            TopMost = true;
        }

        private void Main_Load(object sender, EventArgs e) {
            Update();
            Refresh();

            ProfilePic.Location = new Point(Utils.CenterX(this, ProfilePic), 0);
            UserText.Location = new Point(Utils.CenterX(this, UserText), ProfilePic.Height / 2 + 10);
            
            StatusLabel.Location = addMargin(StatusLabel, UserText, 20);
            PassText.Location = addMargin(PassText, StatusLabel, 30);
            PassPanel.Location = addMargin(PassPanel, PassText, 10);
            PassIndicator.Width = 0;
            PassIndicator.Location = addMargin(PassIndicator, PassPanel, 5);

            Utils.SmoothBorder(this, 12);
            Utils.SmoothBorder(MainPanel, 12);
            Utils.SmoothBorder(ProfilePic, ProfilePic.Width);
            Utils.SmoothBorder(PassPanel, PassPanel.Height / 2);
            Utils.SmoothBorder(PassIndicator, 6);
        }

        private void MainTimer_Tick(object sender, EventArgs e) {
            //lockdown.Lock();
        }
        
        private void PassBox_TextChanged(object sender, EventArgs e) {
            PassBoxIndicator.Text = new string(PassBox.PasswordChar, PassBox.TextLength);

            if (PassBoxIndicator.Width >= PassBox.Width)
                PassIndicator.Width = PassBox.Width;
            else
                PassIndicator.Width = PassBoxIndicator.Width;

            PassIndicator.Left = Utils.CenterX(this, PassIndicator);
            PassIndicator.Refresh();
            Utils.SmoothBorder(PassIndicator, 6);

            if (PassBox.Text.Equals(pass)) {
                MainTimer.Enabled = false;
                lockdown.Unlock();
                MessageBox.Show("This PC has been successfully unlocked!");
                Close();
            }
        }

        private void PassIndicator_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;
            Rectangle rectangle = new Rectangle(0, 0, PassIndicator.Width, PassIndicator.Height);
            Brush brush = new LinearGradientBrush(rectangle, Color.FromArgb(40, 242, 156), Color.FromArgb(12, 184, 224), 65f);
            graphics.FillRectangle(brush, rectangle);
        }

        private Point addMargin(Control ctl, Control upper, int margin) {
            return new Point((Width - ctl.Width) / 2, upper.Top + upper.Height + margin);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e) {
            Utils.MouseMove(Handle);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Lockdown {
    public partial class Main : Form {

        Lockdown lockdown;
        string pass = "1234";

        public Main() {
            InitializeComponent();
            lockdown = new Lockdown();
        }

        private void Main_Load(object sender, EventArgs e) {
            label1.Font = pwBox.Font;
        }

        private void checkPW(string pw) {
            if (pw.Equals(pass)) {
                lockdown.Unlock();
                mainTimer.Enabled = false;
            }
        }

        private void PwBox_TextChanged(object sender, EventArgs e) {
            label1.Text = pwBox.Text;
            label1.Update();
            panel1.Width = label1.Width;
        }

        private void Button1_Click(object sender, EventArgs e) { checkPW(pwBox.Text); }

        private void mainTimer_Tick(object sender, EventArgs e) {
            lockdown.Lock();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Lockdown {
    public partial class Main : Form {

        #region Win32 API
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowText", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);
        private static List<IntPtr> WindowHandles;

        private struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        RECT info;
        private static int GWL_EXSTYLE = -20;
        private static int WS_EX_LAYERED = 0x80000;
        private static uint LWA_ALPHA = 0x2;
        #endregion

        string pass = "1234";
        string[] ignore = { "WindowsInternal", "ApplicationFrameHost", "NVIDIA Share", "Taskmgr" };

        //https://www.meziantou.net/detect-the-opening-of-a-new-window-in-csharp.htm
        //https://stackoverflow.com/questions/27086816/how-to-get-process-type-app-background-process-or-windows-process

        public Main() { InitializeComponent(); }

        private void Main_Load(object sender, EventArgs e) {
            label1.Font = pwBox.Font;

            List<IntPtr> handles;
            GetDesktopWindowHandlesAndTitles(out handles);

            foreach(IntPtr h in handles) {
                string name = getWindowTitle(h);
                uint pid;
                GetWindowThreadProcessId(h, out pid);
                Process p = Process.GetProcessById((int)pid);
                if (!string.IsNullOrEmpty(name) && !ifContainArray(p.ProcessName, ignore) && p.Responding)
                    lockdownWindow(h);
            }

            //Process[] processes = Process.GetProcesses();
            //foreach (Process p in processes) {
            //    if (!string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowHandle != IntPtr.Zero && p.Responding && !ifContainArray(p.ProcessName, ignore)) {
            //        //richTextBox1.AppendText(p.ProcessName + "\n" + p.MainWindowTitle + "\n===========\n");
            //        lockdownWindow(p);
            //    }
            //}
        }

        private static void GetDesktopWindowHandlesAndTitles(out List<IntPtr> handles) {
            WindowHandles = new List<IntPtr>();
            if (!EnumDesktopWindows(IntPtr.Zero, FilterCallback, IntPtr.Zero))
                handles = null;
            else 
                handles = WindowHandles;
        }

        private static bool FilterCallback(IntPtr hWnd, int lParam) {
            if (IsWindowVisible(hWnd))
                WindowHandles.Add(hWnd);
            return true;
        }

        private string getWindowTitle(IntPtr hWnd) {
            StringBuilder sb_title = new StringBuilder(1024);
            int length = GetWindowText(hWnd, sb_title, sb_title.Capacity);
            string title = sb_title.ToString();
            return title;
        }

        private void lockdownWindow(IntPtr p) {
            GetWindowRect(p, out info);
            int w = info.Right - info.Left;
            int h = info.Bottom - info.Top;

            LockdownForm lockdown = new LockdownForm();
            BackgroundForm background = new BackgroundForm(lockdown);
            background.Size = new Size(w, h);

            SetWindowLong(background.Handle, GWL_EXSTYLE, GetWindowLong(background.Handle, GWL_EXSTYLE) | WS_EX_LAYERED);
            SetParent(background.Handle, p);
            background.SetBounds(0, 0, 0, 0, BoundsSpecified.Location);
            background.Show();
            SetLayeredWindowAttributes(background.Handle, 0, 200, LWA_ALPHA);

            lockdown.Width = w;
            SetWindowLong(lockdown.Handle, GWL_EXSTYLE, GetWindowLong(lockdown.Handle, GWL_EXSTYLE) | WS_EX_LAYERED);
            SetParent(lockdown.Handle, p);
            lockdown.SetBounds(0, (h - lockdown.Height) / 2, 0, 0, BoundsSpecified.Location);
            lockdown.Show();
            SetLayeredWindowAttributes(lockdown.Handle, 0, 255, LWA_ALPHA);
        }

        private void checkPW(string pw) {
            if (pw.Equals(pass))
                Close();
        }

        private void PwBox_TextChanged(object sender, EventArgs e) {
            label1.Text = pwBox.Text;
            label1.Update();
            panel1.Width = label1.Width;
        }

        private bool ifContainArray(string text, string[] arr) {
            foreach (string a in arr)
                if (text.Contains(a))
                    return true;
            return false;
        }

        private void Button1_Click(object sender, EventArgs e) { checkPW(pwBox.Text); }
    }
}
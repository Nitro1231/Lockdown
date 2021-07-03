using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Lockdown {
    class Lockdown {

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

        private Timer timer;
        private List<string> whitelist;
        private List<IntPtr> handles, blockedHandles;

        //https://www.meziantou.net/detect-the-opening-of-a-new-window-in-csharp.htm
        //https://stackoverflow.com/questions/27086816/how-to-get-process-type-app-background-process-or-windows-process

        public Lockdown() {
            whitelist = new List<string>();
            handles = new List<IntPtr>();
            blockedHandles = new List<IntPtr>();

            whitelist.Add("WindowsInternal");
            whitelist.Add("ApplicationFrameHost");
            whitelist.Add("NVIDIA Share");
            whitelist.Add("Taskmgr");
            whitelist.Add("Lockdown");
            //whitelist.Add("devenv");
            whitelist.Add("TextInputHost");


            timer = new Timer();
            timer.Interval = 1000; // 10 Sec.
            timer.Elapsed += new ElapsedEventHandler(MainTick);
        }

        public void Lock(bool status) {
            handles.Clear();
            blockedHandles.Clear();

            if (status)
                timer.Start();
            else
                timer.Stop();
        }

        private void MainTick(object sender, ElapsedEventArgs e) {
            GetWindowHandles(out handles);
            foreach (IntPtr h in handles) {
                uint pid;
                GetWindowThreadProcessId(h, out pid);
                Process p = Process.GetProcessById((int)pid);
                if (!whitelist.Contains(p.ProcessName) && p.Responding) {
                    Console.WriteLine(p.ProcessName);
                    blockedHandles.Add(h);
                    LockdownWindow(h);
                }
            }

            //Process[] processes = Process.GetProcesses();
            //foreach (Process p in processes) {
            //    if (!string.IsNullOrEmpty(p.MainWindowTitle) && p.MainWindowHandle != IntPtr.Zero && p.Responding && !ifContainArray(p.ProcessName, ignore)) {
            //        //richTextBox1.AppendText(p.ProcessName + "\n" + p.MainWindowTitle + "\n===========\n");
            //        lockdownWindow(p);
            //    }
            //}

            timer.Enabled = false;
        }

        private void LockdownWindow(IntPtr p) {
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

        private void GetWindowHandles(out List<IntPtr> handles) {
            WindowHandles = new List<IntPtr>();
            if (!EnumDesktopWindows(IntPtr.Zero, FilterCallback, IntPtr.Zero))
                handles = null;
            else
                handles = WindowHandles;
        }

        private bool FilterCallback(IntPtr hWnd, int lParam) {
            string title = GetWindowTitle(hWnd);
            
            if (IsWindowVisible(hWnd) && !blockedHandles.Contains(hWnd) && !whitelist.Contains(title) && !string.IsNullOrEmpty(title))
                WindowHandles.Add(hWnd);
            return true;
        }

        private string GetWindowTitle(IntPtr hWnd) {
            StringBuilder sb_title = new StringBuilder(1024);
            GetWindowText(hWnd, sb_title, sb_title.Capacity);
            return sb_title.ToString();
        }
    }
}

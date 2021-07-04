using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

//https://www.meziantou.net/detect-the-opening-of-a-new-window-in-csharp.htm
//https://stackoverflow.com/questions/27086816/how-to-get-process-type-app-background-process-or-windows-process

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
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

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

        private List<string> exception;
        private List<LockdownForm> lockdownForms;
        private List<BackgroundForm> backgroundForms;
        private List<IntPtr> handles, blockedHandles;

        /// <summary>
        ///     Initialize the Lockdown class and add processes to the whitelist.
        /// </summary>
        public Lockdown() {
            exception = new List<string>();
            lockdownForms = new List<LockdownForm>();
            backgroundForms = new List<BackgroundForm>();
            handles = new List<IntPtr>();
            blockedHandles = new List<IntPtr>();

            // Add exceptions.
            exception.Add("WindowsInternal");
            exception.Add("ApplicationFrameHost");
            exception.Add("NVIDIA Share");
            exception.Add("Taskmgr");
            exception.Add("Lockdown");
            exception.Add("devenv");
            exception.Add("TextInputHost");
        }

        /// <summary>
        ///     Lockdown currently opened windows except for the windows that are already blocked.
        /// </summary>
        public void Lock() {
            // Get all window handles.
            handles = GetWindowHandles();
            foreach (IntPtr hWnd in handles) {
                uint pid;
                GetWindowThreadProcessId(hWnd, out pid);
                Process p = Process.GetProcessById((int)pid);
                if (!exception.Contains(p.ProcessName) && p.Responding) {
                    //Console.WriteLine(p.ProcessName);
                    blockedHandles.Add(hWnd);
                    LockdownWindow(hWnd); // Lockdown window
                }
            }
        }

        /// <summary>
        ///     Close all the LockdownForm and Back BackgroundForm that are currently open.
        /// </summary>
        public void Unlock() {
            foreach (LockdownForm lockdown in lockdownForms)
                lockdown.Close();
            foreach (BackgroundForm background in backgroundForms)
                background.Close();
            handles.Clear();
            blockedHandles.Clear();
        }

        /// <summary>
        ///     Create a new LockdownForm and BackgroundForm that fits the target window and add them as a child window.
        /// </summary>
        /// <param name="hWnd">
        ///     Window handle ID.
        /// </param>
        private void LockdownWindow(IntPtr hWnd) {
            GetWindowRect(hWnd, out info);
            int w = info.Right - info.Left;
            int h = info.Bottom - info.Top;

            // Initialize new forms and change the BackgroundForm's size identical to the target.
            LockdownForm lockdown = new LockdownForm();
            BackgroundForm background = new BackgroundForm(lockdown);
            background.Size = new Size(w, h);

            // Change window style and set a target as the parent window.
            SetWindowLong(background.Handle, GWL_EXSTYLE, GetWindowLong(background.Handle, GWL_EXSTYLE) | WS_EX_LAYERED);
            SetParent(background.Handle, hWnd);
            background.SetBounds(0, 0, 0, 0, BoundsSpecified.Location);
            background.Show();
            SetLayeredWindowAttributes(background.Handle, 0, 200, LWA_ALPHA);

            lockdown.Width = w;
            SetWindowLong(lockdown.Handle, GWL_EXSTYLE, GetWindowLong(lockdown.Handle, GWL_EXSTYLE) | WS_EX_LAYERED);
            SetParent(lockdown.Handle, hWnd);
            lockdown.SetBounds(0, (h - lockdown.Height) / 2, 0, 0, BoundsSpecified.Location);
            lockdown.Show();
            SetLayeredWindowAttributes(lockdown.Handle, 0, 255, LWA_ALPHA);

            lockdownForms.Add(lockdown);
            backgroundForms.Add(background);
        }

        /// <summary>
        ///     Get and filter currently opened window handles.
        /// </summary>
        /// <returns>
        ///     Return the group of window handles in form of List<IntPtr>.
        /// </returns>
        private List<IntPtr> GetWindowHandles() {
            WindowHandles = new List<IntPtr>();
            if (!EnumDesktopWindows(IntPtr.Zero, FilterCallback, IntPtr.Zero))
                return null;
            else
                return WindowHandles;
        }

        /// <summary>
        ///     Filter if the handle actually has a window, visible, and suitable to block.
        /// </summary>
        /// <param name="hWnd">
        ///     Window handle ID.
        /// </param>
        /// <returns>
        ///     Return true if it passes the filter.
        /// </returns>
        private bool FilterCallback(IntPtr hWnd, int lParam) {
            string title = GetWindowTitle(hWnd);
            if (IsWindowVisible(hWnd) && !blockedHandles.Contains(hWnd) && !exception.Contains(title) && !string.IsNullOrEmpty(title))
                WindowHandles.Add(hWnd);
            return true;
        }

        /// <summary>
        ///     Get window title associated with window handle ID.
        /// </summary>
        /// <param name="hWnd">
        ///     Window handle ID.
        /// </param>
        /// <returns>
        ///     The title of window handle ID.
        /// </returns>
        private string GetWindowTitle(IntPtr hWnd) {
            StringBuilder sb_title = new StringBuilder(1024);
            GetWindowText(hWnd, sb_title, sb_title.Capacity);
            return sb_title.ToString();
        }
    }
}

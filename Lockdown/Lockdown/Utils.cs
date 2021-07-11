// by Nitro
// E-Mail: nitro0@naver.com
// GitHub: https://github.com/Nitro1231

// [License]
// This work is licensed under a Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License
// Check the more detail about the license at here: https://creativecommons.org/licenses/by-nc-sa/4.0/

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace Lockdown {
    class Utils {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("Gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(IntPtr hObject);

        public static void MouseMove(IntPtr handle) {
            ReleaseCapture();
            SendMessage(handle, 0xA1, 0x2, 0);
        }

        public static void SmoothBorder(Form form, int round) {
            IntPtr ptr = CreateRoundRectRgn(0, 0, form.Width, form.Height, round, round);
            form.Region = Region.FromHrgn(ptr);
            DeleteObject(ptr);
        }

        public static void SmoothBorder(Control ctl, int round) {
            IntPtr ptr = CreateRoundRectRgn(0, 0, ctl.Width, ctl.Height, round, round);
            ctl.Region = Region.FromHrgn(ptr);
            DeleteObject(ptr);
        }

        public static Point Center(Control parent, Control target, int x = 0, int y = 0) {
            return new Point((parent.Width - target.Width) / 2 + x, (parent.Height - parent.Height) / 2 + y);
        }

        public static int CenterX(Control parent, Control target, int x = 0) {
            return (parent.Width - target.Width) / 2 + x;
        }

        public static int CenterY(Control parent, Control target, int y = 0) {
            return (parent.Height - target.Height) / 2 + y;
        }
    }
}

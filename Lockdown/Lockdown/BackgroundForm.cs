using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lockdown {
    public partial class BackgroundForm : Form {
        LockdownForm lk;
        public BackgroundForm(LockdownForm lockdown) {
            InitializeComponent();
            lk = lockdown;
        }

        private void LockdownForm_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(51, 136, 119), Color.FromArgb(17, 17, 51), 45f);
            graphics.FillRectangle(b, gradient_rectangle);
        }

        private void BackgroundForm_MouseDown(object sender, MouseEventArgs e) { lk.BringToFront(); }
    }
}
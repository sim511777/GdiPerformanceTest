using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GdiPerformanceTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            ChangeDoubleBuffering();
        }

        private void chkDoubleBuffered_CheckedChanged(object sender, EventArgs e) {
            ChangeDoubleBuffering();
        }

        private void ChangeDoubleBuffering() {
            PropertyInfo propertyInfo = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfo.SetValue(pnlDraw, chkDoubleBuffered.Checked, null);
        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e) {
            var t0 = Util.GetTimeMs();
            DrawItems(e.Graphics);
            var t1 = Util.GetTimeMs();
            string msg = 
                $@"doubleBuffered : {chkDoubleBuffered.Checked}
time : {t1 - t0:f0}ms
";
            var size = e.Graphics.MeasureString(msg, Font);
            e.Graphics.FillRectangle(Brushes.White, new RectangleF(Point.Empty, size));
            e.Graphics.DrawString(msg, Font, Brushes.Black, Point.Empty);
        }

        private void DrawItems(Graphics g) {
            string s = "gdi";
            Font font = Font;
            Brush brush = Brushes.Lime;
            for (int y = 0; y < 800; y += 16) {
                for (int x = 0; x < 1600; x += 16) {
                    g.DrawString(s, font, brush, x + szPan.Width, y + szPan.Height);
                }
            }
        }

        Size szPan = Size.Empty;
        Point lastPos = Point.Empty;
        private void pnlDraw_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button.HasFlag(MouseButtons.Left)) {
                szPan += ((Size)e.Location - (Size)lastPos);
                pnlDraw.Invalidate();
            }
            lastPos = e.Location;
        }
    }
}

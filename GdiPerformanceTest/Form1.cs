using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GdiPerformanceTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            pnlDraw.MouseWheel += PnlDraw_MouseWheel;
        }

        private void PnlDraw_MouseWheel(object sender, MouseEventArgs e) {
            zoomLevel += (e.Delta > 0 ? 1 : -1);
            zoomLevel = Math.Min(Math.Max(zoomLevel, 0), 8);
            Redraw();
        }

        Size szPan = Size.Empty;
        int zoomLevel = 4;
        Bitmap bmp = null;

        private void chkDoubleBuffered_CheckedChanged(object sender, EventArgs e) {
            ChangeDoubleBuffering();
        }

        private void ChangeDoubleBuffering() {
            PropertyInfo propertyInfo = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfo.SetValue(pnlDraw, chkDoubleBuffered.Checked, null);
        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e) {
            var t0 = Util.GetTime();

            if (chkUseBackBuffer.Checked) {
                using (var g = Graphics.FromImage(bmp)) {
                    g.Clear(pnlDraw.BackColor);
                    var drawItem = lbxDrawItem.SelectedIndex;
                    switch (drawItem) {
                        case 0: DrawString_Gdip(g); break;
                        case 2: DrawRectangle_Gdip(g); break;
                        case 3: FillRectangle_Gdip(g); break;
                        case 4: DrawEllipse_Gdip(g); break;
                        case 5: FillEllipse_Gdip(g); break;
                        default: break;
                    }
                }
                e.Graphics.DrawImage(bmp, 0, 0);
            } else {
                var g = e.Graphics;
                var drawItem = lbxDrawItem.SelectedIndex;
                switch (drawItem) {
                    case 0: DrawString_Gdip(g); break;
                    case 1: DrawString_Gdi(g); break;   // 이상하게 Bmp.Graphics에 그리면 뻗음
                    case 2: DrawRectangle_Gdip(g); break;
                    case 3: FillRectangle_Gdip(g); break;
                    case 4: DrawEllipse_Gdip(g); break;
                    case 5: FillEllipse_Gdip(g); break;
                    default: break;
                }
            }

            var t1 = Util.GetTime();
            var dt = t1 - t0;
            var fps = 1 / dt;
            var dtms = dt * 1000;
            var drawPerUs = dt * 1000 * 1000 / 5000;
            string msg =
$@"DoubleBuffered : {chkDoubleBuffered.Checked}
time : {dtms:f0}ms
fps : {fps:f0}
time per draw : {drawPerUs:f0}us
zoom : {zoomLevel}, pan : {szPan}
";
            var size = e.Graphics.MeasureString(msg, Font);
            e.Graphics.FillRectangle(Brushes.White, new RectangleF(Point.Empty, size));
            e.Graphics.DrawString(msg, Font, Brushes.Black, Point.Empty);
        }

        private void DrawLoop(Action<int, int> drawAction) {
            int step = (int)Math.Pow(2, zoomLevel);
            for (int y = 0; y < 50; y += 1) {
                for (int x = 0; x < 100; x += 1) {
                    drawAction(x * step + szPan.Width, y * step + szPan.Height);
                }
            }
        }

        private void DrawString_Gdip(Graphics g) {
            string s = "G";
            Font font = Font;
            Brush brush = Brushes.Lime;
            Color color = Color.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.DrawString(s, font, brush, x, y);
            };
            DrawLoop(drawAction);
        }

        private void DrawString_Gdi(Graphics g) {
            string s = "G";
            Font font = Font;
            Brush brush = Brushes.Lime;
            Color color = Color.Lime;
            Action<int, int> drawAction = (x, y) => {
                TextRenderer.DrawText(g, s, font, new Point(x, y), color);
            };
            DrawLoop(drawAction);
        }

        private void DrawRectangle_Gdip(Graphics g) {
            Pen pen = Pens.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.DrawRectangle(pen, x, y, 14, 14);
            };
            DrawLoop(drawAction);
        }

        private void FillRectangle_Gdip(Graphics g) {
            Brush br = Brushes.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.FillRectangle(br, x, y, 14, 14);
            };
            DrawLoop(drawAction);
        }

        private void DrawEllipse_Gdip(Graphics g) {
            Pen pen = Pens.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.DrawEllipse(pen, x, y, 14, 14);
            };
            DrawLoop(drawAction);
        }

        private void FillEllipse_Gdip(Graphics g) {
            Brush br = Brushes.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.FillEllipse(br, x, y, 14, 14);
            };
            DrawLoop(drawAction);
        }

        Point lastPos = Point.Empty;
        private void pnlDraw_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button.HasFlag(MouseButtons.Left)) {
                szPan += ((Size)e.Location - (Size)lastPos);
                Redraw();
            }
            lastPos = e.Location;
        }

        private void Redraw() {
            pnlDraw.Invalidate();
        }

        private void pnlDraw_Layout(object sender, LayoutEventArgs e) {
            if (bmp != null)
                bmp.Dispose();
            bmp = new Bitmap(pnlDraw.Width, pnlDraw.Height, PixelFormat.Format32bppPArgb);
        }

        private void btnRedraw_Click(object sender, EventArgs e) {
            Redraw();
        }
    }
}

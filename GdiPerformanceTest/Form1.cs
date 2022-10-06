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
        }

        Size szPan = Size.Empty;

        private void chkDoubleBuffered_CheckedChanged(object sender, EventArgs e) {
            ChangeDoubleBuffering();
        }

        private void ChangeDoubleBuffering() {
            PropertyInfo propertyInfo = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfo.SetValue(pnlDraw, chkDoubleBuffered.Checked, null);
        }

        private void pnlDraw_Paint(object sender, PaintEventArgs e) {
            var t0 = Util.GetTimeMs();

            var drawItem = lbxDrawItem.SelectedIndex;
            switch (drawItem) {
                case 0: DrawString_Gdip(e.Graphics); break;
                case 1: DrawRectangle_Gdip(e.Graphics); break;
                case 2: FillRectangle_Gdip(e.Graphics); break;
                case 3: DrawEllipse_Gdip(e.Graphics); break;
                case 4: FillEllipse_Gdip(e.Graphics); break;
                default: break;
            }
            
            var t1 = Util.GetTimeMs();
            string msg = 
                $@"DoubleBuffered : {chkDoubleBuffered.Checked}
Use GDI instead GDI+ : {chkUseGDI.Checked}
time : {t1 - t0:f0}ms
";
            var size = e.Graphics.MeasureString(msg, Font);
            e.Graphics.FillRectangle(Brushes.White, new RectangleF(Point.Empty, size));
            e.Graphics.DrawString(msg, Font, Brushes.Black, Point.Empty);
        }

        private void DrawLoop(Action<int, int> drawAction) {
            for (int y = 0; y < 800; y += 16) {
                for (int x = 0; x < 1600; x += 16) {
                    drawAction(x, y);
                }
            }
        }

        private void DrawString_Gdip(Graphics g) {
            string s = "gdi";
            Font font = Font;
            Brush brush = Brushes.Lime;
            Color color = Color.Lime;
            if (chkUseGDI.Checked) {
                Action<int, int> drawAction = (x, y) => {
                    TextRenderer.DrawText(g, s, font, new Point(x + szPan.Width, y + szPan.Height), color);
                };
                DrawLoop(drawAction);
            } else {
                Action<int, int> drawAction = (x, y) => {
                    g.DrawString(s, font, brush, x + szPan.Width, y + szPan.Height);
                };
                DrawLoop(drawAction);
            }
        }

        private void DrawRectangle_Gdip(Graphics g) {
            Pen pen = Pens.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.DrawRectangle(pen, x + szPan.Width, y + szPan.Height, 14, 14);
            };
            DrawLoop(drawAction);
        }

        private void FillRectangle_Gdip(Graphics g) {
            Brush br = Brushes.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.FillRectangle(br, x + szPan.Width, y + szPan.Height, 14, 14);
            };
            DrawLoop(drawAction);
        }

        private void DrawEllipse_Gdip(Graphics g) {
            Pen pen = Pens.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.DrawEllipse(pen, x + szPan.Width, y + szPan.Height, 14, 14);
            };
            DrawLoop(drawAction);
        }

        private void FillEllipse_Gdip(Graphics g) {
            Brush br = Brushes.Lime;
            Action<int, int> drawAction = (x, y) => {
                g.FillEllipse(br, x + szPan.Width, y + szPan.Height, 14, 14);
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

        }

        private void btnRedraw_Click(object sender, EventArgs e) {
            Redraw();
        }
    }
}

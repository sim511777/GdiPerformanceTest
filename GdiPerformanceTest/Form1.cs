﻿using System;
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
            var t0 = Util.GetTimeMs();

            if (chkUseBackBuffer.Checked) {
                using (var bmpG = Graphics.FromImage(bmp)) {
                    bmpG.Clear(pnlDraw.BackColor);
                    DrawStuffs(bmpG);
                }
                e.Graphics.DrawImage(bmp, 0, 0);
            } else {
                DrawStuffs(e.Graphics);
            }

            var t1 = Util.GetTimeMs();
            string msg =
                $@"DoubleBuffered : {chkDoubleBuffered.Checked}
Use GDI instead GDI+ : {chkUseGDI.Checked}
time : {t1 - t0:f0}ms
zoom : {zoomLevel}, pan : {szPan}
";
            var size = e.Graphics.MeasureString(msg, Font);
            e.Graphics.FillRectangle(Brushes.White, new RectangleF(Point.Empty, size));
            e.Graphics.DrawString(msg, Font, Brushes.Black, Point.Empty);
        }

        private void DrawStuffs(Graphics g) {
            var drawItem = lbxDrawItem.SelectedIndex;
            switch (drawItem) {
                case 0: DrawString_Gdip(g); break;
                case 1: DrawRectangle_Gdip(g); break;
                case 2: FillRectangle_Gdip(g); break;
                case 3: DrawEllipse_Gdip(g); break;
                case 4: FillEllipse_Gdip(g); break;
                default: break;
            }
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
            string s = "gdi";
            Font font = Font;
            Brush brush = Brushes.Lime;
            Color color = Color.Lime;
            if (chkUseGDI.Checked) {
                Action<int, int> drawAction = (x, y) => {
                    TextRenderer.DrawText(g, s, font, new Point(x, y), color);
                };
                DrawLoop(drawAction);
            } else {
                Action<int, int> drawAction = (x, y) => {
                    g.DrawString(s, font, brush, x, y);
                };
                DrawLoop(drawAction);
            }
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

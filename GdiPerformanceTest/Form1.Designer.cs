namespace GdiPerformanceTest {
    partial class Form1 {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkUseBackBuffer = new System.Windows.Forms.CheckBox();
            this.lbxDrawItem = new System.Windows.Forms.ListBox();
            this.btnRedraw = new System.Windows.Forms.Button();
            this.chkDoubleBuffered = new System.Windows.Forms.CheckBox();
            this.pnlDraw = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chkUseBackBuffer);
            this.panel1.Controls.Add(this.lbxDrawItem);
            this.panel1.Controls.Add(this.btnRedraw);
            this.panel1.Controls.Add(this.chkDoubleBuffered);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 961);
            this.panel1.TabIndex = 0;
            // 
            // chkUseBackBuffer
            // 
            this.chkUseBackBuffer.AutoSize = true;
            this.chkUseBackBuffer.Location = new System.Drawing.Point(11, 200);
            this.chkUseBackBuffer.Name = "chkUseBackBuffer";
            this.chkUseBackBuffer.Size = new System.Drawing.Size(88, 16);
            this.chkUseBackBuffer.TabIndex = 4;
            this.chkUseBackBuffer.Text = "Back Buffer";
            this.chkUseBackBuffer.UseVisualStyleBackColor = true;
            // 
            // lbxDrawItem
            // 
            this.lbxDrawItem.FormattingEnabled = true;
            this.lbxDrawItem.ItemHeight = 12;
            this.lbxDrawItem.Items.AddRange(new object[] {
            "DrawString",
            "DrawString(GDI)",
            "DrawString(NativeGDI)",
            "DrawRectangle",
            "FillRectangle",
            "DrawEllipse",
            "FillEllipse"});
            this.lbxDrawItem.Location = new System.Drawing.Point(11, 40);
            this.lbxDrawItem.Name = "lbxDrawItem";
            this.lbxDrawItem.Size = new System.Drawing.Size(120, 100);
            this.lbxDrawItem.TabIndex = 2;
            // 
            // btnRedraw
            // 
            this.btnRedraw.Location = new System.Drawing.Point(11, 11);
            this.btnRedraw.Name = "btnRedraw";
            this.btnRedraw.Size = new System.Drawing.Size(124, 23);
            this.btnRedraw.TabIndex = 1;
            this.btnRedraw.Text = "Redraw";
            this.btnRedraw.UseVisualStyleBackColor = true;
            this.btnRedraw.Click += new System.EventHandler(this.btnRedraw_Click);
            // 
            // chkDoubleBuffered
            // 
            this.chkDoubleBuffered.AutoSize = true;
            this.chkDoubleBuffered.Location = new System.Drawing.Point(11, 156);
            this.chkDoubleBuffered.Name = "chkDoubleBuffered";
            this.chkDoubleBuffered.Size = new System.Drawing.Size(114, 16);
            this.chkDoubleBuffered.TabIndex = 0;
            this.chkDoubleBuffered.Text = "double buffering";
            this.chkDoubleBuffered.UseVisualStyleBackColor = true;
            this.chkDoubleBuffered.CheckedChanged += new System.EventHandler(this.chkDoubleBuffered_CheckedChanged);
            // 
            // pnlDraw
            // 
            this.pnlDraw.BackColor = System.Drawing.Color.Gray;
            this.pnlDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDraw.Location = new System.Drawing.Point(154, 0);
            this.pnlDraw.Name = "pnlDraw";
            this.pnlDraw.Size = new System.Drawing.Size(1630, 961);
            this.pnlDraw.TabIndex = 1;
            this.pnlDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDraw_Paint);
            this.pnlDraw.Layout += new System.Windows.Forms.LayoutEventHandler(this.pnlDraw_Layout);
            this.pnlDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlDraw_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1784, 961);
            this.Controls.Add(this.pnlDraw);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlDraw;
        private System.Windows.Forms.CheckBox chkDoubleBuffered;
        private System.Windows.Forms.Button btnRedraw;
        private System.Windows.Forms.ListBox lbxDrawItem;
        private System.Windows.Forms.CheckBox chkUseBackBuffer;
    }
}


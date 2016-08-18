namespace DemoShapeComperer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.canvas1 = new System.Windows.Forms.PictureBox();
            this.canvas2 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(762, 482);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.SplitterWidth = 9;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.canvas1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.canvas2);
            this.splitContainer2.Size = new System.Drawing.Size(762, 356);
            this.splitContainer2.SplitterDistance = 381;
            this.splitContainer2.SplitterWidth = 8;
            this.splitContainer2.TabIndex = 0;
            // 
            // canvas1
            // 
            this.canvas1.BackColor = System.Drawing.Color.White;
            this.canvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas1.Location = new System.Drawing.Point(0, 0);
            this.canvas1.Name = "canvas1";
            this.canvas1.Size = new System.Drawing.Size(381, 356);
            this.canvas1.TabIndex = 0;
            this.canvas1.TabStop = false;
            this.canvas1.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas1_Paint);
            this.canvas1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas1_MouseDown);
            this.canvas1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas1_MouseMove);
            this.canvas1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas1_MouseUp);
            // 
            // canvas2
            // 
            this.canvas2.BackColor = System.Drawing.Color.White;
            this.canvas2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas2.Location = new System.Drawing.Point(0, 0);
            this.canvas2.Name = "canvas2";
            this.canvas2.Size = new System.Drawing.Size(373, 356);
            this.canvas2.TabIndex = 0;
            this.canvas2.TabStop = false;
            this.canvas2.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas1_Paint);
            this.canvas2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas1_MouseDown);
            this.canvas2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas1_MouseMove);
            this.canvas2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas1_MouseUp);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(558, 73);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(192, 32);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Dump debug info";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(200, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(445, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "DISSIMILARITY = 0.00";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 12);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Caculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 482);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "Form1";
            this.Text = "DemoShapeComparer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canvas2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox canvas1;
        private System.Windows.Forms.PictureBox canvas2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}


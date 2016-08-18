using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoShapeComperer
{
    public partial class Form1 : Form
    {
        bool isDrawing = false;

        List<PointF> path1 = new List<PointF>();
        List<PointF> path2 = new List<PointF>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var comparer = new FLib.ShapeComparer(59, 19);
            comparer.DumpOnCalcDissimilarity = checkBox1.Checked;

            float dissimilarity  = comparer.CalcDissimilarity(path1, path2);
            label1.Text = string.Format("DISSIMILARITY = {0:0.00000}", dissimilarity);
        }

        private void canvas1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                isDrawing = false;
                return;
            }

            isDrawing = true;

            var path = sender == canvas1 ? path1 : path2;
            var canvas = sender == canvas1 ? canvas1: canvas2;

            path.Clear();
            path.Add(e.Location);
            canvas.Invalidate();
        }

        private void canvas1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                isDrawing = false;
                return;
            }
            
            isDrawing = true;

            var path = sender == canvas1 ? path1 : path2;
            var canvas = sender == canvas1 ? canvas1 : canvas2;

            path.Add(e.Location);
            canvas.Invalidate();
        }

        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                isDrawing = false;
                return;
            }

            isDrawing = true;
            
            var path = sender == canvas1 ? path1 : path2;
            var canvas = sender == canvas1 ? canvas1 : canvas2;

            if (path.Count >= 1)
            {
                float dx = path.Last().X - e.Location.X;
                float dy = path.Last().Y - e.Location.Y;
                if (dx * dx + dy * dy > 10 * 10) // 10フレーム以上動いたら
                {
                    path.Add(e.Location);
                    canvas.Invalidate();
                }
            }
        }

        Pen pen = new Pen(Brushes.Black, 2);

        private void canvas1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            // ユーザが描いたストロークを描画する
            var path = sender == canvas1 ? path1.ToList() : path2.ToList();

            if (path.Count >= 2)
            {
                if (false == isDrawing)
                {
                    path.Add(path[0]); // 閉路にする
                }

                e.Graphics.DrawLines(pen, path.ToArray());
                for (int i = 0; i < path.Count; i++)
                {
                    e.Graphics.FillRectangle(Brushes.DarkBlue, new RectangleF(path[i].X - 4, path[i].Y - 4, 8, 8));
                }
            }
        }
    }
}

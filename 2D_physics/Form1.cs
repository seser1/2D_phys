using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _2D_physics
{
    public partial class Form1 : Form
    {
        private static readonly int[] screen_size = new int[2] { 600, 600 };

        private Manager manager = new Manager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            PointF[] points = { new PointF(10, 10), new PointF(20, 10), new PointF(20, 20), new PointF(10, 20) };

            g.FillPolygon(Brushes.Black, points);

        }
    }
}

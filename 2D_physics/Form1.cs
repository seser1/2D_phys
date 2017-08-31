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

        private DrawManager drawManager;

        public Form1()
        {
            InitializeComponent();

            drawManager = new DrawManager();

            //メインスレッドを登録
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(MainThread);
            timer.Interval = (int)(1000/60);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void MainThread(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            drawManager.GoNextFrame();
            drawManager.Draw(g);
        }


    

        protected void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawManager.Draw(g);
        }
    }
}

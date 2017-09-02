﻿using System;
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

            //描画スレッド登録
            System.Timers.Timer drawTimer = new System.Timers.Timer();
            drawTimer.Elapsed += new System.Timers.ElapsedEventHandler(DrawThread);
            drawTimer.Interval = (int)(1000 / 40);
            drawTimer.AutoReset = true;
            drawTimer.Enabled = true;

            //演算スレッド登録
            System.Timers.Timer calcTimer = new System.Timers.Timer();
            calcTimer.Elapsed += new System.Timers.ElapsedEventHandler(CalcThread);
            calcTimer.Interval = (int)(1000 / 60);
            calcTimer.AutoReset = true;
            calcTimer.Enabled = true;
        }

        private void DrawThread(object sender, EventArgs e)
        {

            BufferedGraphicsContext currentContext;
            BufferedGraphics myBuffer;

            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(this.CreateGraphics(),
               this.DisplayRectangle);

            drawManager.Draw(myBuffer);


            myBuffer.Render();
            myBuffer.Render(this.CreateGraphics());

            myBuffer.Dispose();
        }

        private void CalcThread(object sender, EventArgs e)
        {
            drawManager.GoNextFrame();
        }

    }
}

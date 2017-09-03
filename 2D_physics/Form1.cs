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

        private List<Figure> figures;
        private DrawManager drawManager;
        private CalcManager calcManager;

        public Form1()
        {
            InitializeComponent();

            //図形初期化場所
            figures = new List<Figure>();

            //初期画像生成（コーディング時のテスト用）
            //今後どう生成していくかは要検討
            List<PointF> points = new List<PointF>();
            points.Add(new PointF(0, 0));
            points.Add(new PointF(30, 0));
            points.Add(new PointF(30, 30));
            points.Add(new PointF(0, 30));
            figures.Add(new Figure(points, new PointF(10, 50), new SizeF((float)1.2, 1), 0.05));

            points = new List<PointF>();
            points.Add(new PointF(0, 0));
            points.Add(new PointF(30, 0));
            points.Add(new PointF(50, 30));
            points.Add(new PointF(15, 90));
            points.Add(new PointF(0, 90));
            points.Add(new PointF(0,15));
            figures.Add(new Figure(points, new PointF(100, 50), new SizeF((float)0.5, 1), 0.1));
            //ここまで図形生成

            //各処理を担当するクラスのインスタンス生成
            drawManager = new DrawManager(figures);
            calcManager = new CalcManager(figures);

            //描画スレッド登録
            System.Timers.Timer drawTimer = new System.Timers.Timer();
            drawTimer.Elapsed += new System.Timers.ElapsedEventHandler(DrawThread);
            drawTimer.Interval = (int)(1000 / 60);
            drawTimer.AutoReset = true;
            drawTimer.Enabled = true;

            //演算スレッド登録
            System.Timers.Timer calcTimer = new System.Timers.Timer();
            calcTimer.Elapsed += new System.Timers.ElapsedEventHandler(CalcThread);
            calcTimer.Interval = (int)(1000 / 60);
            calcTimer.AutoReset = true;
            calcTimer.Enabled = true;
            
        }

        //描画スレッド
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

        //計算スレッド
        private void CalcThread(object sender, EventArgs e)
        {
            calcManager.MoveFigures();
        }

    }
}

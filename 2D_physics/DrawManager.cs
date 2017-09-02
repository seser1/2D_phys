using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_physics
{
    //描画管理全般
    class DrawManager
    {
        private List<Figure> figures;

        public DrawManager()
        {

            figures = new List<Figure>();
        
            //初期画像生成（コーディング時のテスト用）
            //今後どう生成していくかは要検討
            List<PointF> points = new List<PointF>();
            points.Add(new PointF(0, 0));
            points.Add(new PointF(20, 0));
            points.Add(new PointF(20, 20));
            points.Add(new PointF(0, 20));
            figures.Add(new Figure(points, new PointF(10,50), new SizeF(1,1), 0.05));

            points = new List<PointF>();
            points.Add(new PointF(0, 0));
            points.Add(new PointF(40, 0));
            points.Add(new PointF(60, 40));
            points.Add(new PointF(20, 120));
            points.Add(new PointF(0, 120));
            points.Add(new PointF(0, 20));
            figures.Add(new Figure(points, new PointF(200, 100), new SizeF(1, 2), 0.1));
        }
        

        //描画の度に呼び出される
        //頻繁に呼ばれるので出来るだけ動作は軽くしたい
        public void Draw(BufferedGraphics g)
        {
            g.Graphics.Clear(Color.White);
            figures.ForEach(figure =>
             g.Graphics.FillPolygon(Brushes.Black, figure.GetPoint()) );

        }

        public void GoNextFrame()
        {
            figures.ForEach(figure => figure.Move());
            
        }
    }
}

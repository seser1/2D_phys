using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_physics
{
    class CalcManager
    {
        private List<Figure> figures;

        public CalcManager(List<Figure> figures)
        {
            this.figures = figures;
        }

        //図形を次のフレームへ移動させる
        public void MoveFigures()
        {
            figures.ForEach(figure =>
            {
                figure.Center += figure.Vel;
            });
            this.RotateFigures();
        }

        //回転動作のみを切り出したメソッド
        private void RotateFigures()
        {
            figures.ForEach(figure =>
            {
                /*                figure.RelatePoints.ForEach(point =>
                                {
                                    point.X = (float)(point.X * Math.Cos(figure.Angv) 
                                                        - point.Y * Math.Sin(figure.Angv));

                                    point.Y = (float)(point.X * Math.Sin(figure.Angv) 
                                                        + point.Y * Math.Cos(figure.Angv));
                                });*/
                //ラムダ式使うとうまく更新されない（構造体は参照渡しにならない？）ので暫定対応
                List<PointF> points = figure.RelatePoints;
                for (int i = 0; i < points.Count; i++)
                {
                    PointF myPoint = points[i];
                    myPoint.X = (float)(points[i].X * Math.Cos(figure.Angv)
                                        - points[i].Y * Math.Sin(figure.Angv));

                    myPoint.Y = (float)(points[i].X * Math.Sin(figure.Angv)
                                        + points[i].Y * Math.Cos(figure.Angv));
                    points[i] = myPoint;
                }

            });
        }

    }
}

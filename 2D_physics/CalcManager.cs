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

        //衝突判定
        //移動前に実行され、図形情報を更新
        private void DecideCollision()
        {
            for (int i = 0; i < figures.Count; i++)
            {
                Figure figureF = figures[i];
                for (int j = i + 1; j < figures.Count; j++)
                {
                    Figure figureL = figures[j];

                    if(BroadDecision(figureF, figureL))
                    {
                        //figureFとfigureLでNarrowDecisionを行う

                    }
                }
            }

        }

        //ブロード検出　true:衝突　false:非衝突
        private bool BroadDecision(Figure figure1, Figure figure2)
        {
            RectangleF Rect1 = GetOuterRect(figure1);
            RectangleF Rect2 = GetOuterRect(figure2);

            return Rect1.IntersectsWith(Rect2);
        }
        //与えられた図形に外接する四角形を返す
        private RectangleF GetOuterRect(Figure figure)
        {
            List<PointF> points = figure.Points;

            PointF min = new PointF(points[0].X, points[0].Y);
            PointF max = new PointF(points[0].X, points[0].Y);

            points.ForEach(point =>
            {
                if (min.X > point.X) min.X = point.X;
                if (min.Y > point.Y) min.Y = point.Y;
                if (max.X < point.X) max.X = point.X;
                if (max.Y < point.Y) min.Y = point.Y;
            });

            return new RectangleF(min, new SizeF(max.X - min.X, max.Y - min.Y));
        }

        //図形を次のフレームへ移動させる
        public void MoveFigures()
        {
            //衝突判定
            DecideCollision();

            //平行移動
            figures.ForEach(figure =>
                figure.Center += figure.Vel);

            //回転
            this.RotateFigures();
        }

        //回転動作のみを切り出したメソッド
        //重心からの相対位置を回転させる
        private void RotateFigures()
        {
            figures.ForEach(figure =>
            {
                //各点に対して回転動作
                //ForEach使うとうまく更新されない（構造体は参照渡しにならない？）ので通常ループで暫定対応
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

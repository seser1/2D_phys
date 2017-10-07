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
            //デバッグ用 衝突していない際はLightSlateGrayを指定。
            //figures.ForEach(figure => figure.DrawBrush = Brushes.LightSlateGray);
            //ここまで

            for (int i = 0; i < figures.Count; i++)
            {
                Figure figureF = figures[i];
                for (int j = i + 1; j < figures.Count; j++)
                {
                    Figure figureL = figures[j];

                    if(BroadDecision(figureF, figureL))
                    {
                        //figureFとfigureLでNarrowDecisionを行う
                        NarrowDecision(figureF, figureL);
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
                if (max.Y < point.Y) max.Y = point.Y;
            });

            return new RectangleF(min, new SizeF(max.X - min.X, max.Y - min.Y));
        }

        //ナロー検出　衝突していたならば動作変更まで行う
        private void NarrowDecision(Figure figure1, Figure figure2)
        {
            //デバッグ用 ブロード検出に引っかかると色がKhakiに。
            //figure1.DrawBrush = Brushes.Khaki;
            //figure2.DrawBrush = Brushes.Khaki;
            //ここまで

            PointF point;
            //figure1からfigure2への衝突
            for (int i = 0; i < figure1.Points.Count; i++)
            {
                point = figure1.Points[i];
                if (IsInner(point, figure2))
                    Collision(point, figure1, figure2);
            }

            //figure2からfigure1への衝突
            for (int i = 0; i < figure2.Points.Count; i++)
            {
                point = figure2.Points[i];
                if (IsInner(point, figure1))
                    Collision(point, figure2, figure1);
            }
        }
        //ポイントが図形内部に含まれるかどうかの判定
        private bool IsInner(PointF point, Figure figure)
        {
            //含まれていたならばtrueを返す
            //triangleの中にあるかどうかで判定
            List<Triangle> triangles = figure.Triangles;
            for (int i = 0; i < triangles.Count ; i++)
            {
                if (MyMath.PointInTriangle(point, triangles[i])) return true;
            }

            return false;
        }
        //めり込んだ頂点（figure1）から双方の図形の運動情報を更新
        private void Collision(PointF point, Figure figure1, Figure figure2)
        {
            //デバッグ用 衝突すると色がCrimsonに。
            //figure1.DrawBrush = Brushes.Crimson;
            //figure2.DrawBrush = Brushes.Crimson;
            //ここまで

            Line line = Nearest(point, figure2);
            PointF sink = MyMath.GetNormalVector(point, line);

            double energy = figure1.Weight * Math.Pow(MyMath.VectorAbs(figure1.Vel), 2) / 2 +
                figure2.Weight * Math.Pow(MyMath.VectorAbs(figure2.Vel), 2) / 2 +
                figure1.Moment * Math.Pow(figure1.Angv, 2) / 2 + 
                figure2.Moment * Math.Pow(figure2.Angv, 2) / 2;

            ChangeMove(new Line(point, new PointF(point.X + sink.X, point.Y + sink.Y)), figure1);
            ChangeMove(new Line(new PointF(point.X + sink.X, point.Y + sink.Y), point), figure2);

            double _energy = figure1.Weight * Math.Pow(MyMath.VectorAbs(figure1.Vel), 2) / 2 +
                figure2.Weight * Math.Pow(MyMath.VectorAbs(figure2.Vel), 2) / 2 +
                figure1.Moment * Math.Pow(figure1.Angv, 2) / 2 +
                figure2.Moment * Math.Pow(figure2.Angv, 2) / 2;

            double normalize = Math.Pow(_energy / energy, 0.5) *1.05;

            figure1.Angv /= normalize;
            figure1.Vel = new PointF((float)(figure1.Vel.X / normalize), (float)(figure1.Vel.Y / normalize));

            figure2.Angv /= normalize;
            figure2.Vel = new PointF((float)(figure2.Vel.X / normalize), (float)(figure2.Vel.Y / normalize));



        }
        //与えられた点に最も近い辺を返す
        private Line Nearest(PointF point, Figure figure)
        {
            List<Line> lines = figure.Lines;
            Line retline = lines[0];
            double nearestDist = MyMath.NormalVectorLength(point, retline);
            lines.ForEach(line =>
            {
                double dist = MyMath.NormalVectorLength(point, line);
                if (dist < nearestDist)
                {
                    retline = line;
                    nearestDist = dist;
                }
                    
            });
            return retline;
        }
        //外力のベクトルで図形の運動情報を更新
        //powerのstartが作用点
        //とりあえずは運動量保存は無視
        private void ChangeMove(Line power, Figure figure)
        {
            PointF v1 = new PointF(power.end.X - power.start.X, power.end.Y - power.start.Y);
            PointF v2 = new PointF(figure.Center.X - power.start.X, figure.Center.Y - power.start.Y);
            double distance = Math.Abs(MyMath.CrossProduct(v1, v2)) / MyMath.VectorAbs(v1);

            figure.Vel += new SizeF(30 * (power.end.X - power.start.X) / (float)figure.Weight,
                                    30 * (power.end.Y - power.start.Y) / (float)figure.Weight);
            figure.Angv += 5 * distance * MyMath.VectorAbs(v1)
                * MyMath.DecideRotate(new PointF(power.start.X - figure.Center.X, power.start.Y - figure.Center.Y),
                                        new PointF(power.end.X - figure.Center.X, power.end.Y - figure.Center.Y))
                           / figure.Moment;
        }



        //図形を次のフレームへ移動させる
        public void MoveFigures()
        {
            //衝突判定
            DecideCollision();

            //平行移動
            figures.ForEach(figure =>
                 figure.Center += new SizeF((float)figure.Vel.X, (float)figure.Vel.Y) );

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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_physics
{
    static class MyMath
    {
        //ベクトルの絶対値
        public static double VectorAbs(PointF vector)
        {
            return Math.Pow(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2), 0.5);
        }
        //ベクトルの外積
        public static double CrossProduct(PointF v1, PointF v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }
        //ベクトルの回転方向計算
        public static int DecideRotate(PointF v1, PointF v2)
        {
            var conc = CrossProduct(v1, v2);
            return (conc >= 0 ? 1 : -1);
        }
        //2点間の距離
        public static double Distance(PointF p1, PointF p2)
        {
            return Math.Pow(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2), 0.5);
        }
        //点から線への法線ベクトル
        public static PointF GetNormalVector(PointF point, Line line)
        {
            var M = (point.X - line.start.X) * (line.end.X - line.start.X)
                + (point.Y - line.start.Y) * (line.end.Y - line.start.Y);
            var N = (point.X - line.end.X) * (line.start.X - line.end.X)
                + (point.Y - line.end.Y) * (line.start.Y - line.end.Y);
            return new PointF(
                (N * line.start.X + M * line.end.X) / (M + N) - point.X,
                (N * line.start.Y + M * line.end.Y) / (M + N) - point.Y
                );
        }

    }
}

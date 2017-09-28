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
        //GetNormalVectorの距離
        public static double NormalVectorLength(PointF point, Line line)
        {
            PointF vector = GetNormalVector(point, line);
            return VectorAbs(vector);
        }
        //線分が交差しているかの判定関数
        public static bool IsLineCross(Line line1, Line line2)
        {
            var ta = (line2.start.X - line2.end.X) * (line1.start.Y - line2.start.Y)
                + (line2.start.Y - line2.end.Y) * (line2.start.X - line1.start.X);
            var tb = (line2.start.X - line2.end.X) * (line1.end.Y - line2.start.Y)
                + (line2.start.Y - line2.end.Y) * (line2.start.X - line1.end.X);

            var tc = (line1.start.X - line1.end.X) * (line2.start.Y - line1.start.Y)
                + (line1.start.Y - line1.end.Y) * (line1.start.X - line2.start.X);
            var td = (line1.start.X - line1.end.X) * (line2.end.Y - line1.start.Y)
                + (line1.start.Y - line1.end.Y) * (line1.start.X - line2.end.X);

            return (tc * td) < 0 && (ta * tb) < 0;
        }
        //三角形内に点が存在するかどうかの判定関数
        public static bool PointInTriangle(PointF point, Triangle triangle)
        {
            List<double> c = new List<double>();

            for (int i = 0; i < triangle.points.Count; i++)
            {
                PointF currentPoint = triangle.points[i];
                PointF nextPoint = triangle.points[(i + 1) % 3];
                c.Add(CrossProduct(
                    new PointF(nextPoint.X - currentPoint.X, nextPoint.Y - currentPoint.Y),
                    new PointF(point.X - nextPoint.X, point.Y - nextPoint.Y)
                    ));
            }

            if ((c[0] > 0 && c[1] > 0 && c[2] > 0) || (c[0] < 0 && c[1] < 0 && c[2] < 0))
            {
                //三角形の内側に点がある
                Console.WriteLine("inner: "  + point + "outer :" + triangle.points[0] + triangle.points[1] + triangle.points[2]);
                return true;
            }

            return false;
        }
    }
}

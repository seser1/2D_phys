using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_physics
{
    class Figure
    {
        
        private List<PointF> locPoints;//ローカル座標（図形の固定座標情報）
        private List<PointF> pubPoints;//絶対座標（現在の図形位置）
        private PointF locCenter;//ローカル座標内の重心
        private PointF pubCenter;//絶対座標での重心

        private double ang = 0;//角度

        private SizeF vel;//速度
        private double angv;//角速度

        //コンストラクタ
        public Figure(List<PointF> locPoints, PointF pubCenter, SizeF vel, double angv)
        {
            this.locPoints = locPoints;
            this.pubCenter = pubCenter;
            this.vel = vel;
            this.angv = angv;

            CalcCenter();//ローカル重心初期化

            pubPoints = new List<PointF>();
            this.TransPoints();//絶対座標生成
        }

        //ローカル重心位置計算
        private void CalcCenter()
        {
            PointF sumXY = new PointF(0, 0);
            locPoints.ForEach(locPoint => { sumXY.X += locPoint.X; sumXY.Y += locPoint.Y; });

            sumXY.X /= locPoints.Count;
            sumXY.Y /= locPoints.Count;
            locCenter = sumXY;
        }

        //ローカル図形を絶対座標上へ変換
        public void TransPoints()
        {
            //一度クリアしてからすべて変換
            pubPoints.Clear();
            locPoints.ForEach(locPoint =>
                pubPoints.Add(TransPoint(locPoint))
                );
        }
        //入力ローカルポイントを絶対ポイントに変換
        private PointF TransPoint(PointF point)
        {
            PointF relateXY = new PointF();

            relateXY.X = point.X - locCenter.X;
            relateXY.Y = point.Y - locCenter.Y;

            return new PointF(pubCenter.X + (float)(relateXY.X * Math.Cos(ang) - relateXY.Y * Math.Sin(ang)),
                                pubCenter.Y + (float)(relateXY.X * Math.Sin(ang) + relateXY.Y * Math.Cos(ang)));
        }

        //移動　一フレームに一回実行
        public void Move()
        {
            pubCenter = PointF.Add(pubCenter, vel);
            ang += angv;
            this.TransPoints();
        }

        //絶対座標取得
        public PointF[] GetPubPoints()
        {
            return pubPoints.ToArray();
        }
    }
}

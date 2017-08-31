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
        //ローカル座標（図形の固定座標情報）
        private List<PointF> locPoints;
        //絶対座標（現在の図形位置）
        private List<PointF> pubPoints;
        private PointF pubCenter;

        private SizeF vel;

        //コンストラクタ
        public Figure(List<PointF> locPoints, PointF pubCenter, SizeF vel)
        {
            this.locPoints = locPoints;
            this.pubCenter = pubCenter;
            this.vel = vel;

            pubPoints = new List<PointF>();

            this.TransPoints();
        }

        //ローカル座標を絶対座標へ変換
        private void TransPoints()
        {
            //一度クリアしてからすべて変換
            pubPoints.Clear();
            locPoints.ForEach(locPoint =>
                pubPoints.Add(PointF.Add(locPoint, new SizeF(pubCenter))));
        }

        //移動　一フレームに一回実行
        public void Move()
        {
            pubCenter = PointF.Add(pubCenter, vel);
            this.TransPoints();
        }

        //絶対座標取得
        public PointF[] GetPoint()
        {
            return pubPoints.ToArray();
        }
    }
}

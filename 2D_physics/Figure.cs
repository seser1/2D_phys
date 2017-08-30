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

        //コンストラクタ
        public Figure(List<PointF> _locPoints, PointF _pubCenter)
        {
            locPoints = _locPoints;
            pubCenter = _pubCenter;

            pubPoints = new List<PointF>();

            //ローカル座標を絶対座標へ変換
            locPoints.ForEach(locPoint => pubPoints.Add(PointF.Add(locPoint,new SizeF(pubCenter))));
        }


        public PointF[] GetPoint()
        {
            return pubPoints.ToArray();
        }
    }
}

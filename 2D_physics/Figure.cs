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
        private List<PointF> locPoints;
        private List<PointF> pubPoints;
        private PointF pubCenter;

        public Figure()
        {
            locPoints = new List<PointF>();
            //図形情報登録
            //どこからデータ取ってくるかは考える必要あり
            locPoints.Add(new PointF(0, 0));
            locPoints.Add(new PointF(20, 0));
            locPoints.Add(new PointF(20, 20));
            locPoints.Add(new PointF(0, 20));

            pubPoints = new List<PointF>();

            pubCenter = new PointF(20, 20);
        }


        public PointF[] GetPoint()
        {
            /*
            List<PointF> pubPoints = new List<PointF>();
            locPoints.ForEach(locPoint => pubPoints.Add(locPoint));
            */

            return locPoints.ToArray();
        }
    }
}

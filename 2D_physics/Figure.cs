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
        
        public List<PointF> RelatePoints { get; set; }//各点の重心からの相対位置
        public List<PointF> Points//各点の絶対位置　描画用？
        {
            get
            {
                List<PointF> retPoints = new List<PointF>();
                RelatePoints.ForEach(point =>
                    retPoints.Add(PointF.Add(point, new SizeF(Center))));
                return retPoints;
            }
            set
            {
                List<PointF> inPoints = value;
                RelatePoints.Clear();
                inPoints.ForEach(point =>
                    RelatePoints.Add(PointF.Add(point, new SizeF(Center))));
            }
        }

        public PointF Center { get; set; }//重心位置

        public SizeF Vel { get; set; }//速度
        public double Angv { get; set; }//角速度

        //コンストラクタ
        public Figure(List<PointF> RelatePoints, PointF Center, SizeF Vel, double Angv)
        {
            this.RelatePoints = RelatePoints;
            this.Center = Center;
            this.Vel = Vel;
            this.Angv = Angv;
        }


    }
}

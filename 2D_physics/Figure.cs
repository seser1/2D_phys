using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_physics
{

    //図形情報のbeanみたいなもの
    //初期化以外の演算はこの中では極力行わないようにする
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
        public Figure(List<PointF> InitialPoints, PointF Center, SizeF Vel, double Angv)
        {
            this.GenerateRelatePoints(InitialPoints);
            this.Center = Center;
            this.Vel = Vel;
            this.Angv = Angv;
        }

        //コンストラクタ内でのRelatePointsの初期化用
        private void GenerateRelatePoints(List<PointF> InitialPoints)
        {
            RelatePoints = new List<PointF>();

            PointF centerTemp = new PointF();
            InitialPoints.ForEach(point =>
            {
                centerTemp.X += point.X / InitialPoints.Count;
                centerTemp.Y += point.Y / InitialPoints.Count;
            });

            InitialPoints.ForEach(point =>
                RelatePoints.Add(new PointF(point.X - centerTemp.X, point.Y - centerTemp.Y))
                );
        }

    }
}

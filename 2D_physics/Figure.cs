using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_physics
{
    //線分情報保持用の構造体
    struct Line
    {
        public PointF start;
        public PointF end;
        public int[] suf;//添え字保存
        public Line(PointF start, PointF end, int[] suf)
        {
            this.start = start;
            this.end = end;
            this.suf = suf;
        }
        public Line(PointF start, PointF end)
        {
            this.start = start;
            this.end = end;
            suf = null;
        }

    }

    //図形情報のbeanみたいなもの
    //初期化以外の演算はこの中では極力行わないようにしたい
    class Figure
    {
        public List<PointF> RelatePoints { get; set; }//各点の重心からの相対位置
        public List<PointF> Points
        {
            //各点の絶対位置　描画用？
            //毎回相対位置を参照して計算するので動作が遅いかも 性能面で問題が出るなら要検討
            get
            {
                //Debug中に時たまInvalidOperationExceptionを吐く
                //オブジェクトが破棄された後にアクセスしに行った際の例外処理を書いておく必要？
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
        public List<Line> Lines
        {
            //図形を構成する線を返す
            get
            {
                List<Line> lines = new List<Line>();
                for (int i = 0; i < Points.Count; i++)
                {
                    int next = (i + 1) % Points.Count;
                    lines.Add(new Line(Points[i],
                                Points[next],
                                new int[] {i, next}
                                ));
                }
                return lines;
            }

        }

        public PointF Center { get; set; }//（絶対）重心位置

        public PointF Vel { get; set; }//速度
        public double Angv { get; set; }//角速度

        //コンストラクタ
        public Figure(List<PointF> InitialPoints, PointF Center, PointF Vel, double Angv)
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

            //ローカル座標内の重心位置計算
            PointF centerTemp = new PointF();
            InitialPoints.ForEach(point =>
            {
                centerTemp.X += point.X / InitialPoints.Count;
                centerTemp.Y += point.Y / InitialPoints.Count;
            });

            //重心位置に基づき各点の相対位置を更新
            InitialPoints.ForEach(point =>
                RelatePoints.Add(new PointF(point.X - centerTemp.X, point.Y - centerTemp.Y))
                );
        }

    }
}

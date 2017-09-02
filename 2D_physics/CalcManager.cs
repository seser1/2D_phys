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

        public void MoveFigures()
        {
            List<PointF> points = new List<PointF>();
            figures.ForEach(figure =>
            {
            });

        }

    }
}

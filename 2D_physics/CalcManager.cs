using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_physics
{
    class CalcManager
    {
        private List<Figure> figures;

        public CalcManager(List<Figure> figures)
        {
            this.figures = figures;
        }

        public void GoNextFrame()
        {
            figures.ForEach(figure => figure.Move());

        }

    }
}

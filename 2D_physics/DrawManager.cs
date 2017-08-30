using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_physics
{
    class DrawManager
    {
        private List<Figure> figures;

        public DrawManager()
        {
            figures = new List<Figure>();
            figures.Add(new Figure());
        }

        public void Draw(Graphics g)
        {
            g.Clear(Color.White);

            figures.ForEach(figure => g.FillPolygon(Brushes.Black, figure.GetPoint() ));

            
        }

        void GoNextFrame()
        {
            
        }
    }
}

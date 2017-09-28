using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2D_physics
{
    //描画管理全般
    class DrawManager
    {
        private List<Figure> figures;

        public DrawManager(List<Figure> figures)
        {
            this.figures = figures;
        }
 
        //描画の度に呼び出される
        public void Draw(BufferedGraphics g)
        {
            g.Graphics.Clear(Color.White);
            try
            {
                figures.ForEach(figure =>
                    g.Graphics.FillPolygon(figure.DrawBrush, figure.Points.ToArray()));
            }
            catch (InvalidOperationException) { }


        }

    }
}

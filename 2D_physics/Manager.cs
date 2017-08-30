using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_physics
{
    //一つのインスタンスをフォーム上で管理する
    class Manager
    {
        private List<Graph> graph;

        public Manager()
        {
            graph = new List<Graph>();
            graph.Add(new Graph());
        }



        void GoNextFrame()
        {
            
        }
    }
}

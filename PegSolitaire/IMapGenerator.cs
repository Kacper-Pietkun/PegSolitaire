using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PegSolitaire
{
    internal interface IMapGenerator
    {
        public List<Pawn> GenerateMap(double canvasWidth, double canvasHeight, double percentageOfCanvasPlayable);
    }
}

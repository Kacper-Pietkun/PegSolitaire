using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PegSolitaire
{
    internal class StandardMap : IMapGenerator
    {
        public List<Pawn> GenerateMap(double canvasWidth, double canvasHeight, double percentageOfCanvasPlayable)
        {
            double playableCanvasWidth = canvasWidth * percentageOfCanvasPlayable / 100;
            double playableCanvasHeight = canvasHeight * percentageOfCanvasPlayable / 100;
            double playableCanvasLeftTopX = canvasWidth * (1 - percentageOfCanvasPlayable / 100) / 2;
            double playableCanvasLeftTopY = canvasHeight * (1 - percentageOfCanvasPlayable / 100) / 2;
            
            int numberPawnsWidth = 7;
            int numberPawnsHeight = 7;
            double pawnWidht = playableCanvasWidth / numberPawnsWidth;
            double pawnHeight = playableCanvasHeight / numberPawnsHeight;

            List<Pawn> pawns = new List<Pawn>();

            for (int i = 0; i < numberPawnsWidth; i++)
            {
                for (int j = 0; j < numberPawnsHeight; j++)
                {
                    if (i == 3 && j == 3)
                        pawns.Add(new Pawn(playableCanvasLeftTopX + i * pawnWidht, playableCanvasLeftTopY + j * pawnHeight, pawnWidht, pawnHeight, Pawn.Status.Empty));
                    else if (i >= 2 && i <= 4)
                        pawns.Add(new Pawn(playableCanvasLeftTopX + i * pawnWidht, playableCanvasLeftTopY + j * pawnHeight, pawnWidht, pawnHeight));
                    else if (j >= 2 && j <= 4 && (i < 2 || i > 4))
                        pawns.Add(new Pawn(playableCanvasLeftTopX + i * pawnWidht, playableCanvasLeftTopY + j * pawnHeight, pawnWidht, pawnHeight));
                }
            }

            return pawns;
        }
    }
}

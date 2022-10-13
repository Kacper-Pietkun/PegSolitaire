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
        public List<List<Pawn>> GenerateMap(double canvasWidth, double canvasHeight, double percentageOfCanvasPlayable)
        {
            double playableCanvasWidth = canvasWidth * percentageOfCanvasPlayable / 100;
            double playableCanvasHeight = canvasHeight * percentageOfCanvasPlayable / 100;
            double playableCanvasLeftTopX = canvasWidth * (1 - percentageOfCanvasPlayable / 100) / 2;
            double playableCanvasLeftTopY = canvasHeight * (1 - percentageOfCanvasPlayable / 100) / 2;
            
            int numberPawnsWidth = 7;
            int numberPawnsHeight = 7;
            double pawnWidht = playableCanvasWidth / numberPawnsWidth;
            double pawnHeight = playableCanvasHeight / numberPawnsHeight;

            List<List<Pawn>> pawns = new List<List<Pawn>>();
            for (int i = 0; i < numberPawnsWidth; i++)
            {
                pawns.Add(new List<Pawn>());
                for (int j = 0; j < numberPawnsHeight; j++)
                    pawns[i].Add(new Pawn(i, j, playableCanvasLeftTopX + i * pawnWidht, playableCanvasLeftTopY + j * pawnHeight, pawnWidht, pawnHeight, Pawn.Status.Border));
            }

            for (int i = 0; i < numberPawnsWidth; i++)
            {
                for (int j = 0; j < numberPawnsHeight; j++)
                {
                    if (i == 3 && j == 3)
                        pawns[i][j].status = Pawn.Status.Empty;
                    else if ((i >= 2 && i <= 4) || (j >= 2 && j <= 4 && (i < 2 || i > 4)))
                        pawns[i][j].status = Pawn.Status.Idle;
                }
            }

            return pawns;
        }
    }
}

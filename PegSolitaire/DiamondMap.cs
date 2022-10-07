using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PegSolitaire
{
    internal class DiamondMap : IMapGenerator
    {
        public List<List<Pawn>> GenerateMap(double canvasWidth, double canvasHeight, double percentageOfCanvasPlayable)
        {
            double playableCanvasWidth = canvasWidth * percentageOfCanvasPlayable / 100;
            double playableCanvasHeight = canvasHeight * percentageOfCanvasPlayable / 100;
            double playableCanvasLeftTopX = canvasWidth * (1 - percentageOfCanvasPlayable / 100) / 2;
            double playableCanvasLeftTopY = canvasHeight * (1 - percentageOfCanvasPlayable / 100) / 2;

            int numberPawnsWidth = 9;
            int numberPawnsHeight = 9;
            double pawnWidht = playableCanvasWidth / numberPawnsWidth;
            double pawnHeight = playableCanvasHeight / numberPawnsHeight;

            List<List<Pawn>> pawns = new List<List<Pawn>>();
            for (int i = 0; i < numberPawnsWidth; i++)
            {
                pawns.Add(new List<Pawn>());
                for (int j = 0; j < numberPawnsHeight; j++)
                    pawns[i].Add(new Pawn(playableCanvasLeftTopX + i * pawnWidht, playableCanvasLeftTopY + j * pawnHeight, pawnWidht, pawnHeight, Pawn.Status.Border));
                
            }

            for (int i = 0; i < numberPawnsWidth; i++)
            {
                for (int j = 0; j < numberPawnsHeight; j++)
                {
                    if (i == 4 && j == 4)
                        pawns[i][j].status = Pawn.Status.Empty;
                    else
                    {
                        int numberInRow = j * 2 + 1;
                        numberInRow = numberInRow > 9 ? 18 - numberInRow : numberInRow;
                        if (i >= 4 - (numberInRow - 1) / 2 && i <= 4 + (numberInRow - 1) / 2)
                            pawns[i][j].status = Pawn.Status.Idle;
                    }

                }
            }

            return pawns;
        }
    
    }
}

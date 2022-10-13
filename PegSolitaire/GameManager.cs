using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PegSolitaire
{
    internal class GameManager
    {
        private static readonly GameManager _instance = new GameManager();

        private Canvas canvasGame;
        private ComboBox comboBoxMap;
        private List<List<Pawn>> pawns = new List<List<Pawn>>();
        private Pawn? activePawn = null;
        private IMapGenerator mapGenerator = new StandardMap();
        private float percentageOfCanvasPlayable = 66;

        private GameManager() 
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    canvasGame = ((MainWindow) window).canvasGame;
                    comboBoxMap = ((MainWindow)window).comboBoxMap;
                }
            }
            if (canvasGame == null || comboBoxMap == null)
            {
                MessageBox.Show("Cannot find all of the game's elements!");
                throw new Exception("Cannot find all of the game's elements!");
            }
        }

        public static GameManager GetGameManager()
        {
            return _instance;
        }

        public void StartGame()
        {
            activePawn = null;
            pawns = mapGenerator.GenerateMap(canvasGame.ActualWidth, canvasGame.ActualHeight, percentageOfCanvasPlayable);
            canvasGame.Children.Clear();
            RefreshEveryPawnOnCanvas();
        }

        public void SetMap(IMapGenerator mapGenerator)
        {
            this.mapGenerator = mapGenerator;
        }

        public void PawnWasClicked(Ellipse ellipse)
        {
            for (int i = 0; i < pawns.Count; i++)
            {
                for (int j = 0; j < pawns[i].Count; j++)
                {
                    if (pawns[i][j].CompareEllipses(ellipse))
                    {
                        Pawn? tempPawn = pawns[i][j].Clicked(canvasGame);
                        if (tempPawn == null)
                            activePawn = null;
                        else
                        {
                            if (activePawn != null)
                                activePawn.ChangeStatusAndDraw(Pawn.Status.Idle, canvasGame);
                            activePawn = tempPawn;
                        }
                        break;
                    }
                }
            }
        }

        private void RefreshEveryPawnOnCanvas()
        {
            for (int i = 0; i < pawns.Count; i++)
                for (int j = 0; j < pawns[i].Count; j++)
                    pawns[i][j].DrawItself(canvasGame);
        }

        public void ResetActivePawn()
        {
            if (activePawn != null)
            {
                activePawn.ChangeStatusAndDraw(Pawn.Status.Idle, canvasGame);
                activePawn = null;
            }
        }


        private bool CanPawnBeMoved(int i, int j)
        {
            if (i - 2 >= 0 && pawns[i - 1][j].status == Pawn.Status.Idle && pawns[i - 2][j].status == Pawn.Status.Empty)
                return true;
            if (i + 2 < pawns.Count && pawns[i + 1][j].status == Pawn.Status.Idle && pawns[i + 2][j].status == Pawn.Status.Empty)
                return true;
            if (j - 2 >= 0 && pawns[i][j-1].status == Pawn.Status.Idle && pawns[i][j - 2].status == Pawn.Status.Empty)
                return true;
            if (j + 2 < pawns[0].Count && pawns[i][j + 1].status == Pawn.Status.Idle && pawns[i][j + 2].status == Pawn.Status.Empty)
                return true;
            return false;
        }

        // Need to be checked when every pawn is Idle (there is no active pawn)
        private bool IsGameLost()
        {
            for (int i = 0; i < pawns.Count; i++)
                for (int j = 0; j < pawns[i].Count; j++)
                    if (CanPawnBeMoved(i, j) == true)
                        return false;
            return true;
        }


        private int GetNumberOfPawnsWithStatus(Pawn.Status status)
        {
            int counter = 0;
            foreach (List<Pawn> row in pawns)
                foreach (Pawn pawn in row)
                    if (pawn.status == status)
                        counter++;
            return counter;
        }

        // Need to be checked when every pawn is Idle (there is no active pawn)
        private bool IsGameWon()
        {
            return GetNumberOfPawnsWithStatus(Pawn.Status.Idle) == 1;
        }
    }
}

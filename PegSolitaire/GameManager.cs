using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xaml;
using static PegSolitaire.Pawn;

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
                    canvasGame = ((MainWindow)window).canvasGame;
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

        private void CheckGameStatus()
        {
            if (IsGameWon())
                MessageBox.Show("You Won!");
            else if (IsGameLost())
                MessageBox.Show("You Lost!");
        }

        public void SetMap(IMapGenerator mapGenerator)
        {
            this.mapGenerator = mapGenerator;
        }

        private bool DeletePawnBetween(Pawn pawn1, Pawn pawn2)
        {
            int deletedI = 0;
            int deletedJ = 0;

            if (pawn1.indexI == pawn2.indexI)
            {
                deletedI = pawn1.indexI;
                if (pawn1.indexJ > pawn2.indexJ)
                    deletedJ = pawn1.indexJ - 1;
                else
                    deletedJ = pawn1.indexJ + 1;
            }
            else if (pawn1.indexJ == pawn2.indexJ)
            {
                deletedJ = pawn1.indexJ;
                if (pawn1.indexI > pawn2.indexI)
                    deletedI = pawn1.indexI - 1;
                else
                    deletedI = pawn1.indexI + 1;
            }
            if (pawns[deletedI][deletedJ].status == Pawn.Status.Idle)
            {
                pawns[deletedI][deletedJ].status = Pawn.Status.Empty;
                pawns[deletedI][deletedJ].DrawItself(canvasGame);
                return true;
            }
            return false;
        }

        public void PawnWasClicked(Ellipse ellipse)
        {
            for (int i = 0; i < pawns.Count; i++)
            {
                for (int j = 0; j < pawns[i].Count; j++)
                {
                    if (pawns[i][j].CompareEllipses(ellipse))
                    {
                        if (activePawn != null && pawns[i][j].status == Pawn.Status.Empty && CanPawnBeMovedToThisSpot(activePawn.indexI, activePawn.indexJ, i, j))
                        {
                            if (DeletePawnBetween(activePawn, pawns[i][j]))
                            {
                                activePawn.ChangeStatusAndDraw(Status.Empty, canvasGame);
                                pawns[i][j].ChangeStatusAndDraw(Status.Idle, canvasGame);
                                activePawn = null;
                                CheckGameStatus();
                            }
                        }
                        else if (activePawn != null && pawns[i][j].status == Pawn.Status.Active)
                        {
                            pawns[i][j].ChangeStatusAndDraw(Status.Idle, canvasGame);
                            activePawn = null;
                        }
                        else if (pawns[i][j].status == Pawn.Status.Idle)
                        {
                            if (activePawn != null)
                                activePawn.ChangeStatusAndDraw(Pawn.Status.Idle, canvasGame);
                            pawns[i][j].ChangeStatusAndDraw(Status.Active, canvasGame);
                            activePawn = pawns[i][j];
                        }
                        else if (pawns[i][j].status == Pawn.Status.Border)
                            ResetActivePawn();

                    }
                }
            }
        }

        private bool CanPawnBeMovedToThisSpot(int firstI, int firstJ, int secondI, int secondJ)
        {
            if (firstI == secondI && Math.Abs(firstJ - secondJ) == 2)
                return true;
            if (firstJ == secondJ && Math.Abs(firstI - secondI) == 2)
                return true;
            return false;
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
            if (pawns[i][j].status != Pawn.Status.Idle)
                return false;
            if (i - 2 >= 0 && pawns[i - 1][j].status == Pawn.Status.Idle && pawns[i - 2][j].status == Pawn.Status.Empty)
                return true;
            if (i + 2 < pawns.Count && pawns[i + 1][j].status == Pawn.Status.Idle && pawns[i + 2][j].status == Pawn.Status.Empty)
                return true;
            if (j - 2 >= 0 && pawns[i][j - 1].status == Pawn.Status.Idle && pawns[i][j - 2].status == Pawn.Status.Empty)
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

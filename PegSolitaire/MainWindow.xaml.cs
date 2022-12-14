using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PegSolitaire
{
    public partial class MainWindow : Window
    {

        private Dictionary<String, IMapGenerator> mapsDict = new Dictionary<string, IMapGenerator>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxMap();
            DataContext = GameManager.GetGameManager();
        }

        private void InitializeComboBoxMap()
        {
            comboBoxMap.ItemsSource = new BindingList<String>(new string[]
            {
                "Standard",
                "Diamond"
            });
            comboBoxMap.SelectedIndex = 0;
            mapsDict.Add("Standard", new StandardMap());
            mapsDict.Add("Diamond", new DiamondMap());
        }

        private void ButtonStartNewGameClick(object sender, RoutedEventArgs e)
        {
            GameManager.GetGameManager().StartGame();
        }


        private void ComboBoxMapDropDownClosed(object sender, EventArgs e)
        {
            GameManager.GetGameManager().SetMap(mapsDict[comboBoxMap.Text]);
        }

        private void CanvasGameMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Ellipse)
            {
                Ellipse clickedEllpise = (Ellipse)e.OriginalSource;
                GameManager.GetGameManager().PawnWasClicked(clickedEllpise);
            }
            else
            {
                GameManager.GetGameManager().ResetActivePawn();
            }
            if (GameManager.GetGameManager().WonGame)
            {
                myPopUp.Information = "You Won!";
                myPopUp.IsShowed = true;
            }
            else if (GameManager.GetGameManager().LostGame)
            {
                myPopUp.Information = "You Lost!";
                myPopUp.IsShowed = true;
            }
        }

        private void UndoCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = GameManager.GetGameManager().moves.Count > 0;
        }

        private void UndoCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            GameManager.GetGameManager().UndoLastMove();
        }
    }
}

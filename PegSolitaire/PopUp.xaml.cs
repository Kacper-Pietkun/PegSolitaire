using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// <summary>
    /// Interaction logic for PopUp.xaml
    /// </summary>
    public partial class PopUp : UserControl, INotifyPropertyChanged
    {
        private bool _isShowed;
        public bool IsShowed
        {
            get
            {
                return _isShowed;
            }
            set
            {
                if (_isShowed != value)
                {
                    _isShowed = value;
                    if (_isShowed)
                        container.Visibility = Visibility.Visible;
                    else
                        container.Visibility = Visibility.Hidden;
                }
            }
        }

        private String _information;
        public String Information
        {
            get
            {
                return _information;
            }
            set
            {
                if (_information != value)
                {
                    _information = value;
                    OnPropertyChanged();
                }
            }
        }

        public PopUp()
        {
            InitializeComponent();
            DataContext = this;
            Information = "Welcome to PegSolitaire!";
            IsShowed = false;
        }

        private void ButtonClosePopUpClick(object sender, RoutedEventArgs e)
        {
            IsShowed = false;
            container.Visibility = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

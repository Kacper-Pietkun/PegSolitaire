using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PegSolitaire
{
    public class GameStatistics : INotifyPropertyChanged
    {
        private int _movesDone;
        public int MovesDone
        {
            get
            {
                return _movesDone;
            }
            set
            {
                if (_movesDone != value)
                {
                    _movesDone = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _undoDone;
        public int UndoDone
        {
            get
            {
                return _undoDone;
            }
            set
            {
                if (_undoDone != value)
                {
                    _undoDone = value;
                    OnPropertyChanged();
                }
            }
        }

        public GameStatistics(int movesDone=0, int undoDone=0)
        {
            _movesDone = movesDone;
            MovesDone = movesDone;
            _undoDone = undoDone;
            UndoDone = undoDone;
        }

        public void Reset()
        {
            MovesDone = 0;
            UndoDone = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

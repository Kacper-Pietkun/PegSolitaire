using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PegSolitaire
{
    internal class Pawn
    {
        public enum Status
        {
            Idle,
            Active,
            Empty,
            Border
        }

        public Status status { get; set; }
        public int indexI { get; set; }
        public int indexJ { get; set; }
        public double posX { get; set; }
        public double posY { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public bool CanMove { get; set; } = false;
        private double pawnPadding = 3;
        private Ellipse? ellipse;
        

        public Pawn(int indexI, int indexJ, double posX, double posY, double width, double height, Status status = Status.Idle)
        {
            this.indexI = indexI;
            this.indexJ = indexJ;
            this.posX = posX + pawnPadding;
            this.posY = posY + pawnPadding;
            this.width = width - 2 * pawnPadding;
            this.height = height - 2 * pawnPadding;
            this.status = status;
            ellipse = null;
        }

        public void DrawItself(Canvas canvas)
        {
            if (ellipse != null)
            {
                canvas.Children.Remove(ellipse);
                ellipse = null;
            }

            Ellipse newEllipse = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = GetColor(),
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Opacity = GetOpacity()
            };
            Canvas.SetLeft(newEllipse, posX);
            Canvas.SetTop(newEllipse, posY);
            ellipse = newEllipse;
            canvas.Children.Add(newEllipse);
        }

        public bool CompareEllipses(Ellipse ellipse)
        {
            return this.ellipse == ellipse;
        }

        public void ChangeStatusAndDraw(Status status, Canvas canvas)
        {
            this.status = status;
            DrawItself(canvas);
        }

        private SolidColorBrush GetColor()
        {
            switch (status)
            {
                case Status.Idle:
                    return new SolidColorBrush(Color.FromRgb(255, 255, 204));
                case Status.Active:
                    return new SolidColorBrush(Color.FromRgb(238, 124, 124));
                case Status.Empty:
                    return new SolidColorBrush(Color.FromRgb(199, 191, 191));
                case Status.Border:
                    return new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            return new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private double GetOpacity()
        {
            switch (status)
            {
                case Status.Idle:
                case Status.Active:
                    return 1;
                case Status.Empty:
                    return 0.4;
            }
            return 0;
        }
    }
}

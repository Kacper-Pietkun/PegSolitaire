using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Collections.Generic;

namespace PegSolitaire
{
    internal class Animator
    {
        public void MovePawn(GameManager gameManager, Canvas canvasGame, List<List<Pawn>> pawns, MoveDescriptor moveDescriptor)
        {
            Ellipse newEllipse = new Ellipse
            {
                Width = pawns[moveDescriptor.SourceIndices.X][moveDescriptor.SourceIndices.Y].width,
                Height = pawns[moveDescriptor.SourceIndices.X][moveDescriptor.SourceIndices.Y].height,
                Fill = new SolidColorBrush(Color.FromRgb(255, 255, 204)),
                StrokeThickness = 1,
                Stroke = Brushes.Black,
                Opacity = 1,
                
            };
            Canvas.SetLeft(newEllipse, pawns[moveDescriptor.SourceIndices.X][moveDescriptor.SourceIndices.Y].posX);
            Canvas.SetTop(newEllipse, pawns[moveDescriptor.SourceIndices.X][moveDescriptor.SourceIndices.Y].posY);
            canvasGame.Children.Add(newEllipse);

            DoubleAnimation XAnimation = new DoubleAnimation(pawns[moveDescriptor.DestinationIndices.X][moveDescriptor.DestinationIndices.Y].posX, new Duration(TimeSpan.FromSeconds(0.2)));
            DoubleAnimation YAnimation = new DoubleAnimation(pawns[moveDescriptor.DestinationIndices.X][moveDescriptor.DestinationIndices.Y].posY, new Duration(TimeSpan.FromSeconds(0.2)));

            // Create a clock from the animation.
            AnimationClock myControllableClockX = XAnimation.CreateClock();
            AnimationClock myControllableClockY = YAnimation.CreateClock();

            // Apply animations
            newEllipse.ApplyAnimationClock(
                Canvas.LeftProperty, myControllableClockX);
            newEllipse.ApplyAnimationClock(
                Canvas.TopProperty, myControllableClockY);
            int pos= 2;
            myControllableClockX.Completed += new EventHandler((sender, e) => gameManager.AfterAnimationHandler(sender, e, newEllipse, moveDescriptor));
        }
    }
}

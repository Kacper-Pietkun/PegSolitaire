using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PegSolitaire.Pawn;

namespace PegSolitaire
{
    internal static class MovesExecutor
    {
        public static (bool, Point) ExecuteMove(List<List<Pawn>> pawns, MoveDescriptor moveDescriptor)
        {
            Point intermediateIndices = InferIntermediateIndices(moveDescriptor.SourceIndices, moveDescriptor.DestinationIndices);
            if (moveDescriptor.IsMoveReverted || IsMoveEvenPossible(pawns, moveDescriptor.SourceIndices, moveDescriptor.DestinationIndices, intermediateIndices))
            {
                MovePawn(pawns, moveDescriptor.SourceIndices, moveDescriptor.DestinationIndices);
                if (moveDescriptor.IsMoveReverted)
                    InstantiatePawn(pawns, intermediateIndices);
                else
                    DeletePawn(pawns, intermediateIndices);
                return (true, intermediateIndices);
            }
            return (false, new Point());
        }
        private static bool IsMoveEvenPossible(List<List<Pawn>> pawns, Point sourceIndices, Point destinationIndices, Point intermediateIndices)
        {
            if (pawns[intermediateIndices.X][intermediateIndices.Y].status != Pawn.Status.Idle)
                return false;
            if (sourceIndices.X == destinationIndices.X && Math.Abs(sourceIndices.Y - destinationIndices.Y) == 2)
                return true;
            if (sourceIndices.Y == destinationIndices.Y && Math.Abs(sourceIndices.X - destinationIndices.X) == 2)
                return true;
            return false;
        }

        private static Point InferIntermediateIndices(Point sourceIndices, Point destinationIndices)
        {
            Point intermediateIndices = new Point();
            if (sourceIndices.X == destinationIndices.X)
            {
                intermediateIndices.X = sourceIndices.X;
                if (sourceIndices.Y > destinationIndices.Y)
                    intermediateIndices.Y = sourceIndices.Y - 1;
                else
                    intermediateIndices.Y = sourceIndices.Y + 1;
            }
            else if (sourceIndices.Y == destinationIndices.Y)
            {
                intermediateIndices.Y = sourceIndices.Y;
                if (sourceIndices.X > destinationIndices.X)
                    intermediateIndices.X = sourceIndices.X - 1;
                else
                    intermediateIndices.X = sourceIndices.X + 1;
            }
            return intermediateIndices;
        }

        private static void MovePawn(List<List<Pawn>> pawns, Point sourceIndices, Point destinationIndices)
        {
            pawns[sourceIndices.X][sourceIndices.Y].status = Status.Empty;
            pawns[destinationIndices.X][destinationIndices.Y].status = Status.Idle;
        }

        private static void DeletePawn(List<List<Pawn>> pawns, Point indices)
        {
            pawns[indices.X][indices.Y].status = Status.Empty;
        }
        private static void InstantiatePawn(List<List<Pawn>> pawns, Point indices)
        {
            pawns[indices.X][indices.Y].status = Status.Idle;
        }
    }
}

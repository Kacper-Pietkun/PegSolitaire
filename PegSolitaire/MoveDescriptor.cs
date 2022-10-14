using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegSolitaire
{
    internal class MoveDescriptor
    {
        // Indices of the pawn that will be moved
        public Point SourceIndices { get; set; }

        // Indices of the place where pawn will be moved
        public Point DestinationIndices { get; set; }

        // Is this a normal move or an "undo" move
        public bool IsMoveReverted { get; set; }

        public MoveDescriptor(Point sourceIndices, Point destinationIndices)
        {
            this.SourceIndices = sourceIndices;
            this.DestinationIndices = destinationIndices;
            this.IsMoveReverted = false;
        }

        public void RevertMove()
        {
            (SourceIndices, DestinationIndices) = (DestinationIndices, SourceIndices);
            IsMoveReverted = (IsMoveReverted == false);
        }
    }
}

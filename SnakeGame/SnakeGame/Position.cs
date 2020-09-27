using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int GetRow
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int GetCol
        {
            get { return this.col; }
            set { this.col = value; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Snake
    {
        public Queue<Position> snakeElements;

        public Snake()
        {
            snakeElements = new Queue<Position>();
        }

        public Queue<Position> GetPos
        {
            get { return snakeElements; }
        }

        public void DrawSnake()
        {
            for (int i = 0; i <= 3; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }
        }

        public void AddSnakeLength()
        {
            snakeElements.Enqueue(new Position(0, 1));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class Snake
    {
        public Queue<Position> snake;

        public Snake()
        {
            snake = new Queue<Position>();
        }

        public Queue<Position> GetPos
        {
            get { return snake; }
        }

        public void DrawSnake()
        {
            int i = 0;
            while (i <= 5)
            {
                snake.Enqueue(new Position(0, i));
                i++;
            }
        }
    }
}

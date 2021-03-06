﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    public class Snake
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

        public void IncreaseSnakeLength()
        {
            snakeElements.Enqueue(new Position(0, 1));
        }

        public void IncreaseSnakeLengthSpecial()
        {
            snakeElements.Enqueue(new Position(0, 2));
        }
    }
}

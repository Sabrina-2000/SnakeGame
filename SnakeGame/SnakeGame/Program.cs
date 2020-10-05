using System;
using System.Linq;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Media.SoundPlayer bgm = new System.Media.SoundPlayer();
            bgm.SoundLocation = "../../../sound/bgm.wav";
            bgm.Play();

            // initialize objects
            int CURRENTSCORE = 0;

            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            int lastFoodTime = 0;
            int foodDissapearTime = 16000;


            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            Console.BufferHeight = Console.WindowHeight;
            double sleepTime = 100;
            lastFoodTime = Environment.TickCount;

            // Initialize the obstacle and draw the obstacles
            Obstacle obs = new Obstacle();
            obs.Generate_random_obstacle();

            foreach (Position obstacle in obs.GetObsPos)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.col, obstacle.row);
                Console.Write("=");
            }

            // Iniatitlize the food and draw food
            Food food = new Food();
            food.Generate_random_food();

            // Initialize the snake and draw snake
            Snake snake = new Snake();
            snake.DrawSnake();
            int direct = right;

            // looping
            while (true)
            {
                // Set the score at the top right corner
                Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                Console.WriteLine("Score: " + CURRENTSCORE);

                // check whats the key is pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direct != right) direct = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direct != left) direct = right;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direct != down) direct = up;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direct != up) direct = down;
                    }
                }

                // update position of the snake
                Position snakeHead = snake.GetPos.Last();
                Position nextDirection = directions[direct];

                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                    snakeHead.col + nextDirection.col);

                // check if the snake exceed the width or height
                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;

                // check if the  snake collison with self or obstacles
                if (snake.GetPos.Contains(snakeNewHead) || obs.GetObsPos.Contains(snakeNewHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game over!");
                    return;
                }

                // actions for eating the food
                if (snakeNewHead.col == food.x && snakeNewHead.row == food.y)
                {
                    CURRENTSCORE++;
                    Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                    Console.WriteLine("Score: " + CURRENTSCORE);
                    food = new Food();
                    food.Generate_random_food();
                    snake.AddSnakeLength();
                }
                // draw the body of the snake
                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");

                // moven=ment of the snake
                snake.GetPos.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;

                // draw snake head
                if (direct == right)
                {
                    Console.Write(">");
                }
                if (direct == left)
                {
                    Console.Write("<");
                }
                if (direct == up)
                {
                    Console.Write("^");
                }
                if (direct == down)
                {
                    Console.Write("v");
                }

                // moving...
                Position last = snake.GetPos.Dequeue();
                Console.SetCursorPosition(last.col, last.row);
                Console.Write(" ");

                // winning condition score = 6
                if (CURRENTSCORE == 6)
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 3+10, Console.WindowHeight / 3+2);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("!! Stage Clear!!");
                    String Ending_Press = Console.ReadLine();
                    return;
                }

                // food lasting time
                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    Console.SetCursorPosition(food.x, food.y);
                    Console.Write(" ");
                    food = new Food();
                    food.Generate_random_food();

                    lastFoodTime = Environment.TickCount;
                }

                sleepTime -= 0.01;
                Thread.Sleep((int)sleepTime);
            }
        }
    }
}


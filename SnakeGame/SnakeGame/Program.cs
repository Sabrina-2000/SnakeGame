using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {


            // initialize objects
            int CURRENTSCORE = 0;

            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            int lastFoodTime = 0;
            int lastSpecialFoodTime = 0;
            int foodDissapearTime = 16000;
            int specialFoodDissapearTime = 10000;

            bool play = false;
            bool difficulty = false;
            bool scoreBoard = false;
            bool help = false;
            bool welcome = true;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            var path = "../../../text/score.txt";

            while (true)
            {
                while(welcome == true)
                {   
                    //Welcome Screen
                    Console.Clear();
                    play = false;
                    difficulty = false;
                    scoreBoard = false;
                    help = false;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 3);
                    Console.Write("Welcome to Snake Game");
                    Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 3 + 3);
                    Console.WriteLine("Please Select the Action below");
                    Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 3 + 4);
                    Console.WriteLine("1. Play --Easy mode--");
                    Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 3 + 5);
                    Console.WriteLine("2. Hard Mode");
                    Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 3 + 6);
                    Console.WriteLine("3. Score Board");
                    Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 3 + 7);
                    Console.WriteLine("4. Help");
                    Console.SetCursorPosition(Console.WindowWidth / 3 + 10, Console.WindowHeight / 3 + 8);


                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            play = true;
                            welcome = false;
                            break;
                        case ConsoleKey.D2:
                            difficulty = true;
                            welcome = false;
                            break;
                        case ConsoleKey.D3:
                            scoreBoard = true;
                            welcome = false;
                            break;
                        case ConsoleKey.D4:
                            help = true;
                            welcome = false;
                            break;
                        default:
                            Console.WriteLine("Wrong input");
                            break;

                    }
                }


                //Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();

                while(help == true)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 3);
                    Console.WriteLine("############ INSTRUCTION ############");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 3+2);
                    Console.WriteLine("1. ARROW KEY on the keyboard is to control the movement of the snake");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 3+3);
                    Console.WriteLine("2. EAT FOOD on the map to get higher score and it will dissapear in a short time");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 3+4);
                    Console.Write("3. DONT HIT the obstacles as it will end the game.");
                    Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 3+6);
                    Console.Write("Press ESC to back to menu");
                    ConsoleKeyInfo helpKey = Console.ReadKey();
                    if(helpKey.Key == ConsoleKey.Escape)
                    {
                        welcome = true;
                        help = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Input");
                    }
                }

                while(scoreBoard == true)
                {
                    Console.WriteLine("##### SCORE BOARD #####");
                    using (StreamReader file = new StreamReader(path))
                    {
                        string ln;
                        while ((ln = file.ReadLine()) != null)
                        {
                            Console.WriteLine(ln);
                        }
                    }
                    Console.WriteLine("Press ESC to back to menu");
                    ConsoleKeyInfo scoreKey = Console.ReadKey();
                    if(scoreKey.Key == ConsoleKey.Escape)
                    {
                        welcome = true;
                        scoreBoard = false;
                        break;
                    }

                }

                if (play == true || difficulty == true)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    System.Media.SoundPlayer bgm = new System.Media.SoundPlayer();
                    bgm.SoundLocation = "../../../sound/bgm.wav";
                    //bgm.Play();


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
                    //Position obstacleList = obs.GetObsPos;
                    obs.Generate_random_obstacle();

                    foreach (Position obstacle in obs.GetObsPos)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.SetCursorPosition(obstacle.col, obstacle.row);
                        Console.Write("[");
                    }

                    // Iniatialize the food and draw food
                    Food food = new Food();
                    food.Generate_random_food();

                    // Initialize the snake and draw snake
                    Snake snake = new Snake();
                    snake.DrawSnake();
                    int direct = right;

                    //Initialize the special food and draw it
                    SpecialFood specialFood = new SpecialFood();
                    specialFood.Generate_random_food();

                    // looping
                    while (play == true || difficulty==true)
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
                            if (userInput.Key == ConsoleKey.Enter)
                            {
                                Environment.Exit(0);
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
                        foreach (Position obstacleList in obs.GetObsPos)
                        {
                            if ((snake.GetPos.Contains(snakeNewHead)) || ((snakeHead.row == obstacleList.row) && (snakeHead.col == obstacleList.col)))
                            {
                                StreamWriter sw = File.AppendText(path);
                                sw.WriteLine("Score: " + CURRENTSCORE.ToString());
                                sw.Close();
                                Console.Clear();
                                //onsole.WriteLine("HIT!");
                                Console.Clear();
                                Console.SetCursorPosition(Console.WindowWidth / 3 + 6, Console.WindowHeight / 3 + 2);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("!!!!!!!! Gameover !!!!!!!");
                                Console.SetCursorPosition(Console.WindowWidth / 3 + 9, Console.WindowHeight / 3 + 3);
                                Console.WriteLine("Current Score: " + CURRENTSCORE);
                                Console.SetCursorPosition(Console.WindowWidth / 3 + 9, Console.WindowHeight / 3 + 4);
                                Console.WriteLine("Press ESC key to back to menu");
                                Console.SetCursorPosition(Console.WindowWidth / 3 + 9, Console.WindowHeight / 3 + 5);
                                Console.WriteLine("Press Enter key to exit");
                                ConsoleKeyInfo loseKey = Console.ReadKey();
                                if(loseKey.Key == ConsoleKey.Escape)
                                {
                                    welcome = true;
                                    play = false;
                                    break;
                                }
                                else if (loseKey.Key == ConsoleKey.Enter)
                                {
                                    return;

                                }
                                else
                                {
                                    Console.WriteLine("Wrong Input");
                                }
                            }

                        }


                        // actions for eating the food
                        if (snakeNewHead.col == food.x && snakeNewHead.row == food.y)
                        {
                            System.Media.SoundPlayer coin = new System.Media.SoundPlayer();
                            coin.SoundLocation = "../../../sound/coin.wav";
                            coin.Play();

                            CURRENTSCORE++;
                            Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                            Console.WriteLine("Score: " + CURRENTSCORE);
                            food = new Food();
                            food.Generate_random_food();
                            snake.IncreaseSnakeLength();
                        }
                        // actions for eating the special food
                        if (snakeNewHead.col == specialFood.x && snakeNewHead.row == specialFood.y)
                        {
                            System.Media.SoundPlayer coin = new System.Media.SoundPlayer();
                            coin.SoundLocation = "../../../sound/coin.wav";
                            coin.Play();

                            CURRENTSCORE = CURRENTSCORE + 2;
                            Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                            Console.WriteLine("Score: " + CURRENTSCORE);
                            specialFood = new SpecialFood();
                            specialFood.Generate_random_food();
                            snake.IncreaseSnakeLengthSpecial();
                        }

                        // draw the body of the snake
                        Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("*");

                        // moven=ment of the snake
                        snake.GetPos.Enqueue(snakeNewHead);
                        Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                        Console.ForegroundColor = ConsoleColor.Yellow;

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

                        // winning condition score >= 6
                        if (CURRENTSCORE >= 6)
                        {
                            StreamWriter sw = File.AppendText(path);
                            sw.WriteLine("Score: " + CURRENTSCORE.ToString());
                            sw.Close();
                            Console.Clear();
                            Console.SetCursorPosition(Console.WindowWidth / 3 + 6, Console.WindowHeight / 3 + 2);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("!!!!!!!! Stage Clear!!!!!!!!");
                            Console.SetCursorPosition(Console.WindowWidth / 3 + 9, Console.WindowHeight / 3 + 3);
                            Console.WriteLine("Current Score: " + CURRENTSCORE);
                            Console.SetCursorPosition(Console.WindowWidth / 3 + 9, Console.WindowHeight / 3 + 4);
                            Console.WriteLine("Press ESC key to back to menu");
                            Console.SetCursorPosition(Console.WindowWidth / 3 + 9, Console.WindowHeight / 3 + 5);
                            Console.WriteLine("Press Enter key to exit");
                            ConsoleKeyInfo winKey = Console.ReadKey();
                            if (winKey.Key == ConsoleKey.Escape)
                            {
                                welcome = true;
                                play = false;
                                break;
                            }
                            else if (winKey.Key == ConsoleKey.Enter)
                            {
                                return;

                            }
                            else
                            {
                                Console.WriteLine("Wrong Input");
                            }
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

                        // food lasting time
                        if (Environment.TickCount - lastSpecialFoodTime >= specialFoodDissapearTime)
                        {
                            Console.SetCursorPosition(specialFood.x, specialFood.y);
                            Console.Write(" ");
                            specialFood = new SpecialFood();
                            specialFood.Generate_random_food();

                            lastSpecialFoodTime = Environment.TickCount;
                        }
                        if (play == true)
                        { sleepTime -= 0.01; }

                        if (difficulty == true)
                        { sleepTime = 20;}
                        
                        Thread.Sleep((int)sleepTime);
                    }

                }
                //Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 2);
                //String Ending__Press = Console.ReadLine();
            }
        }
    }
}


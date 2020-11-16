using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    public class Food
    {
        public int x { get; set; }
        public int y { get; set; }

        public void Generate_random_food()
        {
            Random random = new Random();
            this.x = random.Next(Console.WindowWidth);
            this.y = random.Next(Console.WindowHeight);
            Console.SetCursorPosition(this.x, this.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@");
        }

        public int getFoodRow()
        {
            return x;
        }
        public int getFoodCol()
        {
            return y;
        }


    }
}

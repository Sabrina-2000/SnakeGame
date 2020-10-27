using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame
{
    class SpecialFood
    {
        public int x { get; set; }
        public int y { get; set; }
        public SpecialFood()
        {
            Random random = new Random();
            this.x = random.Next(Console.WindowWidth);
            this.y = random.Next(Console.WindowHeight);
        }

        public void Generate_random_food()
        {
            Console.SetCursorPosition(this.x, this.y);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("&");
        }
    }
}

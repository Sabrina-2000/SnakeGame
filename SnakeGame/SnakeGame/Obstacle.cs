using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using System.Threading;

namespace SnakeGame
{
    public class Obstacle
    {
        private List<Position> obstacles = new List<Position>();
        private List<int> _x;
        private List<int> _y;
        private List<int> obsX;
        private List<int> obsY;

        public void Generate_random_obstacle()
        {
            Random random = new Random();
            _x = new List<int>();
            _y = new List<int>();

            for (int i = 1; i < 6; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                int x = random.Next(Console.WindowHeight);
                int y = random.Next(Console.WindowWidth);
                Position positions = new Position(x, y);
                obstacles.Add(positions);
            }
        }

        List<Position> obsList = new List<Position>();

        public void AddObstacle()
        {
            for(int i = 0; i < 5; i++)
            {
                int x = 10;
                int y = 9 + i;
                obsList.Add(new Position(x, y));
            }
        }

        public int GetCount()
        {
            return obsList.Count();
        }

        public List<Position> GetObsPos
        {
            get { return obstacles; }
        }

        public List<int> GetObsX()
        {
            obsX = new List<int>() ;
            foreach (Position obs in obstacles)
            {
                obsX.Add(obs.col);

            }
            return obsX;
        }

        public List<int> GetObsY()
        {
            obsY = new List<int>();
            foreach (Position obs in obstacles)
            {
                obsY.Add(obs.row);

            }
            return obsY;
        }
    }
}

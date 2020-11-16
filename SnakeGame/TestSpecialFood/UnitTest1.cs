using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame;
using System;


namespace Test
{
    [TestClass]
    public class UnitTest1s
    {
       
        [TestMethod]
        public void position_GetCol_TEST()
        {
            SnakeGame.Position p = new SnakeGame.Position(3,5);
           // int r = 3;
           // int c = 5;
            Assert.AreEqual(5, p.GetCol);
            //Assert.AreEqual(3, p.GetRow);

        }
        [TestMethod]
        public void position_GetRow_TEST()
        {
            SnakeGame.Position p = new SnakeGame.Position(3, 5);
            // int r = 3;
            // int c = 5;
            //Assert.AreEqual(5, p.Ge);
            Assert.AreEqual(3, p.GetRow);

        }

        [TestMethod]
        public void obstacle_Count_TEST()
        {
            Obstacle o = new Obstacle();
            o.AddObstacle();
            Assert.AreEqual(5, o.GetCount());
        }

        [TestMethod]
        public void food_Position_TEST()
        {
            Food f = new Food();
            f.x = 5;
            f.y = 5;
            Assert.AreEqual(5, f.getFoodRow());
            Assert.AreEqual(5, f.getFoodCol());
        }

    }
}

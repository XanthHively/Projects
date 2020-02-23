using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Snakes.Classes
{
    class Snake
    {
        //body location data
        Queue<int> snakeTailX = new Queue<int>();
        Queue<int> snakeTailY = new Queue<int>();
        Queue<string> snakeLocations = new Queue<string>();
        //size of board
        int height;
        int width;
        //spawn location
        int xStart;
        int yStart;
        //coordinate system
        int YAxis;
        int XAxis;
        public string FoodPosition { get; set; }
        public char Direction { get; private set; }
        public bool Alive { get; private set; }

        public Snake(Dictionary<string,int> diff)
        {
            //difficulty peramiters
            width = diff["width"];
            height = diff["height"];
            xStart = diff["xStart"] + 2;
            yStart = diff["yStart"] + 1;
            //draw initial snake
            Console.SetCursorPosition(xStart+2, yStart+1);
            for (int i = 1; i <= 10; i++)
            {
                Console.Write("██");
            }
            //ginerate body location data
            for(int i = 1; i <= 10; i++)
            {
                snakeTailX.Enqueue(i);
                snakeTailY.Enqueue(1);
                snakeLocations.Enqueue(i.ToString() + "," + 1);
            }
            YAxis = 1;
            XAxis = 10;
            Direction = 'd';
            FoodPosition = "";
            Alive = true;
        }
        
        public void UpdateDirection(char newDirection)
        {
            switch (newDirection)
            {
                case 'w':
                    if (Direction != 's') Direction = 'w';
                    break;
                case 'a':
                    if (Direction != 'd') Direction = 'a';
                    break;
                case 's':
                    if (Direction != 'w') Direction = 's';
                    break;
                case 'd':
                    if (Direction != 'a') Direction = 'd';
                    break;
            }
        }
        public void Move()
        {
            bool eatingFood = (XAxis.ToString() + "," + YAxis.ToString()) == FoodPosition;
            switch (Direction)
            {
                case 'w':
                    if (!eatingFood) CutTail();
                    else FoodPosition = "";
                    YAxis--;
                    GrowHead();
                    break;
                case 'a':
                    if (!eatingFood) CutTail();
                    else FoodPosition = "";
                    XAxis--;
                    GrowHead();
                    break;
                case 's':
                    if (!eatingFood) CutTail();
                    else FoodPosition = "";
                    YAxis++;
                    GrowHead();
                    break;
                case 'd':
                    if (!eatingFood) CutTail();
                    else FoodPosition = "";
                    XAxis++;
                    GrowHead();
                    break;
            }
            Console.SetCursorPosition(0, 1);
        }
        private void CutTail()
        {
            Console.SetCursorPosition((snakeTailX.Dequeue() * 2) + xStart, snakeTailY.Dequeue() + yStart);
            Console.Write("  ");
            snakeLocations.Dequeue();
        }
        private void GrowHead()
        {
            Console.SetCursorPosition((XAxis * 2) + xStart, YAxis + yStart);
            Console.Write("██");
            snakeTailX.Enqueue(XAxis);
            snakeTailY.Enqueue(YAxis);
            DidWeDie();
            snakeLocations.Enqueue(XAxis.ToString() + "," + YAxis.ToString());
        }
        private void DidWeDie()
        {
            if(snakeLocations.Contains(XAxis.ToString() + "," + YAxis.ToString()))
            {
                Alive = false;
            }
            else if (XAxis >= width || XAxis < 0 || YAxis < 0 || YAxis >= height)
            {
                Alive = false;
            }
        }
        public void DeathAnimation()
        {
            List<int> xDeath = new List<int>();
            List<int> yDeath = new List<int>();
            xDeath.AddRange(snakeTailX);
            yDeath.AddRange(snakeTailY);
            int deathLeangth = xDeath.Count;
            for (int i = deathLeangth-1; i >= 0; i--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((xDeath[i] * 2) + xStart, yDeath[i] + yStart);
                Console.Write("██");
                Thread.Sleep(60);
            }
            Console.SetCursorPosition(0, 30);
        }
        public Queue<string> getSnakeLocations()
        {
            return snakeLocations;
        }
        public string getSnakeHeadLocations()
        {
            return XAxis.ToString() + "," + YAxis.ToString();
        }
    }
}

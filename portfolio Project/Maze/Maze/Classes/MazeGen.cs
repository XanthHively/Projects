using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Maze.Classes
{
    public class MazeGen
    {
        MazeData mazeData = new MazeData();


        Stack<string> path = new Stack<string>();

        int xAxes, yAxes;
        int rightEdge;

        public MazeGen()
        {
            string[] startXY = mazeData.StartPosition.Split(",");
            xAxes = int.Parse(startXY[0])-1;
            yAxes = int.Parse(startXY[1]);
            rightEdge = 60;
        }
        public MazeData Generate()
        {
            var random = new Random();
            Draw(2, 4);
            while (path.Count != 0)
            {
                if (CheckAllDirections())
                {
                    Draw(random.Next(1, 5), 2);
                }
            }
            return mazeData;
        }
        public void Draw(int direction,int length)
        {
            bool deadEnd = false;
            for(int i = 0;i < length && !deadEnd; i++)
            {
                switch (direction)
                {
                    case 1://left
                        if (CheckLeft()) xAxes--;
                        else deadEnd = true;
                        break;
                    case 2://right
                        if (CheckRight()) xAxes++;
                        else deadEnd = true;
                        break;
                    case 3://up
                        if (CheckUp()) yAxes--;
                        else deadEnd = true;
                        break;
                    case 4://down
                        if (CheckDown()) yAxes++;
                        else deadEnd = true;
                        break;
                }
                if (!deadEnd)
                {
                    AddMazeBlock();
                    EndFoundCheck();
                    Thread.Sleep(10);
                }
            }
        }
        public void EndFoundCheck()
        {
            if (xAxes == 59)
            {
                mazeData.EndPosition = $"{xAxes},{yAxes}";
                rightEdge -= 2;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(xAxes * 2, yAxes);
                Console.Write("██");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void AddMazeBlock()
        {
            Console.SetCursorPosition(xAxes * 2, yAxes);
            Console.Write("██");
            path.Push($"{xAxes},{yAxes}");
            mazeData.AddMazeBlock(xAxes, yAxes);
        }
        public bool CheckAllDirections()
        {
            int bad = 0;
            if (!CheckLeft()) bad++;
            if (!CheckRight()) bad++;
            if (!CheckUp()) bad++;
            if (!CheckDown()) bad++;

            if(bad == 4)
            {
                string[] xy = path.Pop().Split(",");
                xAxes = int.Parse(xy[0]);
                yAxes = int.Parse(xy[1]);
            }

            return bad < 4;
        }
        private bool CheckUp()
        {
            bool freeToMove = true;

            int x = xAxes - 1;
            int y = yAxes - 1;
            for(int i = 0;i < 2; i++,y--)
            {
                for (int j = 0; j < 3; j++,x++)
                {
                    if (mazeData.Contains(x,y) || y < 0 || x > rightEdge) 
                    { 
                        freeToMove = false;
                        if (!freeToMove) break;
                    }
                }
                if (!freeToMove) break;
                x = xAxes - 1;
            }
            return freeToMove;
        }
        private bool CheckDown()
        {
            bool freeToMove = true;

            int x = xAxes - 1;
            int y = yAxes + 1;
            for (int i = 0; i < 2; i++, y++)
            {
                for (int j = 0; j < 3; j++, x++)
                {
                    if (mazeData.Contains(x, y) || y > 28 || x > rightEdge)
                    {
                        freeToMove = false;
                        if (!freeToMove) break;
                    }
                }
                if (!freeToMove) break;
                x = xAxes - 1;
            }
            return freeToMove;
        }
        private bool CheckLeft()
        {
            bool freeToMove = true;

            int x = xAxes - 1;
            int y = yAxes - 1;
            for (int i = 0; i < 2; i++, x--)
            {
                for (int j = 0; j < 3; j++, y++)
                {
                    if (mazeData.Contains(x, y) || x < 0)
                    {
                        freeToMove = false;
                        if (!freeToMove) break;
                    }
                }
                if (!freeToMove) break;
                y = yAxes - 1;
            }
            return freeToMove;
        }
        private bool CheckRight()
        {
            bool freeToMove = true;

            int x = xAxes + 1;
            int y = yAxes - 1;
            for (int i = 0; i < 2; i++, x++)
            {
                for (int j = 0; j < 3; j++, y++)
                {
                    if (mazeData.Contains(x, y) || x > rightEdge)
                    {
                        freeToMove = false;
                        if (!freeToMove) break;
                    }
                }
                if (!freeToMove) break;
                y = yAxes - 1;
            }
            return freeToMove;
        }
    }
}

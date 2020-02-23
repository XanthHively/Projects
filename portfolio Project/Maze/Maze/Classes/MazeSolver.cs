using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Maze.Classes
{
    class MazeSolver
    {
        MazeData mazeData;
        Stack<string> path = new Stack<string>();
        List<string> haveCheched = new List<string>();

        int xAxes, yAxes;
        public MazeSolver(MazeData mazeData)
        {
            this.mazeData = mazeData;
            string[] startXY = mazeData.StartPosition.Split(",");
            xAxes = int.Parse(startXY[0]);
            yAxes = int.Parse(startXY[1]);
        }
        public void Solve()
        {
            var random = new Random();
            while ($"{xAxes},{yAxes}" != mazeData.EndPosition)
            {
                if (CheckAllDirections())
                {
                    Move(random.Next(1, 5));
                }
            }
            DrawMarker();
        }

        private bool CheckAllDirections()
        {
            int bad = 0;
            if (!freeToMove(xAxes - 1, yAxes)) bad++;
            if (!freeToMove(xAxes + 1, yAxes)) bad++;
            if (!freeToMove(xAxes, yAxes - 1)) bad++;
            if (!freeToMove(xAxes, yAxes + 1)) bad++;

            if (bad == 4)
            {
                clearMarker();
                string[] xy = path.Pop().Split(",");
                xAxes = int.Parse(xy[0]);
                yAxes = int.Parse(xy[1]);
                DrawMarker();
                Thread.Sleep(50);
            }

            return bad < 4;
        }

        public void Move(int direction)
        {
            bool hitWall = false;
            while (!hitWall)
            {
                clearMarker();
                switch (direction)
                {
                    case 1://left
                        if (freeToMove(xAxes-1,yAxes)) xAxes--;
                        else hitWall = true;
                        break;
                    case 2://right
                        if (freeToMove(xAxes + 1, yAxes)) xAxes++;
                        else hitWall = true;
                        break;
                    case 3://up
                        if (freeToMove(xAxes, yAxes - 1)) yAxes--;
                        else hitWall = true;
                        break;
                    case 4://down
                        if (freeToMove(xAxes, yAxes + 1)) yAxes++;
                        else hitWall = true;
                        break;
                }
                if (!hitWall)
                {
                    DrawMarker();
                    path.Push($"{xAxes},{yAxes}");
                    haveCheched.Add($"{xAxes},{yAxes}");
                    Thread.Sleep(50);
                }
            }
        }
        public void clearMarker()
        {
            Console.SetCursorPosition(xAxes * 2, yAxes);
            Console.Write("██");
        }
        public void DrawMarker()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xAxes * 2, yAxes);
            Console.Write("██");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public bool freeToMove(int x,int y)
        {
            return !haveCheched.Contains($"{x},{y}") && mazeData.Contains(x, y);
        }
    }
}

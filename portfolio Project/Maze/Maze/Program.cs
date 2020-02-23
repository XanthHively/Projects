using System;
using Maze.Classes;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            MazeGen maze = new MazeGen();
            MazeData mazeData = maze.Generate();

            MazeSolver mazeSolver = new MazeSolver(mazeData);
            mazeSolver.Solve();

            Console.ReadLine();
        }
    }
}

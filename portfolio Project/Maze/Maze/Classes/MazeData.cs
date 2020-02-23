using System;
using System.Collections.Generic;
using System.Text;

namespace Maze.Classes
{
    public class MazeData
    {
        List<string> maze = new List<string>();
        public string StartPosition { get; }
        public string EndPosition { get; set; }
        public MazeData()
        {
            StartPosition = "0,15";
        }
        public void AddMazeBlock(int x,int y)
        {
            maze.Add($"{x},{y}");
        }
        public bool Contains(int x, int y)
        {
            return maze.Contains($"{x},{y}");
        }
    }
}

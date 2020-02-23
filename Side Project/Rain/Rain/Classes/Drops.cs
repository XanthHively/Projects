using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Rain.Classes
{
    class Drops
    {
        int color;
        int length;
        int screenHeight;
        int height;
        int vertStart;
        public bool Active { get; private set; }
        public Drops()
        {
            var random = new Random();
            color = random.Next(1, 5);
            length = random.Next(5, 30);
            screenHeight = 30;
            height = 0;
            vertStart = random.Next(0,Console.BufferWidth);
            Active = true;
        }
        public void MoveDrop()
        {
            if (height < screenHeight)
            {
                Console.SetCursorPosition(vertStart, height);
                switch (color)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                }
                Console.Write("█");
            }
            if(height-length >= 0 && height-length < screenHeight)
            {
                Console.SetCursorPosition(vertStart, height-length);
                Console.Write(" ");
            }
            height++;
            if (height - length > screenHeight) Active = false;
        }
    }
}

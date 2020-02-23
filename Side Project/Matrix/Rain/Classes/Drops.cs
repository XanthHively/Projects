using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Rain.Classes
{
    class Drops
    {
        int length;
        int screenHeight;
        int height;
        int vertStart;
        int color;
        public bool Active { get; private set; }
        public Drops()
        {
            var random = new Random();
            length = random.Next(10, 30);
            screenHeight = 30;
            height = 0;
            vertStart = random.Next(0,Console.BufferWidth);
            Active = true;
            color = random.Next(1, 2);
        }
        public void MoveDrop()
        {
            if (height < screenHeight)
            {
                var random2 = new Random();
                Console.SetCursorPosition(vertStart, height);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(random2.Next(10,99));
            }
            if(height-length >= 0 && height-length < screenHeight)
            {
                Console.SetCursorPosition(vertStart, height-length);
                Console.Write("  ");
            }
            height++;
            if (height - length > screenHeight) Active = false;
        }
    }
}

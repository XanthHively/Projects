using System;
using System.Collections.Generic;
using System.Text;

namespace MoveOnGrid.Classes
{
    class BadBoy
    {
        int sizeOfBoard;
        public int YAxis { get; private set; }
        public int XAxis { get; private set; }
        public int Health { get; private set; }

        public BadBoy(int size)
        {
            sizeOfBoard = size;
            YAxis = sizeOfBoard-2;
            XAxis = sizeOfBoard-2;
            Health = 3;
        }
        public string MoveIn(int playerX,int playerY)
        {
            int xDistance, yDistance;
            if (playerX >= XAxis) xDistance = playerX - XAxis;
            else xDistance = XAxis - playerX;
            if (playerY >= YAxis) yDistance = playerY - YAxis;
            else yDistance = YAxis - playerY;

            Console.ForegroundColor = ConsoleColor.Red;
            if (xDistance >= yDistance)
            {
                if (playerX >= XAxis)
                {
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(" ");
                    XAxis++;
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(Health);
                }
                else
                {
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(" ");
                    XAxis--;
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(Health);
                }
            }
            else
            {
                if (playerY > YAxis)
                {
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(" ");
                    YAxis++;
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(Health);
                }
                else
                {
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(" ");
                    YAxis--;
                    Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                    Console.Write(Health);
                }
            }
            Console.SetCursorPosition(8, sizeOfBoard + 4);
            return XAxis.ToString() + YAxis.ToString();
        }
    }
}

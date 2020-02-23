using System;
using System.Collections.Generic;
using System.Text;

namespace MoveOnGrid.Classes
{
    public class Player
    {
        int sizeOfBoard;
        public int YAxis { get; private set; }
        public int XAxis { get; private set; }

        public Player(int size)
        {
            sizeOfBoard = size;
            YAxis = 1;
            XAxis = 1;
        }

        public string Move(char playerDirection, Dictionary<string, string> locations)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string ret = XAxis.ToString() + YAxis.ToString();
            if (playerDirection == 'w' && YAxis > 0)
            {
                if (XAxis.ToString() + (YAxis - 1) == locations["badBoy"]) ret = "attackUp";
                else MoveUp();
            }
            else if (playerDirection == 'a' && XAxis > 0)
            {
                if ((XAxis - 1) + YAxis.ToString() == locations["badBoy"]) ret = "attackLeft";
                else MoveLeft();
            }
            else if (playerDirection == 's' && YAxis < sizeOfBoard - 1)
            {
                if (XAxis.ToString() + (YAxis + 1) == locations["badBoy"]) ret = "attackDown";
                else MoveDown();
            }
            else if (playerDirection == 'd' && XAxis < sizeOfBoard - 1)
            {
                if ((XAxis + 1) + YAxis.ToString() == locations["badBoy"]) ret = "attackRight";
                else MoveRight();
            }
            Console.SetCursorPosition(8, sizeOfBoard + 4);
            return ret;
        }
        public void MoveUp()
        {
            if (YAxis > 0)
            {
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write(" ");
                YAxis--;
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write("@");
            }
        }
        public void MoveLeft()
        {
            if(XAxis > 0)
            {
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write(" ");
                XAxis--;
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write("@");
            }
        }
        public void MoveDown()
        {
            if(YAxis < sizeOfBoard - 1)
            {
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write(" ");
                YAxis++;
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write("@");
            }
        }
        public void MoveRight()
        {
            if(XAxis < sizeOfBoard - 1)
            {
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write(" ");
                XAxis++;
                Console.SetCursorPosition((XAxis * 2) + 10, YAxis + 2);
                Console.Write("@");
            }
        }
    }
}

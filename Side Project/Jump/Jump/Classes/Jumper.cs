using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Jump.Classes
{
    public class Jumper
    {
        int YAxis;
        int XAxis;
        public Jumper()
        {
            Console.SetCursorPosition(10, 28);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("█");

            YAxis = 28;
            XAxis = 10;
            Momentum = 100;
            onGround = true;
            jumpDirection = 'l';
        }
        public void Move(char direction)
        {
            switch (direction)
            {
                case 'a':
                    jumpDirection = 'a';
                    MoveLeft();
                    break;
                case 'd':
                    jumpDirection = 'd';
                    MoveRight();
                    break;
                case 'w':
                    if (onGround)
                    {
                        Momentum = 199;
                        onGround = false;
                        MoveUp();
                        Jump();
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine(" ");
                    }
                    break;
            }
            Console.SetCursorPosition(0, 0);
        }
        int Momentum;
        bool onGround;
        public char jumpDirection { get; set; }
        public void Jump()
        {
            while (YAxis < 28)
            {
                if (jumpDirection == 'a' && Momentum % 6 == 0) MoveLeft();
                else if (jumpDirection == 'd' && Momentum % 6 == 0) MoveRight();

                if (Momentum > 150)
                {
                    if (Momentum % 6 == 0) MoveUp();
                    Momentum--;
                }
                else if (Momentum > 100)
                {
                    if (Momentum % 11 == 0) MoveUp();
                    Momentum--;
                }
                else if (Momentum > 50)
                {
                    if (Momentum % 11 == 0) MoveDown();
                    Momentum--;
                }
                else if (Momentum >= 0)
                {
                    if (Momentum % 6 == 0) MoveDown();
                    Momentum--;
                }
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(6);
            }
            onGround = true;
        }
        public void MoveUp()
        {
            if (XAxis > 0)
            {
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write(" ");
                YAxis--;
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write("█");
            }
        }
        public void MoveDown()
        {
            if (XAxis > 0)
            {
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write(" ");
                YAxis++;
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write("█");
            }
        }
        public void MoveLeft()
        {
            if(XAxis > 0)
            {
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write(" ");
                XAxis--;
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write("█");
            }
            Thread.Sleep(6);
        }
        public void MoveRight()
        {
            if (XAxis < 119)
            {
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write(" ");
                XAxis++;
                Console.SetCursorPosition(XAxis, YAxis);
                Console.Write("█");
            }
            Thread.Sleep(4);
        }
    }
}

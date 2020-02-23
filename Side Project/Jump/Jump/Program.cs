using System;
using System.Threading;
using Jump.Classes;

namespace Jump
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 29);
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 70; i++)
            {
                Console.Write("██");
            }
            Console.SetCursorPosition(0, 0);

            Jumper boy = new Jumper();
            int jump = 0;
            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    if (jump == 20)
                    {
                        boy.jumpDirection = 'l';
                        jump = 0;
                    }
                    else jump++;
                    Thread.Sleep(10);
                }
                jump = 0;
                boy.Move(Console.ReadKey().KeyChar);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using MoveOnGrid.Classes;
using System.Threading;

namespace MoveOnGrid
{
    class Program
    {
        static void Main(string[] args)
        {
            //set values
            int sizeOfBoard = 15;
            Player player = new Player(sizeOfBoard);
            BadBoy badBoy = new BadBoy(sizeOfBoard);
            Dictionary<string, string> locations = new Dictionary<string, string>();
            locations["badBoy"] = "";

            //populate board
            buildBoard(sizeOfBoard);
            Console.SetCursorPosition(12, 3);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("@");
            Console.SetCursorPosition(sizeOfBoard*2+6, sizeOfBoard);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(badBoy.Health);
            Console.SetCursorPosition(8, sizeOfBoard + 4);

            //loop movements
            for (int i = 0; i < 100; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                locations["player"] = player.Move(Console.ReadKey().KeyChar, locations);
                Thread.Sleep(500);
                locations["badBoy"] = badBoy.MoveIn(player.XAxis, player.YAxis);
            }

            static void buildBoard(int size)
            {
                Console.SetCursorPosition(8, 1);
                Console.Write("╔═");
                for (int i = 0; i < size; i++)
                {
                    Console.Write("══");
                }
                Console.Write("╗");
                for(int i = 0; i < size; i++)
                {
                    Console.SetCursorPosition(8, i + 2);
                    Console.Write("║");
                    Console.SetCursorPosition((size * 2) + 10, i + 2);
                    Console.Write("║");
                }
                Console.SetCursorPosition(8, size + 2);
                Console.Write("╚═");
                for (int i = 0; i < size; i++)
                {
                    Console.Write("══");
                }
                Console.Write("╝");
            }
        }
    }
}

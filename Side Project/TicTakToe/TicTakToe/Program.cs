using System;
using System.Collections.Generic;

namespace TicTakToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> board = new Dictionary<string, string>()
            {
                {"A1"," "},{"B1"," "},{"C1"," "},
                {"A2"," "},{"B2"," "},{"C2"," "},
                {"A3"," "},{"B3"," "},{"C3"," "}
            };

            buildBoard(board);

            bool gameDone = false;
            string player = "X";
            while (!gameDone)
            {

                Console.Write($"Enter coordinates player {player}: ");
                string coordinate = Console.ReadLine().ToUpper();
                while (!board.ContainsKey(coordinate) || board[coordinate] == "O" || board[coordinate] == "X")
                {
                    Console.Write("Invalid Location: ");
                    coordinate = Console.ReadLine().ToUpper();
                }
                board[coordinate] = player;
                if (player == "X") player = "O";
                else player = "X";

                
                Console.Clear();
                buildBoard(board);
                gameDone = isWinner(board);
            }

            Winner(player);

            static void buildBoard(Dictionary<string, string> board)
            {
                Console.WriteLine("    A     B     C");
                Console.WriteLine("       #     #");
                Console.WriteLine($" 1  {board["A1"]}  #  {board["B1"]}  #  {board["C1"]}");
                Console.WriteLine("       #     #");
                Console.WriteLine("   ###############");
                Console.WriteLine("       #     #");
                Console.WriteLine($" 2  {board["A2"]}  #  {board["B2"]}  #  {board["C2"]}");
                Console.WriteLine("       #     #");
                Console.WriteLine("   ###############");
                Console.WriteLine("       #     #");
                Console.WriteLine($" 3  {board["A3"]}  #  {board["B3"]}  #  {board["C3"]}");
                Console.WriteLine("       #     #");
                return;
            }

            static bool isWinner(Dictionary<string, string> board)
            {
                bool winner = false;
                //verticle
                if (board["A1"] == board["B1"] && board["B1"] == board["C1"] && board["A1"] != " ") winner = true;
                else if (board["A2"] == board["B2"] && board["B2"] == board["C2"] && board["A2"] != " ") winner = true;
                else if (board["A3"] == board["B3"] && board["B3"] == board["C3"] && board["A3"] != " ") winner = true;
                //horizontal
                else if (board["A1"] == board["A2"] && board["A2"] == board["A3"] && board["A1"] != " ") winner = true;
                else if (board["B1"] == board["B2"] && board["B2"] == board["B3"] && board["B1"] != " ") winner = true;
                else if (board["C1"] == board["C2"] && board["C2"] == board["C3"] && board["C1"] != " ") winner = true;
                //cross
                else if (board["A1"] == board["B2"] && board["B2"] == board["C3"] && board["A1"] != " ") winner = true;
                else if (board["C1"] == board["B2"] && board["B2"] == board["A3"] && board["C1"] != " ") winner = true;
                return winner;
            }

            static void Winner(string player)
            {
                Console.WriteLine("                    __ooooooooo__");
                Console.WriteLine("                 oOOOOOOOOOOOOOOOOOOOOOo");
                Console.WriteLine("             oOOOOOOOOOOOOOOOOOOOOOOOOOOOOOo");
                Console.WriteLine("          oOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOo");
                Console.WriteLine("        oOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOo");
                Console.WriteLine("      oOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOo");
                Console.WriteLine("     oOOOOOOOOOOO * *OOOOOOOOOOOOOO * *OOOOOOOOOOOOo");
                Console.WriteLine("    oOOOOOOOOOOO      OOOOOOOOOOOO      OOOOOOOOOOOOo");
                Console.WriteLine("    oOOOOOOOOOOOOo  oOOOOOOOOOOOOOOo  oOOOOOOOOOOOOOo");
                Console.WriteLine("   oOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOo");
                Console.WriteLine("   oOOOO     OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO     OOOOo");
                Console.WriteLine("   oOOOOOO OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO OOOOOOo");
                Console.WriteLine("   * OOOOO  OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO  OOOOO *");
                Console.WriteLine("   * OOOOOO * OOOOOOOOOOOOOOOOOOOOOOOOOOOOO * OOOOOO *");
                Console.WriteLine("    * OOOOOO * OOOOOOOOOOOOOOOOOOOOOOOOOOO * OOOOOO *");
                Console.WriteLine("     * OOOOOOo * OOOOOOOOOOOOOOOOOOOOOOO * oOOOOOO *");
                Console.WriteLine("       * OOOOOOOo * OOOOOOOOOOOOOOOOO * oOOOOOOO *");
                Console.WriteLine("         * OOOOOOOOo * OOOOOOOOOOO * oOOOOOOOO * ");
                Console.WriteLine("            * OOOOOOOOo           oOOOOOOOO * ");
                Console.WriteLine("                * OOOOOOOOOOOOOOOOOOOOO * ");
                Console.WriteLine("                     \"\"ooooooooo\"\"");
                Console.WriteLine("");
                Console.WriteLine($"                PLAYER {player} IS A WINNER!!!!");

            }
        }
    }
}

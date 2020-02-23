using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Snakes.Classes;

namespace Snakes
{
    class Program
    {
        static void Main(string[] args)
        {
            bool newGame = true;
            do//restart game loop
            {
                //menu setup
                string difficulty = "Easy";
                Dictionary<string, int> diffparameters = setdifficultydata(difficulty);
                List<string> easyLocation = new List<string>() { "22,12", "23,12" };
                List<string> MediumLocation = new List<string>() { "26,12", "27,12", "28,12" };
                List<string> HardLocation = new List<string>() { "31,12", "32,12" };
                Snake snake;
                bool closeMenu = false;
                //MENU
                while (!closeMenu)
                {
                    Console.SetCursorPosition(48, 14);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Easy    ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Medium    ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Hard");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(52, 0);
                    Console.Write("Move with W-A-S-D");
                    buildBoard(diffparameters);
                    snake = new Snake(diffparameters);
                    while (snake.Alive)
                    {
                        do
                        {
                            snake.Move();
                            Thread.Sleep(100);
                            string diffSelect = snake.getSnakeHeadLocations();
                            if (easyLocation.Contains(diffSelect))
                            {
                                difficulty = "Easy";
                                closeMenu = true;
                            }
                            if (MediumLocation.Contains(diffSelect))
                            {
                                difficulty = "Medium";
                                closeMenu = true;
                            }
                            if (HardLocation.Contains(diffSelect))
                            {
                                difficulty = "Hard";
                                closeMenu = true;
                            }
                            if (!snake.Alive || closeMenu) break;
                        }
                        while (!Console.KeyAvailable);
                        if (!snake.Alive || closeMenu) break;
                        snake.UpdateDirection(Console.ReadKey().KeyChar);
                    }
                    Console.Clear();
                }

                //game setup
                diffparameters = setdifficultydata(difficulty);
                snake = new Snake(diffparameters);
                buildBoard(diffparameters);
                double speed = diffparameters["speed"];
                int score = -1;
                //START GAME!
                while (snake.Alive)
                {
                    do
                    {
                        snake.Move();
                        Thread.Sleep((int)speed);
                        if (!snake.Alive) break;
                        if (snake.FoodPosition == "")
                        {
                            snake.FoodPosition = spawnFood(snake.getSnakeLocations(), diffparameters);
                            if (snake.FoodPosition != "")
                            {
                                score++;
                                speed -= 0.2;
                                updateScore(diffparameters, score);
                            }
                        }
                    }
                    while (!Console.KeyAvailable);
                    if (!snake.Alive) break;
                    snake.UpdateDirection(Console.ReadKey().KeyChar);
                }
                snake.DeathAnimation();
                //High Score
                Console.SetCursorPosition(49, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Press ENTER to continue");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 1);
                Console.ReadLine();//wait for input
                Console.Clear();
                diffparameters = setdifficultydata("Easy");
                buildBoard(diffparameters);
                highScore(score, difficulty);

                //do we want to restart the game?
                Console.SetCursorPosition(49, 0);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Start New Game? (Y) (N)");
                Console.ForegroundColor = ConsoleColor.White;
                bool wait = true;
                while (wait)
                {
                    Console.SetCursorPosition(0, 1);
                    char newGameImput = Console.ReadKey().KeyChar;
                    if (newGameImput == 'y')
                    {
                        newGame = true;
                        wait = false;
                    }
                    else if (newGameImput == 'n')
                    {
                        newGame = false;
                        wait = false;
                    }
                }
                Console.Clear();
            }
            while (newGame);



            static void highScore(int score, string diff)
            {
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
                string fullPath = di + $@"\High Scores\{diff}.txt";
                List<int> highScoreList = new List<int>();
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        highScoreList.Add(int.Parse(sr.ReadLine()));
                    }
                }
                highScoreList.Sort();
                if (highScoreList[0] < score)
                {
                    highScoreList.Add(score);
                    highScoreList.Sort();
                    highScoreList.Reverse();
                    using (StreamWriter sw = new StreamWriter(fullPath, false))
                    {
                        for(int i = 0; i < 9; i++)
                        {
                            sw.WriteLine(highScoreList[i]);
                        }
                    }
                }
                else highScoreList.Reverse();
                Console.SetCursorPosition(52, 5);
                Console.Write("HIGH SCORES");
                for (int i = 0; i < 9; i++)
                {
                    Console.SetCursorPosition(53, i+6);
                    Console.Write($"{i+1}) {highScoreList[i]}");
                }
            }

            static string spawnFood(Queue<string> snakeLocations,Dictionary<string,int> diff)
            {
                var random = new Random();
                int xFood = random.Next(0, diff["width"]-1);
                int yFood = random.Next(0, diff["height"]-1);
                string foodPosition = xFood.ToString() + "," + yFood.ToString();
                if (!snakeLocations.Contains(foodPosition))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition((xFood * 2) + diff["xStart"] + 2, yFood + diff["yStart"] + 1);
                    Console.Write("[]");
                    Console.SetCursorPosition(0, 1);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else foodPosition = "";
                return foodPosition;
            }

            static Dictionary<string, int> setdifficultydata(string difficulty)
            {
                Dictionary<string, int> diffperamiters = new Dictionary<string, int>();
                if (difficulty == "Easy")
                {
                    diffperamiters["width"] = 56;//size of board
                    diffperamiters["height"] = 26;
                    diffperamiters["xStart"] = 2;// top left corner
                    diffperamiters["yStart"] = 1;
                    diffperamiters["speed"] = 120;
                }
                else if (difficulty == "Medium")
                {
                    diffperamiters["width"] = 40;
                    diffperamiters["height"] = 18;
                    diffperamiters["xStart"] = 18;
                    diffperamiters["yStart"] = 5;
                    diffperamiters["speed"] = 110;
                }
                else if (difficulty == "Hard")
                {
                    diffperamiters["width"] = 28;
                    diffperamiters["height"] = 14;
                    diffperamiters["xStart"] = 30;
                    diffperamiters["yStart"] = 7;
                    diffperamiters["speed"] = 100;
                }
                return diffperamiters;
            }

            static void buildBoard(Dictionary<string,int> diff)// 1,1 is (6,3)
            {
                Console.SetCursorPosition(diff["xStart"], diff["yStart"]);
                Console.Write("╔═");
                for (int i = 0; i < diff["width"]; i++)
                {
                    Console.Write("══");
                }
                Console.Write("═╗");
                for (int i = 0; i < diff["height"]; i++)
                {
                    Console.SetCursorPosition(diff["xStart"], i + diff["yStart"] + 1);
                    Console.Write("║");
                    Console.SetCursorPosition((diff["width"] * 2) + diff["xStart"] + 3, i + diff["yStart"] + 1);
                    Console.Write("║");
                }
                Console.SetCursorPosition(diff["xStart"], diff["height"] + diff["yStart"] + 1);
                Console.Write("╚═");
                for (int i = 0; i < diff["width"]; i++)
                {
                    Console.Write("══");
                }
                Console.Write("═╝");
            }
            static void updateScore(Dictionary<string, int> diff,int score)
            {
                Console.SetCursorPosition(diff["xStart"], diff["yStart"] - 1);
                Console.Write($"Score: {score}     ");
                Console.SetCursorPosition(0, 1);
            }
        }
    }
}

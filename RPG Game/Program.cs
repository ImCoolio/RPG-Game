using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Console;
using static RPG_Game.MapGeneration;
using static RPG_Game.Player;

namespace RPG_Game
{
    internal class GameHandler
    {
        static void Main(String[] args)
        {
            Title = "The RPG";
            ForegroundColor = ConsoleColor.Yellow;
            WindowHeight = 30;
            WindowWidth = 180;
            int choice = 0;
            Boolean closeGame = false;

            WriteLine("TTTTTTTT  GGGG  LL         RRRRR  PPPPP   GGGG ");
            WriteLine("   TT    G      LL         R    R P    P G    ");
            WriteLine("   TT    G   GG LL         RRRR   PPPPP  G   GG");
            WriteLine("   TT    G    G LL         RR R   PP     G    G");
            WriteLine("   TT     GGGG  LLLLLL     RR  RR PP      GGGG \n\n");
            WriteLine("Welcome to \"The Graceful Legend RPG.\"\n");


            while (!closeGame)
            {
                WriteLine("1 - Play");
                WriteLine("2 - Information");
                WriteLine("3 - Exit");
                choice = int.Parse(ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            String[] names = CreatePlayerPrompt();
                            Player player = new Player(names[0], names[1]);

                            WriteLine(player.ToString() + "\n");
                            WriteLine("Player created!");
                            Thread.Sleep(1000);

                            WriteLine("Generating world...");
                            CreateMap();
                            Clear();

                            WriteLine("Map created! Beginning game...");

                            ReadLine();
                            break;
                        case 2:
                            WriteLine("\nINFORMATION\nThe Graceful Legend RPG is a C# created by coolius. In this game, the worlds are randomly generated and so are the enemies.");
                            WriteLine("The 4 stats are HP, Strength, Magic and Speed. You can gain magic moves and heal at churches.");
                            WriteLine("At towns, you can purchase new items (NOT YET IMPLEMENTED) and heal. As well, you can get quests (NOT YET IMPLEMENTED).");
                            WriteLine("The 3 classes are Warrior (Primarily HP/Strength), Mage (Primary Magic) and Rogue (Primary Speed & Balanced Strength/Magic).\n");
                            break;
                        case 3:
                            closeGame = true;
                            break;
                        default:
                            WriteLine("Invalid option, please reselect an option.");
                            break;
                    }
                }
                catch (Exception)
                {
                    WriteLine("Invalid option, please reselect an option.");
                }
            }
        }
    }
}

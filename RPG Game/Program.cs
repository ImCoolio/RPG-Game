using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using static System.Console;
using static RPG_Game.MapGeneration;
using static RPG_Game.Player;

namespace RPG_Game
{
    internal class GameHandler
    {
        static StreamWriter sw;
        static StreamReader sr;
        static void Main(String[] args)
        {
            Title = "The Graceful Legend RPG";
            ForegroundColor = ConsoleColor.Yellow;
            WindowHeight = 40;
            WindowWidth = 200;
            int choice = 0;
            Boolean closeGame = false;

            while (!closeGame)
            { 
                WriteLine("TTTTTTTT  GGGG  LL         RRRRR  PPPPP   GGGG ");
                WriteLine("   TT    G      LL         R    R P    P G    ");
                WriteLine("   TT    G   GG LL         RRRR   PPPPP  G   GG");
                WriteLine("   TT    G    G LL         R R    P      G    G");
                WriteLine("   TT     GGGG  LLLLLL     R  RR  P       GGGG \n\n");

                WriteLine("Welcome to \"The Graceful Legend RPG.\"\n");
                WriteLine("1 - Play");
                Write("2 - Continue Previous Save "); SaveFileCount();
                WriteLine("3 - Information");
                WriteLine("4 - Exit");
                while (true) {
                    try
                    {
                        choice = int.Parse(ReadLine());
                        break;
                    }
                    catch (Exception)
                    {
                        Clear();
                        break;
                    }
                }
                switch (choice)
                {
                    case 1:
                        String[] names = CreatePlayerPrompt();
                        Player player = new Player(names[0], names[1]);
                        
                        player.GetStats();
                        WriteLine("Player created!");
                        Thread.Sleep(2000);

                        //Map Generation

                        WriteLine("Generating world...");
                        CreateMap();
                        Clear();

                        WriteLine("Map created! Beginning game...");
                        Thread.Sleep(2000);

                        Clear();
                        //Beginning of game
                        while (true)
                        {
                            int[] currentLocation = player.getLocation();
                            Write("You are currently at (" + currentLocation[0] + ", " + currentLocation[1] + "). ");
                            player.getLocationType();

                            Write("\nChoose your next option: ");

                            String decision = ReadLine(); decision = decision.ToLower();

                            switch (decision)
                            {
                                case ("w"): case ("s"): case ("a"): case ("d"):
                                    Clear();
                                    player.move(decision);
                                    break;

                                case ("e"):
                                    while (!enemies[player.GetX(), player.GetY()].Dead)
                                    {
                                        player.Attack(enemies[player.GetX(), player.GetY()], player);
                                    }

                                    Clear();
                                    break;

                                case ("x"):
                                    Clear(); player.GetStats();
                                    break;

                                case ("z"):
                                    Clear();
                                    SaveGameFile(player);
                                    WriteLine("Thanks for playing The Graceful Legend RPG.");
                                    Thread.Sleep(1000);
                                    closeGame = true;
                                    break;

                                default:
                                    Clear();
                                    WriteLine("Invalid choice, please retry.\n");
                                    break;
                            }

                            if (closeGame == true)
                            {
                                break;
                            }
                        }
                            break;
                        case 2:
                            /*LoadSaveFile();*/
                            break;
                        case 3:
                            WriteLine("\nINFORMATION\nThe Graceful Legend RPG is a C# created by coolius. In this game, the worlds are randomly generated and so are the enemies.");
                            WriteLine("The 4 stats are HP, Strength, Magic and MP. You can gain magic moves and heal at churches. (NOT YET IMPLEMENTED)");
                            WriteLine("At towns, you can purchase new items (NOT YET IMPLEMENTED) and heal. As well, you can get quests (NOT YET IMPLEMENTED).");
                            WriteLine("The 3 classes are Warrior (Primarily HP/Strength), Mage (Primary Magic/MP) (Currently only has a strength attack. Implementing magic next.) and Rogue (Balanced Strength/Magic).\n");
                            break;
                        case 4:
                            closeGame = true;
                            break;
                    default:
                        Clear();
                            WriteLine("Invalid option, please reselect an option.\n");
                            break;
                }
            }
        }
        public static void SaveFileCount()
        {
            try
            {
                int SaveCount = 0;
                string fileName = @".\CharacterSaves.txt";
                String line;
                if (!File.Exists(fileName))
                {
                    sw = File.CreateText(fileName);
                    WriteLine("(No previous characters detected.)");
                    sw.Close();
                }
                else
                {
                    try
                    {
                        StreamReader sr = new StreamReader(fileName);
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            line = sr.ReadLine();
                                SaveCount++;
                            sr.Close();
                            WriteLine("(" + SaveCount + " saved characters detected!)");
                        }
                        else
                        {
                            WriteLine("(No previous characters detected.)");
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void SaveGameFile(Player player)
        {
            string fileName = @".\CharacterSaves.txt";
            FileInfo fInfo = new FileInfo(fileName);
            fInfo.IsReadOnly = false;
            sw = new StreamWriter(fileName, true);
            try
            {
                    sw.WriteLine(player.GetPlayerData());
                    sw.Close();
                    sr = new StreamReader(fileName);
                    sr.Close();
                    fInfo.IsReadOnly = true;
                    File.SetAttributes(fileName, File.GetAttributes(fileName) | FileAttributes.Hidden);
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
    }
}

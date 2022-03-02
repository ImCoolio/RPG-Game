using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Windows.Forms;
using static RPG_Game.dialogBox;
using static RPG_Game.MapGeneration;

namespace RPG_Game
{
    // Stat Order, [Max HP, Strength, Magic, Speed]
    internal class Player
    {
        static String name;
        String finishedName;
        static String classSelection;
        int level;
        int xp;
        int gold;
        String playerClass;
        int[] stats;
        int X;
        int Y;
        int maxhp;
        int maxmp;
        int str;
        int mag;

        Weapon shortsword = new Weapon("Shortsword", 20, 5);
        Weapon staff = new Weapon("Staff", 5, 20);
        Weapon dagger = new Weapon("Dagger", 12, 7);
        Weapon playerWeapon;

        public Player(String name, String playerClass)
        {
            this.finishedName = name;
            this.level = 1;
            this.xp = 0;
            this.gold = 100;
            this.playerClass = playerClass;
            this.X = 5;
            this.Y = 5;

            switch (playerClass)
            {
                case "Warrior":
                    playerWeapon = shortsword;
                    str = 20;
                    mag = 5;
                    maxhp = 50 + (int) (str * 1.4);
                    maxmp = 10 + (mag * 2);
                    this.stats = new int[] { maxhp, str, mag, maxmp };
                    break;
                case "Mage":
                    mag = 20;
                    str = 7;
                    maxhp = 20 + (int) (str * 1.4);
                    maxmp = 40 + (mag * 2);
                    playerWeapon = staff;
                    this.stats = new int[] { maxhp, str, mag, maxmp };
                    break;
                case "Rogue":
                    str = 14;
                    mag = 8;
                    maxhp = 50 + (int) (str * 1.4);
                    maxmp = 15 + (mag * 2);
                    playerWeapon = dagger;
                    this.stats = new int[] { maxhp, str, mag, maxmp };
                    break;
            }
        }

        public static String[] CreatePlayerPrompt()
        {
            while (true)
            {
                Console.Write("Please input the name you would like for your character: ");
                name = ReadLine();
                name = name.Trim();
                try
                {
                    if (name.Length == 0 || name == null)
                    {
                        throw new Exception();
                    }
                    var nameDecision = CreateQuestionBox("Is your name " + name + "?", "Name Verification");
                    if (nameDecision == DialogResult.Yes)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    WriteLine("\nInvalid name, please re-enter your name correctly.\n");
                }
            }

            while (true)
            {
                Write("\nPlease input your class choice (1: Warrior, 2: Mage, 3: Rogue) or input the number then \", info\" for the info on that class: ");
                String choice = ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            classSelection = "Warrior";
                            break;
                        case "2":
                            classSelection = "Mage";
                            break;
                        case "3":
                            classSelection = "Rogue";
                            break;
                        case "1, info":
                            Clear();
                            WriteLine("The Warrior class is a primarily melee build, with very little use of magic moves. It has a high base strength and health stat, with low speed and magic.\n");
                            break;
                        case "2, info":
                            Clear();
                            WriteLine("The Mage class is a primarily magic build, with little usage of melee attacks. It has a high base magic with mediocre speed. It has low strength and health.\n");
                            break;
                        case "3, info":
                            Clear();
                            WriteLine("The Rogue class is a primarily melee build, with a balanced usage of melee and magic attacks. It has a high base speed with mediocre health. It has low base strength and magic stats.\n");
                            break;
                        default:
                            throw new Exception();
                    }
                    var nameDecision = CreateQuestionBox("Are you sure you want to be a " + classSelection + "?", "Class Verification");
                    if (nameDecision == DialogResult.Yes)
                    {
                        Clear();
                        break;
                    }
                }
                catch (Exception)
                {
                    WriteLine("\nInvalid class choice.\n");
                }
            }

            WriteLine("Name selected: " + name + "...");
            WriteLine("Class Selected: " + classSelection + "...\n");
            String[] playerDetails = { name, classSelection };
            return playerDetails;
        }

        public void move(String decision)
        {
            if (decision == "w") {
                if (Y != 10)
                    Y++;
                else
                    WriteLine("You have reached the northern border of the island.");
            }
            if (decision == "s")
            {
                if (Y != 0)
                    Y--;
                else
                    WriteLine("You have reached the southern border of the island.");
            }
            if (decision == "a")
            {
                if (X != 0)
                    X--;
                else
                    WriteLine("You have reached the western border of the island.");
            }
            if (decision == "d")
            {
                if (X != 10)
                    X++;
                else
                    WriteLine("\nYou have reached the eastern border of the island.\n");
            }
        }

        public int[] getLocation()
        {
            int[] location = { X - 5, Y - 5 };
            return location;
        }

        public void getLocationType()
        {
            if (mapSize[X, Y] == 1)
            {
                WriteLine("You see a town.\n");
                WriteLine("W/S/A/D to Move (W = Up, A = Left, S = Down, D = Right");
                WriteLine("Q - Enter Shop (NOT YET IMPLEMENTED)");
                WriteLine("H - Heal at the Inn | 5 Gold\n");
                WriteLine("Z - Quit Game");
            }

            if (mapSize[X, Y] == 2)
            {
                WriteLine("You are in a cave.\n");
                WriteLine("W/S/A/D to Move (W = Up, A = Left, S = Down, D = Right");
                WriteLine("E to Fight " + EnemyGeneration());
                WriteLine("Z - Quit Game");
            }

            if (mapSize[X, Y] == 3)
            {
                WriteLine("You see a church.\n");
                WriteLine("W/S/A/D to Move (W = Up, A = Left, S = Down, D = Right");
                WriteLine("Q - Speak to Priest");
                WriteLine("H - Heal at the Church | 3 Gold\n");
                WriteLine("Z - Quit Game");
            }

            if (mapSize[X, Y] == 4)
            {
                WriteLine("You see grass and many trees, but not a soul in sight.\n");
                WriteLine("W/S/A/D to Move (W = Up, A = Left, S = Down, D = Right");
                WriteLine("E - Search Around");
                WriteLine("Z - Quit Game");
            }

        }

        public string GetStats()
        {
           WriteLine("\n" + name + "'s information");

           return "name= " + name + ", level= " + level + ", xp= " + xp + ", gold=" + gold + ", playerClass= " + playerClass + ", currentWeapon= " + playerWeapon.ToString() + ", stats: HP " + stats[0] + ", STR " + stats[1] + ", Magic " + stats[2] + ", Max MP " + stats[3] + "]";
        }

        public void GainXP()
        {

        }
    }
}

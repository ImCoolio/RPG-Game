using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

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
            

            switch (playerClass)
            {
                case "Warrior":
                    playerWeapon = shortsword;
                    this.stats = new int[] { 50, 20, 5, 7 };
                    break;
                case "Mage":
                    playerWeapon = staff;
                    this.stats = new int[] { 25, 5, 20, 10 };
                    break;
                case "Rogue":
                    playerWeapon = dagger; 
                    this.stats = new int[] { 35, 14, 8, 15 };
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
                    break;
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
                    Clear();
                    break;
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

        override
        public String ToString()
        {
            return "Player [name= " + name + ", level= " + level + ", xp= " + xp + ", gold=" + gold + ", playerClass= " + playerClass + ", playerWeapon= " + playerWeapon.ToString() + ", stats: HP " + stats[0] + ", STR " + stats[1] + ", Magic " + stats[2] + ", SPD " + stats[3] + "]";
        }
    }
}

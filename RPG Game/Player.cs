using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using static System.Console;
using System.Windows.Forms;
using static RPG_Game.dialogBox;
using static RPG_Game.MapGeneration;

namespace RPG_Game
{
    // Stat Order, [Max HP, Strength, Magic, Speed]
    public class Player
    {
        String movePrompt = "W/S/A/D to Move (W = Up, A = Left, S = Down, D = Right)";
        String quitPrompt = "Z - Quit Game";
        String statPrompt = "X - Character Information";

        static String name;
        String finishedName;
        static String classSelection;
        int level;
        int xp;
        int gold;
        String playerClass;
        int[] stats;
        int X = 10;
        int Y = 10;
        int maxhp;
        int maxmp;
        int str;
        int mag;
        int hp;
        int mp;
        public Boolean run = false;
        public Boolean dead = false;
        int defending;

        Weapon shortsword = new Weapon("Shortsword", 15, 5);
        Weapon staff = new Weapon("Staff", 5, 15);
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
                    str = 15;
                    mag = 7;
                    maxhp = 50 + (int) (str * 1.4);
                    maxmp = 10 + (int) (mag * 1.75);
                    hp = maxhp;
                    mp = maxmp;
                    stats = new int[] { maxhp, str, mag, maxmp };
                    break;
                case "Mage":
                    mag = 15;
                    str = 7;
                    maxhp = 25 + (int) (str * 1.4);
                    maxmp = 40 + (int) (mag * 1.75);
                    playerWeapon = staff;
                    hp = maxhp;
                    mp = maxmp;
                    stats = new int[] { maxhp, str, mag, maxmp };
                    break;
                case "Rogue":
                    str = 14;
                    mag = 8;
                    maxhp = 35 + (int) (str * 1.4);
                    maxmp = 20 + (int) (mag * 1/75);
                    playerWeapon = dagger;
                    hp = maxhp;
                    mp = maxmp;
                    stats = new int[] { maxhp, str, mag, maxmp };
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
                            WriteLine("The Warrior class is a primarily melee build, with very little use of magic moves. It has a high base strength and health stat, with low speed and magic.");
                            break;
                        case "2, info":
                            Clear();
                            WriteLine("The Mage class is a primarily magic build, with little usage of melee attacks. It has a high base magic with mediocre speed. It has low strength and health.");
                            break;
                        case "3, info":
                            Clear();
                            WriteLine("The Rogue class is a primarily melee build, with a balanced usage of melee and magic attacks. It has a high base speed with mediocre health. It has low base strength and magic stats.");
                            break;
                        default:
                            throw new Exception();
                    }

                    if (choice == "1" || choice == "2" || choice == "3")
                    {
                        var nameDecision = CreateQuestionBox("Are you sure you want to be a " + classSelection + "?", "Class Verification");
                        if (nameDecision == DialogResult.Yes)
                        {
                            Clear();
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    WriteLine("\nInvalid class choice.");
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
                if (Y != 20)
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
                if (X != 20)
                    X++;
                else
                    WriteLine("\nYou have reached the eastern border of the island.\n");
            }
        }

        public int[] getLocation()
        {
            int[] location = { X - 10, Y - 10 };
            return location;
        }

        public int getLocationType()
        {
            if (mapSize[X, Y] == 1)
            {
                WriteLine("You see a town.\n");
                WriteLine(movePrompt);
                WriteLine("Q - Enter Shop (NOT YET IMPLEMENTED)");
                WriteLine("F - Heal at the Inn | 5 Gold (NOT YET IMPLEMENTED)\n");
                WriteLine(statPrompt);
                WriteLine(quitPrompt);
                return 1;
            }

            if (mapSize[X, Y] == 2)
            {
                WriteLine("You are in a cave.\n");
                WriteLine(movePrompt);
                if (enemies[X, Y] != null)
                {
                    if (enemies[X, Y].Dead == false)
                    {
                        WriteLine("E to Fight " + enemies[X, Y].enemyName);
                    }
                }
                else if (enemies[X, Y] == null)
                {
                    EnemyGeneration(X, Y);

                    if (enemies[X,Y] != null)
                    {
                        WriteLine("E to Fight " + enemies[X, Y].enemyName);
                    }
                }
                WriteLine(statPrompt);
                WriteLine(quitPrompt);
                return 2;
            }

            if (mapSize[X, Y] == 3)
            {
                WriteLine("You see a church.\n");
                WriteLine(movePrompt);
                WriteLine("Q - Speak to Priest");
                WriteLine("F - Heal at the Church | 3 Gold\n");
                WriteLine(statPrompt);
                WriteLine(quitPrompt);
                return 3;
            }

            if (mapSize[X, Y] == 4)
            {
                WriteLine("You see grass and many trees, but not a soul in sight.\n");
                WriteLine(movePrompt);
                WriteLine("E - Search Around");
                WriteLine(statPrompt);
                WriteLine(quitPrompt);
                return 4;
            }

            return 0;

        }

        public void Attack(EnemyHandler enemy, Player player)
        {
            Clear();
            run = false;
            int damage = str + playerWeapon.GetStr();
            defending = 0;
            WriteLine("A " + enemy.enemyName + " has appeared!\n");
            while (true || !enemy.escape || !getDead())
            {
                enemy.waiting();

                WriteLine("Your HP: " + hp + "/" + maxhp + " & MP: " + mp + "/" + maxmp);

                WriteLine("Choose your next action:");
                WriteLine("Q - Defend");
                WriteLine("E - Attack");
                WriteLine("F - Use Magic");
                WriteLine("G - Run");
                while (true)
                {
                    try
                    {
                        String decision = ReadLine(); decision = decision.ToLower();

                        if (decision == "q" || decision == "e" || decision == "f" || decision == "g")
                        {

                            switch (decision)
                            {
                                case "q":
                                    if (defending == 1)
                                    {
                                        WriteLine("You already are defending, choose another option.\n");
                                        Thread.Sleep(1000);
                                    }
                                    else if (defending == 0)
                                    {
                                        defending = 1;
                                        WriteLine("You are now defending against any incoming attack. (Take 1/2 Damage)");
                                        Thread.Sleep(1000);
                                        break;
                                    }
                                    break;
                                case "e":
                                    if (enemy.defending == 1)
                                    {
                                        WriteLine("You attacked the " + enemy.enemyName + " for " + (int) damage/2 + " damage!");
                                    }
                                    else
                                        WriteLine("You attacked the " + enemy.enemyName + " for " + damage + " damage!");
                                        
                                    enemy.subtractHP(damage);
                                    WriteLine("The " + enemy.enemyName + " has " + enemy.hp + " out of " + enemy.maxhp + " HP remaining!");
                                    Thread.Sleep(1000);
                                    break;
                                case "f":
                                    Clear();
                                    WriteLine("Please choose another option as this has not been implemented.");
                                    Thread.Sleep(1000);
                                    break;
                                case "g":
                                    Clear();
                                    WriteLine("You successfully escaped!");
                                    run = true;
                                    Thread.Sleep(1000);
                                    break;
                            }
                            break;
                        }
                        else
                            throw new Exception();
                    }
                    catch (Exception e)
                    {
                        WriteLine("Invalid choice. Please retry.");
                    }
                }

                if (getRun())
                {
                    Clear(); break;
                }

                
                if (!enemy.Dead)
                {
                    enemy.chooseMove(player);
                }

                if (enemy.escape)
                {
                    Clear(); break;
                }

                if (player.hp == 0)
                {
                    player.isDead(enemy);
                    break;
                }

                if (enemy.Dead)
                {
                        WriteLine("You have defeated the " + enemy.enemyName + "!");
                        WriteLine("You gained " + enemy.getxp() + " XP!");
                        WriteLine("The enemy dropped " + enemy.getgold() + " gold.");
                        GainXP(enemy);
                        GainGold(enemy);
                        Thread.Sleep(3000);
                        Clear();
                        break;
                }
            }
        }

        public void GetStats()
        {
            WriteLine(name + "'s information");
            WriteLine("level= " + level + ", xp= " + xp + ", gold=" + gold + "\nplayerClass= " + playerClass + ", currentWeapon= " + playerWeapon.ToString() + "\nstats: HP: " + hp + "/" + maxhp + ", STR " + str + ", Magic " + mag + ", Max MP " + mp + "/" + maxmp + "\n");
        }

        public int GetX()
        {
            return X;
        }

        public int GetY()
        {
            return Y;
        }

        public void changeHP(int damage)
        {
            if (defending == 1)
            {
                defending = 0;
                hp -= (int)damage / 2;
                if (hp < 0)
                {
                    hp = 0;
                }
                WriteLine("The attack did " + (int)damage / 2 + " damage!");
            }
            else
            {
                WriteLine("The attack did " + damage + " damage!");
                hp -= damage;
                if (hp < 0)
                {
                    hp = 0;
                }
            }
        }

        public void Heal(int place)
        {
            if (hp != maxhp)
            {
                switch (place)
                {
                    case 1:
                        if (gold >= 5)
                        {
                            var inn = CreateQuestionBox("Are you sure you heal at the inn?", "Inn");
                            if (inn == DialogResult.Yes)
                            {
                                Clear();
                                hp = maxhp;
                                mp = maxmp;
                                gold -= 5;
                                WriteLine("You healed at the inn. Your HP/MP is now full.\n");
                            }
                            else
                                Clear();
                        }
                        break;
                    case 2:
                        if (gold >= 3)
                        {
                            var inn = CreateQuestionBox("Are you sure you heal at the church?", "Church");
                            if (inn == DialogResult.Yes)
                            {
                                Clear();
                                hp = maxhp;
                                mp = maxmp;
                                gold -= 3;
                                WriteLine("You healed at the inn. Your HP/MP is now full.\n");
                            }
                            else
                                Clear();
                        }
                        break;
                }
            }
            else
            {
                Clear();
                WriteLine("Your health is max. You cannot stay at the inn.\n");
            }
            
        }

        public string GetPlayerData()
        {
            return "name= " + name + ", level= " + level + ", xp= " + xp + ", gold=" + gold + ", playerClass= " + playerClass + ", currentWeapon= " + playerWeapon + ", currentWeaponStats=" + playerWeapon.ToString() + ", stats: HP: " + hp + "/" + maxhp + ", STR " + str + ", Magic " + mag + ", Max MP " + mp + "/" + maxmp;
        }

        public void GainXP(EnemyHandler enemy)
        {
            xp += enemy.getxp();
        }

        public void GainGold(EnemyHandler enemy)
        {
            gold += enemy.getgold();
        }

        public void isDead(EnemyHandler enemy)
        {
            Clear();
            WriteLine("You have been slain by " + enemy.enemyName + "!");
            WriteLine("Here are your final stats: \n");
            GetStats();
            WriteLine("\nPress any key to return to main menu... ");
            ReadKey();
            Clear();
            dead = true;
        }

        public Boolean getDead()
        {
            return dead;
        }
        public Boolean getRun()
        {
            return run;
        }
    }
}

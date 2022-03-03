using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static System.Console;
using static RPG_Game.MoveHandler;
using static RPG_Game.Player;

namespace RPG_Game
{
    public abstract class EnemyHandler
    {
        public Random ran = new Random();
        public string enemyName;
        public int maxhp;
        public int maxmp;
        public int hp;
        public int mp;
        public int str;
        public int mag;
        public int xp;
        public int damageTaken;
        public int defending;
        public Boolean Dead = false;
        public Boolean escape = false;

        protected EnemyHandler(string enemyName, int maxhp, int maxmp, int hp, int mp, int str, int mag, int xp)
        {
            this.enemyName = enemyName;
            this.maxhp = maxhp;
            this.maxmp = maxmp;
            this.hp = hp;
            this.mp = mp;
            this.str = str;
            this.mag = mag;
            this.xp = xp;
        }

        public abstract void chooseMove(Player player);

        public abstract int attack();

        public abstract void subtractHP(int damageRecieved);

        public abstract void run();

        public abstract void defend();

        public abstract void isDead();

        public abstract void waiting();

        public abstract int getxp();
    }

    public class Rat : EnemyHandler
    {
        public Rat() : base("Rat", 40, 20, 40, 20, 8, 5, 25)
        {
            
        } 

        public override void chooseMove(Player player)
        {
            if (hp != 0)
            {
                if (defending == 0)
                {
                    int random = ran.Next(0, 100);
                    if (random >= 0 && random <= 50)
                    {
                        WriteLine("The attack did " + attack() + " damage!");
                        player.changeHP(attack());
                        Thread.Sleep(1000);
                    }
                    else if (random > 50 && random <= 75)
                    {
                        defend();
                    }
                    else if (random > 75 && random <= 100)
                    {
                        run();
                    }
                }
                else if (defending == 1)
                    WriteLine(enemyName + " is currently defending...");
            }
            else if (hp == 0)
            {
                isDead();
            }
        }
            
        public override void waiting()
        {
            Clear();
            WriteLine("The wild rat is waiting... it has " + hp + "/" + maxhp + " HP remaining...\n");
        }

        public override int attack()
        {
            Bite bite = new Bite("Bite", base.str, 1.2);

            WriteLine("Rat used move " + bite.getName() + "!");

            return bite.getDamage();
        }

        public override void defend()
        {
            if (defending != 1)
            {
                defending = 1;
                WriteLine("The rat is now defending.. (takes 1/2 damage from attacks)");
                Thread.Sleep(1000);
            }
        }

        public override void subtractHP(int damageTaken)
        {
            if (defending == 1)
            {
                hp -= (damageTaken / 2);
                defending = 0;
            }
            else
                hp -= damageTaken;

            if (hp < 0)
            {
                hp = 0;
                Dead = true;
            }
        }

        public override void run()
        {
            int random = ran.Next(0, 100);
            if (random <= 15)
            {
                WriteLine("The rat escaped!");
                escape = true;
                Thread.Sleep(1000);
            }
            else
            {
                WriteLine("The rat attempted to run but you kept up!");
                Thread.Sleep(1000);
            }
        }

        public override void isDead()
        {
            Dead = true;
        }

        public override int getxp()
        {
            return xp;
        }
    }
}

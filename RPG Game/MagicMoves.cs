using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    abstract class MagicMoves
    {
        int damage;
        int mpCost;
        int mag;
        string moveName;

        public abstract string getName();

        public abstract int getMpCost();

        public abstract int getDamageOrHeal(EnemyHandler enemy);

        public class Fireball : MagicMoves {
            public Fireball(int mag)
            {
                this.mag = mag;
                this.damage = (int)(mag * 1.2);
                this.moveName = "Fireball";
                this.mpCost = 20;
            }
            
            public override string getName()
            {
                return moveName;
            }

            public override int getMpCost()
            {
                return mpCost;
            }

            public override int getDamageOrHeal(EnemyHandler enemy)
            {
                if (enemy.defending == 1)
                    return (int)damage / 2;
                else
                    return damage;
            }
        }
    }
}

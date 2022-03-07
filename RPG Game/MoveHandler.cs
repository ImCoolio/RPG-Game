using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    public abstract class MoveHandler
    {
        string moveName;
        int strValue;
        double multiplier;
        int damage;

        public abstract int getDamage();

        public abstract string getName();

        public class Bite : MoveHandler
        {
            public Bite(string moveName, int strValue, double multiplier)
            {
                this.moveName = moveName;
                this.strValue = strValue;
                this.multiplier = multiplier;
                damage = (int)(strValue * multiplier);
            }

            public override int getDamage()
            {
                return damage;
            }
            public override string getName()
            {
                return moveName;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    public abstract class MoveHandler
    {
        public string moveName;
        public int strValue;
        public double multiplier;
        public int damage;

        protected MoveHandler(String moveName, int strValue, double multiplier)
        {
            this.moveName = moveName;
            this.strValue = strValue;
            this.multiplier = multiplier;
            damage = (int)(strValue * multiplier);
        }

        public abstract int getDamage();

        public abstract string getName();

        public class Bite : MoveHandler
        {
            public Bite() : base("Bite", 8, 1.2)
            {

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

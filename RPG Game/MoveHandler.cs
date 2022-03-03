using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    internal class MoveHandler
    {

        public class Bite
        {
            public string moveName;
            public int strValue;
            public double multiplier;
            int damage;
            public Bite(string moveName, int strValue, double multiplier) 
            {
                this.moveName = moveName;
                this.strValue = strValue;
                this.multiplier = multiplier;
                damage = (int) (strValue * multiplier);
            }

            public int getDamage()
            {
                return damage;
            }
            public string getName()
            {
                return moveName;
            }
        }
    }
}

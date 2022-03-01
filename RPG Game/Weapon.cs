using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Game
{
    internal class Weapon
    {
        String name;
        int str;
        int mag;
        public Weapon(String name, int str, int mag)
        {
            this.name = name;
            this.str = str;
            this.mag = mag;
        }

        public String GetName()
        {
            return name;
        }

        public int GetStr()
        {
            return str;
        }

        public int GetMag()
        {
            return mag;
        }

        override
        public String ToString()
        {
            return name + ", Strength+: " + str + ", Magic+: " + mag;
        }
    }
}

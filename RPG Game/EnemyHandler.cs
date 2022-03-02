using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace RPG_Game
{
    internal class EnemyHandler
    {   
        public class Rat
        {
            int str = 8;
            int mag = 5;
            int hp = 40;
            int mp = 20;
        }

        public class Bat
        {
            int str = 3;
            int mag = 5;
            int hp = 25;
            int mp = 15;
        }

        public class Alligator
        {
            int str = 14;
            int mag = 2;
            int hp = 50;
            int mp = 10;
        }

        public class Skeleton
        {
            int str = 10;
            int mag = 3;
            int hp = 15;
            int mp = 15;
        }

        public class SkeletonMage
        {
            int str = 2;
            int mag = 12;
            int hp = 30;
            int mp = 50;
        }
    }
}

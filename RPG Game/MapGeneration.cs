using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static RPG_Game.EnemyHandler;

namespace RPG_Game
{
    public class MapGeneration
    {
        public static int currentChurches = 0; public static int maxChurches = 5;
        public static int currentTowns = 0; public static int maxTowns = 3;
        public static Random ran = new Random();
        public static int[,] mapSize = new int[11,11];
        public static int progress = 0;

        public static void CreateMap()
        {
            for (int i = 0; i <= mapSize.GetUpperBound(0) - 1; i++)
                for (int j = 0; j <= mapSize.GetUpperBound(1) - 1; j++) 
                {
                        progress++;
                        int randomPlace = ran.Next(0, 100);
                        if (randomPlace <= 10 && currentTowns != maxTowns)
                        {
                            currentTowns++;
                            mapSize[i, j] = 1;
                        }
                        else if (randomPlace > 10 && randomPlace <= 55)
                            mapSize[i, j] = 2;
                        else if (randomPlace > 55 && randomPlace <= 70 && currentChurches != maxChurches)
                        {
                            currentChurches++;
                            mapSize[i, j] = 3;
                        }
                        else if (randomPlace > 70 || currentChurches == maxChurches || currentTowns == maxTowns)
                            mapSize[i, j] = 4;

                        if (progress < 121)
                            Write("Loading.. " + String.Format("{0:0.##}", progress / 1.21) + "%\n");
                }
            mapSize[5, 5] = 1;
        }

        public static String EnemyGeneration()
        {
            int randomPlace = ran.Next(0, 100);

            if (randomPlace <= 10)
            {
                Alligator alligator = new Alligator();
                return "Alligator (HARD)";
            }
            else if (randomPlace > 10 && randomPlace <= 25)
            {
                SkeletonMage skeleMage = new SkeletonMage();
                return "Skeleton Mage (Medium)";
            }
            else if (randomPlace > 25 && randomPlace <= 35)
            {
                Skeleton skeleton = new Skeleton();
                return "Skeleton (Normal)";
            }
            else if (randomPlace > 35 && randomPlace <= 85)
            {
                Rat rat = new Rat();
                return "Rat (Easy)";
            }
            else if (randomPlace > 85)
            {
                Bat bat = new Bat();
                return "Bat (Easy)";
            }

            return "Error.";
        }
    }
}

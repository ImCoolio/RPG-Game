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
        public static int[,] enemies = new int[11, 11];
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
                        else if (randomPlace > 10 && randomPlace <= 35)
                            mapSize[i, j] = 2;
                        else if (randomPlace > 35 && randomPlace <= 50 && currentChurches != maxChurches)
                        {
                            currentChurches++;
                            mapSize[i, j] = 3;
                        }
                        else if (randomPlace > 50 || currentChurches == maxChurches || currentTowns == maxTowns)
                            mapSize[i, j] = 4;

                        if (progress < 121)
                            Write("Loading.. " + String.Format("{0:0.##}", progress / 1.21) + "%\n");
                }
            mapSize[5, 5] = 1;
        }

        public static int[,] getMapDetails()
        {
            return mapSize;
        }

        public static String EnemyGeneration(int x, int y)
        {
            if (enemies[x, y] == 0)
            {
                int randomPlace = ran.Next(0, 100);

                if (randomPlace <= 10)
                {
                    Alligator alligator = new Alligator();
                    enemies[x, y] = 1;
                    return "Alligator (HARD)";
                }
                else if (randomPlace > 10 && randomPlace <= 25)
                {
                    SkeletonMage skeleMage = new SkeletonMage();
                    enemies[x, y] = 2;
                    return "Skeleton Mage (Medium)";
                }
                else if (randomPlace > 25 && randomPlace <= 35)
                {
                    Skeleton skeleton = new Skeleton();
                    enemies[x, y] = 3;
                    return "Skeleton (Normal)";
                }
                else if (randomPlace > 35 && randomPlace <= 85)
                {
                    Rat rat = new Rat();
                    enemies[x, y] = 4;
                    return "Rat (Easy)";
                }
                else if (randomPlace > 85)
                {
                    Bat bat = new Bat();
                    enemies[x, y] = 5;
                    return "Bat (Easy)";
                }
            }
            else
                switch (enemies[x, y])
                {
                    case 1:
                        return "Alligator (HARD)";
                        break;
                    case 2:
                        return "Skeleton Mage (Medium)";
                        break;
                    case 3:
                        return "Skeleton (Normal)";
                        break;
                    case 4:
                        return "Rat (Easy)";
                        break;
                    case 5:
                        return "Bat (Easy)";
                        break;
                }


            return "Error.";
        }
    }
}

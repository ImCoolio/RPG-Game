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
        public static int currentChurches = 0; public static int maxChurches = 10;
        public static int currentTowns = 0; public static int maxTowns = 6;
        public static Random ran = new Random();
        public static int[,] mapSize = new int[21, 21];
        public static EnemyHandler[,] enemies = new EnemyHandler[21, 21];
        public static int progress = 0;

        public static void CreateMap()
        {
            for (int i = 0; i <= mapSize.GetUpperBound(0); i++)
                for (int j = 0; j <= mapSize.GetUpperBound(1); j++) 
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

                        if (progress < 441)
                            Write("Loading.. " + String.Format("{0:0.##}", progress / 4.41 + "%\n"));
                }
            mapSize[10,10] = 1;
        }

        public static int[,] getMapDetails()
        {
            return mapSize;
        }

        public static String EnemyGeneration(int x, int y)
        {
            if (enemies[x, y] == null)
            {
                int randomPlace = ran.Next(0, 100);

                if (randomPlace <= 10)
                {
                    /*Alligator alligator = new Alligator();*//*
                    enemies[x, y] = alligator;*/
                }
                else if (randomPlace > 10 && randomPlace <= 25)
                {
                    /*SkeletonMage skeleMage = new SkeletonMage();
                    enemies[x, y] = skeleMage;*/
                }
                else if (randomPlace > 25 && randomPlace <= 35)
                {
                    /*Skeleton skeleton = new Skeleton();
                    enemies[x, y] = skeleton;*/
                }
                else if (randomPlace > 35 && randomPlace <= 85)
                {
                    Rat rat = new Rat();
                    enemies[x, y] = rat;
                    return "Rat (Easy)";
                }
                else if (randomPlace > 85)
                {
                    /*Bat bat = new Bat();
                    enemies[x, y] = bat;*/
                }
            }
            else
                switch (enemies[x, y])
                {
/*                    case Alligator alligator:
                        return "Alligator (HARD)";
                        break;*/
/*                    case 2:
                        return "Skeleton Mage (Medium)";
                        break;*/
/*                    case 3:
                        return "Skeleton (Normal)";
                        break;*/
                    case Rat rat:
                        return "Rat (Easy)";
                        break;
/*                    case 5:
                        return "Bat (Easy)";
                        break;*/
                    default:
                        break;
                }


            return "Error.";
        }
    }
}

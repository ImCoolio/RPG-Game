﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

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
            for (int i = 0; i <= 10; i++)
                for (int j = 0; j <= 10; j++) 
                {
                        progress++;
                        int randomPlace = ran.Next(0, 100);
                        if (randomPlace <= 10 && currentTowns != maxTowns)
                        {
                            currentTowns++;
                            mapSize[i, j] = 1;
                        }
                        else if (randomPlace > 10 && randomPlace <= 55)
                        {
                            mapSize[i, j] = 2;
                        }
                        else if (randomPlace > 55 && randomPlace <= 70 && currentChurches != maxChurches)
                        {
                            currentChurches++;
                            mapSize[i, j] = 3;
                        }
                        else if (randomPlace > 70 || currentChurches == maxChurches || currentTowns == maxTowns)
                        {
                            mapSize[i, j] = 4;
                        }
                    if (progress < 121)
                        Write("Loading.. " + String.Format("{0:0.##}", progress / 1.21) + "%\n");
                }
            mapSize[5, 5] = 1;
        }

        public static int getLocation(int x, int y)
        {
            return mapSize[x, y];
        }
    }
}
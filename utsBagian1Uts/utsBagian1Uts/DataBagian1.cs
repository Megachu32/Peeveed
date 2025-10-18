using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace utsBagian1Uts
{
    public static class DataBagian1
    {
        // data total resources
        static int totalWood = 0;
        static int totalIRon = 0;
        static int totalClay = 0;
        static int totalCrop = 0;

        //data rate production resource per hour
        static int rateWood = 0;
        static int rateIron = 0;
        static int rateClay = 0;
        static int rateCrop = 0;

        //data timer normal
        static int second = 0;
        static int minute = 0;
        static int hour = 0;

        //data timer upgrade
        static int upgradeSecond = 0;
        static int upgradeMinute = 0;
        static int upgradeHour = 0;

        //data bonus
        static int bonus = 0;

        //speed multiplier
        static int speedmul = 0;


        static int[,] reqUpgradeTree =
        {
            {40,100,50,60,260,7}, // format = wood,clay,iron,crop,waktu(detik),production/hour
            {65,165,85,100,620,13},//level 2
            {110,280,140,165,1190,21},//level 3
            {185,465,235,280,2100,31},//level 4 etc
            {310,780,390,465,3560,46},
            {520,1300,650,780,5890,70},
            {870,2170,1085,1300,9620,98},
            {1450,3625,1810,2175,15590,140},
            {2420,6050,3025,3630,25150,203},
            {4040,10105,5050,6060,40440,280},
            {6750,16870,8435,10125,64900,392},
            {11270,28175,14090,16905,104050,525},
            {18820,47055,23525,28230,166680,693},
            {31430,78580,39290,47150,266880,889},
            {52490,131230,65615,78740,427210,1120},
            {87660,219155,109575,131490,683730,1400},
            {146395,365985,182995,219590,1094170,1820},
            {244480,611195,305600,366715,1750880,2240},
            {408280,1020695,510350,612420,2801600,2800},
            {681825,1704565,852280,1022740,4482770,3430}
        };

        static int[,] reqUpgradeClay =
        {
            {80,40,80,50,220,7},
            {135,65,135,85,550,13},
            {225,110,225,140,1080,21},
            {375,185,375,235,1930,31},
            {620,310,620,390,3290,46},
            {1040,520,1040,650,5470,70},
            {1735,870,1735,1085,8950,98},
            {2900,1450,2900,1810,14520,140},
            {4840,2420,4840,3025,23430,203},
            {8080,4040,8080,5050,37690,280},
            {13500,6750,13500,8435,60510,392},
            {22540,11270,22540,14090,97010,525},
            {37645,18820,37645,23525,155420,693},
            {62865,31430,62865,39290,248870,889},
            {104985,52490,104985,65615,398390,1120},
            {175320,87660,175320,109575,637620,1400},
            {292790,146395,292790,182995,1020390,1820},
            {488955,244480,488955,305600,1632820,2240},
            {816555,408280,816555,510350,2612710,2800},
            {1363650,681825,1363650,852280,4180540,3430}
        };

        static int[,] reqUpgradeIron =
        {
            {100,80,30,60,450,7},
            {165,135,50,100,920,13},
            {280,225,85,165,1670,21},
            {465,375,140,280,2880,31},
            {780,620,235,465,4800,46},
            {1300,1040,390,780,7880,70},
            {2170,1735,650,1300,12810,98},
            {3625,2900,1085,2175,20690,140},
            {6050,4840,1815,3630,33310,203},
            {10105,8080,3030,6060,53500,280},
            {16870,13500,5060,10125,85800,392},
            {28175,22540,8455,16905,137470,525},
            {47055,37645,14115,28230,220160,693},
            {78580,62865,23575,47150,352450,889},
            {131230,104985,39370,78740,564120,1120},
            {219155,175320,65745,131490,902790,1400},
            {365985,292790,109795,219590,1444660,1820},
            {611195,488955,183360,366715,2311660,2240},
            {1020695,816555,306210,612420,3698850,2800},
            {1704565,1363650,511370,1022740,5918370,3430}
        };

        static int[,] reqUpgradeCrop =
        {
            {100,80,30,60,450,7},
            {165,135,50,100,920,13},
            {280,225,85,165,1670,21},
            {465,375,140,280,2880,31},
            {780,620,235,465,4800,46},
            {1300,1040,390,780,7880,70},
            {2170,1735,650,1300,12810,98},
            {3625,2900,1085,2175,20690,140},
            {6050,4840,1815,3630,33310,203},
            {10105,8080,3030,6060,53500,280},
            {16870,13500,5060,10125,85800,392},
            {28175,22540,8455,16905,137470,525},
            {47055,37645,14115,28230,220160,693},
            {78580,62865,23575,47150,352450,889},
            {131230,104985,39370,78740,564120,1120},
            {219155,175320,65745,131490,902790,1400},
            {365985,292790,109795,219590,1444660,1820},
            {611195,488955,183360,366715,2311660,2240},
            {1020695,816555,306210,612420,3698850,2800},
            {1704565,1363650,511370,1022740,5918370,3430}
        };

        public static int[] getUpgradeData(string tipeUpgrade, int level)
        {
            int index = level - 1;


            switch (tipeUpgrade)
            {
                case "tree":
                    return new int[]
                    {
                        reqUpgradeTree[index,0], //wood
                        reqUpgradeTree[index,1], //clay
                        reqUpgradeTree[index,2], //iron
                        reqUpgradeTree[index,3], //crop
                        reqUpgradeTree[index,4],//waku(detik)
                        reqUpgradeTree[index,6] //production 
                    };
                case "clay":
                    return new int[]
                    {
                        reqUpgradeClay[index,0], //wood
                        reqUpgradeClay[index,1], //clay
                        reqUpgradeClay[index,2], //iron
                        reqUpgradeClay[index,3], //crop
                        reqUpgradeClay[index,4],//waku(detik)
                        reqUpgradeClay[index,6] //production 
                    };
                case "iron":
                    return new int[]
                    {
                        reqUpgradeIron[index,0], //wood
                        reqUpgradeIron[index,1], //clay
                        reqUpgradeIron[index,2], //iron
                        reqUpgradeIron[index,3], //crop
                        reqUpgradeIron[index,4],//waku(detik)
                        reqUpgradeIron[index,6] //production 
                    };
                case "crop":
                    return new int[]
                    {
                        reqUpgradeCrop[index,0], //wood
                        reqUpgradeCrop[index,1], //clay
                        reqUpgradeCrop[index,2], //iron
                        reqUpgradeCrop[index,3], //crop
                        reqUpgradeCrop[index,4],//waku(detik)
                        reqUpgradeCrop[index,6] //production 
                    };
                default:
                    throw new ArgumentException("unkown build");
            }

        }








    }
}

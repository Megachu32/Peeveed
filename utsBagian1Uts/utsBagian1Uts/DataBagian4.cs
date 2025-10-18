using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace utsBagian1Uts
{
    internal class DataBagian4
    {
        /*
         the function of this class is to hold the data where there would be a foreach loop timer
            that run for every single List<DataBagian4> in Form1.cs, which would tick down the upgrade
            timer for each building in upgrade. this would also hanlde the function for updateing the text.
         */
        int buildingLevel;
        int timeUpgrade; //in seconds
        int index;

        public DataBagian4(int buildingLevel, int timeUpgrade, int index)
        {
            this.buildingLevel = buildingLevel;
            this.timeUpgrade = timeUpgrade;
            this.index = index;
        }

        public int BuildingLevel { get => buildingLevel; set => buildingLevel = value; }
        public int TimeUpgrade { get => timeUpgrade; set => timeUpgrade = value; }
        public int Index { get => index; set => index = value; }
        /// <summary>
        /// would tick the building upgrade timer down by 1 second, and if it reaches 0, it would increase the building level of selected index by 1
        /// </summary>
        public bool tickBuilding()
        {
            if (timeUpgrade-1 <= 0)
            {
                return true;
            }
            else
            {
                //timeUpgrade--;
                return false;
            }
           
        }
    }
}

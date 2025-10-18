using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace utsBagian1Uts
{
    public partial class Form1 : Form
    {
        //has been called in DataBagian1.cs
        //private int [] income = { 32,32,32,48 }; //clay, iron, wood, crop
        //private double[] resource = { 0,0,0,0 }; //clay, iron, wood, crop

        private int chosenResourceIndex = -1; // -1 = default, 0-3 = clay, 4-7 = iron, 8-11 = wood, 12-17 = crop
        private List<DataBagian4> pendingUpgrade = new List<DataBagian4>(); // holds all the building currently upgrading
        private int multiplier = 1;
        public Form1()
        {
            InitializeComponent();
            timerNormal.Enabled = true;

            setIncome();
        }
        // all variable is in here

        public static void saveData(string content)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.DefaultExt = "txt";
            sfd.FileName = "data.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Get chosen path
                string filePath = sfd.FileName;

                // Save your data to that file
                File.WriteAllText(filePath, content);

                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timerNormal_Tick(object sender, EventArgs e)
        {
            //income per hour
            for (int i = 0; i < 4; i++)
            {
                DataBagian1.resource[i] += (DataBagian1.income[i] * multiplier) / 3600 ;
            }
        }

        private void setIncome()
        {
            //clay
            int tempResource = 0;
            int[] data;

            if (int.Parse(buttonClay1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonClay2.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay2.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonClay3.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay3.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonClay4.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay4.Text));
                tempResource += data[5];
            }
            DataBagian1.income[0] = 32 + tempResource;

            //iron
            tempResource = 0;

            if (int.Parse(buttonIron1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonIron1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonIron1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonIron1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron1.Text));
                tempResource += data[5];
            }

            DataBagian1.income[1] = 32 + tempResource;

            //wood
            tempResource = 0;

            if (int.Parse(buttonTree1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonTree1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonTree1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonTree1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree1.Text));
                tempResource += data[5];
            }

            DataBagian1.income[2] = 32 + tempResource;

            //crop
            tempResource = 0;

            if (int.Parse(buttonCrop1.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop1.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonCrop2.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop2.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonCrop3.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop3.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonCrop4.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop4.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonCrop5.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop5.Text));
                tempResource += data[5];
            }
            if (int.Parse(buttonCrop6.Text) != 0)
            {
                data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop6.Text));
                tempResource += data[5];
            }

            DataBagian1.income[3] = 48 + tempResource;

            updateIncomeText();
        }

        //just update the income text
        private void updateIncomeText()
        {
            labelRateClay.Text = DataBagian1.income[0].ToString();
            labelRateIron.Text = DataBagian1.income[1].ToString();
            labelRateWood.Text = DataBagian1.income[2].ToString();
            labelRateCorp.Text = DataBagian1.income[3].ToString();
        }

        /// <summary>
        /// Handles the periodic tick event for processing building upgrades.
        /// </summary>
        /// <remarks>This method iterates through the list of pending upgrades and processes each one.  If
        /// an upgrade is completed during the tick, the corresponding building is upgraded.  Otherwise, the remaining
        /// upgrade time is decremented.</remarks>
        /// <param name="sender">The source of the event, typically the timer triggering the tick.</param>
        /// <param name="e">The event data associated with the tick event.</param>
        private void timerUpgrade_Tick(object sender, EventArgs e)
        {
            bool doneUpgrading = false;
            foreach (DataBagian4 pendingup in pendingUpgrade)
            {
                doneUpgrading = false;
                pendingup.tickBuilding();
                if (pendingup.tickBuilding())
                {
                    upgradeBuilding(pendingup.Index);
                }
                else
                {
                    pendingup.TimeUpgrade -= 1;
                }
            }
        }

/// <summary>
/// Upgrades the specified building by incrementing its level.
/// </summary>
/// <remarks>This method updates the text of the button associated with the specified building to reflect the new
/// level. Ensure that the index provided corresponds to a valid building; otherwise, the method will have no
/// effect.</remarks>
/// <param name="i">The index of the building to upgrade. Valid values range from 0 to 17, where each index corresponds  to a specific
/// building (e.g., clay, iron, tree, or crop buildings).</param>
        private void upgradeBuilding(int i)
        {
            switch (i)
            {
                case 0:
                    //clay 1
                    buttonClay1.Text = (int.Parse(buttonClay1.Text) + 1).ToString();
                    break;
                case 1:
                    //clay 2
                    buttonClay2.Text = (int.Parse(buttonClay2.Text) + 1).ToString();
                    break;
                case 2:
                    //clay 3
                    buttonClay3.Text = (int.Parse(buttonClay3.Text) + 1).ToString();
                    break;
                case 3:
                    //clay 4
                    buttonClay4.Text = (int.Parse(buttonClay4.Text) + 1).ToString();    
                    break;
                case 4:
                    //iron 1
                    buttonIron1.Text = (int.Parse(buttonIron1.Text) + 1).ToString();
                    break;
                case 5:
                    //iron 2
                    buttonIron2.Text = (int.Parse(buttonIron2.Text) + 1).ToString();
                    break;
                case 6:
                    //iron 3
                    buttonIron3.Text = (int.Parse(buttonIron3.Text) + 1).ToString();
                    break;
                case 7:
                    //iron 4
                    buttonIron4.Text = (int.Parse(buttonIron4.Text) + 1).ToString();
                    break;
                case 8:
                    //tree 1
                    buttonTree1.Text = (int.Parse(buttonTree1.Text) + 1).ToString();
                    break;
                case 9:
                    //tree 2
                    buttonTree2.Text = (int.Parse(buttonTree2.Text) + 1).ToString();
                    break;
                case 10:
                    //tree 3
                    buttonTree3.Text = (int.Parse(buttonTree3.Text) + 1).ToString();
                    break;
                case 11:
                    //tree 4
                    buttonTree4.Text = (int.Parse(buttonTree4.Text) + 1).ToString();
                    break;
                case 12:
                    //crop 1
                    buttonCrop1.Text = (int.Parse(buttonCrop1.Text) + 1).ToString();
                    break;
                case 13:
                    //crop 2
                    buttonCrop2.Text = (int.Parse(buttonCrop2.Text) + 1).ToString();
                    break;
                case 14:
                    //crop 3
                    buttonCrop3.Text = (int.Parse(buttonCrop3.Text) + 1).ToString();
                    break;
                case 15:
                    //crop 4
                    buttonCrop4.Text = (int.Parse(buttonCrop4.Text) + 1).ToString();
                    break;
                case 16:
                    //crop 5
                    buttonCrop5.Text = (int.Parse(buttonCrop5.Text) + 1).ToString();
                    break;
                case 17:
                    //crop 6
                    buttonCrop6.Text = (int.Parse(buttonCrop6.Text) + 1).ToString();
                    break;
            }
        }

        //clay
        private void buttonClay1_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 0;
        }

        private void buttonClay2_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 1;
        }

        private void buttonClay3_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 2;
        }

        private void buttonClay4_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 3;
        }
        //iron
        private void buttonIron1_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 4;
        }

        private void buttonIron2_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 5;
        }

        private void buttonIron3_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 6;
        }

        private void buttonIron4_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 7;
        }
        //tree
        private void buttonTree1_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 8;
        }

        private void buttonTree2_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 9;
        }

        private void buttonTree3_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 10;
        }

        private void buttonTree4_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 11;
        }

        //crop
        private void buttonCrop1_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 12;
        }

        private void buttonCrop2_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 13;
        }

        private void buttonCrop3_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 14;
        }

        private void buttonCrop4_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 15;
        }

        private void buttonCrop5_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 16;
        }

        private void buttonCrop6_Click(object sender, EventArgs e)
        {
            chosenResourceIndex = 17;
        }
        /// <summary>
        /// call people out when resource is not enough
        /// </summary>
        /// <param name="text"></param>
        private void callOutBroke(string text)
        {
            MessageBox.Show("You do not have the resource to upgrade " + text,"BROKE",MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        private void buttonUpgrade_Click(object sender, EventArgs e)
        {
            int level = -1;
            int[] data;
            switch (chosenResourceIndex)
            {
                case 0:
                    //clay 1
                    level = int.Parse(buttonClay1.Text);
                    if (level+1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4],0));
                    }
                    else
                    {
                        callOutBroke("Clay 1");
                    }
                    break;
                case 1:
                    //clay 2
                    level = int.Parse(buttonClay2.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 1));
                    }
                    else
                    {
                        callOutBroke("Clay 2");
                    }
                    break;
                case 2:
                    //clay 3
                    level = int.Parse(buttonClay3.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 2));
                    }
                    else
                    {
                        callOutBroke("Clay 4");
                    }
                    break;
                case 3:
                    //clay 4
                    level = int.Parse(buttonClay4.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 3));
                    }
                    else
                    {
                        callOutBroke("Clay 4");
                    }
                    break;
                case 4:
                    //iron 1
                    level = int.Parse(buttonIron1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 4));
                    }
                    else
                    {
                        callOutBroke("Iron 1");
                    }
                    break;
                case 5:
                    //iron 2
                    level = int.Parse(buttonIron2.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 5));
                    }
                    else
                    {
                        callOutBroke("Iron 2");
                    }
                    break;
                case 6:
                    //iron 3
                    level = int.Parse(buttonIron3.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 6));
                    }
                    else
                    {
                        callOutBroke("Iron 3");
                    }
                    break;
                case 7:
                    //iron 4
                    level = int.Parse(buttonIron4.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 7));
                    }
                    else
                    {
                        callOutBroke("Iron 4");
                    }
                    break;
                case 8:
                    //tree 1
                    level = int.Parse(buttonTree1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 8));
                    }
                    else
                    {
                        callOutBroke("Tree 1");
                    }
                    break;
                case 9:
                    //tree 2
                    level = int.Parse(buttonTree2.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 9));
                    }
                    else
                    {
                        callOutBroke("Tree 2");
                    }
                    break;
                case 10:
                    //tree 3
                    level = int.Parse(buttonTree3.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 10));
                    }
                    else
                    {
                        callOutBroke("Tree 3");
                    }
                    break;
                case 11:
                    //tree 4
                    level = int.Parse(buttonTree4.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 11));
                    }
                    else
                    {
                        callOutBroke("Tree 4");
                    }
                    break;
                case 12:
                    //crop 1
                    level = int.Parse(buttonCrop1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 12));
                    }
                    else
                    {
                        callOutBroke("Crop 1");
                    }
                    break;
                case 13:
                    //crop 2
                    level = int.Parse(buttonCrop1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 13));
                    }
                    else
                    {
                        callOutBroke("Crop 2");
                    }
                    break;
                case 14:
                    //crop 3
                    level = int.Parse(buttonCrop1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 14));
                    }
                    else
                    {
                        callOutBroke("Crop 3");
                    }
                    break;
                case 15:
                    //crop 4
                    level = int.Parse(buttonCrop1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 15));
                    }
                    else
                    {
                        callOutBroke("Crop 4");
                    }
                    break;
                case 16:
                    //crop 5
                    level = int.Parse(buttonCrop1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 16));
                    }
                    else
                    {
                        callOutBroke("Crop 5");
                    }
                    break;
                case 17:
                    //crop 6
                    level = int.Parse(buttonCrop1.Text);
                    if (level + 1 > 20)
                    {
                        MessageBox.Show("Maximum Level Reached", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    }
                    data = DataBagian1.getUpgradeData("clay", level);
                    if (hasResourceToUpgrade(data))
                    {
                        pendingUpgrade.Add(new DataBagian4(level, data[4], 17));
                    }
                    else
                    {
                        callOutBroke("Crop 6");
                    }
                    break;
                default:
                    MessageBox.Show("Please Select a building to Upgrade", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;

            }
        }
        private bool hasResourceToUpgrade(int[] dataUpgrade)
        {
            //MegNote dataUpgrade: wood[0], clay[1], iron[2], crop[3], waktu(detik)[4], production/hour[5]
            //MegNote resource : //clay[0], iron[1], wood[2], crop[3]
            if (DataBagian1.resource[2] >= dataUpgrade[0] && //wood and wood
                DataBagian1.resource[1] >= dataUpgrade[2] && //iron and iron
                DataBagian1.resource[0] >= dataUpgrade[1] && //clay and clay 
                DataBagian1.resource[3] >= dataUpgrade[3]) // crop and crop
            {
                //has enough resource
                DataBagian1.resource[2] -= dataUpgrade[0]; // reduce wood
                DataBagian1.resource[1] -= dataUpgrade[2]; // reduce iron
                DataBagian1.resource[0] -= dataUpgrade[1]; // reduce clay
                DataBagian1.resource[3] -= dataUpgrade[3]; // reduce crop
                return true;
            }
            else
            {
                //not enough resource
                MessageBox.Show("Not Enough Resource to Upgrade", "Upgrade Menu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }
    }
}

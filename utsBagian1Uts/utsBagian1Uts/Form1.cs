using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public Form1()
        {
            InitializeComponent();
            timerNormal.Enabled = true;
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
                DataBagian1.resource[i] += DataBagian1.income[i] / 3600;
            }
        }

        private void setIncome()
        {
            //clay
            int tempResource = 0 ;
            int[] data;

            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay1.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay2.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay3.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonClay4.Text));
            tempResource += data[5];

            DataBagian1.income[0] = 32 + tempResource;

            //iron
            tempResource = 0 ;

            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron1.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron2.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron3.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonIron4.Text));
            tempResource += data[5];

            DataBagian1.income[1] = 32 + tempResource;

            //wood
            tempResource = 0 ;

            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree1.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree2.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree3.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonTree4.Text));
            tempResource += data[5];

            DataBagian1.income[2] = 32 + tempResource;

            //crop
            tempResource = 0 ;

            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop1.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop2.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop3.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop4.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop5.Text));
            tempResource += data[5];
            data = DataBagian1.getUpgradeData("clay", int.Parse(buttonCrop6.Text));
            tempResource += data[5];

            DataBagian1.income[3] = 48 + tempResource;

            updateIncomeText();
        }

        private void updateIncomeText()
        {
            labelRateClay.Text = DataBagian1.income[0].ToString();
            labelRateIron.Text = DataBagian1.income[1].ToString();
            labelRateWood.Text = DataBagian1.income[2].ToString();
            labelRateCorp.Text = DataBagian1.income[3].ToString();
        }

        private void timerUpgrade_Tick(object sender, EventArgs e)
        {

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
    }

}

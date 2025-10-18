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
        public Form1()
        {
            InitializeComponent();
            timerNormal.Start();
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

        public void timerNormal_Tick(object sender, EventArgs e)
        {
            //upadte current resource and rate
            labelTotalClay.Text = DataBagian1.totalClay.ToString();
            labelTotalCrop.Text = DataBagian1.totalCrop.ToString();
            labelTotalIron.Text = DataBagian1.totalIRon.ToString();
            labelTotalWood.Text = DataBagian1.totalWood.ToString();
            labelRateCorp.Text = DataBagian1.rateCrop.ToString();
            labelRateIron.Text = DataBagian1.rateIron.ToString();
            labelRateWood.Text = DataBagian1.rateWood.ToString();
            labelRateClay.Text = DataBagian1.rateClay.ToString();

            // upadte normal timer 
            DataBagian1.second++;
            if (DataBagian1.second == 60)
            {
                DataBagian1.second = 0;
                DataBagian1.minute++;
            }
            if (DataBagian1.minute == 60)
            {
                DataBagian1.minute = 0;
                DataBagian1.hour++;
            }

        }

        public static void timerUpgrade_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataBagian1.addBuilding("tree", 1, 0);//index 0 tree 1
            DataBagian1.addBuilding("tree", 1, 0);//intex 1 tree 2
            DataBagian1.addBuilding("tree", 1, 0);//index 2 tree 3
            DataBagian1.addBuilding("tree", 1, 0);//etc
            DataBagian1.addBuilding("iron", 1, 0);
            DataBagian1.addBuilding("iron", 1, 0);
            DataBagian1.addBuilding("iron", 1, 0);
            DataBagian1.addBuilding("iron", 1, 0);
            DataBagian1.addBuilding("clay", 1, 0);
            DataBagian1.addBuilding("clay", 1, 0);
            DataBagian1.addBuilding("clay", 1, 0);
            DataBagian1.addBuilding("clay", 1, 0);
            DataBagian1.addBuilding("crop", 1, 0);
            DataBagian1.addBuilding("crop", 1, 0);
            DataBagian1.addBuilding("crop", 1, 0);
            DataBagian1.addBuilding("crop", 1, 0);
            DataBagian1.addBuilding("crop", 1, 0);
            DataBagian1.addBuilding("crop", 1, 0);
        }

        public void refresButton()
        {
            buttonClay1.Text = DataBagian1.buildingLevel[9].ToString();
        }

        }
}

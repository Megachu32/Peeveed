using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace utsBagian1Uts
{
    public partial class Form1 : Form
    {
        private BuildingManager manager;

        // Progress bar tracking
        private int totalUpgradeTime = 0;
        private int remainingUpgradeTime = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeBuildingSystem();
        }

        private void InitializeBuildingSystem()
        {
            manager = new BuildingManager(timerUpgrade);

            // Starting resources
            manager.Wood = 5000;
            manager.Clay = 5000;
            manager.Iron = 5000;
            manager.Crop = 5000;

            // Connect callbacks
            manager.OnStatus = msg => labelUpgradeCountdown.Text = msg;
            manager.OnCostDisplay = cost => DisplayUpgradeCost(cost);
            manager.OnLevelUp = (lvl, btn) => btn.Text = lvl.ToString();
            manager.OnResourceUpdate = UpdateResourceLabels;

            // Attach all resource buttons dynamically
            foreach (Button btn in this.Controls.OfType<Button>())
            {
                if (btn.Name.StartsWith("buttonTree") ||
                    btn.Name.StartsWith("buttonClay") ||
                    btn.Name.StartsWith("buttonIron") ||
                    btn.Name.StartsWith("buttonCrop"))
                {
                    btn.Click += ResourceButton_Click;
                }
            }

            buttonUpgrade.Click += ButtonUpgrade_Click;
            UpdateResourceLabels();
        }

        // When a resource button is clicked
        private void ResourceButton_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            manager.SelectBuilding(clicked);
            buttonUpgrade.Enabled = manager.CanUpgrade();
        }

        // When Upgrade button is clicked
        private void ButtonUpgrade_Click(object sender, EventArgs e)
        {
            manager.StartUpgrade();

            if (manager.CanUpgrade())
            {
                int[] data = DataBagian1.getUpgradeData(manager.BuildingType, manager.Level);
                StartProgressBar((int)(data[4] / manager.SpeedMultiplier));
            }

            buttonUpgrade.Enabled = false;
        }

        // Show upgrade cost and details
        private void DisplayUpgradeCost(string[] info)
        {
            groupBox2.Text = "Building Details";
            groupBox2.Controls.Clear();

            int y = 30;
            foreach (string line in info)
            {
                Label lbl = new Label
                {
                    Text = line,
                    Location = new System.Drawing.Point(20, y),
                    AutoSize = true
                };
                groupBox2.Controls.Add(lbl);
                y += 25;
            }

            groupBox2.Controls.Add(buttonUpgrade);
            buttonUpgrade.Location = new System.Drawing.Point(68, y + 10);

            buttonUpgrade.Enabled = manager.CanUpgrade();
        }

        // Update resource totals on screen
        private void UpdateResourceLabels()
        {
            labelTotalWood.Text = manager.Wood.ToString();
            labelTotalClay.Text = manager.Clay.ToString();
            labelTotalIRon.Text = manager.Iron.ToString();
            labelTotalCorp.Text = manager.Crop.ToString();
        }

        // --- Progress Bar and Timer logic ---
        private void StartProgressBar(int seconds)
        {
            totalUpgradeTime = seconds;
            remainingUpgradeTime = seconds;

            progressBarUpgrade.Maximum = seconds;
            progressBarUpgrade.Value = 0;
            labelUpgradeCountdown.Text = $"Starting upgrade ({seconds}s)";

            timerUpgrade.Tick -= TimerUpgrade_Tick; // prevent double event
            timerUpgrade.Tick += TimerUpgrade_Tick;
            timerUpgrade.Start();
        }

        private void TimerUpgrade_Tick(object sender, EventArgs e)
        {
            if (remainingUpgradeTime > 0)
            {
                remainingUpgradeTime--;
                progressBarUpgrade.Value = totalUpgradeTime - remainingUpgradeTime;
                labelUpgradeCountdown.Text = $"Upgrading... {remainingUpgradeTime}s remaining";
            }
            else
            {
                timerUpgrade.Stop();
                progressBarUpgrade.Value = progressBarUpgrade.Maximum;
                labelUpgradeCountdown.Text = "Upgrade complete!";
                buttonUpgrade.Enabled = true;
            }
        }

        // --- Your existing code ---
        public static void saveData(string content)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.DefaultExt = "txt";
            sfd.FileName = "data.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd.FileName;
                File.WriteAllText(filePath, content);
                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timerNormal_Tick(object sender, EventArgs e)
        {
            // Optional: normal production updates here
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Optional: initialize resources or UI here
        }

        private void timerUpgrade_Tick(object sender, EventArgs e)
        {
            TimerUpgrade_Tick(sender, e);
        }
    }
}

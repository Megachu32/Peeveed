using System;
using System.Windows.Forms;

namespace utsBagian1Uts
{
    public class BuildingManager
    {
        public string BuildingType { get; private set; }
        public Button CurrentButton { get; private set; }
        public int Level { get; private set; } = 1;

        public int Wood { get; set; }
        public int Clay { get; set; }
        public int Iron { get; set; }
        public int Crop { get; set; }

        public double SpeedMultiplier { get; set; } = 1.0;

        private int remainingSeconds;
        private Timer externalTimer;

        // Callbacks to communicate with Form1
        public Action<string> OnStatus;
        public Action<string[]> OnCostDisplay;
        public Action<int, Button> OnLevelUp;
        public Action OnResourceUpdate;

        public BuildingManager(Timer timer)
        {
            externalTimer = timer;
            externalTimer.Tick += Timer_Tick;
        }

        public void SelectBuilding(Button btn)
        {
            CurrentButton = btn;

            if (btn.Name.Contains("Tree")) BuildingType = "tree";
            else if (btn.Name.Contains("Clay")) BuildingType = "clay";
            else if (btn.Name.Contains("Iron")) BuildingType = "iron";
            else if (btn.Name.Contains("Crop")) BuildingType = "crop";
            else BuildingType = "";

            Level = int.Parse(btn.Text);
            ShowUpgradeCost();
        }

        private void ShowUpgradeCost()
        {
            if (string.IsNullOrEmpty(BuildingType)) return;

            int[] data = DataBagian1.getUpgradeData(BuildingType, Math.Max(Level, 1));
            string[] costInfo =
            {
                $"Type: {BuildingType.ToUpper()} (Lvl {Level})",
                $"Wood: {data[0]}",
                $"Clay: {data[1]}",
                $"Iron: {data[2]}",
                $"Crop: {data[3]}",
                $"Time: {data[4]} sec"
            };
            OnCostDisplay?.Invoke(costInfo);
        }

        public bool CanUpgrade()
        {
            int[] data = DataBagian1.getUpgradeData(BuildingType, Math.Max(Level, 1));
            return Wood >= data[0] && Clay >= data[1] && Iron >= data[2] && Crop >= data[3];
        }

        public void StartUpgrade()
        {
            if (string.IsNullOrEmpty(BuildingType))
            {
                OnStatus?.Invoke("Please select a building first!");
                return;
            }

            if (!CanUpgrade())
            {
                OnStatus?.Invoke("Not enough resources!");
                return;
            }

            int[] data = DataBagian1.getUpgradeData(BuildingType, Level);
            int time = (int)(data[4] / SpeedMultiplier);

            // Deduct resources
            Wood -= data[0];
            Clay -= data[1];
            Iron -= data[2];
            Crop -= data[3];
            OnResourceUpdate?.Invoke();

            // Start timer
            remainingSeconds = time;
            externalTimer.Start();
            OnStatus?.Invoke($"Upgrading {BuildingType}... {remainingSeconds}s remaining.");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;

            if (remainingSeconds <= 0)
            {
                externalTimer.Stop();
                Level++;
                CurrentButton.Text = Level.ToString();
                OnStatus?.Invoke($"{BuildingType} upgraded to level {Level}!");
                OnLevelUp?.Invoke(Level, CurrentButton);
                ShowUpgradeCost();
            }
            else
            {
                OnStatus?.Invoke($"Upgrading {BuildingType}: {remainingSeconds}s remaining...");
            }
        }
    }
}

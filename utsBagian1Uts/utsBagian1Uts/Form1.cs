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

        }

        private void timerUpgrade_Tick(object sender, EventArgs e)
        {

        }
    }

}

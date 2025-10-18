using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_pv1
{
    public partial class Form1 : Form
    {
        Form oasisForm; // form to display the oasis
        Panel oasisPanel; // panel to hold the oasis reasorce
        Panel[,] oasisResource = new Panel[10,9]; // array of the reasorce for the oasis, 2d to make it easier to manage
        Timer movingTime; // timer to make the reasorce move

        Panel currentPanel; // variable to get the current panel that is clicked so it can be manipulated;
        Panel villagePanel; // variable to get the village panel

        int tempCropAmount = 5000000; // temporary variable crop to make this program actually work
        bool inVillage = false; // to check if the reasorce is moving
        bool noCrop = false; // to check if there is no more crop

        int speedUpMovement = 1; // to increase the speed of the movement

        public Form1()
        {
            InitializeComponent();
            //initialize the oasis
            oasisCreate();
            // plays the oasis
            oasisDisplay();
            this.KeyPreview = true; // to make sure that the form will receive the keydown event
            this.KeyDown += Form2_KeyDown; // when a button is pressed.
        }

        // fungsion for key down event
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1) speedUpMovement = 1; 
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2) speedUpMovement = 2;
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3) speedUpMovement = 3;
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4) speedUpMovement = 4;
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5) speedUpMovement = 5;
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6) speedUpMovement = 6;
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7) speedUpMovement = 7;
            if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8) speedUpMovement = 8;
            if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9) speedUpMovement = 9;
            if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0) speedUpMovement = 10;
        }

        public void oasisCreate()
        {
            Random ran = new Random();

            // inserting the reasorce into the array
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // predetermined where village will be placed
                    if (i == 0 && j ==0)
                    {
                        oasisResource[i, j] = new Panel();
                        oasisResource[i, j].Size = new Size(50, 50);
                        oasisResource[i, j].Location = new Point(50 * i, 50 * j);
                        oasisResource[i, j].BorderStyle = BorderStyle.FixedSingle;
                        oasisResource[i, j].BackColor = Color.Black;
                        villagePanel = oasisResource[i, j]; // assign the village panel
                        continue;
                    }

                    // randomize the reasorce type
                    oasisResource[i, j] = new Panel();
                    oasisResource[i, j].Size = new Size(50, 50);
                    oasisResource[i, j].Location = new Point(50 * i, 50 * j);
                    oasisResource[i, j].BorderStyle = BorderStyle.FixedSingle;
                    int resourceType = ran.Next(1, 11);
                    switch (resourceType)
                    {
                        case 1:
                            oasisResource[i, j].BackColor = Color.Yellow; // croop
                            break;
                        case 2:
                            oasisResource[i, j].BackColor = Color.Red; // clay
                            break;
                        case 3:
                            oasisResource[i, j].BackColor = Color.Green; // wood
                            break;
                        case 4:
                            oasisResource[i, j].BackColor = Color.Gray; // iron
                            break;
                        default :
                            oasisResource[i, j].BackColor = Color.White; // empty
                            break;
                    }
                }
            }

            // temporary variable to hold the oasis resource so it doesn't get affected
            Panel[,] tempOasisResource = oasisResource;

            // make new one every time this function is called
            if (oasisForm == null || oasisForm.IsDisposed)
            {
                oasisForm = new Form()
                {
                    Size = new Size(718, 490),
                };

                oasisForm.FormClosing += (s, e) =>
                {
                    e.Cancel = true;
                    oasisForm.Hide();
                };
            }
            oasisForm.KeyPreview = true; // to make sure that the form will receive the keydown event
            oasisForm.KeyDown += Form2_KeyDown; // when a button is pressed.

            if (oasisPanel == null)
            {
                oasisPanel = new Panel()
                {
                    Size = new Size(500, 450),
                    Location = new Point(0, 0),
                    BorderStyle = BorderStyle.FixedSingle,
                };

                // adding the resources into panel
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        // creating temporary variable to make event click
                        Panel tempPanel = tempOasisResource[i, j];

                        tempPanel.Click += (s, e) =>
                        {
                            // just to check if it's selected 
                            //tempPanel.BorderStyle = BorderStyle.Fixed3D; // change the border style to indicate that it is selected 

                            // gets the current panel that is clicked and moving it to currentPanel variable
                            currentPanel = (Panel)s; // convert the s or sender to panel
                            currentPanel.BringToFront(); // bring the panel to the front so it doesn't get hidden by other panels when it is moving
                            villagePanel.BringToFront(); // bring thw panel to the front
                        };

                        // then adding it to the oasisPanel
                        oasisPanel.Controls.Add(tempPanel);
                    }
                }
            }


            // button to actually pull the resource
            Button getButton = new Button()
            {
                Size = new Size(200, 450),
                Location = new Point(500, 0),
                Text = "GET!!",
            };

            getButton.Click += (s, e) =>
            {
                // change the color of the current panel to gray to indicate that it has been taken
                if (currentPanel != null && currentPanel.BackColor != Color.Black && currentPanel.BackColor != Color.White)
                {
                    inVillage = false; // resource is not in the village
                    noCrop = false; // there is crop to consume

                    // check if there's actualy crop to consume
                    if (tempCropAmount < 500)
                    {
                        MessageBox.Show("No more crop to consume!");
                        noCrop = true; // there is no more crop
                        return;
                    }

                    // timer to make the reasource in the oasis move
                    movingTime = new Timer()
                    {
                        Interval = 100 / speedUpMovement, // 0.05 second
                        Enabled = true,
                    };

                    // timer to consume the crop while the reasorce is moving
                    Timer consumeCropTime = new Timer()
                    {
                        Interval = 1000, // 1 second
                        Enabled = true,
                    };

                    consumeCropTime.Tick += consumeCropTime_Tick;
                    consumeCropTime.Start();

                    movingTime.Tick += movingTime_Tick;
                    movingTime.Start();
                }
            };


            // it's adding more than one object into the oasisForm's control
            oasisForm.Controls.AddRange(new Control[] {
                getButton, oasisPanel
            });
        }

        public void oasisDisplay()
        {
            Form tempOasisForm = oasisForm; // temporary variable to hold the oasis form so it doesn't get affected
            tempOasisForm.Show();
        }

        public void movingTime_Tick(object sender, EventArgs e)
        {
            // stop if there is no more crop
            if (noCrop)
            {
                Timer t = (Timer)sender;
                t.Stop();
                return;
            }
            else
            {
                // chechker if the current panel is at the village
                if (currentPanel.Location.X == 0 && currentPanel.Location.Y == 0)
                {
                    // stop the timer if it is at the village
                    Timer t = (Timer)sender;
                    t.Stop();
                }
                else
                {
                    // do if the current panel is not at the village

                    // move the reasorce to the left and up
                    if (currentPanel.Location.X > 0 && currentPanel.Location.Y > 0) currentPanel.Location = new Point(currentPanel.Location.X - 1, currentPanel.Location.Y - 1);

                    // move only to the left if it is at the top row
                    if (currentPanel.Location.X > 0 && currentPanel.Location.Y == 0) currentPanel.Location = new Point(currentPanel.Location.X - 1, currentPanel.Location.Y);

                    // move only up if it is at the left column
                    if (currentPanel.Location.X == 0 && currentPanel.Location.Y > 0) currentPanel.Location = new Point(currentPanel.Location.X, currentPanel.Location.Y - 1);

                    // stop if it is at the village
                    if (currentPanel.Location.X == 0 && currentPanel.Location.Y == 0)
                    {
                        // stop the timer if it is at the village
                        Timer t = (Timer)sender;
                        t.Stop();
                        inVillage = true; // reasorce is now in the village

                        // use array to check the color of the panel and give the corresponding reasorce
                        Color[] colors = { Color.Yellow, Color.Red, Color.Green, Color.Gray };
                        Color panelColor = currentPanel.BackColor;

                        for (int i = 0; i < colors.Length; i++)
                        {
                            // if it's the same color then give the corresponding reasorce
                            if (panelColor == colors[i])
                            {
                                switch (i)
                                {
                                    case 0:
                                        saveOasisData.CropPercentagePlus += 25; // increase all production by 10%
                                        break;
                                    case 1:
                                        saveOasisData.clayPlus += 1000000; // increase clay by 1000000
                                        break;
                                    case 2:
                                        saveOasisData.woodPlus += 1000000; // increase wood by 1000000
                                        break;
                                    case 3:
                                        saveOasisData.ironPlus += 1000000; // increase iron by 1000000
                                        break;
                                }
                            }
                        }

                        currentPanel.Visible = false;
                    }
                }
            }

        }

        public void consumeCropTime_Tick(object sender, EventArgs e)
        {
            // do nothing if the reasorce is in the village
            if (inVillage)
            {
                Timer t = (Timer)sender;
                t.Stop();
                return;
            }

            // consume the crop while the reasorce is moving
            if (tempCropAmount > 500)
            {
                tempCropAmount -= 500; // consume 1000 crop per second
                Console.WriteLine("Crop Amount: " + tempCropAmount);
            }
            else
            {
                // stop the timer if there is no more crop
                noCrop = true; // there is no more crop
                Timer t = (Timer)sender;
                t.Stop();
                MessageBox.Show("No more crop to consume!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oasisDisplay();
        }
    }
}

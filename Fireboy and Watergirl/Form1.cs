using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;


namespace Fireboy_and_Watergirl
{
    public partial class Form1 : Form
    {
        //creating player 1 and 2
        Rectangle player1 = new Rectangle(25, 410, 25, 25);

        Rectangle player2 = new Rectangle(75, 410, 25, 25);

        //different levels
        Rectangle ground = new Rectangle(0, 435, 450, 17);
        Rectangle lvl2 = new Rectangle(0, 330, 450, 17);
        Rectangle lvl3 = new Rectangle(0, 225, 450, 17);
        Rectangle lvl4 = new Rectangle(0, 120, 450, 17);

        //portal 1
        Rectangle portal1 = new Rectangle(345, 375, 60, 60);
        Rectangle portal1layer1 = new Rectangle(355, 385, 40, 40);
        Rectangle portal1layer2 = new Rectangle(365, 395, 20, 20);

        //portal 2
        Rectangle portal2 = new Rectangle(345, 270, 60, 60);
        Rectangle portal2layer1 = new Rectangle(355, 280, 40, 40);
        Rectangle portal2layer2 = new Rectangle(365, 290, 20, 20);

        //portal 3
        Rectangle portal3 = new Rectangle(345, 165, 60, 60);
        Rectangle portal3layer1 = new Rectangle(355, 175, 40, 40);
        Rectangle portal3layer2 = new Rectangle(365, 185, 20, 20);

        //spike borders
        Rectangle triRec1 = new Rectangle(200, 408, 50, 27);
        Rectangle triRec2 = new Rectangle(200, 198, 50, 27);

        //barrier
        Rectangle barrier = new Rectangle(215, 242, 20, 88);

        //doors
        Rectangle redDoor = new Rectangle(125, 32, 50, 88);
        Rectangle blueDoor = new Rectangle(275, 32, 50, 88);

        //brushes
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush barrierBrush = new SolidBrush(Color.Orange);
        SolidBrush brownBrush = new SolidBrush(Color.SaddleBrown);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush aquaBrush = new SolidBrush(Color.DeepSkyBlue);
        SolidBrush limeBrush = new SolidBrush(Color.OliveDrab);
        SolidBrush goldBrush = new SolidBrush(Color.Gold);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        //movement
        bool rightPressed = false;
        bool leftPressed = false;
        bool dPressed = false;
        bool aPressed = false;

        //random generator
        Random randGen = new Random();
        int randValue = 0;

        //player speeds
        int p1speed = 5;
        int p2speed = 5;
        int trianglespeeds = -4;

        //lists
        List<string> barriercolours = new List<string>();
        List<Rectangle> spikes = new List<Rectangle>();

        //stopwatches
        int stopwatch = 0;
        int barrierChange = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void InitializeGame()
        {
            titleLabel.Text = "";
            subtitleLabel.Text = "";
            stopwatchLabel.Text = "";

            gameTimer.Enabled = true;

            stopwatch = 0;

            player1 = new Rectangle(25, 410, 25, 25);
            player2 = new Rectangle(75, 410, 25, 25);

            pictureBox1.Visible = false;
            pictureBox1.Image = null;

            spikes.Add(triRec1);
            barriercolours.Add("orange");

            spikes.Add(triRec2);
            barriercolours.Add("orange");

            barrier = new Rectangle(215, 242, 20, 88);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Escape:
                    if (gameTimer.Enabled == false)
                    {
                        Application.Exit();
                    }
                    break;
                case Keys.Space:
                    if (gameTimer.Enabled == false)
                    {
                        InitializeGame();
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player 1
            if (aPressed == true && player1.X > 0)
            {
                player1.X = player1.X - p1speed;
            }

            if (dPressed == true && player1.X < 450 - player1.Width)
            {
                player1.X = player1.X + p1speed;
            }

            //move player 2
            if (leftPressed == true && player2.X > 0)
            {
                player2.X = player2.X - p2speed;
            }

            if (rightPressed == true && player2.X < 450 - player2.Width)
            {
                player2.X = player2.X + p2speed;
            }

            //check for collision between spikes and player 1
            for (int i = 0; i < spikes.Count(); i++)
            {
                if (spikes[i].IntersectsWith(player1))
                {
                    player1 = new Rectangle(25, 410, 25, 25);
                }
                else if (spikes[i].IntersectsWith(player1))
                {
                    player1 = new Rectangle(25, 410, 25, 25);
                }

                //check for collision between spikes and player 2
                if (spikes[i].IntersectsWith(player2))
                {
                    player2 = new Rectangle(75, 410, 25, 25);
                }
                else if (spikes[i].IntersectsWith(player2))
                {
                    player2 = new Rectangle(75, 410, 25, 25);
                }
            }

            //check for collision with portals and player 1
            if (player1.IntersectsWith(portal1))
            {
                player1 = new Rectangle(25, 305, 25, 25);
            }
            else if (player1.IntersectsWith(portal2))
            {
                player1 = new Rectangle(25, 200, 25, 25);
            }
            else if (player1.IntersectsWith(portal3))
            {
                player1 = new Rectangle(25, 95, 25, 25);
            }

            //check for collision with portals and player 2
            if (player2.IntersectsWith(portal1))
            {
                player2 = new Rectangle(25, 305, 25, 25);
            }
            else if (player2.IntersectsWith(portal2))
            {
                player2 = new Rectangle(25, 200, 25, 25);
            }
            else if (player2.IntersectsWith(portal3))
            {
                player2 = new Rectangle(25, 95, 25, 25);
            }

            //check for collision between player1 and door
            if (player1.IntersectsWith(blueDoor) && player2.IntersectsWith(redDoor))
            {
                gameTimer.Stop();
            }

            //move spikes
            for (int i = 0; i < spikes.Count(); i++)
            {
                int y = spikes[i].Y + trianglespeeds;

                spikes[i] = new Rectangle(spikes[i].X, y, spikes[i].Width, spikes[i].Height);

                if (spikes[i].IntersectsWith(lvl2) || spikes[i].IntersectsWith(lvl4) || spikes[i].IntersectsWith(ground))
                {
                    trianglespeeds *= -1;
                }
            }
            barrierChange++;

            //random colour for barrier
            if (barrierChange % 50 == 0)
            {
                randValue = randGen.Next(0, 100);

                if (randValue < 20)
                {
                    barrierBrush = new SolidBrush(Color.Orange);

                    
                }
                else if (randValue < 40)
                {
                    barrierBrush = new SolidBrush(Color.Gold);

                }
                else if (randValue < 60)
                {
                    barrierBrush = new SolidBrush(Color.Red);

                    
                }
                else if (randValue < 80)
                {
                    barrierBrush = new SolidBrush(Color.Blue);

                    
                }
                else if (randValue < 100)
                {
                    barrierBrush = new SolidBrush(Color.White);

                    
                }
            }

            //if (player1.IntersectsWith(barrier) && randValue < 20 || player1.IntersectsWith(barrier) && randValue < 40 || player1.IntersectsWith(barrier) && randValue < 80 || player1.IntersectsWith(barrier) && randValue < 100)
            //{
            //    player1.X = 215 - 20;
            //}

            //if (player2.IntersectsWith(barrier) && randValue < 20 || player2.IntersectsWith(barrier) && randValue < 40 || player2.IntersectsWith(barrier) && randValue < 60 || player2.IntersectsWith(barrier) && randValue < 100)
            //{
            //    player2.X = 215 - 20;
            //}

            stopwatch++;

            Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //start screen
            if (gameTimer.Enabled == false && stopwatch == 0)
            {
                titleLabel.Text = "";
                subtitleLabel.Text = "";
                stopwatchLabel.Text = "";
            }
            else if (gameTimer.Enabled == true)
            {
                stopwatchLabel.Text = $"Your Time: {stopwatch}";

                e.Graphics.FillRectangle(blueBrush, player1);
                e.Graphics.FillRectangle(redBrush, player2);

                e.Graphics.FillRectangle(brownBrush, ground);
                e.Graphics.FillRectangle(brownBrush, lvl2);
                e.Graphics.FillRectangle(brownBrush, lvl3);
                e.Graphics.FillRectangle(brownBrush, lvl4);

                e.Graphics.FillRectangle(aquaBrush, portal1);
                e.Graphics.FillRectangle(limeBrush, portal1layer1);
                e.Graphics.FillRectangle(aquaBrush, portal1layer2);

                e.Graphics.FillRectangle(aquaBrush, portal2);
                e.Graphics.FillRectangle(limeBrush, portal2layer1);
                e.Graphics.FillRectangle(aquaBrush, portal2layer2);

                e.Graphics.FillRectangle(aquaBrush, portal3);
                e.Graphics.FillRectangle(limeBrush, portal3layer1);
                e.Graphics.FillRectangle(aquaBrush, portal3layer2);

                e.Graphics.FillRectangle(redBrush, redDoor);
                e.Graphics.FillRectangle(blueBrush, blueDoor);

                e.Graphics.FillRectangle(barrierBrush, barrier);

                for (int i = 0; i < spikes.Count(); i++)
                {
                    Point point1 = new Point(spikes[i].X, spikes[i].Y + spikes[i].Height);
                    Point point2 = new Point(spikes[i].X + spikes[i].Width / 2, spikes[i].Y);
                    Point point3 = new Point(spikes[i].X + spikes[i].Width, spikes[i].Y + spikes[i].Height);

                    Point[] triangle1 = new Point[] { point1, point2, point3 };
                    e.Graphics.FillPolygon(Brushes.White, triangle1);
                }

            }
            else if (gameTimer.Enabled == false && player1.IntersectsWith(blueDoor) && player2.IntersectsWith(redDoor))
            {
                titleLabel.Visible = true;
                titleLabel.Text = $"YOU WIN";
                titleLabel.Text = $"\nYour time was  {stopwatch}s";
                subtitleLabel.Text = "Press Space to Start or Esc to Exit";
                stopwatchLabel.Text = "";
            }
        }
    }
}

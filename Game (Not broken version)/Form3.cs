using Game__Not_broken_version_.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game__Not_broken_version_
{
    public partial class Form3 : Form
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Keys vKeys);

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.SuperMarioBros);
            sound.Play(); //plays sound
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        int time = 399; //starts time at 399
        int lives = 2; //starts lives at 2
        int coins = 0;
        int score = 0;
        bool goombaMoveCheck = false; // If variable is false thats mean go left. If is true go right
        bool goombaDead = false;

        private void goomba() // for moving goomba
        {
            if(goombaMoveCheck == false)
            {
                if(ptrGoomba.Location.X > 380) //checks postition of goomba
                {
                    ptrGoomba.Location = new Point(ptrGoomba.Location.X - 20, ptrGoomba.Location.Y); //moves goomba in its set direction
                }
                else
                {
                    goombaMoveCheck = true;
                }
            }
            else
            {
                if(ptrGoomba.Location.X < 560)
                {
                    ptrGoomba.Location = new Point(ptrGoomba.Location.X + 20, ptrGoomba.Location.Y);  //moves goomba in its set direction
                }
                else
                {
                    goombaMoveCheck= false;
                }
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            time -= 1; //timer goes down
            label8.Text = Convert.ToString(time);
            if (time == 0)
            {
                if (lives == 2)
                {
                    lives -= 1;
                    label10.Text = "1";
                    time = 399;
                }
            }
           if(goombaDead == false)
            {
                goomba();
            }
            move();
        }
        int Y = 0;
        int X = 0;
        bool check = false;
        private void flipImage(string LR)
        {
            if(LR == "Right")
            {
                Image img = Properties.Resources.Mario; //changes mario image
                ptrMario.Image = img;
            }
            else
            {
                Image img = Properties.Resources.marioflipped; //changes mario image
                ptrMario.Image = img;
            }
        }
        bool firstCoin = false;
        bool secondCoin = false;
        char checkCoin = '-'; //public variable only allows 1 action
        char checkMushroom = '0';
        private void move()
        {

            if(check == true)
            {
                ptrMario.Location = new Point(X, Y);
                check = false;
            }
            if(GetAsyncKeyState(Keys.Up) > 0) //when the up arrow is pressed
            { 
                X = ptrMario.Location.X;
                Y = ptrMario.Location.Y;

                ptrMario.Location = new Point(ptrMario.Location.X, ptrMario.Location.Y -70);
                if (ptrMario.Location.X == 280 && ptrMario.Location.Y == 284) // First pipe when wego right side
                {
                    X = 280;
                    Y = 284;
                }
                else if(ptrMario.Location.X == 580 && ptrMario.Location.Y == 284) // second pipe when we go right side
                {
                    X = 580;
                    Y = 284;
                    
                }
                else if(ptrMario.Location.X == 380 && ptrMario.Location.Y == 284) // First pipe when we come from left side
                {
                    X = 380;
                    Y = 284;
                }
                else if(ptrMario.Location.X ==  680 && ptrMario.Location.Y == 284) //second pipe from left side
                {
                    X = 680;
                    Y = 284;
                }
                check = true;

                if(ptrMario.Location.X == 80 && firstCoin == false)
                {
                    ptrCoin.Image = Properties.Resources.Coin; //shows coin
                    pictureBox24.Image = Properties.Resources.BrickBlockBrown; //chnages the question block to be bricks
                    firstCoin = true;
                    coins++; //adds to the coin variable
                    score += 200;
                    labelCoins.Text = Convert.ToString(coins); //converts coin variable to string
                    labelScore.Text = Convert.ToString(score);
                    checkCoin = '+';
                    timer2.Start(); //starts timer2
                    
                }


                if ((ptrMario.Location.X == 140 || ptrMario.Location.X == 160) && checkMushroom == '0')
                {
                    ptrMushroom.Image = Properties.Resources.TDS_Super_Mushroom;
                    pictureBox23.Image = Properties.Resources.BrickBlockBrown;
                    secondCoin = true;
                    timer2.Start();
                }
            }
            if(GetAsyncKeyState(Keys.Right) > 0) //when right arrow is pressed
            {
                if(ptrMario.Location.X == 780 && ptrMario.Location.Y == 354)
                {

                }
                else
                {
                    if (ptrMario.Location.X >= 280 && ptrMario.Location.X < 380) /////////// first pipe
                    {
                        if (ptrMario.Location.Y == 284)
                        {
                            flipImage("Right"); //changes image when mario is facing the right
                            ptrMario.Location = new Point(ptrMario.Location.X + 20, ptrMario.Location.Y);
                            if (ptrMario.Location.X > 360) //checks if marios position is more than 360
                            {
                                ptrMario.Location = new Point(ptrMario.Location.X, 354); //sets a new postion for mario
                                Y = 354; //sets y
                            }
                        }
                    }
                    else if (ptrMario.Location.X >= 580 && ptrMario.Location.X < 680) //checks where mario is
                    {
                        if (ptrMario.Location.Y == 284) //checks marios y position
                        {
                            flipImage("Right"); //flips marios imae to the right
                            ptrMario.Location = new Point(ptrMario.Location.X + 20, ptrMario.Location.Y); //makes a new point/location for mario
                            if (ptrMario.Location.X > 660) //checks if marios location is more than 660
                            {
                                ptrMario.Location = new Point(ptrMario.Location.X, 354); //makes a new point for mario
                                Y = 354; //sets y to 354
                            }
                        }
                    }
                    else
                    {
                        flipImage("Right"); //flips marios image to the right
                        ptrMario.Location = new Point(ptrMario.Location.X, 354); //makes a new point for mario
                        ptrMario.Location = new Point(ptrMario.Location.X + 20, ptrMario.Location.Y); //makes a new point for mario
                    }


                    if(ptrMario.Location.X == 280 && checkMushroom == '?') //checks marios location
                    {
                        checkMushroom = 'G'; //sets the mushrroms variable to G
                        score += 200; //adds 200 to the score
                        labelScore.Text = Convert.ToString(score); //converts the score to sring
                        ptrMushroom.Visible = false; //makes the mushroom visible
                        ptrMario.Width += 5; //sets marios width
                        ptrMario.Height += 5; //sets marios height

                    }
                }

            }

            // First pipe location is 280
            if(GetAsyncKeyState(Keys.Left) > 0) //when left arrow is pressed
            {
                if(ptrMario.Location.X == 0 && ptrMario.Location.Y == 354) //checks marios location
                {

                }
                else
                {
                    if (ptrMario.Location.X <= 380 && ptrMario.Location.X > 280) //First pipe
                    {
                        if (ptrMario.Location.Y == 284) //checks marios position
                        {
                            flipImage("Left"); //flips marios image to the right
                            ptrMario.Location = new Point(ptrMario.Location.X - 20, ptrMario.Location.Y); //makes a new point for mario
                            if (ptrMario.Location.X == 280) //checks marios location
                            {
                                ptrMario.Location = new Point(ptrMario.Location.X, 354); //makes a new location for mario
                                Y = 354; //sets marios y position to 354
                            }
                        }
                    }
                    else if (ptrMario.Location.X <= 680 && ptrMario.Location.X > 580) // Second Pipe
                    {
                        if (ptrMario.Location.Y == 284) //checks marios position
                        {
                            flipImage("Left"); //changes image when mario is facing keft
                            ptrMario.Location = new Point(ptrMario.Location.X - 20, ptrMario.Location.Y); //makes a new location for mario
                            if (ptrMario.Location.X == 580) //checks marios position
                            {
                                ptrMario.Location = new Point(ptrMario.Location.X, 354); //makes a new point for mario
                                Y = 354; //sets marios y position
                            }
                        }
                    }
                    else
                    {
                        flipImage("Left"); //flips marios image 
                        ptrMario.Location = new Point(ptrMario.Location.X, 354); //makes a new locatiion for mario
                        ptrMario.Location = new Point(ptrMario.Location.X - 20, ptrMario.Location.Y); ///makes a new locatiion for mario
                    }
                }

            }
        }
     
        private void timer2_Tick(object sender, EventArgs e) //makes coin jump when mario hits question block
        {
            if(ptrCoin.Location.Y > 151 && checkCoin == '+' && ptrMario.Location.X == 80) //checks the coin variable
            {
                ptrCoin.Location = new Point(ptrCoin.Location.X, ptrCoin.Location.Y - 5); //sets coin position
                if(ptrCoin.Location.Y == 151) //checks coin position
                {
                    checkCoin = '*'; //sets coin variable to +
                }
            }
            else if(ptrCoin.Location.Y < 221 && checkCoin == '*') //checks the coun location
            {
                ptrCoin.Location = new Point(ptrCoin.Location.X, ptrCoin.Location.Y + 5); //makes a new position for coin
                if (ptrCoin.Location.Y == 221) //checks the coins location
                {
                    checkCoin = '?'; //stes coin variable to ?
                    ptrCoin.Visible = false; //sets coin visible variable to false
                    //timer2.Stop();
                }
            }

            if ((ptrMushroom.Location.Y > 151 && checkMushroom == '0') && secondCoin) //checks the mushrooms position and the second coins position
            {
                ptrMushroom.Location = new Point(ptrMushroom.Location.X, ptrMushroom.Location.Y - 5); //checks the mushrooms position
                if (ptrMushroom.Location.Y == 151) //checks the mushrooms location
                {
                    checkMushroom = '1'; //sets the mushroom variable to 1
                }

            }
            else if (ptrMushroom.Location.Y < 370 && checkMushroom == '1') //checks the location and if the mushroom variable is equal to 1
            {
                ptrMushroom.Location = new Point(ptrMushroom.Location.X + 5, ptrMushroom.Location.Y + 8); //mushroom moves
                if (ptrMushroom.Location.Y >= 370) //if the postion is less than 370
                {
                    checkMushroom = '?'; //variable gets set to ? after the mushroom stops moving
                     //timer2.Stop();
                }
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            /*
            if(((ptrMario.Location.X == ptrGoomba.Location.X) && (ptrMario.Location.Y < 354)) || ((ptrMario.Location.X == ptrGoomba.Location.X - 20) && (ptrMario.Location.Y < 354)) || ((ptrMario.Location.X == ptrGoomba.Location.X + 20) && (ptrMario.Location.Y < 354)))
            {
                timer3.Enabled = false;
                MessageBox.Show("Mario Kill the Goomba");
            }

            if (ptrMario.Location.X == ptrGoomba.Location.X || ptrMario.Location.X == ptrGoomba.Location.X - 20 || ptrMario.Location.X == ptrGoomba.Location.X +20) //if mario touches the goomba
            {
                ResetGame();
                MessageBox.Show("Game Over", "Mario Game", MessageBoxButtons.OK, MessageBoxIcon.Hand) ; //shows a game over message
            }
            
            */


            if ((ptrGoomba.Location.X - ptrMario.Location.X) <= 33 || (ptrMario.Location.X - ptrGoomba.Location.X) >= -33)
            {
                timer3.Enabled = false;
                if (ptrMario.Location.Y < 354)
                {
                    MessageBox.Show("Goomba dead...");
                    goombaDead = true;
                    ptrGoomba.Size = new Size(46, 10);
                    ptrGoomba.Location = new Point(ptrMario.Location.X, 390);
                }
                else
                {
                    ResetGame();
                    MessageBox.Show("Mario Dead....");
                }
            }
        }
        private void ResetGame()
        {
            SoundPlayer sound = new SoundPlayer(Properties.Resources.Mario1_GameOver);
            sound.Play(); //plays sound
            if (lives <= 1)
            {
                timer3.Enabled = false;
                MessageBox.Show("See you next time :)", "Mario Game", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
            else
            {
                Thread.Sleep(4000);
                SoundPlayer sound2 = new SoundPlayer(Properties.Resources.SuperMarioBros);
                sound2.Play(); //plays sound
                time = 354; //sets time
                label8.Text = Convert.ToString(time);
                coins = 0;  //sets coins
                labelCoins.Text = Convert.ToString(coins);
                score = 0;  //sets score
                labelScore.Text = Convert.ToString(score);
                lives -= 1;
                label10.Text = Convert.ToString(lives);
                X = 0; //sets x postion
                Y = 0; //sets y postion
                check = false; //mario left and right variable set to false
                flipImage("Right"); //makes mario face to the right
                firstCoin = false; //variable for the first coin that says the coin has not been taken
                secondCoin = false; //variable for the second coin that says the coin has not been taken
                checkCoin = '-'; //actually dont know :(
                checkMushroom = '0'; //tells musroom if its moving or not
                ptrMario.Location = new Point(0, 354); //sets marios x and y positiosn
                firstCoin = false;
                secondCoin = false;
                pictureBox24.Image = Properties.Resources.Question_block;
                pictureBox23.Image = Properties.Resources.Question_block;
                ptrMario.Width = 33;
                ptrMario.Height = 48;
                ptrCoin.Location = new Point(83, 221);
                ptrMushroom.Location = new Point(153, 221);
                ptrCoin.Image = null;
                ptrMushroom.Image = null;
                ptrCoin.Visible = true;
                ptrMushroom.Visible = true;
                timer3.Enabled = true;
                goombaDead = false;
            }
          
        }

    }
}

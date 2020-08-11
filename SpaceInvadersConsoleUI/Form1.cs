using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvadersConsoleUI
{
    public partial class Form1 : Form
    {
        bool goLeft;
        bool goRight;
        int speed = 5;
        int score = 0;
        bool isPressed;
        int totalEnemies = 12;
        int playerSpeed = 6;
        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //reads the key press. Left reads the left key arrow pressed
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            //Right reads the right key arrow pressed
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            //space reads the space bar being pressed
            if(e.KeyCode == Keys.Space && !isPressed)
            {
                isPressed = true;
                makebullet();
            }
            
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //These do the opposite of the above keypressed code. It makes it so you cant spam the buttons
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (isPressed)
            {
                isPressed = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //-= makes the sprite move left
            if (goLeft)
            {
                player.Left -= playerSpeed;
            }
            //+= makes the sprite go right
            if(goRight)
            {
                player.Left += playerSpeed;
            }
            //enemies movement on the form
            foreach(Control x in Controls)
            {

                if (x is PictureBox && x.Tag == "invader")
                {

                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        GameOver();
                    }
                ((PictureBox)x).Left += speed; //moves invaders to the right when the game starts
                    if (((PictureBox)x).Left > 720) //if the picture boxes go over the edge of the frame
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10; //move them down
                        ((PictureBox)x).Left = -50;//start them back on the left side
                    }
                }
            }
            //bullet animation
            foreach (Control y in this.Controls) 
            { 
                if(y is PictureBox && y.Tag == "bullet") //selects the bullet picture box
                {
                    y.Top -= 20; //moves the bullet 20 px per trigger
                    if(((PictureBox)y).Top < this.Height - 490)
                    {
                        this.Controls.Remove(y); //this is to remove the bullet if it reaches the top of the form
                    }
                }
            }
            //bullet and enemy contact
            foreach (Control i in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && i.Tag == "invader") //if invader
                    {
                        if(j is PictureBox && j.Tag == "bullet") //if bullet
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds)) //if they are within each others bounds
                            {
                                score++; //score up 1
                                this.Controls.Remove(i); //remove invader
                                this.Controls.Remove(j); //remove bullet
                            }
                        }
                    }
                }
            }
            label1.Text = "Score : " + score;
            if (score > totalEnemies - 1)
            {
                GameOver();
                MessageBox.Show("You Saved Earth!");
            }
        }

        private void makebullet()
        {//this method makes the bullet spawn
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.bullet;
            bullet.Size = new Size(5, 20);
            bullet.Tag = "bullet";
            bullet.Left = player.Left + player.Width / 2;
            bullet.Top = player.Top - 20;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }
        private void GameOver()
        {//this function displayes the Game over text
            timer1.Stop();
            label1.Text += "Game Over!";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

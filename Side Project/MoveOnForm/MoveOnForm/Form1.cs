using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoveOnForm
{
    public partial class Form1 : Form
    {
        bool goLeft = false;
        bool goRight = false;
        bool goUp = false;
        bool goDown = false;

        bool slideLeft = true;
        public Form1()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = true;
            if (e.KeyCode == Keys.D) goRight = true;
            if (e.KeyCode == Keys.W) goUp = true;
            if (e.KeyCode == Keys.S) goDown = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = false;
            if (e.KeyCode == Keys.D) goRight = false;
            if (e.KeyCode == Keys.W) goUp = false;
            if (e.KeyCode == Keys.S) goDown = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (goLeft) MovePlayer("Left");
            if (goRight) MovePlayer("Right");
            if (goUp) MovePlayer("Up");
            if (goDown) MovePlayer("Down");

            Slider();
        }
        private void Slider()
        {
            if (slideLeft)
            {
                Slide.Left -= 10;
                if (Slide.Left <= 100) slideLeft = false;
                if(Slide.Bounds.IntersectsWith(Player.Bounds)) 
                    Player.Left = Slide.Left - Player.Width;
            }
            else if (!slideLeft)
            {
                Slide.Left += 10;
                if (Slide.Right >= this.Width - 100) slideLeft = true;
                if (Slide.Bounds.IntersectsWith(Player.Bounds))
                    Player.Left = Slide.Right;
            }
        }
        private void MovePlayer(string direction)
        {
            switch (direction)
            {
                case "Left": 
                    Player.Left -= 5; 
                    break;
                case "Right": 
                    Player.Left += 5;
                    break;
                case "Up": 
                    Player.Top -= 5;
                    break;
                case "Down": 
                    Player.Top += 5;
                    break;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "Wall" && Player.Bounds.IntersectsWith(x.Bounds))
                {
                    switch (direction)
                    {
                        case "Left":
                            Player.Left = x.Right;
                            break;
                        case "Right":
                            Player.Left = x.Left - Player.Width;
                            break;
                        case "Up":
                            Player.Top = x.Bottom;
                            break;
                        case "Down":
                            Player.Top = x.Top - Player.Height;
                            break;
                    }
                    
                }
            }
        }
    }
}

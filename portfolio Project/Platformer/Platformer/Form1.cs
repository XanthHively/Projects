using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Platformer.Classes;

namespace Platformer
{
    public partial class Platformer : Form
    {
        PlayerClass playerClass = new PlayerClass();

        bool goLeft = false;
        bool goRight = false;

        bool jump = false;
        int velocity = 100;
        bool OnGround = false;

        bool slideUp = true;
        public Platformer()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }
        private void IsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = true;
            if (e.KeyCode == Keys.D) goRight = true;
            if (e.KeyCode == Keys.Space) jump = true;
        }
        private void IsKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) goLeft = false;
            if (e.KeyCode == Keys.D) goRight = false;
            if (e.KeyCode == Keys.Space) jump = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (goLeft) MovePlayer("Left");
            if (goRight) MovePlayer("Right");
            JumpCheck();
            Slider();
            AdjustWorld();
        }
        private void AdjustWorld()
        {
            if(Player.Left < 400)
            {
                foreach (Control x in Controls)
                {
                    if (x is PictureBox && x.Tag == "Wall" && goLeft) x.Left += 8;
                }
            }
            if(Player.Right > Width - 400)
            {
                foreach (Control x in Controls)
                {
                    if (x is PictureBox && x.Tag == "Wall" && goRight) x.Left -= 8;
                }
            }
        }
        private void Slider()
        {
            if (slideUp)
            {
                Slide.Top -= 5;
                if (Slide.Top < 100) slideUp = false;
                if (Slide.Bounds.IntersectsWith(Player.Bounds))
                    Player.Top = Slide.Top - Player.Height;
            }
            else if (!slideUp)
            {
                Slide.Top += 5;
                if (Slide.Bottom > this.Height - 100) slideUp = true;
            }
        }
        private void JumpCheck()
        {
            if(OnGround && jump)
            {
                velocity = 200;
                OnGround = false;
            }
            else if (OnGround)
            {
                Player.Top += 1;
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "Wall")
                    {
                        if (Player.Bounds.IntersectsWith(x.Bounds))
                        {
                            Player.Top -= 1;
                        }
                        else
                        {
                            OnGround = false;
                            velocity = 99;
                        }
                    }
                }
            }
            else if(velocity > 150 && !OnGround)
            {
                Player.Top -= 12;
                velocity -= 8;
                JumpColide("Up");
            }
            else if(velocity > 100 && !OnGround)
            {
                Player.Top -= 6;
                velocity -= 8;
                JumpColide("Up");
            }
            else if(velocity > 50 && !OnGround)
            {
                Player.Top += 6;
                velocity -= 8;
                JumpColide("Down");
            }
            else if(velocity > 0 && !OnGround)
            {
                Player.Top += 12;
                velocity -= 8;
                JumpColide("Down");
            }
            else if (!OnGround)
            {
                Player.Top += 16;
                JumpColide("Down");
            }
        }
        private void JumpColide(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "Wall" && Player.Bounds.IntersectsWith(x.Bounds))
                {
                    switch (direction)
                    {
                        case "Up":
                            Player.Top = x.Bottom;
                            velocity = 100;
                            break;
                        case "Down":
                            Player.Top = x.Top - Player.Height;
                            OnGround = true;
                            break;
                    }
                }
            }
        }
        private void MovePlayer(string direction)
        {
            switch (direction)
            {
                case "Left":
                    if (Player.Left > 400) Player.Left -= 8;
                    break;
                case "Right":
                    if (Player.Right < Width - 400) Player.Left += 8;
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
                    }

                }
            }
        }
    }
}

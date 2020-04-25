using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Textures;

namespace MyGame.Sprites
{
    public class Player : Sprite
    {
        public Vector2 VerticalPosition;
        public Vector2 HorizontalPosition;
        public float Speed;

        bool wasMovingLeft = false;
        int jumpVelocity = 0;
        public int jumpPower;
        bool onGround = false;

        TextureAtlas atlas;

        int CoordinatesX = 0;

        private int playerWidth = 60;
        private int playerHeight = 155;
        private int spawnX = 930;
        private int spawnY = 855;
        public override Rectangle BoundingBox
        {
            get{ return new Rectangle((int)Position.X, (int)Position.Y, playerWidth, playerHeight); }
        }
        public string Coordinates
        {
            get { return $"X:{CoordinatesX/50} Y:{-(int)(Position.Y - spawnY)/50}"; }
        }

        public Player(Texture2D Player_Atlas)
      : base(Player_Atlas)
        {
            Position = new Vector2(spawnX, spawnY);
            Speed = 7;
            jumpPower = 25;
            SetColor = Color.Black;
            atlas = new TextureAtlas(Player_Atlas, 2, 13);
        }

        public Vector2 Update(List<Sprite> sprites)
        {
            HorizontalPosition = Vector2.Zero;
            Move();
            Jump();

            foreach (var sprite in sprites)
            {
                if (this.HorizontalPosition.X > 0 && this.IsTouchingRight(sprite))
                    this.HorizontalPosition.X = sprite.BoundingBox.Left - this.BoundingBox.Right;

                if (this.HorizontalPosition.X < 0 && this.IsTouchingLeft(sprite))
                    this.HorizontalPosition.X = sprite.BoundingBox.Right - this.BoundingBox.Left;

                if (this.VerticalPosition.Y < 0 && this.IsTouchingTop(sprite))
                {
                    this.VerticalPosition.Y = sprite.BoundingBox.Bottom - this.BoundingBox.Top;
                    jumpVelocity = 0;
                }

                if(!onGround && this.VerticalPosition.Y > 0 && this.IsTouchingBottom(sprite))
                {
                    this.VerticalPosition.Y = sprite.BoundingBox.Top - this.BoundingBox.Bottom;
                    onGround = true;
                }
                else if(onGround && this.VerticalPosition.Y > 0 && !this.IsTouchingBottom(sprite))
                {
                    jumpVelocity = 0;
                    onGround = false;
                }
                else if(onGround)
                {
                    this.VerticalPosition.Y = 0;
                }
            }

            Position += VerticalPosition;

            VerticalPosition = Vector2.Zero;

            CoordinatesX += (int)HorizontalPosition.X;
            return HorizontalPosition;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            atlas.Draw(spriteBatch, Position);
        }
        private void Move()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                HorizontalPosition.X = -Speed;
                wasMovingLeft = true;
                if (onGround)
                    atlas.Update(13, 11);
                else atlas.Update(25, 1);
            }
               
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                HorizontalPosition.X = Speed;
                wasMovingLeft = false;
                if (onGround)
                    atlas.Update(0, 11);
                else atlas.Update(12, 1);
            }
                
            if (onGround && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                jumpVelocity = -jumpPower;
                onGround = false;
                if(wasMovingLeft)
                    atlas.Update(25, 1);
                else atlas.Update(12, 1);
            }
            
            if(onGround && Keyboard.GetState().IsKeyUp(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.A))
            {
                if (wasMovingLeft)
                    atlas.Update(24, 1);
                else atlas.Update(11, 1);
            }
        }
        private void Jump()
        {
            if (onGround)
            {
                VerticalPosition.Y = 1;
            }
            else
            {
                VerticalPosition.Y = jumpVelocity;
                jumpVelocity++;
            }
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.BoundingBox.Right + this.HorizontalPosition.X > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Left &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Bottom;
        }

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.BoundingBox.Left + this.HorizontalPosition.X < sprite.BoundingBox.Right &&
              this.BoundingBox.Right > sprite.BoundingBox.Right &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Bottom;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.BoundingBox.Bottom + this.VerticalPosition.Y > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Top &&
              this.BoundingBox.Right > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Right;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.BoundingBox.Top + this.VerticalPosition.Y < sprite.BoundingBox.Bottom &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Bottom &&
              this.BoundingBox.Right > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Right;
        }
    }
}

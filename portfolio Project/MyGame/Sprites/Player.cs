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
        private Vector2 PlayerPosition;
        TextureAtlas atlas;

        int CoordinatesX = 0;
        int CoordinatesY = 0;

        private int jumpVelocity = 0;
        private bool wasMovingLeft = false;
        private bool onGround = false;

        private const int jumpPower = 25;
        private const float Speed = 7;
        private const int playerWidth = 60;
        private const int playerHeight = 155;
        private const int spawnX = 930;
        private const int spawnY = 500;
        public override Rectangle BoundingBox
        {
            get{ return new Rectangle((int)Position.X, (int)Position.Y, playerWidth, playerHeight); }
        }
        public string Coordinates
        {
            get { return $"X:{CoordinatesX/50} Y:{-(CoordinatesY/50)}"; }
        }

        public Player(Texture2D Player_Atlas)
      : base(Player_Atlas)
        {
            //base sprite class properties
            Position = new Vector2(spawnX, spawnY);
            SetColor = Color.Black;

            atlas = new TextureAtlas(Player_Atlas, 2, 13);
        }

        public Vector2 Update(List<Sprite> sprites)
        {
            PlayerPosition = Vector2.Zero;
            Move();
            Jump();

            foreach (var sprite in sprites)
            {
                if (this.PlayerPosition.X > 0 && this.IsTouchingRight(sprite))
                    this.PlayerPosition.X = sprite.BoundingBox.Left - this.BoundingBox.Right;

                if (this.PlayerPosition.X < 0 && this.IsTouchingLeft(sprite))
                    this.PlayerPosition.X = sprite.BoundingBox.Right - this.BoundingBox.Left;

                if (this.PlayerPosition.Y < 0 && this.IsTouchingTop(sprite))
                {
                    this.PlayerPosition.Y = sprite.BoundingBox.Bottom - this.BoundingBox.Top;
                    jumpVelocity = 0;
                }

                if(!onGround && this.PlayerPosition.Y > 0 && this.IsTouchingBottom(sprite))
                {
                    this.PlayerPosition.Y = sprite.BoundingBox.Top - this.BoundingBox.Bottom;
                    onGround = true;
                }
                else if(onGround && this.PlayerPosition.Y > 0 && !this.IsTouchingBottom(sprite))
                {
                    jumpVelocity = 0;
                    onGround = false;
                }
                else if(onGround)
                {
                    this.PlayerPosition.Y = 0;
                }
            }

            Vector2 playerOffset = PlayerPosition;

            CoordinatesX += (int)PlayerPosition.X;
            CoordinatesY += (int)PlayerPosition.Y;

            return playerOffset;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            atlas.Draw(spriteBatch, Position);
        }
        private void Move()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                PlayerPosition.X = -Speed;
                wasMovingLeft = true;
                if (onGround)
                    atlas.Update(13, 11);
                else atlas.Update(25, 1);
            }
               
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlayerPosition.X = Speed;
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
                PlayerPosition.Y = 1;
            }
            else
            {
                PlayerPosition.Y = jumpVelocity;
                if(jumpVelocity < 30)
                    jumpVelocity++;
            }
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.BoundingBox.Right + this.PlayerPosition.X > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Left &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Bottom;
        }

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.BoundingBox.Left + this.PlayerPosition.X < sprite.BoundingBox.Right &&
              this.BoundingBox.Right > sprite.BoundingBox.Right &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Bottom;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.BoundingBox.Bottom + this.PlayerPosition.Y > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Top &&
              this.BoundingBox.Right > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Right;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.BoundingBox.Top + this.PlayerPosition.Y < sprite.BoundingBox.Bottom &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Bottom &&
              this.BoundingBox.Right > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Right;
        }
    }
}

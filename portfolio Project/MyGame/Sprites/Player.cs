using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;

namespace MyGame.Sprites
{
    public class Player : Sprite
    {
        public Vector2 Velocity;
        public float Speed;
        public Player(Texture2D texture)
      : base(texture)
        {

        }

        public void Update(List<Sprite> sprites)
        {
            Move();

            foreach (var sprite in sprites)
            {
                /*
                if (this.Velocity.X > 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = sprite.BoundingBox.Left - this.BoundingBox.Right;

                if (this.Velocity.X < 0 && this.IsTouchingLeft(sprite))
                    this.Velocity.X = this.BoundingBox.Left - sprite.BoundingBox.Right;

                if (this.Velocity.Y > 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = sprite.BoundingBox.Top - this.BoundingBox.Bottom;

                if (this.Velocity.Y < 0 && this.IsTouchingTop(sprite))
                    this.Velocity.Y = this.BoundingBox.Top - sprite.BoundingBox.Bottom;
                */
                if ((this.Velocity.X > 0 && this.IsTouchingRight(sprite)) || (this.Velocity.X < 0 && this.IsTouchingLeft(sprite)))
                    this.Velocity.X = 0;

                if ((this.Velocity.Y > 0 && this.IsTouchingBottom(sprite)) || (this.Velocity.Y < 0 && this.IsTouchingTop(sprite)))
                    this.Velocity.Y = 0;
            }

            Position += Velocity;

            Velocity = Vector2.Zero;
        }

        private void Move()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                Velocity.X = Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                Velocity.Y = Speed;
        }
        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.BoundingBox.Right + this.Velocity.X > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Left &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Bottom;
        }

        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.BoundingBox.Left + this.Velocity.X < sprite.BoundingBox.Right &&
              this.BoundingBox.Right > sprite.BoundingBox.Right &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Bottom;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.BoundingBox.Bottom + this.Velocity.Y > sprite.BoundingBox.Top &&
              this.BoundingBox.Top < sprite.BoundingBox.Top &&
              this.BoundingBox.Right > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Right;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.BoundingBox.Top + this.Velocity.Y < sprite.BoundingBox.Bottom &&
              this.BoundingBox.Bottom > sprite.BoundingBox.Bottom &&
              this.BoundingBox.Right > sprite.BoundingBox.Left &&
              this.BoundingBox.Left < sprite.BoundingBox.Right;
        }
    }
}

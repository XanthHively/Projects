using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MyGame.Sprites
{
    public class Sprite
    {
        protected Texture2D texture;
        public Vector2 Position;
        public Color Colour = Color.White;

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Colour);
        }
    }
}

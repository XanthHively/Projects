using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGame.Input
{
    public class MouseInput
    {
        private MouseState oldState;
        public float x,y;
        public void Update()
        {
            MouseState newState = Mouse.GetState();

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                x = newState.X;
                y = newState.Y;
            }

            oldState = newState;
        }
    }
}

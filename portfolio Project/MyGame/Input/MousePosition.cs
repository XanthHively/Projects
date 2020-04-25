using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGame.Input
{
    public class MousePosition
    {
        public float x,y;
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            x = mouseState.X;
            y = mouseState.Y;
        }
    }
}

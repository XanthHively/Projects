using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;

namespace MyGame.Menu
{
    public class ExitMenu
    {
        Sprite exit;

        bool openMenu = false;
        public ExitMenu(Sprite exit)
        {
            this.exit = exit;
        }
    }
}

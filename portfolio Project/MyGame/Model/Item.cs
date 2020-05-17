using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;

namespace MyGame.Model
{
    public class Item
    {
        public Sprite ItemSprite { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Index { get; set; }
        //public bool InHand { get; set; }
        public Item(Texture2D texture, string name, int index, Vector2 position)
        {
            ItemSprite = new Sprite(texture) { Position = position};
            Name = name;
            Index = index;
            Quantity = 1;
        }
        public void SetIndex(int index, Vector2 position)
        {
            Index = index;
            //InHand = false;
            ItemSprite.Position = position;
        }
    }
}

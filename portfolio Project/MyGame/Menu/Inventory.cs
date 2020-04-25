using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;
using MyGame.Textures;
using MyGame.Model;

namespace MyGame.Menu
{
    public class Inventory
    {
        Sprite inventorySprite;
        List<Sprite> selectSprites = new List<Sprite>();
        List<Item> items = new List<Item>();
        Dictionary<string,Texture2D> itemTextures;

        SpriteFont text;

        private bool oldStateE, oldStateClick;
        private bool itemInHand = false;
        int inHandIndex = 0;
        bool Open = false;
        public Inventory(Sprite inventorySprite,Sprite hotbarSprite,Texture2D inventorySelect, Dictionary<string, Texture2D> itemTextures, SpriteFont text)
        {
            this.inventorySprite = inventorySprite;
            this.itemTextures = itemTextures;
            this.text = text;
            SetSelectBoxLocations(inventorySelect);

            items.Add(new Item(itemTextures["Log"], "Log", 3, selectSprites[3].Position) { Quantity = 50 });
        }
        public void Update()
        {
            bool newStateE = Keyboard.GetState().IsKeyDown(Keys.E);
            //open close inventory
            if (newStateE && !oldStateE)
            {
                if (Open)
                {
                    Open = false;
                    if (itemInHand)
                    {
                        itemInHand = false;
                        foreach(Item item in items)
                        {
                            if(item.InHand == true)
                            {
                                item.InHand = false;
                                if (items.Exists(x => x.Index == inHandIndex))
                                {
                                    items.Find(x => x.Index == inHandIndex).Quantity += item.Quantity;
                                    items.Remove(item);
                                }
                                else item.SetIndex(inHandIndex, selectSprites[inHandIndex].Position);
                                break;
                            }
                        }
                    }
                }
                else Open = true;
            }
            oldStateE = newStateE;
            //move items around inventory
            if (Open)
            {
                MouseState mouseState = Mouse.GetState();
                bool leftClick = mouseState.LeftButton == ButtonState.Pressed;
                bool rightClick = mouseState.RightButton == ButtonState.Pressed;
                bool newStateClick = leftClick || rightClick;
                if (newStateClick && !oldStateClick)
                {
                    for(int i = 0;i < selectSprites.Count; i++)
                    {
                        if (selectSprites[i].BoundingBox.Left < mouseState.X && selectSprites[i].BoundingBox.Right > mouseState.X && selectSprites[i].BoundingBox.Top < mouseState.Y && selectSprites[i].BoundingBox.Bottom > mouseState.Y)
                        {
                            if (itemInHand)
                            {
                                if(!items.Exists(x => x.Index == i))//empty slot with item in hand
                                {
                                    if (leftClick)
                                    {
                                        items.Find(x => x.InHand == true).SetIndex(i, selectSprites[i].Position);
                                        itemInHand = false;
                                    }
                                    else//rightclick
                                    {
                                        foreach(Item item in items)
                                        {
                                            if (item.InHand == true)
                                            {
                                                items.Add(new Item(itemTextures[item.Name], item.Name, i, selectSprites[i].Position));
                                                item.Quantity--;
                                                if(item.Quantity == 0)
                                                {
                                                    items.Remove(item);
                                                    itemInHand = false;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                else//acupied slot with item in hand
                                {
                                    if (leftClick)
                                    {
                                        items.Find(x => x.Index == i).Quantity += items.Find(x => x.InHand == true).Quantity;
                                        items.Remove(items.Find(x => x.InHand == true));
                                        itemInHand = false;
                                    }
                                    else//rightclick
                                    {
                                        items.Find(x => x.Index == i).Quantity++;
                                        foreach(Item item in items)
                                        {
                                            if(item.InHand == true)
                                            {
                                                item.Quantity--;
                                                if (item.Quantity == 0)
                                                {
                                                    items.Remove(item);
                                                    itemInHand = false;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else//empty hand
                            {
                                if (items.Exists(x => x.Index == i))
                                {
                                    if (leftClick)//take full stack
                                    {
                                        foreach (Item item in items)
                                        {
                                            if (item.Index == i)
                                            {
                                                item.InHand = true;
                                                inHandIndex = i;
                                                item.Index = -1;
                                                break;
                                            }
                                        }
                                        itemInHand = true;
                                    }
                                    else//rightclick take half stack
                                    {
                                        foreach (Item item in items)
                                        {
                                            if(item.Index == i)
                                            {
                                                if(item.Quantity == 1)
                                                {
                                                    item.InHand = true;
                                                    itemInHand = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    int newStackCount = item.Quantity / 2;
                                                    item.Quantity -= newStackCount;
                                                    items.Add(new Item(itemTextures[item.Name], item.Name, i, selectSprites[i].Position) 
                                                    { Quantity = newStackCount,InHand = true});
                                                    itemInHand = true;
                                                    inHandIndex = i;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                oldStateClick = newStateClick;

                if (itemInHand) items.Find(x => x.InHand == true).ItemSprite.Position = new Vector2(mouseState.X, mouseState.Y);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Open)
            {
                inventorySprite.Draw(spriteBatch);

                MouseState mouseState = Mouse.GetState();
                foreach (Sprite box in selectSprites)
                {
                    if (box.BoundingBox.Left < mouseState.X && box.BoundingBox.Right > mouseState.X && box.BoundingBox.Top < mouseState.Y && box.BoundingBox.Bottom > mouseState.Y)
                    {
                        box.Draw(spriteBatch);
                        break;
                    }
                }
                if (items.Count > 0)
                {
                    foreach (Item item in items)
                        if (!item.InHand) DrawItem(item, spriteBatch);
                    foreach (Item item in items)
                        if (item.InHand) DrawItem(item, spriteBatch);
                }
            }
        }
        private void DrawItem(Item item, SpriteBatch spriteBatch)
        {
            item.ItemSprite.Draw(spriteBatch);
            if (item.Quantity > 1)
                spriteBatch.DrawString(text, item.Quantity.ToString(), item.ItemSprite.Position + new Vector2(40, 42), Color.Black);
        }
        private void SetSelectBoxLocations(Texture2D inventorySelect)
        {
            //ginerate inventory slots on startup
            int verticalOffset = 0;
            for(int i = 0; i < 3; i++)
            {
                if (i == 0) verticalOffset = 145;
                else if (i == 1) verticalOffset = 10;
                else if (i == 2) verticalOffset = 75;

                int horizontalOffset = 10;
                for (int j = 0; j < 9; j++)
                {
                    selectSprites.Add(new Sprite(inventorySelect) { 
                        Position = inventorySprite.Position + new Vector2(horizontalOffset, verticalOffset), 
                        SetColor = Color.White * .2f 
                    });
                    horizontalOffset += 65;
                }
            }
        }
        private void SetItemLocations()
        {
            //set items to apropriate slots on startup
            if (items.Count > 0)
            {
                foreach (Item item in items)
                {
                    item.ItemSprite.Position = selectSprites[item.Index].Position;
                }
            }
        }
    }
}

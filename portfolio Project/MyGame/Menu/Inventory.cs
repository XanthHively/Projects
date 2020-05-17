using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;
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

        private bool areWeHoldingItem = false;
        Item itemInHand;
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
                    if (areWeHoldingItem)
                    {
                        addItemToFirstAvailableSlot(itemInHand);
                        areWeHoldingItem = false;
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

                //trigers oance when click is detected
                if (newStateClick && !oldStateClick)
                {
                    for (int selectedIndex = 0; selectedIndex < selectSprites.Count; selectedIndex++)
                    {
                        //is cursor over this inventory slot
                        if (selectSprites[selectedIndex].BoundingBox.Left < mouseState.X && selectSprites[selectedIndex].BoundingBox.Right > mouseState.X 
                            && selectSprites[selectedIndex].BoundingBox.Top < mouseState.Y && selectSprites[selectedIndex].BoundingBox.Bottom > mouseState.Y)
                        {
                            if (areWeHoldingItem) //--------------item in hand when player clicked--------------------------------------------//
                            {
                                Item selectedItem = items.Find(x => x.Index == selectedIndex);
                                if (leftClick)
                                {
                                    //is there an item in this slot
                                    if (selectedItem != null && selectedItem.Name == itemInHand.Name)
                                    {
                                        selectedItem.Quantity += itemInHand.Quantity;
                                        areWeHoldingItem = false;
                                    }
                                    else if (selectedItem == null) //no item in slot
                                    {
                                        itemInHand.SetIndex(selectedIndex, selectSprites[selectedIndex].Position);
                                        items.Add(itemInHand);
                                        areWeHoldingItem = false;
                                    }
                                }
                                if (rightClick)
                                {
                                    //is there an item in this slot
                                    if (selectedItem != null && selectedItem.Name == itemInHand.Name)
                                    {
                                        selectedItem.Quantity += 1;
                                        itemInHand.Quantity -= 1;
                                        if(itemInHand.Quantity == 0)
                                            areWeHoldingItem = false;
                                    }
                                    else if (selectedItem == null) //no item in slot
                                    {
                                        items.Add(new Item(itemTextures[itemInHand.Name], itemInHand.Name, selectedIndex, selectSprites[selectedIndex].Position)
                                        { Quantity = 1 });
                                        itemInHand.Quantity -= 1;
                                        if (itemInHand.Quantity == 0)
                                            areWeHoldingItem = false;
                                    }
                                }
                            }
                            else //----------------------- no item in hand when player clicked------------------------------------------//
                            {
                                //is there an item in this slot
                                if (items.Exists(x => x.Index == selectedIndex))
                                {
                                    Item selectedItem = items.Find(x => x.Index == selectedIndex);
                                    areWeHoldingItem = true;
                                    if (leftClick)
                                    {
                                        itemInHand = selectedItem;
                                        items.Remove(itemInHand);
                                    }
                                    if (rightClick)
                                    {
                                        if (selectedItem.Quantity == 1)
                                        {
                                            itemInHand = selectedItem;
                                            items.Remove(itemInHand);
                                        }
                                        else
                                        {
                                            int newStackCount = selectedItem.Quantity / 2;
                                            selectedItem.Quantity -= newStackCount;
                                            itemInHand = new Item(itemTextures[selectedItem.Name], selectedItem.Name, 99, selectSprites[selectedIndex].Position)
                                            { Quantity = newStackCount };
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                oldStateClick = newStateClick;

                if (areWeHoldingItem) itemInHand.ItemSprite.Position = new Vector2(mouseState.X, mouseState.Y);
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
                foreach (Item item in items)
                    DrawItem(item, spriteBatch);
                if(areWeHoldingItem)
                    DrawItem(itemInHand, spriteBatch);
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
        public void addItemToFirstAvailableSlot(Item item)
        {
            for (int i = 0; i < selectSprites.Count; i++)
            {
                Item selectedItem = items.Find(x => x.Index == i);
                if (selectedItem != null && selectedItem.Name == item.Name)
                {
                    selectedItem.Quantity += item.Quantity;
                    break;
                }
                else if (selectedItem == null) //no item in slot
                {
                    item.SetIndex(i, selectSprites[i].Position);
                    items.Add(item);
                    break;
                }
            }
        }
    }
}

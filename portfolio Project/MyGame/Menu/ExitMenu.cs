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
        Sprite quitMenu;
        Sprite quitBackground;
        Sprite selectYes;
        Sprite selectNo;
        bool selectYesActive;
        bool selectNoActive;

        private bool oldState1,oldState2 = true;
        public bool Open = false;
        public ExitMenu(Sprite quitMenuSprite, Sprite quitBackgroundSprite, Texture2D quitSelectTexture)
        {
            quitMenu = quitMenuSprite;
            quitBackground = quitBackgroundSprite;
            selectYes = new Sprite(quitSelectTexture) { Position = quitMenu.Position + new Vector2(128, 163) };
            selectNo = new Sprite(quitSelectTexture) { Position = quitMenu.Position + new Vector2(282, 163) };
        }
        public bool Update()
        {
            bool newState1 = Keyboard.GetState().IsKeyDown(Keys.Escape);
            if (Open || (newState1 && !oldState1))
            {
                Open = true;
                bool newState2 = Keyboard.GetState().IsKeyDown(Keys.Escape);

                if (newState2 && !oldState2)
                {
                    Open = false;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    return true;
                }

                oldState2 = newState2;
            }
            oldState1 = newState1;
            return MouseSelect();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Open)
            {
                quitBackground.Draw(spriteBatch);
                quitMenu.Draw(spriteBatch);
                if (selectYesActive) selectYes.Draw(spriteBatch);
                if (selectNoActive) selectNo.Draw(spriteBatch);
            }
        }
        public bool MouseSelect()
        {
            MouseState mouseState = Mouse.GetState();
            if (Open)
            {
                if (selectYes.BoundingBox.Left < mouseState.X && selectYes.BoundingBox.Right > mouseState.X && selectYes.BoundingBox.Top < mouseState.Y && selectYes.BoundingBox.Bottom > mouseState.Y)
                {
                    selectYesActive = true;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                        return true;
                }
                else selectYesActive = false;
                if (selectNo.BoundingBox.Left < mouseState.X && selectNo.BoundingBox.Right > mouseState.X && selectNo.BoundingBox.Top < mouseState.Y && selectNo.BoundingBox.Bottom > mouseState.Y)
                {
                    selectNoActive = true;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Open = false;
                        oldState2 = true;
                    }
                }
                else selectNoActive = false;
            }
            return false;
        }
    }
}

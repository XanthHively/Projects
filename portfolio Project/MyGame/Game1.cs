using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using MyGame.Menu;
using MyGame.Textures;

namespace MyGame
{
    public class Game1 : Game
    {
        List<Sprite> solid_Sprites;
        List<Sprite> decoration_Sprites;
        Dictionary<string, Texture2D> items;
        Player player;

        ExitMenu exitMenu;
        Inventory inventory;

        SpriteFont text;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            text = Content.Load<SpriteFont>("Text");

            Texture2D player_Atlas_Texture = Content.Load<Texture2D>("Player_Atlas");
            Texture2D chunk_Of_Stone_Texture = Content.Load<Texture2D>("chunk");
            Texture2D ground_Grass_Texture = Content.Load<Texture2D>("Grass");
            Texture2D cloud_Texture = Content.Load<Texture2D>("Cloud");
            Texture2D quit_Menu_Texture = Content.Load<Texture2D>("Menu/Quit");
            Texture2D quit_Menu_Select_Texture = Content.Load<Texture2D>("Menu/QuitSelectBox");
            Texture2D quit_Background = Content.Load<Texture2D>("Menu/ExitBackground");
            Texture2D inventory_texture = Content.Load<Texture2D>("Menu/Inventory");
            Texture2D inventory_Select_Texture = Content.Load<Texture2D>("Menu/InventorySelect");
            Texture2D hotbar_Texture = Content.Load<Texture2D>("Menu/Hotbar");

            items = new Dictionary<string, Texture2D>()
            {
                {"Log", Content.Load<Texture2D>("items/itemLog") }
            };

            player = new Player(player_Atlas_Texture);

            solid_Sprites = new List<Sprite>()
            {
                new Sprite(chunk_Of_Stone_Texture){Position = new Vector2(1100, 850)},
                new Sprite(chunk_Of_Stone_Texture){Position = new Vector2(1400, 550)},
                new Sprite(chunk_Of_Stone_Texture){Position = new Vector2(1700, 500)},
                new Sprite(chunk_Of_Stone_Texture){Position = new Vector2(1950, 300)},
                new Sprite(chunk_Of_Stone_Texture){Position = new Vector2(2475, 500)},
                new Sprite(ground_Grass_Texture){Position = new Vector2(0, 1010)},
                new Sprite(ground_Grass_Texture){Position = new Vector2(1920, 1010)},
                new Sprite(ground_Grass_Texture){Position = new Vector2(-1920, 1010)}
            };

            decoration_Sprites = new List<Sprite>();
            var random = new Random();
            int cloudPlacement = -2500;
            for (int i = 0; i < 20; i++)//clouds
            {
                decoration_Sprites.Add(new Sprite(cloud_Texture) { Position = new Vector2(cloudPlacement+=random.Next(200,800), random.Next(0,300)) });
            }

            Sprite quitMenuSprite = new Sprite(quit_Menu_Texture) { Position = new Vector2((GraphicsDevice.DisplayMode.Width - quit_Menu_Texture.Width) / 2, (GraphicsDevice.DisplayMode.Height - quit_Menu_Texture.Height) / 2) };
            Sprite quitBackgroundSprite = new Sprite(quit_Background) {SetColor = Color.Black*.5f };
            exitMenu = new ExitMenu(quitMenuSprite, quitBackgroundSprite, quit_Menu_Select_Texture);

            Sprite inventorySprite = new Sprite(inventory_texture) { Position = new Vector2((GraphicsDevice.DisplayMode.Width - inventory_texture.Width) / 2, (GraphicsDevice.DisplayMode.Height - inventory_texture.Height) / 2) };
            Sprite hotbarSprite = new Sprite(inventory_texture);
            inventory = new Inventory(inventorySprite, hotbarSprite, inventory_Select_Texture, items, text);
        }

        protected override void Update(GameTime gameTime)
        {
            if (exitMenu.Update()) Exit();

            if (!exitMenu.Open)
            {
                inventory.Update();

                Vector2 HorizontalPosition = player.Update(solid_Sprites);
                foreach (Sprite x in solid_Sprites)
                    x.Position -= HorizontalPosition;
                foreach (Sprite x in decoration_Sprites)
                    x.Position -= (HorizontalPosition / 2);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach(Sprite x in decoration_Sprites)
                x.Draw(spriteBatch);
            foreach (Sprite x in solid_Sprites)
                x.Draw(spriteBatch);

            player.Draw(spriteBatch);

            inventory.Draw(spriteBatch);
            spriteBatch.DrawString(text, player.Coordinates, new Vector2(1800, 10), Color.Black);
            exitMenu.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

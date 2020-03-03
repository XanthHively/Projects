using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using MyGame.Menu;

namespace MyGame
{
    public class Game1 : Game
    {
        List<Sprite> solid_Sprites;
        List<Sprite> decoration_Sprites;
        Player player;

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

            Texture2D Player_Atlas = Content.Load<Texture2D>("Player_Atlas");

            Texture2D chunk_Of_Stone = Content.Load<Texture2D>("chunk");
            Texture2D ground_grass_texture = Content.Load<Texture2D>("Grass");
            Texture2D cloud = Content.Load<Texture2D>("Cloud");
            Texture2D ExitTexture = Content.Load<Texture2D>("Quit");

            player = new Player(Player_Atlas)
            {
                Position = new Vector2(935, 600),
                Speed = 7,
                jumpPower = 25,
                SetColor = Color.Black
            };

            solid_Sprites = new List<Sprite>()
            {
                new Sprite(chunk_Of_Stone){Position = new Vector2(1100, 850)},
                new Sprite(chunk_Of_Stone){Position = new Vector2(1400, 550)},
                new Sprite(chunk_Of_Stone){Position = new Vector2(1700, 500)},
                new Sprite(chunk_Of_Stone){Position = new Vector2(1950, 300)},
                new Sprite(chunk_Of_Stone){Position = new Vector2(2475, 500)},
                new Sprite(ground_grass_texture){Position = new Vector2(0, 1010)},
                new Sprite(ground_grass_texture){Position = new Vector2(1920, 1010)},
                new Sprite(ground_grass_texture){Position = new Vector2(-1920, 1010)}
            };

            Sprite exit = new Sprite(ExitTexture);
            ExitMenu exitMenu = new ExitMenu(exit);

            decoration_Sprites = new List<Sprite>();
            var random = new Random();
            int cloudPlacement = -2500;
            for (int i = 0; i < 20; i++)
            {
                decoration_Sprites.Add(new Sprite(cloud) { Position = new Vector2(cloudPlacement+=random.Next(200,800), random.Next(0,300)) });
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 HorizontalPosition = player.Update(solid_Sprites);
            foreach (Sprite x in solid_Sprites)
            {
                x.Position -= HorizontalPosition;
            }
            foreach (Sprite x in decoration_Sprites)
            {
                x.Position -= (HorizontalPosition / 2);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach(Sprite x in decoration_Sprites)
            {
                x.Draw(spriteBatch);
            }
            foreach (Sprite x in solid_Sprites)
            {
                x.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

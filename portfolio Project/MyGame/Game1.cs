using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Sprites;
using System.Collections.Generic;

namespace MyGame
{
    public class Game1 : Game
    {
        List<Sprite> sprites;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D ball = Content.Load<Texture2D>("ball");
            Texture2D block = Content.Load<Texture2D>("chunk");

            player = new Player(ball)
            {
                Position = new Vector2(400, 200),
                Speed = 5,
            };

            sprites = new List<Sprite>()
            {
                new Sprite(block){Position = new Vector2(100, 200)},
                new Sprite(block){Position = new Vector2(600, 100)}
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(sprites);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            player.Draw(spriteBatch);

            foreach(Sprite x in sprites)
            {
                x.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;
using CatmullRom;
using System.Diagnostics;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private LevelManager _levelManager;
        private CarManager _carManager;
        private PoliceManager _policeManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 900;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            AssetManager.LoadTextures(Content, GraphicsDevice);

            _levelManager = new LevelManager();
            _levelManager.AddLevel(GraphicsDevice);

            _carManager = new CarManager(GraphicsDevice);

            _policeManager = new PoliceManager();
            _policeManager.AddPolice(new Vector2(1000, 300));
        }

    

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _levelManager.Update(gameTime);
            _carManager.Update(gameTime);
            _policeManager.Update(gameTime);

            base.Update(gameTime);
        }
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            //Draw road
            _levelManager.Draw(_spriteBatch);
            _carManager.Draw(_spriteBatch);
            _policeManager.Draw(_spriteBatch);
           

            base.Draw(gameTime);
        }
    }
}

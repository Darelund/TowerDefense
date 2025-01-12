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
        private string _textures = "scoutCar,heavyCar,DollarSign,Bullet_Cannon,Bullet_MG,Cannon,Cannon2,Cannon3,car,transparentSquareBackground,MG,MG2,MG3,Missile,Missile_Launcher,Missile_Launcher2,Missile_Launcher3,police,road,Tower,yellow";
        private string _soundEffects = "";
        private string _music = "";
        private string _spriteFonts = "GameText";
        private string _effects = "";



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
            GameManager.SetUp(Window, Content, GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ResourceManager.LoadResources(Content, _textures, _soundEffects, _music, _spriteFonts, _effects);

            GameManager.ContentLoad(_spriteBatch);
        }

    

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameManager.Update(gameTime);


         

                base.Update(gameTime);
        }
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSeaGreen);
            GameManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}

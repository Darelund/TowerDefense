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
        private string _textures = "Bullet_Cannon,Bullet_MG,Cannon,Cannon2,Cannon3,car,transparentSquareBackground,MG,MG2,MG3,Missile,Missile_Launcher,Missile_Launcher2,Missile_Launcher3,police,road,Tower,yellow";
        private string _soundEffects = "";
        private string _music = "";
        private string _spriteFonts = "";
        private string _effects = "";


        private LevelManager _levelManager;
        private EnemyManager _enemyManager;
        private TowerManager _towereManager;

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
            ResourceManager.LoadResources(Content, _textures, _soundEffects, _music, _spriteFonts, _effects);

            _levelManager = new LevelManager();
            _levelManager.AddLevel(GraphicsDevice);

            _enemyManager = new EnemyManager(GraphicsDevice);

            _towereManager = new TowerManager();
            _towereManager.AddTower(new Vector2(1000, 300));
        }

    

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _levelManager.Update(gameTime);
            _enemyManager.Update(gameTime);
            _towereManager.Update(gameTime);

            base.Update(gameTime);
        }
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            //Draw road
            _levelManager.Draw(_spriteBatch);
            _enemyManager.Draw(_spriteBatch);
            _towereManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}

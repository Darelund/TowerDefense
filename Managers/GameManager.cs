using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace TowerDefense
{
    public class GameManager
    {
        public enum GameState
        {
            MainMenu,
            Playing,
            Pause,
            GameOver,
            Victory,
            Restart,
            Exit
        }
      //  public static List<GameObject> GameObjects { get; set; } = new List<GameObject>();
      //  public static List<GameObject> GetGameObjects => GameObjects;
      //  private static List<FlashEffect> _flashEffects = new List<FlashEffect>();

        public static GameState CurrentGameState { get; private set; } = GameState.Playing;

        public static GameWindow Window;
        public static ContentManager Content;
        public static GraphicsDevice Device;
        // private static SceneSwitcher _sceneSwitcher;

        public static SelectedTower _selectedObject = null;
        private static List<GameObjectSelector> gameObjectSelectors;


        public static string Name { get; set; }

        public static event Action<Color, GameState> OnPlaying, OnMainMenu, OnGameOver, OnWin, OnPause;

        public static void SetUp(GameWindow window, ContentManager content, GraphicsDevice device)
        {

            Window = window;
            Content = content;
            Device = device;
           // _sceneSwitcher = new SceneSwitcher(Window, Device);
        }
        public static void ContentLoad(SpriteBatch spriteBatch)
        {
              UIManager.LoadContent();
            // AudioManager.LoadContent();
            // HighScore.LoadScores();
            //LevelManager.CreateLevels();
            //LevelManager.SpecificLevel(0, false);

            LevelManager.AddLevel(Device, "map1.txt");
            EnemyManager.SetUp(Device);
           // TowerManager.AddTower(new Vector2(1000, 300));

            GameObjectPlacer.SetUp(Device, Window, spriteBatch);
            GameObjectPlacer.DrawOnRenderTarget();

          //  _selectedObject = new Tower(ResourceManager.GetTexture("Cannon"), new Vector2(-ResourceManager.GetTexture("Cannon").Width / 2, -ResourceManager.GetTexture("Cannon").Height / 2) + Mouse.GetState().Position.ToVector2(), 0.2f);

            gameObjectSelectors = new List<GameObjectSelector>()
            {
                new GameObjectSelector(ResourceManager.GetTexture("Cannon"), new SelectedTower("Cannon", ResourceManager.GetTexture("Cannon"), Vector2.Zero, 0.2f), new Vector2(Window.ClientBounds.Width - 300, 0), 0.2f),
                new GameObjectSelector(ResourceManager.GetTexture("MG"), new SelectedTower("MG", ResourceManager.GetTexture("MG"), Vector2.Zero, 0.2f), new Vector2(Window.ClientBounds.Width - 200, 0), 0.2f),
                new GameObjectSelector(ResourceManager.GetTexture("Missile_Launcher"), new SelectedTower("Missile", ResourceManager.GetTexture("Missile_Launcher"), Vector2.Zero, 0.2f), new Vector2(Window.ClientBounds.Width - 100, 0), 0.2f)
            };

            EnemyUIManager.SetUp();
        }


        public static void Update(GameTime gameTime)
        {
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    InputManager.Update();
                   
                    break;
                case GameState.Playing:
                    InputManager.Update();
                    // LevelEditor.Update();


                      UIManager.Update(gameTime);

                    LevelManager.Update(gameTime);
                    EnemyManager.Update(gameTime);

                    foreach (var selector in gameObjectSelectors)
                    {
                        selector.Color = Color.White;
                    }

                        //Select a tower to place down
                        if (_selectedObject == null && InputManager.CurrentMouse.LeftButton == ButtonState.Pressed)
                    {
                        foreach (var selector in gameObjectSelectors)
                        {
                            if (selector.IsMouseOver())
                            {
                                // Create a select object
                                if(EconomyManager.MoneyAmount >= selector.Prefab.GetPrefab().Price)
                                {
                                    _selectedObject = selector.SelectedObject();
                                    EconomyManager.UpdateScore(-selector.Prefab.GetPrefab().Price);
                                }
                               else
                                {
                                    selector.Color = Color.Red;
                                }
                                break;
                            }
                        }
                    }
                    if (_selectedObject != null)
                    {
                        GameObjectPlacer.Update(gameTime, _selectedObject);
                    }

                    GameObjectPlacer.UpdateObjectsOnTheRenderTarget(gameTime);


                    ProjectileManager.Update(gameTime);
                    //Debug stuff
                    if (InputManager.RightClick())
                        LevelEditor.OpenLevelMapFile(LevelManager.CurrentLevel);


                    if (InputManager.CurrentKeyboard.IsKeyDown(Keys.U))
                        Debug.WriteLine(ProjectileManager.Projectiles.Count);

                       FlashManager.Update(gameTime); //can't get it to work with the CatmullRomPath dll
                    CollisionManager.CheckCollision();
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    InputManager.Update();
                   // UIManager.Update(gameTime);
                    break;
                case GameState.Victory:
                   // LevelManager.NextLevel(true);
                    //LevelManager.SpecificLevel(5, true);
                    break;
                case GameState.Restart:
                 //   LevelManager.Restart();
                    break;
                case GameState.Exit:
                    break;
            }
           // _sceneSwitcher.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap);
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                 //   UIManager.Draw(spriteBatch);
                  
                    break;
                case GameState.Playing:



                    // LevelEditor.Draw(spriteBatch);



                  



                    spriteBatch.Begin();

                    if (_selectedObject != null)
                    {
                        _selectedObject.Draw(spriteBatch);
                        GameObjectPlacer.Draw(_selectedObject);
                    }
                    if (_selectedObject == null)
                    {
                        spriteBatch.End();
                        Device.Clear(Color.DarkSeaGreen);
                        GameObjectPlacer.Draw();
                        spriteBatch.Begin();
                    }
                    UIManager.Draw(spriteBatch);

                    foreach (var selector in gameObjectSelectors)
                    {
                        selector.Draw(spriteBatch);
                    }
                    spriteBatch.End();
                    LevelManager.Draw(spriteBatch);
                   // EnemyManager.Draw(spriteBatch);

                    spriteBatch.Begin();
                    EnemyManager.Draw(spriteBatch);
                       FlashManager.Draw(spriteBatch); //can't get it to work with the CatmullRomPath dll
                    EnemyUIManager.Draw(spriteBatch, EnemyManager.enemies);
                    spriteBatch.End();

                    ProjectileManager.Draw(spriteBatch);

                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                  //  UIManager.Draw(spriteBatch);
                 //   HighScore.DisplayScores(spriteBatch);
                    break;
                case GameState.Victory:
                 //   UIManager.Draw(spriteBatch);
                    break;
                case GameState.Restart:
                    break;
                case GameState.Exit:
                    break;
            }
           // _sceneSwitcher.Draw(spriteBatch);
          //  spriteBatch.End();
        }
        //public static void AddGameObject(GameObject gameObject)
        //{
        //    _gameObjects.Add(gameObject);
        //}
        //public static void RemoveGameObject(GameObject gameObject)
        //{
        //    _gameObjects.Remove(gameObject);
        //}

        //public static void AddFlashEffect(FlashEffect flashEffect)
        //{
        //    _flashEffects.Add(flashEffect);
        //}
        public static void ChangeGameState(object passedState)
        {
            if (passedState is GameState newState)
            {
                if (newState == CurrentGameState) return;
                CurrentGameState = newState;
            }
        }
    }
}

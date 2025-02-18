﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using WinForm;

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

        public static GameState CurrentGameState { get; private set; } = GameState.Playing;

        public static GameWindow Window;
        public static ContentManager Content;
        public static GraphicsDevice Device;

        public static SelectedTower _selectedObject = null;
        private static List<GameObjectSelector> gameObjectSelectors;
        private static Form1 winform;
        private static ParticleSystem particleSystem;
        public static bool UseDebug = false;
        public static bool isUpgrading = false;


        public static string Name { get; set; }

        public static event Action<Color, GameState> OnPlaying, OnMainMenu, OnGameOver, OnWin, OnPause;

        public static void SetUp(GameWindow window, ContentManager content, GraphicsDevice device)
        {

            Window = window;
            Content = content;
            Device = device;
        }
        public static void ContentLoad(SpriteBatch spriteBatch)
        {
              UIManager.LoadContent();

            LevelManager.AddLevel(Device, "map1.txt");
            EnemyManager.SetUp(Device);

            GameObjectPlacer.SetUp(Device, Window, spriteBatch);
            GameObjectPlacer.DrawOnRenderTarget();


            gameObjectSelectors = new List<GameObjectSelector>()
            {
                new GameObjectSelector(ResourceManager.GetTexture("Cannon"), new SelectedTower("Cannon", ResourceManager.GetTexture("Cannon"), Vector2.Zero, 0.2f), new Vector2(Window.ClientBounds.Width - 300, 0), 0.2f),
                new GameObjectSelector(ResourceManager.GetTexture("MG"), new SelectedTower("MG", ResourceManager.GetTexture("MG"), Vector2.Zero, 0.2f), new Vector2(Window.ClientBounds.Width - 200, 0), 0.2f),
                new GameObjectSelector(ResourceManager.GetTexture("Missile_Launcher"), new SelectedTower("Missile", ResourceManager.GetTexture("Missile_Launcher"), Vector2.Zero, 0.2f), new Vector2(Window.ClientBounds.Width - 100, 0), 0.2f)
            };

            EnemyUIManager.SetUp();
            winform = new Form1();
            winform.OnTowerUpgradeRequested1 += TowerManager.UpgradeCannonCost;
            winform.OnTowerUpgradeRequested2 += TowerManager.UpgradeMGCost;
            winform.OnTowerUpgradeRequested3 += TowerManager.UpgradeMissileCost;

            //    winform.Show();

            List<Texture2D> textures = new List<Texture2D>
            {
                ResourceManager.GetTexture("circle"),
                ResourceManager.GetTexture("star"),
                ResourceManager.GetTexture("diamond")
            };
            particleSystem = new ParticleSystem(textures, new Vector2(400, 240));
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
                                   // EconomyManager.UpdateScore(-selector.Prefab.GetPrefab().Price);
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



                    if (InputManager.CurrentKeyboard.IsKeyDown(Keys.U) && InputManager.PreviousKeyboard.IsKeyUp(Keys.U))
                    {
                        isUpgrading = !isUpgrading;
                        if(isUpgrading)
                        {
                            winform.Show();
                        }
                        else
                        {
                            winform.Hide();
                        }
                    }
                        
                    FlashManager.Update(gameTime); 
                    CollisionManager.CheckCollision();

                    if (InputManager.CurrentKeyboard.IsKeyDown(Keys.D) && InputManager.PreviousKeyboard.IsKeyUp(Keys.D))
                        UseDebug = !UseDebug;


                    particleSystem.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                    particleSystem.Update();
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    InputManager.Update();
                    break;
                case GameState.Victory:
                    break;
                case GameState.Restart:
                    break;
                case GameState.Exit:
                    break;
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Playing:
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

                    spriteBatch.Begin();
                    EnemyManager.Draw(spriteBatch);
                       FlashManager.Draw(spriteBatch); //can't get it to work with the CatmullRomPath dll
                    EnemyUIManager.Draw(spriteBatch, EnemyManager.enemies);
                    spriteBatch.End();

                    ProjectileManager.Draw(spriteBatch);

                    particleSystem.Draw(spriteBatch);
                    break;
                case GameState.Pause:
                    break;
                case GameState.GameOver:
                    break;
                case GameState.Victory:
                    break;
                case GameState.Restart:
                    break;
                case GameState.Exit:
                    break;
            }
        }
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

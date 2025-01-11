using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace TowerDefense
{
    public class LevelManager
    {

        private static List<Level> _levels = new List<Level>();

        private static int _currentLevelIndex = 0;
        public static Level CurrentLevel => _levels[CurrentLevelIndex];
        public static int CurrentLevelIndex
        {
            get => _currentLevelIndex;
            set
            {
                if (value <= _levels.Count)
                {
                    Debug.WriteLine("New level set");
                }
                else
                {
                    throw new Exception("Couldn't find that level, are you missing a level?");
                }
            }
        }
       

        public static void AddLevel(GraphicsDevice gd, string mapLevel)
        {
            Texture2D roadTex = ResourceManager.GetTexture("road");
            _levels.Add(new Level(gd, roadTex, mapLevel));
        }
        public static void RemoveLevel(Level gd)
        {
            _levels.Remove(gd);
        }

        public static void Update(GameTime gameTime)
        {
            _levels[_currentLevelIndex].Update(gameTime);
           
        }

        public static void Draw(SpriteBatch sb)
        {
            _levels[_currentLevelIndex].Draw(sb);
        }
    }
}

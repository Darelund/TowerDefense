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

        private List<Level> _levels;

        private int _currentLevel = 0;
        public int CurrentLevel
        {
            get => _currentLevel;
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
        public LevelManager() 
        { 
            _levels = new List<Level>();
        }

        public void AddLevel(GraphicsDevice gd)
        {
            _levels.Add(new Level(gd));
        }
        public void RemoveLevel(GraphicsDevice gd)
        {
            _levels.Remove(new Level(gd));
        }

        public void Update(GameTime gameTime)
        {
            _levels[_currentLevel].Update(gameTime);
           
        }

        public void Draw(SpriteBatch sb)
        {
            _levels[_currentLevel].Draw(sb);
        }

       
    }
}

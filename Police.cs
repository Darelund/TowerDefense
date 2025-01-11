using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class Police
    {
        private Vector2 pos;
        private int delay = 400;
        private int timeSinceLast = 0;

        private LaserBeam _laser = null;
        public Police(Vector2 pos) { 
            this.pos = pos;
        }

        public void AddLaser(LaserBeam laser)
        {
            _laser = laser;
        }

        public void Update(GameTime gameTime)
        {
            //timeSinceLast += gameTime.ElapsedGameTime.Milliseconds;
            //if (timeSinceLast > delay)
            //{
                _laser.Update(gameTime);
            //}
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.policeTex, pos, Color.White);
            _laser.Draw(spriteBatch);
        }
    }
}

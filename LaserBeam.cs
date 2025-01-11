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
    public class LaserBeam
    {
        private float range = 50;
        private float speed = 2;
        private Vector2 pos;
        private Vector2 startPos;
        public Rectangle hitBox;

        public LaserBeam(Vector2 startPos) {
            pos = startPos;
            this.startPos= startPos;
            hitBox=new Rectangle((int)pos.X, (int)pos.Y, AssetManager.yellowTex.Height, AssetManager.yellowTex.Width);
        }

        public void Update(GameTime gameTime)
        {
            pos.X -= speed;
            hitBox.X=(int)pos.X;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.yellowTex, pos, Color.White);
        }



    }
}

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
    public class Tower : GameObject
    {
       // private Texture2D _baseTexture;
        

        public int Price {  get; protected set; }



        //**stats**

        //Textures

        //Bullet speed
        public float BulletSpeed = 1f;

        //Delay/Shooting speed
        public bool _canFire = true;
        public float fireDelay = 0.5f;
        protected float timeSinceLastFired = 0;


        // protected Projectile _laser = null;
        public Tower(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
        {
           // _baseTexture = ResourceManager.GetTexture("Tower");
        }

        //public void AddLaser(Projectile laser)
        //{
        //    _laser = laser;
        //}

        public override void Update(GameTime gameTime)
        {
            //timeSinceLast += gameTime.ElapsedGameTime.Milliseconds;
            //if (timeSinceLast > delay)
            //{
                // if(_laser != null)
                //_laser.Update(gameTime);
            //}
            Detection();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Draw(_baseTexture, position, null, Color.White, _rotation, _origin, _scale, _spriteEffect, _layerDepth);
            spriteBatch.Draw(texture, position, null, Color, _rotation, _origin, _scale, _spriteEffect, _layerDepth);

            //if (_laser != null)
            //    _laser.Draw(spriteBatch);
        }
        //Should make methods that children can override
        public virtual void Detection()
        {

        }
    }
}

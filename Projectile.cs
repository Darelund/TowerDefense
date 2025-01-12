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
    public class Projectile : GameObject
    {
      //  private float range = 50;
        private float _speed = 2;
       // private Vector2 pos;
      //  private Vector2 startPos;
       // public Rectangle hitBox;
        private Vector2 _direction;
        public float dmg { get; private set; } = 1;

        public Projectile(Texture2D tex, Vector2 startPos, Vector2 direction, float rotation, float speed): base(tex, startPos) 
        {
          //  pos = startPos;
           // this.startPos = startPos;
          //  hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Height, texture.Width);

            _rotation = rotation;
            _speed = speed;
            _direction = direction;
        }

        public override void Update(GameTime gameTime)
        {
            position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // hitBox.X = (int)position.X;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            base.Draw(spriteBatch);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if(gameObject is Enemy enemy)
            {
              //  IsActive = false;
              CollisionManager._collidables.Remove(this);
                ProjectileManager.Projectiles.Remove(this);
                //Enemy take damage?
            }
        }
    }
}

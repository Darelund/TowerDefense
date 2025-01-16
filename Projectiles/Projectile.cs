using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public float dmg { get; set; } = 1;

        public Projectile(Texture2D tex, Vector2 startPos, Vector2 direction, float rotation, float speed, float scale): base(tex, startPos, scale) 
        {
          //  pos = startPos;
           // this.startPos = startPos;
          //  hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Height, texture.Width);

            _rotation = rotation;
            _speed = speed;
            _direction = direction;
          //  DebugRectangle.Init(GameManager.Device, (int)(texture.Width * _scale), (int)(texture.Height * _scale));
          DebugSphere.Init(GameManager.Device, (int)BoundingSphereDiamater);
        }

        public override void Update(GameTime gameTime)
        {
            position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            // hitBox.X = (int)position.X;

            if(position.X < -texture.Width * _scale || position.X > GameManager.Window.ClientBounds.Width + texture.Width * _scale || position.Y < -texture.Height * _scale || position.Y > GameManager.Window.ClientBounds.Height + texture.Height * _scale)
            {
                CollisionManager.Collidables.Remove(this);
                ProjectileManager.Projectiles.Remove(this);

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
            base.Draw(spriteBatch);
            // DebugRectangle.DrawRectangle(spriteBatch, new Rectangle((int)_origin.X + (int)position.X, (int)_origin.Y + (int)position.Y, (int)(texture.Width * _scale), (int)(texture.Height * _scale)), Color.Red);

            if (GameManager.UseDebug)
                DebugSphere.DrawSphere(spriteBatch, HitSphere, Color.Green);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if(gameObject is Enemy enemy)
            {
              //  IsActive = false;
                //CollisionManager.Collidables.Remove(this);
                //ProjectileManager.Projectiles.Remove(this);
                //Enemy take damage?
               // Debug.WriteLine("Hit enemy");
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;
      //  protected Rectangle sourceRectangle;
        private Rectangle _hitbox;

        protected float _rotation;
        protected Vector2 _origin;
        public float _scale;
        protected SpriteEffects _spriteEffect;
        protected float _layerDepth;
        public Color Color = Color.White;

        public Texture2D GetTexture => texture;
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }
        public Rectangle Hitbox
        {
            //get => new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            // get => new Rectangle((int)position.X - (int)Origin.X * (int)Size, (int)position.Y - (int)Origin.Y * (int)Size, rec.Width * (int)Size, rec.Height * (int)Size);
            get => new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * _scale), (int)(texture.Height * _scale));
        }


        public GameObject(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            position = pos;
            _hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public GameObject(Texture2D tex, Vector2 pos, float scale) : this(tex, pos)
        {
           _rotation = 0;
            _scale = scale;
            _origin = Vector2.Zero;
            _spriteEffect = SpriteEffects.None;
            _layerDepth = 0;
        }
        public abstract void Update(GameTime gameTime);
      
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color, _rotation, _origin, _scale, _spriteEffect, _layerDepth);
        }
        public virtual void OnCollision(GameObject gameObject)
        {

        }
    }
}

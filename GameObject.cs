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

        public float _rotation;
        public Vector2 _origin { get; set; }
        public float _scale;
        protected SpriteEffects _spriteEffect;
        protected float _layerDepth;
        public Color Color = Color.White;
        public bool IsActive = true;

        public Texture2D GetTexture => texture;
        public Texture2D SetTexture
        {
            set { texture = value; }
        }


        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public float BoundingSphereDiamater { get; set; }
        //So basically bounding box can not rotate, so I added this to make the colliders work better
        public BoundingSphere HitSphere
        {
            get
            {
                //float width = texture.Width * _scale;
                //float height = texture.Height * _scale;
                //float radius = MathF.Sqrt(width * width + height * height) / 2;
                //BoundingSphereDiamater = radius * 2;
                return new BoundingSphere(new Vector3((int)position.X,(int)position.Y, 0), BoundingSphereDiamater / 2);
            }
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
           // _origin = new Vector2(texture.Width / 2f, texture.Height / 2f);


        }
        public GameObject(Texture2D tex, Vector2 pos, float scale) : this(tex, pos)
        {
           _rotation = 0;
            _scale = scale;
            _origin = new Vector2(texture.Width * _scale/ 2f, texture.Height *_scale / 2f);
            _spriteEffect = SpriteEffects.None;
            _layerDepth = 0;

            float width = texture.Width * _scale;
            float height = texture.Height * _scale;
            float radius = MathF.Sqrt(width * width + height * height) / 2;
            BoundingSphereDiamater = radius * 2;
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

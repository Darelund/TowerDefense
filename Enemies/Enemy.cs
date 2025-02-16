using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace TowerDefense
{
    public enum EnemyType
    {
        Normal,
        Heavy,
        Scout
    }
    public class Enemy : GameObject
    {
        
        private CatmullRomPath _car; //Draw car

        private float _currentCurvePos = 0;
        private float _speed;
        public float _health;
        private int _reward;

       

        private EnemyType _enemyType;


        private GraphicsDevice _device;

        public Enemy(GraphicsDevice device, Texture2D tex, Vector2 pos, EnemyType enemyType, float scale) : base(tex, pos, scale)
        {
            _device = device;
            float tensionRoad = 0.5f;

            _car = new CatmullRomPath(_device, tensionRoad);
            _car.Clear();//Vi vill inte ha default punkter, vi vill göra en egen väg

            LoadPath.LoadPathFromFile(_car, LevelManager.CurrentLevel.LevelMap);

            //Grejer för hur fett bil ska vara och vad den ska innehålla
            _car.DrawFillSetup(_device, 2, 1, 256);
            _enemyType = enemyType;
            switch (_enemyType)
            {
                case EnemyType.Normal:
                    _speed = 0.05f;
                    _health = 5;
                    _reward = 1;
                    break;
                case EnemyType.Heavy:
                    _speed = 0.01f;
                    _health = 30;
                    _reward = 15;
                    break;
                case EnemyType.Scout:
                    _speed = 0.1f;
                    _health = 1;
                    _reward = 5;
                    break;
                default:
                    break;
            }
            BoundingSphereDiamater /= 2;
            DebugSphere.Init(GameManager.Device, (int)BoundingSphereDiamater);

        }
        public override void Update(GameTime gameTime)
        {
            _currentCurvePos += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
               Vector2 currentPos = _car.EvaluateAt(_currentCurvePos);
                Vector2 currentRot = _car.EvaluateTangentAt(_currentCurvePos);
                position = currentPos;
                _rotation = MathF.Atan2(currentRot.X, -currentRot.Y);

            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                spriteBatch.Draw(texture, position, null, Color, _rotation, _origin, _scale, _spriteEffect, _layerDepth);
            
                if(GameManager.UseDebug)
                DebugSphere.DrawSphere(spriteBatch, HitSphere, Color.Green);
            }
            else
            {
                //Todo make player lose health
                EnemyManager.enemies.Remove(this);
            }
        }
        public override void OnCollision(GameObject gameObject)
        {
           
            if(gameObject is Projectile projectile)
            {
                CollisionManager.Collidables.Remove(projectile);
                ProjectileManager.Projectiles.Remove(projectile);
                TakeDamage(projectile.dmg);
            }
        }
        public void TakeDamage(float amount)
        {
            _health -= amount;

            float flashTime = 2f;
            Color flashColor = Color.White;
            var flash = new FlashEffect(ResourceManager.GetEffect("FlashEffect"), flashTime, this, flashColor);
            FlashManager.AddFlashEffect(flash);

            if (_health <= 0)
            {
                CollisionManager.Collidables.Remove(this);
                EnemyManager.enemies.Remove(this);
                EconomyManager.UpdateScore(_reward);
            }
        }
    }
}

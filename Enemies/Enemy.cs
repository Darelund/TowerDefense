using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private float _health;
        private int _reward;

        private EnemyType _enemyType;

        //live
        //private float _health = 3;
        public bool IsHit = false; //Gör den till isDead sen

        private GraphicsDevice _device;

        public Enemy(GraphicsDevice device, Texture2D tex, Vector2 pos, EnemyType enemyType, float scale) : base(tex, pos)
        {
            _device = device;
            _scale = scale;
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
          //  DebugRectangle.Init(GameManager.Device, (int)(texture.Width * _scale), (int)(texture.Height * _scale));

        }
        public override void Update(GameTime gameTime)
        {
            _currentCurvePos += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                //Som den där GetPos eller vad den hette
               Vector2 currentPos = _car.EvaluateAt(_currentCurvePos);
                position = currentPos;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                if (!IsHit)
                _car.DrawMovingObject(_currentCurvePos, spriteBatch, texture);
              //  DebugRectangle.DrawRectangle(spriteBatch, new Rectangle((int)_origin.X + (int)position.X, (int)_origin.Y + (int)position.Y, (int)(texture.Width * _scale), (int)(texture.Height * _scale)), Color.Red);
            }
        }
        public override void OnCollision(GameObject gameObject)
        {
            //if(gameObject.GetType() == typeof(Projectile))
            //{
            //    Debug.WriteLine("Bullet hit");

            //}
            if(gameObject is Projectile projectile)
            {
                _health -= projectile.dmg;

                if(_health <= 0)
                {
                    CollisionManager.Collidables.Remove(this);
                    EnemyManager.enemies.Remove(this);
                    EconomyManager.UpdateScore(_reward);
                }
            }
        }
    }
}

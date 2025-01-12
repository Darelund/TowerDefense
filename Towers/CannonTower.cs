﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class CannonTower : Tower
    {
        public float DetectionRadius { get; set; } = 40;
        private GameObject _targetEnemy;
        public CannonTower(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
        {
            Price = 5;
        }
        public override void Update(GameTime gameTime)
        {
            //timeSinceLast += gameTime.ElapsedGameTime.Milliseconds;
            //if (timeSinceLast > delay)
            //{
            //if (_laser != null)
            //    _laser.Update(gameTime);
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
        public override void Detection()
        {
            foreach (var enemy in EnemyManager.enemies)
            {
                if (Vector2.Distance(position, enemy.Position) <= DetectionRadius)
                {
                    _targetEnemy = enemy;
                    RotateTowardsEnemy(enemy);
                    var direction = enemy.Position - position;
                    FireProjectile(direction);
                    break; // Stop checking once an enemy is targeted
                }
            }
        }
        private void RotateTowardsEnemy(GameObject enemy)
        {
            var direction = enemy.Position - position;
            direction.Normalize();

            var angle = MathF.Atan2(direction.Y, direction.X);
            _rotation = angle;


        }
        private void FireProjectile(Vector2 direction)
        {
            var prj = new Projectile(texture, position, direction, _rotation, 10f);
            CollisionManager._collidables.Add(prj);
            ProjectileManager.Projectiles.Add(prj);
        }
    }
}

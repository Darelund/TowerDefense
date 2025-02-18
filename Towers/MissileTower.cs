﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class MissileTower : Tower
    {
        public float DetectionRadius { get; set; } = 200;
        private GameObject _targetEnemy;
        public static int Level = 1;

        public MissileTower(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
        {
            Price = 25;
            _origin = new Vector2(texture.Width / 2f, texture.Height / 4);

            TowerManager.ApplySettings(this);

            //BulletSpeed = 0.7f;
            //fireDelay = 2f;
        }
        public override void Update(GameTime gameTime)
        {
            //timeSinceLast += gameTime.ElapsedGameTime.Milliseconds;
            //if (timeSinceLast > delay)
            //{
            //if (_laser != null)
            //    _laser.Update(gameTime);
            //}
            if (!_canFire)
            {
                timeSinceLastFired += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastFired >= fireDelay)
                {
                    _canFire = true;
                    timeSinceLastFired = 0;
                }
            }
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
                // Debug.WriteLine("Is this getting run?");
                //  Debug.WriteLine(Vector2.Distance(position, enemy.Position));
                if (Vector2.Distance(position, enemy.Position) <= DetectionRadius)
                {
                    //   Debug.WriteLine("Is this getting run?");
                    _targetEnemy = enemy;
                    RotateTowardsEnemy(enemy);
                    var direction = enemy.Position - position;
                    FireProjectile(direction);
                    // throw new Exception("Came this far");
                    break; // Stop checking once an enemy is targeted
                }
            }
        }
        private void RotateTowardsEnemy(GameObject enemy)
        {
            var direction = enemy.Position - position;
            direction.Normalize();

            var angle = MathF.Atan2(direction.X, -direction.Y);
            _rotation = angle;


        }
        // bool test = true;
        private void FireProjectile(Vector2 direction)
        {
            if (!_canFire) return;
            //if (!test) return;
            //test = false;

            _canFire = false;
            // Debug.WriteLine("Is this getting run?");
            var prj = new HeavyBullet(ResourceManager.GetTexture("Missile"), position, direction, _rotation, BulletSpeed, 1.5f);
            CollisionManager.Collidables.Add(prj);
            ProjectileManager.Projectiles.Add(prj);
            //  Debug.WriteLine(prj.Position);

            //  Debug.WriteLine(ProjectileManager.Projectiles.Count);
        }
        //public override void Upgrades()
        //{
        //    Level++;
        //    if (Level == 2)
        //    {
        //        BulletSpeed = 2f;
        //        texture = ResourceManager.GetTexture("Missile_Launcher2");
        //    }
        //    if (Level == 3)
        //    {
        //        BulletSpeed = 3f;
        //        texture = ResourceManager.GetTexture("Missile_Launcher3");
        //    }
        //}
    }
}

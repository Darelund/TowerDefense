using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class HeavyBullet : Projectile
    {
        private float _explosionRadius = 50f;
        public HeavyBullet(Texture2D tex, Vector2 startPos, Vector2 direction, float rotation, float speed, float scale) : base(tex, startPos, direction, rotation, speed, scale)
        {
            dmg = 50;
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject is Enemy enemy)
            {
                CollisionManager.Collidables.Remove(this);
                ProjectileManager.Projectiles.Remove(this);

                AreaDamage();
            }
        }
        private void AreaDamage()
        {
            foreach (var collidable in CollisionManager.Collidables)
            {
                if(collidable is Enemy enemy)
                {
                    float distance = Vector2.Distance(position, enemy.Position);

                    if (distance <= _explosionRadius)
                    {
                        float damageMultiplier = 1 - (distance / _explosionRadius);
                        float damageToApply = dmg * damageMultiplier;

                        enemy.TakeDamage(damageToApply);
                    }
                }
            }
        }
    }
}

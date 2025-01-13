using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense;

namespace TowerDefense
{
    public class HeavyBullet : Projectile
    {
        private float _explosionRadius = 200f;
        //private ParticleSystem _explosionParticles;
        //private bool _particlesTriggered = false;

        public HeavyBullet(Texture2D tex, Vector2 startPos, Vector2 direction, float rotation, float speed, float scale)
            : base(tex, startPos, direction, rotation, speed, scale)
        {
            dmg = 50;
            //List<Texture2D> textures = new List<Texture2D>
            //{
            //    ResourceManager.GetTexture("circle"),
            //    ResourceManager.GetTexture("star"),
            //    ResourceManager.GetTexture("diamond")
            //};
            //_explosionParticles = new ParticleSystem(textures, startPos);
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
            for (int i = 0; i < CollisionManager.Collidables.Count; i++)
            {
                if (CollisionManager.Collidables[i] is Enemy enemy)
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

            // Trigger particle effect at the explosion location
           // TriggerExplosionParticles();
        }

        //private void TriggerExplosionParticles()
        //{
        //    if (!_particlesTriggered)
        //    {
        //        _explosionParticles.EmitterLocation = position;
        //        _particlesTriggered = true;
        //        ProjectileManager.particles.Add(_explosionParticles);
        //    }
        //}

        //public void UpdateParticles()
        //{
        //    if (_particlesTriggered)
        //    {
        //        _explosionParticles.Update();

        //        // Optionally, you can reset or stop particles if needed
        //        // Example: Stop updating particles after a certain condition
        //    }
        //}

        //public void DrawParticles(SpriteBatch spriteBatch)
        //{
        //    if (_particlesTriggered)
        //    {
        //        _explosionParticles.Draw(spriteBatch);
        //    }
        //}
    }
}

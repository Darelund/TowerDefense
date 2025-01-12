using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class ProjectileManager
    {
        public static List<Projectile> Projectiles = new List<Projectile>();
       

        public static List<Projectile> GetAllProjectiles()
        {
            return Projectiles;
        }

        //public static void AddTower(Vector2 pos)
        //{
        //    Projectile prj = new Projectile(ResourceManager.GetTexture("Bullet_Cannon"), pos);
        //    float towerSize = 0.2f;
        //    Tower t = new Tower(ResourceManager.GetTexture("Tower"), pos, towerSize);
        //    t.AddLaser(prj);
        //    towerList.Add(t);  
        //}

        public static void Update(GameTime gameTime)
        {
            foreach (Projectile t in Projectiles)
            {
                t.Update(gameTime);
            }
        }

       public static void Draw(SpriteBatch spriteBatch)
       {
            spriteBatch.Begin();
            foreach (Projectile t in Projectiles)
            {
                t.Draw(spriteBatch);
              //  Debug.WriteLine(t.Position);
            }
            spriteBatch.End();
       }
    }
}

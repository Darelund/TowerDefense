using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class TowerManager
    {
        private static List<Tower> towerList = new List<Tower>();
       

        public static List<Tower> GetAllTowers()
        {
            return towerList;
        }

        public static void AddTower(Vector2 pos)
        {
            Projectile prj = new Projectile(ResourceManager.GetTexture("Bullet_Cannon"), pos);
            float towerSize = 0.2f;
            Tower t = new Tower(ResourceManager.GetTexture("Tower"), pos, towerSize);
            t.AddLaser(prj);
            towerList.Add(t);  
        }

        public static void Update(GameTime gameTime)
        {
            foreach (Tower t in towerList)
            {
                t.Update(gameTime);
            }
        }

       public static void Draw(SpriteBatch spriteBatch)
       {
            spriteBatch.Begin();
            foreach (Tower t in towerList)
            {
                t.Draw(spriteBatch);
            }
            spriteBatch.End();
       }
    }
}

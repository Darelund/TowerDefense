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
        private List<Tower> towerList;
        //LaserManager laserManager;
        public TowerManager() { 
            towerList= new List<Tower>();
            //laserManager= new LaserManager();
        }

        public List<Tower> GetAllTowers()
        {
            return towerList;
        }

        public void AddTower(Vector2 pos)
        {
            Projectile prj = new Projectile(ResourceManager.GetTexture("Bullet_Cannon"), pos);
            Tower t = new Tower(ResourceManager.GetTexture("Tower"), pos, 0.5f);
            t.AddLaser(prj);
            towerList.Add(t);  
        }

        public void Update(GameTime gameTime)
        {
            foreach (Tower t in towerList)
            {
                t.Update(gameTime);
            }
        }

       public void Draw(SpriteBatch spriteBatch)
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

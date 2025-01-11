using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class PoliceManager
    {
        List<Police> policeList;
        //LaserManager laserManager;
        public PoliceManager() { 
            policeList= new List<Police>();
            //laserManager= new LaserManager();
        }

        public List<Police> GetAllOfficers()
        {
            return policeList;
        }

        public void AddPolice(Vector2 pos)
        {
            LaserBeam lb = new LaserBeam(pos);
            Police p = new Police(pos);
            p.AddLaser(lb);
            policeList.Add(p);  
        }

        public void Update(GameTime gameTime)
        {
            foreach (Police p in policeList)
            {
                p.Update(gameTime);
            }
        }

       public void Draw(SpriteBatch spriteBatch)
       {
            spriteBatch.Begin();
            foreach (Police p in policeList)
            {
                p.Draw(spriteBatch);
            }
            spriteBatch.End();
       }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class EnemyManager
    {
        public List<Enemy> cars;
        private GraphicsDevice _device;


        //Spawn Logic
        private int _timeSinceLastCar = 0;
        private int _millisecondsBetweenCreation = 400;
        private int _nbrOfCarsInCurrentWave = 10;
        private int _nbrOfCarsSpawned = 0;

        public EnemyManager(GraphicsDevice graphicsDevice)
        {
            cars = new List<Enemy>();
            _device = graphicsDevice;
        }
        public void LoadWave(GameTime gameTime)
        {
            _timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
            if(_nbrOfCarsInCurrentWave > _nbrOfCarsSpawned && _timeSinceLastCar > _millisecondsBetweenCreation)
            {
                _timeSinceLastCar -= _millisecondsBetweenCreation; //Reset
                Vector2 defaultPos = new Vector2(0, 0);
                Enemy car = new Enemy(_device, ResourceManager.GetTexture("car"), defaultPos);
                cars.Add(car);

                _nbrOfCarsSpawned++;
            }
        }
        public void Update(GameTime gameTime)
        {
            //Should add logic to know if its time to spawn
            LoadWave(gameTime);
            foreach (Enemy car in cars)
            {
                car.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Enemy car in cars)
            {
                if(!car.IsHit)
                car.Draw(spriteBatch);
            }
        }

    }
}

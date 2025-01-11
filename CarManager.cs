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
    public class CarManager
    {
        public List<Car> cars;
        private GraphicsDevice _device;


        //Spawn Logic
        private int _timeSinceLastCar = 0;
        private int _millisecondsBetweenCreation = 400;
        private int _nbrOfCarsInCurrentWave = 10;
        private int _nbrOfCarsSpawned = 0;

        public CarManager(GraphicsDevice graphicsDevice)
        {
            cars = new List<Car>();
            _device = graphicsDevice;
        }
        public void LoadWave(GameTime gameTime)
        {
            _timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
            if(_nbrOfCarsInCurrentWave > _nbrOfCarsSpawned && _timeSinceLastCar > _millisecondsBetweenCreation)
            {
                _timeSinceLastCar -= _millisecondsBetweenCreation; //Reset
                Car car = new Car(_device);
                cars.Add(car);

                _nbrOfCarsSpawned++;
            }
        }
        public void Update(GameTime gameTime)
        {
            //Should add logic to know if its time to spawn
            LoadWave(gameTime);
            foreach (Car car in cars)
            {
                car.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Car car in cars)
            {
                if(!car.IsHit)
                car.Draw(spriteBatch);
            }
        }

    }
}

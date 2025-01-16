using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static List<GameObject> enemies { get; private set; }
        private static GraphicsDevice _device;


        //Spawn Logic
        private static int _waveCount = 1;
        private static int _timeSinceLastCar;
        private static int _millisecondsBetweenCreation;
        private static int _nbrOfNormalCarsInCurrentWave;
        private static int _nbrOfHeavyCarsInCurrentWave;
        private static int _nbrOfScoutCarsInCurrentWave;
        private static int _nbrOfCarsSpawned = 0;

        private static int _nbrOfCarsInCurrentWave = 0;

        private static int _timeUntilNextWave = 10;
        private static float _counter = 0;
        private static bool _randomizeWave = false;

        public static void SetUp(GraphicsDevice graphicsDevice)
        {
            enemies = new List<GameObject>();
            _device = graphicsDevice;
            LoadNextWave();
        }
        public static void LoadWave(GameTime gameTime)
        {
            if (_nbrOfCarsInCurrentWave <= _nbrOfCarsSpawned)
            {
                _counter += (float)gameTime.ElapsedGameTime.TotalSeconds;//Total gives 0... many zeros and then 16, using only seconds gives you 0
                Debug.WriteLine(_counter);
                if(_counter >= _timeUntilNextWave)
                {
                    _counter = 0;
                    _nbrOfCarsSpawned = 0;
                    _waveCount++;
                    LoadNextWave();
                }
                return;
            }

            _timeSinceLastCar += gameTime.ElapsedGameTime.Milliseconds;
            if (_randomizeWave)
            {

            }
            else
            {
                if (_nbrOfCarsInCurrentWave > _nbrOfCarsSpawned && _timeSinceLastCar > _millisecondsBetweenCreation)
                {
                    _timeSinceLastCar -= _millisecondsBetweenCreation; //Reset
                    Vector2 defaultPos = new Vector2(0, 0);
                    Enemy car = null;
                    if (_nbrOfNormalCarsInCurrentWave > 0)
                    {
                        car = new NormalEnemy(_device, ResourceManager.GetTexture("car"), defaultPos, 1f);
                        _nbrOfNormalCarsInCurrentWave--;
                    }
                    else if (_nbrOfScoutCarsInCurrentWave > 0)
                    {
                        car = new ScoutEnemy(_device, ResourceManager.GetTexture("scoutCar"), defaultPos, 1f);
                        _nbrOfScoutCarsInCurrentWave--;
                    }
                    else if (_nbrOfHeavyCarsInCurrentWave > 0)
                    {
                        car = new HeavyEnemy(_device, ResourceManager.GetTexture("heavyCar"), defaultPos, 1f);
                        
                        _nbrOfHeavyCarsInCurrentWave--;
                    }
                    if(car != null)
                    {
                        enemies.Add(car);
                        CollisionManager.Collidables.Add(car);
                    }
                    _nbrOfCarsSpawned++;
                }
            }
        }
        public static void Update(GameTime gameTime)
        {
            LoadWave(gameTime);

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(Enemy enemy in enemies)
            {
              //  if(!enemy.IsHit)
                    enemy.Draw(spriteBatch);
            }
        }
        public static void LoadNextWave()
        {
            switch (_waveCount)
            {
                case 1:
                    _millisecondsBetweenCreation = 2000;
                    _nbrOfNormalCarsInCurrentWave = 10;
                    _nbrOfHeavyCarsInCurrentWave = 0;
                    _nbrOfScoutCarsInCurrentWave = 0;
                    _randomizeWave = false;
                    break;
                case 2:
                    _millisecondsBetweenCreation = 1500;
                    _nbrOfNormalCarsInCurrentWave = 20;
                    _nbrOfHeavyCarsInCurrentWave = 0;
                    _nbrOfScoutCarsInCurrentWave = 0;
                    _randomizeWave = false;
                    break;
                case 3:
                    _millisecondsBetweenCreation = 1000;
                    _nbrOfNormalCarsInCurrentWave = 20;
                    _nbrOfHeavyCarsInCurrentWave = 5;
                    _nbrOfScoutCarsInCurrentWave = 0;
                    _randomizeWave = false;
                    break;
                case 4:
                    _millisecondsBetweenCreation = 1000;
                    _nbrOfNormalCarsInCurrentWave = 30;
                    _nbrOfHeavyCarsInCurrentWave = 20;
                    _nbrOfScoutCarsInCurrentWave = 0;
                    _randomizeWave = false;
                    break;
                case 5:
                    _millisecondsBetweenCreation = 1000;
                    _nbrOfNormalCarsInCurrentWave = 30;
                    _nbrOfHeavyCarsInCurrentWave = 15;
                    _nbrOfScoutCarsInCurrentWave = 5;
                    _randomizeWave = false;
                    break;
                case 6:
                    _millisecondsBetweenCreation = 1000;
                    _nbrOfNormalCarsInCurrentWave = 60;
                    _nbrOfHeavyCarsInCurrentWave = 30;
                    _nbrOfScoutCarsInCurrentWave = 10;
                    _randomizeWave = false;
                    break;
                case 7:
                    _millisecondsBetweenCreation = 800;
                    _nbrOfNormalCarsInCurrentWave = 60;
                    _nbrOfHeavyCarsInCurrentWave = 30;
                    _nbrOfScoutCarsInCurrentWave = 10;
                    _randomizeWave = true;
                    break;
                case 8:
                    _millisecondsBetweenCreation = 600;
                    _nbrOfNormalCarsInCurrentWave = 60;
                    _nbrOfHeavyCarsInCurrentWave = 30;
                    _nbrOfScoutCarsInCurrentWave = 10;
                    _randomizeWave = true;
                    break;
                case 9:
                    _millisecondsBetweenCreation = 500;
                    _nbrOfNormalCarsInCurrentWave = 80;
                    _nbrOfHeavyCarsInCurrentWave = 50;
                    _nbrOfScoutCarsInCurrentWave = 20;
                    _randomizeWave = true;
                    break;
                case 10:
                    _millisecondsBetweenCreation = 500;
                    _nbrOfNormalCarsInCurrentWave = 150;
                    _nbrOfHeavyCarsInCurrentWave = 75;
                    _nbrOfScoutCarsInCurrentWave = 50;
                    _randomizeWave = true;
                    break;
            }
            _nbrOfCarsInCurrentWave = _nbrOfNormalCarsInCurrentWave + _nbrOfHeavyCarsInCurrentWave + _nbrOfScoutCarsInCurrentWave;
        }
    }
}

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
    public class Car
    {
        private CatmullRomPath _car; //Draw car
        public Rectangle HitBox;

        private float _currentCurvePos = 0;
        private float _currentCurveSpeed = 0.2f;

        //live
        //private float _health = 3;
        public bool IsHit = false; //Gör den till isDead sen

        private GraphicsDevice _device;

        public Car(GraphicsDevice device)
        {
            _device = device;

            float tensionRoad = 0.5f;

            _car = new CatmullRomPath(_device, tensionRoad);
            _car.Clear();//Vi vill inte ha default punkter, vi vill göra en egen väg

            LoadPath.LoadPathFromFile(_car, "carpath1.txt");

            //Grejer för hur fett bil ska vara och vad den ska innehålla
            _car.DrawFillSetup(_device, 2, 1, 256);


            HitBox = new Rectangle(0, 0, 50, 50);
        }
        public void Update(GameTime gameTime)
        {
            _currentCurvePos += _currentCurveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                //Som den där GetPos eller vad den hette
               Vector2 currentPos = _car.EvaluateAt(_currentCurvePos);
                HitBox.X = (int)currentPos.X;
                HitBox.Y = (int)currentPos.Y;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                if (!IsHit)
                _car.DrawMovingObject(_currentCurvePos, spriteBatch, AssetManager.carTex);
            }
        }
    }
}

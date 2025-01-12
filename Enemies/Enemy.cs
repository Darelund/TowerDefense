﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public enum EnemyType
    {
        Normal,
        Heavy,
        Scout
    }
    public class Enemy : GameObject
    {
        
        private CatmullRomPath _car; //Draw car

        private float _currentCurvePos = 0;
        private float _speed;
        private float _health;
        private float _reward;

        private EnemyType _enemyType;

        //live
        //private float _health = 3;
        public bool IsHit = false; //Gör den till isDead sen

        private GraphicsDevice _device;

        public Enemy(GraphicsDevice device, Texture2D tex, Vector2 pos, EnemyType enemyType) : base(tex, pos)
        {
            _device = device;

            float tensionRoad = 0.5f;

            _car = new CatmullRomPath(_device, tensionRoad);
            _car.Clear();//Vi vill inte ha default punkter, vi vill göra en egen väg

            LoadPath.LoadPathFromFile(_car, LevelManager.CurrentLevel.LevelMap);

            //Grejer för hur fett bil ska vara och vad den ska innehålla
            _car.DrawFillSetup(_device, 4, 1, 256);
            _enemyType = enemyType;
            switch (_enemyType)
            {
                case EnemyType.Normal:
                    _speed = 0.05f;
                    _health = 5;
                    _reward = 1;
                    break;
                case EnemyType.Heavy:
                    _speed = 0.02f;
                    _health = 30;
                    _reward = 15;
                    break;
                case EnemyType.Scout:
                    _speed = 0.2f;
                    _health = 2;
                    _reward = 5;
                    break;
                default:
                    break;
            }
        }
        public override void Update(GameTime gameTime)
        {
            _currentCurvePos += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                //Som den där GetPos eller vad den hette
               Vector2 currentPos = _car.EvaluateAt(_currentCurvePos);
                position = currentPos;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentCurvePos < 1 && _currentCurvePos > 0)
            {
                if (!IsHit)
                _car.DrawMovingObject(_currentCurvePos, spriteBatch, texture);
            }
        }
    }
}

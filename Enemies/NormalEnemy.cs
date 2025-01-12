﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class NormalEnemy : Enemy
    {
        public NormalEnemy(GraphicsDevice device, Texture2D tex, Vector2 pos, float scale) : base(device, tex, pos, EnemyType.Normal, scale)
        {
        }
    }
}

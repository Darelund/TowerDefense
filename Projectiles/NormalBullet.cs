﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class NormalBullet : Projectile
    {
        public NormalBullet(Texture2D tex, Vector2 startPos, Vector2 direction, float rotation, float speed, float scale) : base(tex, startPos, direction, rotation, speed, scale)
        {
            dmg = 1;
        }
    }
}

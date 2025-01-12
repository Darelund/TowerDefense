using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class MissileTower : Tower
    {
        public MissileTower(Texture2D tex, Vector2 pos, float scale) : base(tex, pos, scale)
        {
        }
    }
}

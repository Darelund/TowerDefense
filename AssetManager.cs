using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public static class AssetManager
    {
        public static Texture2D redTex;
        public static Texture2D yellowTex;
        public static Texture2D carTex;
        public static Texture2D roadTex;
        public static Texture2D policeTex;

        public static void LoadTextures(ContentManager content, GraphicsDevice graphicsDevice)
        {
            carTex = content.Load<Texture2D>("car");
            roadTex = content.Load<Texture2D>("road");
            policeTex = content.Load<Texture2D>("police");

            yellowTex = content.Load<Texture2D>("yellow");

            redTex = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            //redTex.SetData<Microsoft.Xna.Framework.Color>(new Color[] {Color.Red});
            redTex.SetData(new Color[] {Color.Red});

        }
    }
}

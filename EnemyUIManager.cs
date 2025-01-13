using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class EnemyUIManager
    {
        private static UIText _healthText;
        private static SpriteFont font;
        private static string text;
        private static Vector2 _pos;
        private static Color color;
        private static float size;
        private static Vector2 origin;
        private static float layerDepth;
        private static float rotation;


        public static void SetUp()
        {
            font = ResourceManager.GetSpriteFont("GameText");
            text = $"";
            _pos = Vector2.Zero;
            color = Color.Yellow;
            size = 0.5f;
            origin = Vector2.Zero;
            layerDepth = 0;
            rotation = 0;

            _healthText = new UIText(font, text, _pos, color, size, origin, layerDepth, rotation);
        }
        public static void Update(List<Enemy> enemies)
        {
            //foreach (var enemy in enemies)
            //{
            //    var healthText = enemy._health.ToString();
            //    var position = enemy.Position;

            //    // Offset the text to appear above the enemy's head
            //    var textPosition = new Vector2(position.X, position.Y - 20);

            //    //  spriteBatch.DrawString(font, healthText, textPosition, Color.Red);
            //   // _healthText.Draw(spriteBatch);

            //}
          //  _healthText.Position = position;
          //  _healthText.Rotation = _rotation;
        }
        public static void Draw(SpriteBatch spriteBatch, List<GameObject> enemies)
        {
            foreach (var enemy in enemies)
            {
                var e = enemy as Enemy;
                var healthText = e._health.ToString();

                var position = e.Position;
                // Offset the text to appear above the enemy's head
                var textPosition = new Vector2(position.X, position.Y - 20);

                var rotation = e._rotation;

                  _healthText.Position = textPosition;
                  _healthText.Rotation = rotation;
                _healthText._text = healthText;

                //  spriteBatch.DrawString(font, healthText, textPosition, Color.Red);
                _healthText.Draw(spriteBatch);

            }
        }
    }
}

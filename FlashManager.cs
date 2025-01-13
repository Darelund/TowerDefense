using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class FlashManager
    {
        private static List<FlashEffect> _flashEffects = new List<FlashEffect>();

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < _flashEffects.Count; i++)
            {
                if (!_flashEffects[i].IsActive)
                    _flashEffects.RemoveAt(i);
                else
                    _flashEffects[i].Update(gameTime);
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in EnemyManager.enemies)
            {
                bool isFlashing = false;

                foreach (var effect in _flashEffects)
                {
                    if (effect.IsActiveOnObject(gameObject))
                    {
                        effect.ApplyDrawEffect(spriteBatch);
                        isFlashing = true;
                        break;
                    }
                }
                gameObject.Draw(spriteBatch);

                if (isFlashing)
                {
                    spriteBatch.End();
                    spriteBatch.Begin();
                }
            }
        }
        public static void AddFlashEffect(FlashEffect flashEffect)
        {
            _flashEffects.Add(flashEffect);
        }
    }
}

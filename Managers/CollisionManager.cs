using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public static class CollisionManager
    {
        public static List<GameObject> _collidables => EnemyManager.enemies;
        public static void CheckCollision()
        {
            //  Debug.WriteLine(_collidables.Count);
            for (int i = 0; i < _collidables.Count; i++)
            {
                for (int j = i + 1; j < _collidables.Count; j++)
                {
                    if (_collidables[i].Hitbox.Intersects(_collidables[j].Hitbox))
                    {
                        _collidables[j].OnCollision(_collidables[i]);

                        if (CollisionExistsAtPosition(j))
                        {
                            _collidables[i].OnCollision(_collidables[j]);
                        }
                    }
                }
            }
            //   Debug.WriteLine(_collidables.Count);
        }
        private static bool CollisionExistsAtPosition(int collisionPos)
        {
            if (collisionPos >= _collidables.Count) return false;

            return true;
        }
    }
}

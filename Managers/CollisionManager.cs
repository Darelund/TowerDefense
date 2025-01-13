using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public static class CollisionManager
    {
        //   public static List<GameObject> _collidables => EnemyManager.enemies;//THis is the problem
           public static List<GameObject> Collidables { get; private set; } = new List<GameObject>();

        public static void CheckCollision()
        {
            //  Debug.WriteLine(_collidables.Count);
            for (int i = 0; i < Collidables.Count; i++)
            {
                for (int j = i + 1; j < Collidables.Count; j++)
                {
                    if (Collidables[i].HitSphere.Intersects(Collidables[j].HitSphere))
                    {
                        Collidables[j].OnCollision(Collidables[i]);

                        if (CollisionExistsAtPosition(j))
                        {
                            Collidables[i].OnCollision(Collidables[j]);
                        }
                    }
                }
            }
            //   Debug.WriteLine(_collidables.Count);
        }
        private static bool CollisionExistsAtPosition(int collisionPos)
        {
            if (collisionPos >= Collidables.Count) return false;

            return true;
        }
    }
}

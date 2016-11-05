using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static WebApp1.Emeny;

namespace WebApp1
{
    class Tower1
    {


     public class Tower : Sprites
        {
            protected int cost;
            protected int damage;

            protected float radius;

            protected Enemy target;

            public int Cost
            {
                get { return cost; }
            }
            public int Damage
            {
                get { return damage; }
            }

            public float Radius
            {
                get { return radius; }
            }

            public Enemy Target
            {
                get { return target; }
            }

            public Tower(Texture2D texture, Vector2 position)
                : base(texture, position)
            {
                radius = 1000;
            }

            protected void FaceTarget()
            {
                Vector2 direction = center - target.Center;
                direction.Normalize();

                rotation = (float)Math.Atan2(-direction.X, direction.Y);
            }

            public void GetClosestEnemy(List<Enemy> enemies)
            {
                target = null;
                float smallestRange = radius;

                foreach (Enemy enemy in enemies)
                {
                    if (Vector2.Distance(center, enemy.Center) < smallestRange)
                    {
                        smallestRange = Vector2.Distance(center, enemy.Center);
                        target = enemy;
                    }
                }
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);

                if (target != null)
                    FaceTarget();
            }
        }
    }

}


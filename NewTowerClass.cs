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
    class Towers
    {
        public class Tower : Sprites
        {
            protected int cost;
            protected int damage;

            protected float radius;

            protected Enemy target;

            protected float bulletTimer;
            protected Texture2D bulletTexture;

            protected List<ammo> ammoList = new List<ammo>();

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

            public virtual bool HasTarget
            {

                get { return target != null; }
            }

            public Tower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
                : base(texture, position)
            {
                this.bulletTexture = bulletTexture;
            }

            protected void FaceTarget()
            {
                Vector2 direction = center - target.Center;
                direction.Normalize();

                rotation = (float)Math.Atan2(-direction.X, direction.Y);
            }

            public bool IsInRange(Vector2 position)
            {
                return Vector2.Distance(center, position) <= radius;
            }

            public virtual void GetClosestEnemy(List<Enemy> enemies)
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

                bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (target != null)
                {
                    FaceTarget();

                    if (!IsInRange(target.Center) || target.IsDead)
                    {
                        target = null;
                        bulletTimer = 0;
                    }
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);

                foreach (ammo in ammoList)
                   ammo.Draw(spriteBatch);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpritesCS;
using EnemyCS;
using AmmoCS;
//using Placement;

namespace TowersCS
{
    public class Tower : Sprites
    {
        protected int cost; // how much tower will cost to make
        protected int damage;   // damage done to enemies

        protected float radius; // how far the tower can shoot

        protected Enemy target;

        protected float bulletTimer;
        protected Texture2D bulletTexture;

        List<Ammo> ammoList = new List<Ammo>();

        // get/set for class values
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

        // first constructor
        public Tower(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            // testing!!
            radius = 1000;
        }

        // 2nd constructor?
        public Tower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
            : base(texture, position)
        {
            this.bulletTexture = bulletTexture;
        }

        protected void FaceTarget()
        {
            // this gets the direction our target is in
            Vector2 direction = center - target.Center;
            direction.Normalize();
            // this gives us the angle we need to rotate in order to face the target
            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }

        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(center, position) <= radius)
                return true;
            // else
            return false;
        }

        public virtual void GetClosestEnemy(List<Enemy> enemies)
        {
            target = null;
            float smallestRange = radius;

            // loop through every enemy currently alive to check which one is the closest
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

            //bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (target != null)
            {
                // makes sure we always face the target
                FaceTarget();

                //if (!IsInRange(target.Center) || target.IsDead)
                //{
                //    target = null;
                //    bulletTimer = 0;
                //}
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Ammo ammo in ammoList)
                ammo.Draw(spriteBatch);
        }
    }
}
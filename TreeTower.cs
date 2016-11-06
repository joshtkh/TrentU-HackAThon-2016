using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowersCS;
using BulletCS;

namespace TreeTowerCS
{
    public class TreeTower : Tower
    {
        // constructor
        public TreeTower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position)
        {
            //damage and stuff for trees
            this.damage = 15;
            this.cost = 15;
            this.radius = 100;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // bends bullets towards target
            if (bulletTimer >= 0.75f && target != null)
            {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center, new Vector2(bulletTexture.Width / 2)), rotation, 6, damage);

                bulletList.Add(bullet);
                bulletTimer = 0;
            }

            // loops through our bullet list to update it
            for (int i = 0; i < bulletList.Count; i++)
            {
                Bullet bullet = bulletList[i];

                bullet.SetRotation(rotation);
                bullet.Update(gameTime);

                if (!IsInRange(bullet.Center))
                    bullet.Kill();

                if (target != null && Vector2.Distance(bullet.Center, target.Center) < 12) // 12 = (width of bullet / 2) + (width of enemy / 2)
                {
                    target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }

                if (bullet.IsDead())
                {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }
    }
}
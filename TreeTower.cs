using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTower
{
    class TreeTower
    {
        public class TreeTower : Tower
        {
            public TreeTower(Texture2D texture, Texture2D ammoTexture, Vector2 position)
                : base(texture, ammoTexture, position)
            {//damage and stuff for trees
                this.damage = 15;

                this.cost = 15;

                this.radius = 100;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);

                if (bulletTimer >= 0.75f && target != null)
                {
                    Ammo ammo = new Ammo(ammoTexture, Vector2.Subtract(center,
                        new Vector2(ammoTexture.Width / 2)), rotation, 6, damage);

                    ammoList.Add(ammo);
                    bulletTimer = 0;
                }

                for (int i = 0; i < ammoList.Count; i++)
                {
                    Ammo ammo = ammotList[i];

                    ammo.SetRotation(rotation);
                    ammo.Update(gameTime);

                    if (!IsInRange(ammo.Center))
                        ammo.Kill();

                    if (target != null && Vector2.Distance(ammo.Center, target.Center) < 15)
                    {
                        target.CurrentHealth -= ammo.Damage;
                        ammo.Kill();
                    }

                    if (ammo.IsDead())
                    {
                        ammoList.Remove(ammo);
                        i--;
                    }
                }
            }
        }
    




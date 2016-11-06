using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpritesCS;

namespace AmmoCS
{
    public class Ammo : Sprites
    {
        private int damage;
        private int age;

        private int speed;

        public int Damage
        {
            get { return damage; }
        }

        public bool IsDead()
        {
            return age > 100;
        }

        public Ammo(Texture2D texture, Vector2 position, float rotation, int speed, int damage)
            : base(texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;

            this.speed = speed;

            velocity = Vector2.Transform(new Vector2(0, -speed),
                Matrix.CreateRotationZ(rotation));
        }

        public Ammo(Texture2D texture, Vector2 position, Vector2 velocity, int speed, int damage)
            : base(texture, position)
        {
            this.damage = damage;

            this.speed = speed;

            this.velocity = velocity * speed;
        }

        public void Kill()
        {
            this.age = 200;
        }

        public void SetRotation(float value)
        {
            rotation = value;

            velocity = Vector2.Transform(new Vector2(0, -speed),
                Matrix.CreateRotationZ(rotation));
        }

        public override void Update(GameTime gameTime)
        {
            age++;
            position += velocity;

            base.Update(gameTime);
        }
    }
}

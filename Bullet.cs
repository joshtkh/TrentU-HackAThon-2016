using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpritesCS;

namespace BulletCS
{
    public class Bullet : Sprites
    {
        private int damage;
        private int age;

        private int speed;

        public int Damage
        {
            get { return damage; }
        }
        // method to check if bullet is dead
        public bool IsDead()
        {
            return age > 100;
        }

        public Bullet(Texture2D texture, Vector2 position, float rotation, int speed, int damage)
            : base (texture, position)
        {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;
        }

        // method used to kill the bullet
        public void Kill()
        {
            this.age = 200;
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            // increase age of the bullet every update
            age++;
            // add velocity onto bullets pos
            position += velocity;

            base.Update(gameTime);
        }

        // method used to keep the bullet moving towards the target
        public void SetRotation(float value)
        {
            rotation = value;
            // rotate our speed vector to match our tower
            velocity = Vector2.Transform(new Vector2(0, -speed), Matrix.CreateRotationZ(rotation));
        }
    }
}

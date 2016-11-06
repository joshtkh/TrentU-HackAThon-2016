using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpritesCS;

namespace EnemyCS
{
    public class Enemy : Sprites
    {
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        protected float startHealth;
        protected float currentHealth;

        protected bool alive = true;

        protected float speed = 0.5f;
        protected int bountyGiven = 0;

        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public bool IsDead
        {
            get { return !alive; }
        }

        public int BountyGiven
        {
            get { return bountyGiven; }
        }

        public float DistanceToDestination
        {
            // calculates how far we are to the next waypoint
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public Enemy(Texture2D texture, Vector2 position, float health, int bountyGiven, float speed)
            : base(texture, position)
        {
            this.startHealth = health;
            this.currentHealth = startHealth;
            this.bountyGiven = bountyGiven;
            this.speed = speed;
        }

        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            // adds all of our waypoints into the ENEMIES queue of waypoints
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);
            // sets the position of the enemy.
            this.position = this.waypoints.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // waypoint stuff:
            if (waypoints.Count > 0)    // if there are no more waypoints left
            {
                if (DistanceToDestination < speed)  // checks if enemy is close to destination
                {
                    // if enemy is close enough, we just say that the enemy is at the waypoint
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    // directon we need to travel to get from curr pos to next waypoint
                    Vector2 direction = waypoints.Peek() - position;
                    direction.Normalize();
                    // increase our velocity in that direction
                    velocity = Vector2.Multiply(direction, speed);
                    position += velocity;
                }
            }
            else
                alive = false;  // if there are no more waypoints to go to, the unit dies.

            // checks if the enemy is dead or not
            if (currentHealth <= 0)
                alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // check if enemy is alive
            if (alive)
            {
                // work out what % health enemy lost
                float healthPercentage = (float)currentHealth / (float)startHealth;
                // create colour to correspond to health %
                Color color = new Color(new Vector3(1 - healthPercentage, 1 - healthPercentage, 1 - healthPercentage));
                base.Draw(spriteBatch, color);
            }
        }
    } 
}

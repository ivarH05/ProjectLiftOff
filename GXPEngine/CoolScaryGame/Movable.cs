using GXPEngine.Core;
using GXPEngine;
using System;

namespace CoolScaryGame
{
    internal class Movable : InvisibleObject
    {
        internal Vector2 Velocity;
        internal float Friction = 0.1f;
        internal Sprite renderer;

        internal float timer = 0;
        public Movable(int width, int height, Vector2 Position = new Vector2(), bool addCollider = false) : base(width, height, addCollider, 0b10, 0b11)
        {
            position = Position;
        }

        /// <summary>
        /// move the object without collision, though at a consistant rate
        /// </summary>
        public virtual void PhysicsUpdate()
        {
            timer += Time.deltaTime;
            if (timer > Time.TimeStep)
            {
                timer -= Time.deltaTime;
                position += Velocity * Time.TimeStep;
                AddFriction(Friction);
            }
        }

        /// <summary>
        /// add force to the velocity
        /// </summary>
        /// <param name="Force"></param>
        public void AddForce(Vector2 Force)
        {
            Velocity += Force;
        }

        internal void AddFriction(float amount)
        {
            Velocity = Velocity.Lerp(new Vector2(), amount);
        }
    }
}

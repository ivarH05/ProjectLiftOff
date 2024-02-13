using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Movable : Sprite
    {
        internal float mass = 1;

        internal Vector2 Velocity;
        internal float Friction = 0.1f;

        internal float timer = 0;
        public Movable(string spritePath, Vector2 Position = new Vector2(), bool addCollider = false) : base(spritePath, addCollider)
        {
            position = Position;
        }


        public virtual void PhysicsUpdate()
        {
            timer += Time.deltaTime;
            if (timer > Time.TimeStep)
            {
                timer -= Time.deltaTime;
                MoveUntilCollision(Velocity.x * Time.TimeStep, Velocity.y * Time.TimeStep);
                position += Velocity * 0.00001f;
                AddFriction(Friction);
            }
        }
        public void AddForce(Vector2 Force)
        {
            Velocity += Force;
        }

        internal void AddFriction(float amount)
        {
            Velocity = Velocity.Lerp(new Vector2(), amount);
        }
        public float GetMass()
        {
            return mass;
        }
    }
}

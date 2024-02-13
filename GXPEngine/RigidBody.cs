using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class RigidBody : Movable
    {
        float bounciness = 0.1f;
        public RigidBody(string spritePath, Vector2 Position = new Vector2(), bool addCollider = false) : base(spritePath, Position, addCollider)
        {

        }

        /// <summary>
        /// Move the object, collide with others and 
        /// </summary>
        public override void PhysicsUpdate()
        {
            timer += Time.deltaTime;
            if (timer > Time.TimeStep)
            {
                timer -= Time.deltaTime;
                Collision c = MoveUntilCollision(Velocity.x * Time.TimeStep, Velocity.y * Time.TimeStep);
                AddFriction(Friction);

                if (c == null) return;
                if(c.other is Movable)
                {
                    Movable other = (Movable)c.other;
                    other.AddForce(Velocity * 1-bounciness);
                }

                float Mag = Velocity.Magnitude;
                Vector2 Norm = Velocity / Mag;
                Vector2 Out = Norm - 2 * Norm * c.normal * c.normal;
                Velocity = Out * Mag * bounciness;
            }
        }
    }
}

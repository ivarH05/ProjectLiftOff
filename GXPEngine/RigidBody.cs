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
        internal Vector2 ActualVelocity;

        internal bool canPush = true;

        float bounciness = 0.1f;
        public RigidBody(string spritePath, Vector2 Position = new Vector2(), bool addCollider = false) : base(spritePath, Position, addCollider)
        {

        }

        /// <summary>
        /// Move the object, collide with others and add force to other hit objects.
        /// </summary>
        public override void PhysicsUpdate()
        {
            timer += Time.deltaTime;
            if (timer > Time.TimeStep)
            {
                timer -= Time.deltaTime;
                Vector2 LastPos = position;
                Collision c = MoveUntilCollision(Velocity.x * Time.TimeStep, Velocity.y * Time.TimeStep);
                AddFriction(Friction);

                if (c == null) return;
                //Push other movables away
                if(c.other is Movable && canPush)
                {
                    Movable other = (Movable)c.other;
                    other.AddForce(c.normal * -250 * bounciness);
                }
                position -= Velocity * 0.0001f;
                ActualVelocity = position - LastPos;
            }
        }
    }
}

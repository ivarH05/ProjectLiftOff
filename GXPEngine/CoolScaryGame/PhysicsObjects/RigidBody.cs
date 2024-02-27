using GXPEngine.Core;
using GXPEngine;
using System;

namespace CoolScaryGame
{
    public class RigidBody : Movable
    {
        public bool isDissabled = false;
        public bool isKinematic = false;

        internal bool canPush = true;
        internal uint CollisionLayers = 1;
        internal uint CoupleWithLayers = 1;


        float bounciness = 0.1f;
        public RigidBody(int width, int height, Vector2 Position = new Vector2(), bool addCollider = false, uint collisionLayers = 0b1, uint coupleWithLayers = 0b1) : base(width, height, Position, true, collisionLayers, coupleWithLayers)
        {

        }

        /// <summary>
        /// Move the object, collide with others and add force to other hit objects.
        /// </summary>
        public override void PhysicsUpdate()
        {
            depth = -y / 100000;
            if (isDissabled)
                collider.CollisionLayers = 0;
            else
                collider.CollisionLayers = CollisionLayers;

            if (isKinematic) 
            {
                collider.CoupleWithLayers = 0;
                return;
            }
            else
                collider.CoupleWithLayers = CoupleWithLayers;


            _timer += Time.deltaTime;
            while (_timer > Time.TimeStep)
            {
                _timer -= Time.deltaTime;
                Vector2 LastPos = position;
                Vector2 offset = MoveSeperate(Velocity * Time.TimeStep);
                position += offset;
                AddFriction(Friction);
            }
        }

        public Vector2 MoveSeperate(Vector2 vec)
        {
            Vector2 output = new Vector2();

            Collision Coll1 = TryMove(vec.x, 0);
            Collision Coll2 = TryMove(0, vec.y);

            if (Coll1 != null)
                output += Coll1.normal;
            if (Coll2 != null)
                output += Coll2.normal;

            return output;
        }

        Collision TryMove(float x, float y)
        {
            Collision c = MoveUntilCollision(x, y);

            if (c == null) return null;
            //Push other movables away
            if (c.other is Movable && canPush)
            {
                Movable other = (Movable)c.other;
                other.AddForce(c.normal * -250 * bounciness);
            }
            return c;
        }
    }
}

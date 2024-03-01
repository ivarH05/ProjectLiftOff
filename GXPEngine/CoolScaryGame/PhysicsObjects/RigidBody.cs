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
        internal bool UnClip = true;


        float bounciness = 0.1f;
        public RigidBody(int width, int height, Vector2 Position = new Vector2(), bool addCollider = false, uint collisionLayers = 0b1, uint coupleWithLayers = 0b1) : base(width, height, Position, true, collisionLayers, coupleWithLayers)
        {
            CollisionLayers = collisionLayers;
            CoupleWithLayers = coupleWithLayers;
            //debugVisible = true;
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
                Vector2 offset = MoveSeperate(Velocity * Time.TimeStep);
                if (UnClip)
                    position += offset;
                else
                    position += offset * 0.001f;
                AddFriction(Friction);
            }
        }

        /// <summary>
        /// Move sepperately ove the x and y axis
        /// </summary>
        /// <param name="vec"></param>
        /// <returns>Returns a vector that will avoid or stop any collission or clipping</returns>
        public Vector2 MoveSeperate(Vector2 vec)
        {
            Vector2 output = new Vector2();

            Collision Coll1 = TryMove(vec.x, 0);
            Collision Coll2 = TryMove(0, vec.y);

            if (Coll1 != null)
            {
                Collision(Coll1.other);
                output += Coll1.normal;
            }
            if (Coll2 != null)
            {
                Collision(Coll2.other);
                output += Coll2.normal;
            }

            return output;
        }

        /// <summary>
        /// move until collission including pushing
        /// </summary>
        Collision TryMove(float x, float y)
        {
            Collision c = MoveUntilCollision(x, y);
            if (c == null) return null;

            //Push other movables away
            if (c.other is Movable other && canPush)
                other.AddForce(c.normal * -250 * bounciness);
            return c;
        }

        virtual internal void Collision(GameObject Other)
        {

        }
    }
}

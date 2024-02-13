using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Movable : Sprite
    {
        internal Vector2 Velocity;
        internal float Friction = 1;

        float timer = 0;

        public Movable(string spritePath, bool addCollider = false) : base(spritePath, addCollider) { }

        public void AddForce(Vector2 Force)
        {
            Velocity += Force;
        }

        public void PhysicsUpdate()
        {
            timer += Time.deltaTime;
            while (timer > Time.TimeStep)
            {
                timer -= Time.deltaTime;
                position += Velocity * Time.TimeStep;
            }
        }

        void AddFriction(float amount)
        {
            Velocity = Velocity.Lerp(new Vector2(), amount);
        }
    }
}

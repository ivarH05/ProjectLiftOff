using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Seeker : RigidBody
    {
        float speed = 5;
        public Seeker(Vector2 Position) : base("square.png", Position, true) { }

        void Update()
        {
            AddForce(Input.ArrowVector() * Time.deltaMillis * speed);

            PhysicsUpdate();
            CamManager.LerpToPoint(1, position + Velocity * 0.2f, Time.deltaTime * 5);
        }
    }
}

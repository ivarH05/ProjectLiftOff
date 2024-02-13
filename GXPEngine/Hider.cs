using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Hider : RigidBody
    {
        float speed = 5;
        public Hider(Vector2 position) : base("triangle.png", position, true) { }

        void Update()
        {
            AddForce(Input.WASDVector() * Time.deltaMillis * speed);

            PhysicsUpdate();
            CamManager.LerpToPoint(0, position + Velocity * 0.2f, Time.deltaTime * 5);
        }
    }
}

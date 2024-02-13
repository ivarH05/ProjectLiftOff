﻿using GXPEngine.Core;
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
        public Hider(Vector2 position) : base("triangle.png", position, true)
        {
            RenderLayer = 0;
        }

        void Update()
        {
            //move using wasd
            AddForce(Input.WASDVector() * Time.deltaMillis * speed);

            //update all physics
            PhysicsUpdate();

            //move the camera towards the player
            CamManager.LerpToPoint(0, position + ActualVelocity * 0.5f, Time.deltaTime * 5);
        }
    }
}

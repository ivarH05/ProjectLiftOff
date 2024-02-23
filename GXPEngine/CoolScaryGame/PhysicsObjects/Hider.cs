﻿using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    internal class Hider : Player
    {

        public Portable HoldingItem = null;
        public Hider(Vector2 Position) :
            base(Position, new AnimationData("Animations/HiderIdleAnim.png", 3, 3), new AnimationData("Animations/HiderMovementAnim.png", 5, 2))
        {
        }

        void Update()
        {
            //move using wasd
            AddForce(Input.WASDVector() * Time.deltaMillis * speed);
            PlayerUpdates(0);

            if (HoldingItem != null)
            {
                HoldingItem.position = HoldingItem.position.Lerp(position + new Vector2(0, -76 - HoldingItem.renderer.height / 2), Time.deltaTime * 32);
                if (Input.GetKeyDown(Key.E))
                    DropObject();
            }
            else if (Input.GetKeyDown(Key.E))
            {
                Sprite s = new Sprite("Square.png", false, true, 0, 0b10);
                s.position = position + Velocity.Normalized * 64;

                GrabObject((Portable)GetClosestOfType<Portable>(s.GetCollisions()));
            }
        }

        private void GrabObject(Portable obj)
        {
            if (obj == null || obj.isDissabled)
                return;
            obj.isDissabled = true;
            obj.isKinematic = true;
            HoldingItem = obj;
        }

        private void DropObject()
        {
            HoldingItem.position = position + Velocity.Normalized * HoldingItem.width;
            HoldingItem.Velocity = Velocity;
            HoldingItem.isDissabled = false;
            HoldingItem.isKinematic = false;

            GameObject[] objects = HoldingItem.GetCollisions();
            foreach (GameObject obj in objects)
            {
                Console.WriteLine(obj);
                if (obj is Seeker SeekerObject)
                {
                    SeekerObject.Stun(0.5f);
                    Console.WriteLine("hit");
                }
            }
            HoldingItem = null;
        }

        GameObject GetClosestOfType<T>(GameObject[] objects)
        {
            GameObject closest = null;
            float distance = 999999;

            foreach (GameObject obj in objects)
            {
                if (!(obj is T))
                    continue;
                float dist = Vector2.Distance(position, obj.position);
                if (dist < distance)
                {
                    closest = obj;
                    distance = dist;
                }
            }
            return closest;
        }
    }
}

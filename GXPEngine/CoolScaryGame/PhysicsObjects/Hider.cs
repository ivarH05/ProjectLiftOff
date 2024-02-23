using GXPEngine.Core;
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
            base(Position, "Animations/HiderAnimations.png", 5, 4, new AnimationData(10, 9), new AnimationData(0, 10))
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
                GrabObject((Portable)GetObjectInFront<Portable>());
            }
            renderer.alpha = 0.75f;
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
            HoldingItem = null;
        }

        public GameObject GetObjectInFront<T>()
        {
            Sprite s = new Sprite("Square.png", false, true, 0, 0b10);
            s.position = position + Velocity.Normalized * 64;

            return GetClosestOfType<T>(s.GetCollisions());
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

using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.CoolScaryGame.Particles;

namespace CoolScaryGame
{
    public class Hider : Player
    {

        public Portable HoldingItem = null;
        public Hider(Vector2 Position) :
            base(Position, 0, "Animations/HiderAnimations.png", 5, 4, new AnimationData(10, 9), new AnimationData(0, 10))
        {
            PlayerManager.SetHider(this);
            playerColor = 0xA0A0FF;
        }

        void Update()
        {
            speed = Mathf.Lerp(speed, 2.5f, Time.deltaTime * 5) + speedBoost;
            //move using wasd
            AddForce(Input.WASDVector() * Time.deltaMillis * speed);
            PlayerUpdates(0);

            if (HoldingItem != null)
            {
                HoldingItem.position = HoldingItem.position.Lerp(position + new Vector2((width - HoldingItem.width) / 2, 0), Time.deltaTime * 32);
                if (Input.GetKeyDown(Key.E))
                    DropObject();
            }
            else if (Input.GetKeyDown(Key.E))
            {
                GrabObject((Portable)GetObjectInFrontOfType<Portable>());
            }
            renderer.alpha = 0.75f;
            if (Input.GetKeyDown(Key.Q))
                useItem();
        }

        /// <summary>
        /// pickup the specified object
        /// </summary>
        /// <param name="obj">object to pickup</param>
        private void GrabObject(Portable obj)
        {
            if (obj == null || obj.isDissabled)
                return;
            obj.isDissabled = true;
            obj.isKinematic = true;
            HoldingItem = obj;
            renderer.visible = false;
            WalkParticles.RenderLayer = -1;

            ParticleRain();
        }

        /// <summary>
        /// Drop the current held object
        /// </summary>
        private void DropObject()
        {
            HoldingItem.position = position + Velocity.Normalized * 10;

            HoldingItem.Velocity = Velocity + Velocity.Normalized * 100;
            HoldingItem.isDissabled = false;
            HoldingItem.isKinematic = false;

            if (HoldingItem.GetCollisions().Length > 0)
            {
                HoldingItem.position = position + new Vector2((width - HoldingItem.width) / 2, 0);
                Console.WriteLine("Colliding");
            }

            HoldingItem = null;
            renderer.visible = true;
            WalkParticles.RenderLayer = 0;
            ParticleRain();
        }
    }
}

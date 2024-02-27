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
        }

        void Update()
        {
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
                GrabObject((Portable)GetObjectInFront<Portable>());
            }
            renderer.alpha = 0.75f;
        }

        void ParticleRain()
        {
            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = renderer,
                ForceRandomness = 0.625f,
                burst = 15,
                LifeTime = 1,
                Friction = 0.05f,
                EmissionStep = 0,
                EmissionTime = 0,
                Scale = 0.5f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = 0.5f,
                G = 0.65f,
                B = 1f,
            };
            SceneManager.AddParticles(dat);
        }

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

        public GameObject GetObjectInFront<T>()
        {
            Sprite s = new Sprite("Square.png", false, true, 0, 0b10);
            s.position = GetCenter() + Velocity.Normalized * 32;
            s.LookAt(position);
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

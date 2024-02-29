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
        public static int StarterHealth = 40;
        public Portable HoldingItem = null;
        AnimationData Idle = new AnimationData(10, 9);
        AnimationData Walk = new AnimationData(0, 10, 0.6f);
        AnimationData BarrelIdle = new AnimationData(29, 6, 0.1f);
        AnimationData BarrelWalk = new AnimationData(19, 10, 1.5f);
        public Hider(Vector2 Position) : base(Position, 0, "Animations/HiderAnimations.png", 5, 7, new AnimationData(10, 9), new AnimationData(0, 10, 0.1f))
        {
            health = StarterHealth;
            PlayerManager.SetHider(this);
            playerColor = 0xA0A0FF;
        }

        void Update()
        {
            speed = Mathf.Lerp(speed, 2.5f, Time.deltaTime * 5) ;
            //move using wasd
            if (stunTimer < 0)
                AddForce(Input.WASDVector() * Time.deltaMillis * (speed+speedBoost));
            PlayerUpdates(0);

            renderer.alpha = HoldingItem == null ? 0.75f : 1;

            if (Input.GetKeyDown(Key.E))
                if (HoldingItem == null)
                    GrabObject((Portable)GetObjectInFrontOfType<Portable>());
                else
                    DropObject();
            if (Input.GetKeyDown(Key.Q))
                useItem();
            UIManager.UpdateHiderHealth(health);
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
            obj.renderer.visible = false;
            HoldingItem = obj;

            idleAnim = BarrelIdle;
            walkAnim = BarrelWalk;
            ResetAnimation();
            ParticleRain();
        }

        /// <summary>
        /// Drop the current held object
        /// </summary>
        private void DropObject(bool DestroyObj = false)
        {
            if(HoldingItem == null) 
                return;

            HoldingItem.Drop(position, Velocity);

            if(DestroyObj)
                HoldingItem.DestroySelf();
            HoldingItem.renderer.visible = true;

            HoldingItem = null;
            idleAnim = Idle;
            walkAnim = Walk;
            ResetAnimation();
            ParticleRain();
        }

        public void Attacked()
        {
            if (stunTimer < -3)
                Stun(HoldingItem == null ? 3 : 5);
            DropObject(true);
        }
    }
}

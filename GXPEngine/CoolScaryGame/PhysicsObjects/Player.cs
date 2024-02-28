using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GXPEngine.CoolScaryGame.Particles;
using CoolScaryGame.Particles;

namespace CoolScaryGame
{
    public class Player : RigidBody
    {
        internal float speed = 2.5f;
        internal int playerIndex;
        public Vector2 ActualVelocity;

        internal float animationSpeed = 15;

        internal float stunTimer;
        internal float health;

        float timer;
        int State = 0;

        internal AnimationData idleAnim;
        internal AnimationData walkAnim;
        internal ParticleData WalkParticles = new ParticleData();

        protected Vector2i skillInventory;

        protected uint playerColor = 0xFFFFFF;

        public Player(Vector2 Position, int playerIndex, string AnimationSprite, int rows, int columns, AnimationData idleAnim, AnimationData walkAnim) : base(48, 32, Position, true, 0b101, 0b101)
        {
            this.idleAnim = idleAnim;
            this.walkAnim = walkAnim;

            this.playerIndex = playerIndex;
            renderer = new FOVAnimationSprite(AnimationSprite, rows, columns, idleAnim.FrameCount, 200, false, false);
            SetupRenderer();
            SetupWalkParticles();
        }

        void SetupRenderer()
        {
            renderer.depthSort = true;

            proxy.AddChild(renderer);
            renderer.width = 108;
            renderer.height = 108;
            renderer.CenterOrigin();
            renderer.y = -32;
            renderer.x = width / 2;
            SetAnimation(idleAnim);
        }

        void SetupWalkParticles()
         {
            WalkParticles = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = renderer,
                SpawnPosition = new Vector2(0, 32),
                ForceRandomness = 0.75f,
                burst = 0,
                LifeTime = 2,
                Friction = 0.05f,
                EmissionStep = .05f,
                EmissionTime = 999999,
                Scale = 0.25f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                RenderLayer = playerIndex,
                R = 0.9f,
                G = 0.95f,
                B = 1f,
            };
            SceneManager.AddParticles(WalkParticles);
        }

        internal void PlayerUpdates(int playerIndex)
        {
            UIManager.MarkMinimap(position, playerIndex, playerColor);
            WalkParticles.EmissionStep = 0.5f / Mathf.Max(ActualVelocity.Magnitude, 1f);
            WalkParticles.ForceDirection = -ActualVelocity / 15;
            WalkParticles.Depth = TrueDepth() - 0.1f;
            stunTimer -= Time.deltaTime;
            Vector2 LastPos = TransformPoint(0, 0);

            //move the camera towards the player
            CamManager.LerpToPoint(playerIndex, renderer.TransformPoint(0, 0) + ActualVelocity * 15, Time.deltaTime * 6f);

            PhysicsUpdate();

            //switch animation frames if necessary
            AnimationUpdate();

            ActualVelocity = (TransformPoint(0, 0) - LastPos);
        }

        /// <summary>
        /// give the player a skill - discard and return false if inventory is full
        /// </summary>
        /// <param name="skill">the skill index</param>
        public bool TryGetSkill(int skill)
        {
            if(skillInventory.x == 0)
            {
                skillInventory.x = skill;
                UIManager.SetSkills(skillInventory.x, skillInventory.y, playerIndex);
                return true;
            }
            if(skillInventory.y == 0)
            {
                skillInventory.y = skill;
                UIManager.SetSkills(skillInventory.x, skillInventory.y, playerIndex);
                return true;
            }
            return false;
        }

        /// <summary>
        /// stun the player for a specified amount of time
        /// </summary>
        /// <param name="time">the duration</param>
        /// <param name="AddParticles">when true there will be particles emitted over the entire duration of the stun</param>
        public void Stun(float time, bool AddParticles = true)
        {
            stunTimer = time;
            if (!AddParticles)
                return;

            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = renderer,
                burst = 0,
                LifeTime = 2,
                Friction = 0.05f,
                EmissionStep = .05f,
                EmissionTime = time,
                Scale = 0.5f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = 0.75f,
                G = 0.85f,
                B = 1f,
            };
            SceneManager.AddParticles(dat);
        }

        /// <summary>
        /// update the player animations
        /// </summary>
        internal void AnimationUpdate()
        {
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;

            if(Velocity.Magnitude > 150)
                rend.Mirror(Velocity.x < 0, false);

            animationSpeed = ActualVelocity.Magnitude / 1.25f + 6;

            timer += Time.deltaTime;
            if(timer > 1/animationSpeed)
            {
                timer -= 1/animationSpeed;
                if (State != 0 && ActualVelocity.Magnitude < 1)
                {
                    State = 0;
                    SetAnimation(idleAnim);
                }
                if (State != 1 && ActualVelocity.Magnitude > 1)
                {
                    State = 1;
                    SetAnimation(walkAnim);
                }
                rend.NextFrame();
            }
        }

        /// <summary>
        /// set the current animation
        /// </summary>
        /// <param name="dat">the animationdata containing the start frame and frame count of the anination</param>
        private void SetAnimation(AnimationData dat)
        {
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;
            rend.SetCycle(dat.StartFrame, dat.FrameCount);
        }

        /// <summary>
        /// Returns the current health of the player
        /// </summary>
        /// <returns></returns>
        public float GetHealth()
        {
            return health;
        }

        /// <summary>
        /// deal damage to the player
        /// </summary>
        /// <param name="damage">the amount of damage</param>
        public void Damage(float damage)
        {
            health -= damage;
            if (health <= 0)
                Console.WriteLine("Game Over");
        }

        /// <summary>
        /// Get the closest object in front that is of type T
        /// </summary>
        /// <typeparam name="T">The specified type</typeparam>
        /// <returns></returns>
        internal GameObject GetObjectInFrontOfType<T>()
        {
            Sprite s = new Sprite("Square.png", false, true, 0, 0b10);
            s.position = GetCenter() + Velocity.Normalized * 32;
            s.LookAt(position);
            return GetClosestOfType<T>(s.GetCollisions());
        }

        /// <summary>
        /// Returns all objects in front of the player where the velocity decides the forward direction
        /// </summary>
        /// <returns></returns>
        internal GameObject[] GetObjectsInFront()
        {
            Sprite s = new Sprite("Square.png", false, true, 0, 0b10);
            s.position = GetCenter() + Velocity.Normalized * 32;
            s.LookAt(position);

            return s.GetCollisions();
        }

        /// <summary>
        /// Returns the closest object to the player that is also of the specific type T
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="objects">input array of objects</param>
        /// <returns></returns>
        internal GameObject GetClosestOfType<T>(GameObject[] objects)
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

        /// <summary>
        /// Spray a burst of particles on the player location
        /// </summary>
        /// <param name="amount">The amount of particles spawned</param>
        /// <param name="R">The red color value</param>
        /// <param name="G">The green color value</param>
        /// <param name="B">The blue color value</param>
        internal void ParticleRain(int amount = 15, float R = 0.5f, float G = 0.7f, float B = 1f)
        {
            ParticleData dat = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = renderer,
                ForceRandomness = 0.625f,
                burst = amount,
                LifeTime = 1,
                Friction = 0.05f,
                EmissionStep = 0,
                EmissionTime = 0,
                Scale = 0.5f,
                ScaleRandomness = 0.5f,
                ScaleOverLifetime = 0.95f,
                R = R,
                G = G,
                B = B,
            };
            SceneManager.AddParticles(dat);
        }
    }
}

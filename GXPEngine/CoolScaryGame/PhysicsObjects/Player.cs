using System;
using System.Linq;
using GXPEngine;
using GXPEngine.CoolScaryGame.Particles;
using CoolScaryGame.Particles;
using TiledMapParser;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class Player : RigidBody
    {
        internal float speed = 2.5f;
        internal int playerIndex;
        public Vector2 ActualVelocity;

        internal float animationSpeed = 15;
        internal float speedMultiplier = 1;

        internal float stunTimer;
        internal float health = 100;

        float timer;
        internal int State = 0;

        internal AnimationData idleAnim;
        internal AnimationData walkAnim;
        internal ParticleData WalkParticles = new ParticleData();

        protected ItemInventory Items;
        protected float[] activeItemTimers = new float[8] {-1, -1, -1, -1, -1, -1, -1, -1 };
        protected float speedBoost = 0;
        protected Sprite enemyArrow;

        protected uint playerColor = 0xFFFFFF;

        public Player(Vector2 Position, int playerIndex, string AnimationSprite, int rows, int columns, AnimationData idleAnim, AnimationData walkAnim) : base(48, 16, Position, true, 0b101, 0b101)
        {
            this.idleAnim = idleAnim;
            this.walkAnim = walkAnim;

            this.playerIndex = playerIndex;
            renderer = new FOVAnimationSprite(AnimationSprite, rows, columns, idleAnim.FrameCount, 300, false, false);
            SetupRenderer();
            SetupWalkParticles();
            SetupEnemyArrow();
            UnClip = false;
        }

        void SetupRenderer()
        {
            renderer.depthSort = true;

            proxy.AddChild(renderer);
            renderer.width = 108;
            renderer.height = 108;
            renderer.CenterOrigin();
            renderer.y = -32;
            renderer.x = width * 0.6f;
            SetAnimation(idleAnim, 0);
        }

        void SetupWalkParticles()
         {
            WalkParticles = new ParticleData()
            {
                sprite = "TriangleParticle.png",
                TrackObject = renderer,
                SpawnPosition = new Vector2(0, 32),
                directionRandomness = 0,
                ForceRandomness = 0f,
                burst = 0,
                LifeTime = 60,
                LifetimeRandomness = 0,
                Friction = 0.05f,
                EmissionStep = .05f,
                EmissionTime = 999999,
                Scale = 0.5f,
                ScaleRandomness = 0f,
                ScaleOverLifetime = 0.999f,
                RenderLayer = -1,
                R = this is Seeker ? 1 : 0.75f,
                G = 0.85f,
                B = this is Hider ? 1 : 0.75f,
            };
            SceneManager.AddParticles(WalkParticles);
        }

        internal void PlayerUpdates(int playerIndex)
        {
            UIManager.MarkMinimap(position, playerIndex, playerColor);
            WalkParticles.EmissionStep = 20f / Mathf.Max(ActualVelocity.Magnitude, 1f);
            WalkParticles.LookDirection = LookRotation(position + Velocity);
            //WalkParticles.ForceDirection = -ActualVelocity / 15;
            WalkParticles.Depth = TrueDepth() - 0.1f;
            stunTimer -= Time.deltaTime;
            Vector2 LastPos = TransformPoint(0, 0);

            //move the camera towards the player
            CamManager.LerpToPoint(playerIndex, renderer.TransformPoint(0, 0) + ActualVelocity * 15, Time.deltaTime * 6f);

            PhysicsUpdate();

            //switch animation frames if necessary
            AnimationUpdate();

            ActualVelocity = (TransformPoint(0, 0) - LastPos);

            UpdateItems();
        }

        /// <summary>
        /// update the player animations
        /// </summary>
        internal void AnimationUpdate()
        {
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;

            if(Velocity.Magnitude > 150)
                rend.Mirror(Velocity.x < 0, false);

            animationSpeed = ActualVelocity.Magnitude / 1.25f + 6 * speedMultiplier;

            timer += Time.deltaTime;
            if(timer > 1/animationSpeed)
            {
                timer -= 1/animationSpeed;
                if (State == 1 && ActualVelocity.Magnitude < 1)
                    SetAnimation(idleAnim, 0);
                if (State == 0 && ActualVelocity.Magnitude > 1)
                    SetAnimation(walkAnim, 1);
                rend.NextFrame();
            }
        }

        /// <summary>
        /// set the current animation
        /// </summary>
        /// <param name="dat">the animationdata containing the start frame and frame count of the anination</param>
        internal void SetAnimation(AnimationData dat, int state)
        {
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;
            rend.SetCycle(dat.StartFrame, dat.FrameCount);
            speedMultiplier = dat.Speed;
            State = state;
        }
        internal void ResetAnimation()
        {
            FOVAnimationSprite rend = (FOVAnimationSprite)renderer;
            rend.SetCycle(idleAnim.StartFrame, idleAnim.FrameCount);
            speedMultiplier = idleAnim.Speed;
            State = 0;
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
                SceneManager.EndGame(1 - playerIndex);
        }

        /// give the player a skill - discard and return false if inventory is full
        /// </summary>
        /// <param name="item">the item index</param>
        public bool TryGetItems(int item)
        {
            if(Items.first.ID == Item.empty)
            {
                Items.first.ID = item;
                UIManager.SetItems(Items.first.ID, Items.backup.ID, playerIndex);
                return true;
            }
            if(Items.backup.ID == Item.empty)
            {
                Items.backup.ID = item;
                UIManager.SetItems(Items.first.ID, Items.backup.ID, playerIndex);
                return true;
            }
            return false;
        }

        void SetupEnemyArrow()
        {
            enemyArrow = new Sprite("UI/bigArrow.png", true, false);
            enemyArrow.CenterOrigin();
            AddChild(enemyArrow);
            enemyArrow.alpha = 0;
            enemyArrow.visible = false;
            enemyArrow.RenderLayer = playerIndex;
        }

        protected void UpdateItems()
        {
            for(int i = 0; i<activeItemTimers.Count(); i++)
            {
                if (activeItemTimers[i] < 0)
                    continue;
                activeItemTimers[i] -= Time.deltaTime;

                if ( activeItemTimers[i] > 0 )
                    continue;

                ExpireItem(i);
                activeItemTimers[i] = -1000;
            }
            if(enemyArrow.visible)
            {
                Vector2 playerDifference = PlayerManager.GetPosition(playerIndex) - PlayerManager.GetPosition(1 - playerIndex);
                enemyArrow.rotation = 270 + 57.2957795f * Mathf.Atan2(playerDifference.y, playerDifference.x);
                //i am completely and fully mentally stable. oh hey look a page long line of code!
                enemyArrow.alpha = Mathf.Clamp01(Mathf.Min(activeItemTimers[Item.showEnemy], Item.ItemDurations[Item.showEnemy] - activeItemTimers[Item.showEnemy]));
            }
        }

        protected void ExpireItem(int item)
        {
            switch(item)
            {
                default:
                    break;
                case Item.speedUp:
                    speedBoost = 0;
                    break;
                case Item.showEnemy:
                    enemyArrow.visible = false;
                    break;
            }
        }

        /// <summary>
        /// use a item
        /// </summary>
        public void useItem()
        {
            if(activeItemTimers[Items.first.ID] < -.5f)
                switch(Items.first.ID)
                {
                    default:
                        break;
                    case Item.speedUp:
                        speedBoost = 1f;
                        break;
                    case Item.showEnemy:
                        enemyArrow.visible = true;
                        break;
                }
            activeItemTimers[Items.first.ID] = Item.ItemDurations[Items.first.ID];
            Items.first.ID = Items.backup.ID;
            Items.backup.ID = Item.empty;
            UIManager.SetItems(Items.first.ID, Items.backup.ID, playerIndex);
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
        /// Get the closest object in front that is of type T
        /// </summary>
        /// <typeparam name="T">The specified type</typeparam>
        /// <returns></returns>
        internal GameObject GetObjectInFrontOfType<T>()
        {
            Sprite s = new Sprite("Square.png", false, true, 0, 0b10);
            s.CenterOrigin();
            s.scale = 1.5f;
            s.position = GetCenter() + Velocity.Normalized * 32;
            s.LookAt(GetCenter());

            return GetClosestOfType<T>(s.GetCollisions());
        }

        /// <summary>
        /// Returns all objects in front of the player where the velocity decides the forward direction
        /// </summary>
        /// <returns></returns>
        internal GameObject[] GetObjectsInFront()
        {
            Sprite s = new Sprite("Square.png", false, true, 0, 0b11);
            s.CenterOrigin();
            s.scaleX = 1.5f;
            s.position = GetCenter() + Velocity.Normalized * 32;
            s.LookAt(GetCenter());

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
                Scale = 0.3f,
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

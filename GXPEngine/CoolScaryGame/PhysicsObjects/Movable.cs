using GXPEngine.Core;
using GXPEngine;
using System;

namespace CoolScaryGame
{
    public class Movable : InvisibleObject
    {
        internal Vector2 Velocity;
        internal float Friction = 0.1f;
        internal Sprite renderer;

        internal float _timer = 0;
        public Movable(int width, int height, Vector2 Position = new Vector2(), bool addCollider = false, uint collisionLayers = 0xFFFFFFFF, uint coupleWithLayers = 0xFFFFFFFF) : base(width, height, addCollider, collisionLayers, coupleWithLayers)
        {
            position = Position;
        }

        /// <summary>
        /// move the object without collision, though at a consistant rate
        /// </summary>
        public virtual void PhysicsUpdate()
        {
            depth = -y / 100000;
            _timer += Time.deltaTime;
            while (_timer > Time.TimeStep)
            {
                _timer -= Time.deltaTime;
                position += Velocity * Time.TimeStep;
                AddFriction(Friction);
            }
        }

        /// <summary>
        /// add force to the velocity
        /// </summary>
        /// <param name="Force"></param>
        public void AddForce(Vector2 Force)
        {
            Velocity += Force;
        }

        internal void AddFriction(float amount)
        {
            if(Velocity.Magnitude > 0.0001f)
                Velocity = Velocity.Lerp(new Vector2(), amount);
        }
        public override void Render(GLContext glContext, int RenderInt)
        {
            renderer.SetDepthByY(RenderInt);
            base.Render(glContext, RenderInt);
        }
    }
}

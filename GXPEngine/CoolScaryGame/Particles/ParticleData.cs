using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.CoolScaryGame.Particles
{
    public class ParticleData
    {
        ////////// Sprite Settings
        /// <summary>
        /// path to a sprite or sprite sheet
        /// </summary>
        public string sprite;
        /// <summary>
        /// if the path is a sprite sheet, change this to the width / height
        /// </summary>
        public int rows = 1, cols = 1;

        public float Depth = -90;

        public GameObject TrackObject;
        public Vector2 SpawnPosition;
        public float SpawnRadius = 0;

        ////////// Emission settings

        /// <summary>
        /// The amount of particles the object spawns at creation
        /// </summary>
        public int burst = 0;

        /// <summary>
        /// The time inbetween summoning of a particle. wont do anything if emissiontime is 0
        /// </summary>
        public float EmissionStep = 0;

        /// <summary>
        /// The duration that the emitter will be emitter, wont do anything if emission step is 0.
        /// </summary>
        public float EmissionTime = 0;

        ///////// Color Settings
        public float R = 1, G = 1, B = 1, A = 1;

        ///////// Lifetime Settings
        public float LifeTime = 1, LifetimeRandomness = 1;

        ///////// SCale Settings
        public float Scale = 1, ScaleRandomness = 0.1f, ScaleOverLifetime = 0.99f;

        ///////// Force(movement) Settings
        public Vector2 ForceDirection = new Vector2();
        public float ForceRandomness = 0.5f;
        public float Friction = 0.1f, FrictionRandomness = 0.25f;

        public float LookDirection;
        public float directionRandomness = 360;

        ///////// RenderLayer settings
        /// <summary>
        /// Renderlayer -1 will render on both monitors, Renderlayer 0 on camera 0 and Renderlayer 1 or camera 1
        /// </summary>
        public int RenderLayer = -1;
    }
}

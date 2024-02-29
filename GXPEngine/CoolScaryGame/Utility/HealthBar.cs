using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class HealthBar : GameObject
    {
        protected ColorSprite noHealth;
        protected ColorSprite yesHealth;
        protected Sprite renderer;
        public float Progress;
        protected float minHP;
        protected float maxHP;
        public HealthBar(string filename, Vector2 barPosition, Vector2i size, float minHealth = 0, float maxHealth = 1, float startHealth = 1, uint colorHealth = 0xFF6030, uint color = 0x9F0010) : base() 
        {
            scaleX = 1.5f;
            scaleY = 1.5f;

            noHealth = new ColorSprite(size.x, size.y, color);
            noHealth.position = barPosition;
            yesHealth = new ColorSprite(1, 1, colorHealth);
            AddChild(noHealth);
            noHealth.AddChild(yesHealth);

            renderer = new Sprite(filename, true, false);
            AddChild(renderer);

            Vector2 offset = new Vector2(renderer.width/2, renderer.height);
            renderer.position -= offset;
            noHealth.position -= offset;

            renderer.depthSort = true;

            minHP = minHealth;
            maxHP = maxHealth;
            Health = startHealth;

            depth = -98;
            yesHealth.depth = -.1f;
            noHealth.depth = 1;
            depthSort = true;
        }
        public float Health
        {
            get { return minHP + Progress * maxHP; }
            set { 
                Progress = (value - minHP) / (maxHP - minHP); 
                yesHealth.scaleX = Progress;
            }
        }
    }
}

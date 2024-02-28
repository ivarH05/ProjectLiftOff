using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class ItemBox : GameObject
    {
        protected Sprite renderer;
        protected AnimationSprite skill1;
        protected AnimationSprite skill2;
        public ItemBox(string filename, string skillsFilename)
        {
            renderer = new Sprite(filename,true, false);
            skill1 = new AnimationSprite(skillsFilename, 4, 2, 8, true, false);
            skill2 = new AnimationSprite(skillsFilename, 4, 2, 8, true, false);
            skill1.position = new Vector2(0, 0);
            skill2.position = new Vector2(64, 0);
            AddChild(skill1);
            AddChild(skill2);
            AddChild(renderer);
            renderer.depthSort = true;
            skill1.depthSort = true;
            skill2.depthSort = true;
            renderer.depth = 1;
        }

        public void SetItems(int firstskill, int secondskill)
        {
            skill1.SetFrame(firstskill);
            skill2.SetFrame(secondskill);
        }
    }
}

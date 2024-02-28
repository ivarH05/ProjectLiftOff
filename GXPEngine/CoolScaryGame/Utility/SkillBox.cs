using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class SkillBox : GameObject
    {
        protected ColorSprite background;
        protected Sprite renderer;
        protected AnimationSprite skill1;
        protected AnimationSprite skill2;
        public SkillBox(string filename, string skillsFilename)
        {
            background = new ColorSprite(160, 60, 0xAAAAAA);
            renderer = new Sprite(filename,true, false);
            skill1 = new AnimationSprite(skillsFilename, 4, 2, 8, true, false);
            skill2 = new AnimationSprite(skillsFilename, 4, 2, 8, true, false);
            background.position = new Vector2(18, 29);
            skill1.scale = .6f;
            skill1.position = new Vector2(22, 29);
            skill2.scale = .6f;
            skill2.position = new Vector2(100, 29);
            AddChild(background);
            AddChild(skill1);
            AddChild(skill2);
            AddChild(renderer);
            background.depthSort = true;
            renderer.depthSort = true;
            skill1.depthSort = true;
            skill2.depthSort = true;
            background.depth = 1;
            renderer.depth = -1;
        }

        public void SetSkills(int firstskill, int secondskill)
        {
            skill1.SetFrame(firstskill);
            skill2.SetFrame(secondskill);
        }
    }
}

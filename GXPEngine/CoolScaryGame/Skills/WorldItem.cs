using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

namespace CoolScaryGame
{
    public class WorldItem : AnimationSprite
    {
        public Item inquestion;
        public WorldItem(int itemID) : base("UI/items.png", 4, 2, 8, true, true, 0, 0b100)
        {
            inquestion.ID = itemID;
            SetFrame(itemID);
            RenderLayer = -1;
            depthSort = true;
        }
        public override void Render(GLContext glContext, int RenderInt)
        {
            SetDepthByY(RenderInt);
            base.Render(glContext, RenderInt);
        }
        public void OnCollision(GameObject Other)
        {
            if (!(Other is Player))
                return;
            if (!((Player)Other).TryGetItems(inquestion.ID))
                return;
            LateDestroy();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

/// <summary>
/// mainly for use in Tiled - adding collisions to floors, walls, etc for a very low price
/// you can have hundreds of tiles collisionless if you simply replace them with one of these puppies

/// checkers is the texture being used here because i cant be FRICKED to implement collisions myself
/// not touching that stuff with a ten foot pole. looking into GXP3D traumatized me

/// in fact, Tiled objects NEED to inherit from Sprite
/// and this class makes that a whole lot more convenient when the object in question will not have any rendering going on
///
/// optionally visible can be turned back on for debug purposes
/// </summary>
namespace CoolScaryGame
{
    public class InvisibleObject : Sprite
    {
        protected bool debugVisible;
        static bool debugVisibleAll = false;
        public Pivot proxy;
        public InvisibleObject(TiledObject obj, bool addCollider = false, uint collisionLayers = 0, uint coupleWithLayers = 0, bool overrideVisible = false) : 
        base("UI/debug_invisibleObject.png", true, addCollider, collisionLayers, coupleWithLayers)
        {
            obj.Initialize();
            width = (int)obj.GetFloatProperty("width", 0);
            height = (int)obj.GetFloatProperty("height", 0);

            depth = 0;
            debugVisible = overrideVisible;
            if (overrideVisible)
                depthSort = true;
        }
        public InvisibleObject(int width, int height, bool addCollider = false, uint collisionLayers = 0, uint coupleWithLayers = 0, bool overrideVisible = false) : 
        base("UI/debug_invisibleObject.png", true, addCollider, collisionLayers, coupleWithLayers)
        {
            this.width = width;
            this.height = height;

            if(overrideVisible)
                depth = -99;
            debugVisible = overrideVisible;

            AddProxy();
        }
        public void AddProxy()
        {
            proxy = new Pivot();
            AddChild(proxy);
            proxy.scaleX = 1 / scaleX;
            proxy.scaleY = 1 / scaleY;
        }
        public override void Render(GLContext glContext, int RenderInt)
        {
            if (visible && (RenderLayer == -1 || RenderLayer == RenderInt))
            {
                glContext.PushMatrix(matrix);

                if(debugVisible || debugVisibleAll) 
                    RenderSelf(glContext);
                //there shouldnt be any children of the invisibleobject. leaving this in tho 
                foreach (GameObject child in GetChildren(false))
                {
                    child.Render(glContext, RenderInt);
                }

                glContext.PopMatrix();
            }
        }
        virtual public void Setup()
        { }
    }
}
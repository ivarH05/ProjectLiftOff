using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace CoolScaryGame
{
    public class DoorSprite : WallSprite
    {
        public uint thisDoor;
        public DoorSprite(TiledObject obj) : base(obj)
        {

            thisDoor = (uint)obj.GetIntProperty("ThisDoor", 0);
            //Console.WriteLine(thisDoor);
        }
        public void Setup(float roomRot, int wallSprite, uint doors)
        {
            if ((thisDoor & doors) != 0)
            {
                Destroy();
                thisDoor = 0;
            }
            else Setup(roomRot, wallSprite);
        }
    }
}

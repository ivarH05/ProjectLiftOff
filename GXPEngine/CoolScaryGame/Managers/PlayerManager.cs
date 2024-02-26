using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolScaryGame
{
    public static class PlayerManager
    {
        private static Hider _hider;
        public static Hider hider
        {
            get
            {
                if (_hider == null)
                    throw new Exception("Hider not assigned");
                return _hider;
            }
            set
            {
                _hider = value;
            }
        }
        private static Seeker _seeker;
        public static Seeker seeker
        {
            get
            {
                if (_seeker == null)
                    throw new Exception("Hider not assigned");
                return _seeker;
            }
            set
            {
                _seeker = value;
            }
        }

        public static void SetHider(Hider h) { hider = h; }
        public static void SetSeeker(Seeker s) { seeker = s; }

        static Player player(int index)
        {
            Player output = index == 0 ? (Player)hider : (Player)seeker;
            if (output == null) //remove this part if you also want to call withouth the player.
                throw new Exception("Player has not been found");
            return output;
        }

        public static Vector2 GetPosition(int index)
        {
            return player(index).position;
        }
    }
}

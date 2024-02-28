using GXPEngine;
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
        private static int TalismanCount;
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

        /// <summary>
        /// Get either players based on the index
        /// </summary>
        /// <param name="index">Player index</param>
        static Player player(int index)
        {
            Player output = index == 0 ? (Player)hider : (Player)seeker;
            if (output == null) //remove this part if you also want to call withouth the player.
                throw new Exception("Player has not been found");
            return output;
        }

        /// <summary>
        /// Returns the position of the specified player
        /// </summary>
        /// <param name="index">Player index</param>
        /// <returns></returns>
        public static Vector2 GetPosition(int index)
        {
            return player(index).TransformPoint(0,0);
        }

        /// <summary>
        /// Returns the health of the specified player
        /// </summary>
        /// <param name="index">Player index</param>
        /// <returns></returns>
        public static float GetHealth(int index)
        {
            return player(index).GetHealth();
        }

        /// <summary>
        /// Slow down the player 
        /// </summary>
        /// <param name="index">Player index</param>
        /// <param name="amount">The amount the player should be slowed down</param>
        /// <param name="clamp">when true the speed cannot go below 0</param>
        public static void SlowPlayer(int index, float amount, bool clamp = true)
        {
            float s = player(index).speed;
            if(clamp)
                player(index).speed = Mathf.Clamp(s - amount, 0, 100000);
            else
                player(index).speed = s;
        }

        /// <summary>
        /// Deal damage
        /// </summary>
        /// <param name="index">Player index</param>
        /// <param name="amount">the amount of damage</param>
        public static void DamagePlayer(int index, float amount)
        {
            player(index).Damage(amount);
        }

        public static void AddTalisman()
        {
            TalismanCount++;
            if (TalismanCount >= 4)
                SceneManager.EndGame(0);
        }

    }
}

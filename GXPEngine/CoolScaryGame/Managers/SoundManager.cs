using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

namespace CoolScaryGame
{
    public static class SoundManager
    {
        private static List<SoundChannel> AllSounds = new List<SoundChannel>();

        public static void EndSounds()
        {
            foreach (var sound in AllSounds)
            {
                if (sound == null)
                    continue;
                sound.Stop();
            }
            AllSounds.Clear();
        }

        public static SoundChannel PlaySound(Sound s)
        {
            SoundChannel c = s.Play();
            AllSounds.Add(c);
            return c;
        }

        public static void StopSound(SoundChannel c)
        {
            if(c == null) return;
            c.Stop();
            AllSounds.Remove(c);
        }
    }
}

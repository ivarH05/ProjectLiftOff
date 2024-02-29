using GXPEngine.Core;
using GXPEngine;
using System;
using GXPEngine.CoolScaryGame.Particles;
using CoolScaryGame.Particles;
using GXPEngine.CoolScaryGame.Level;

namespace CoolScaryGame
{
    public class Scene : GameObject
    {
        private Camera viewLeft;
        private Camera viewRight;

        float Timer = 301;

        SoundChannel Heartbeat;
        public Scene()
        {
            SoundManager.EndSounds();
            SceneManager.SetScene(this);

            viewLeft = new Camera(0, 0, 960, 1080, 0, false);
            viewRight = new Camera(960, 0, 960, 1080, 1, false);
            viewLeft.scale = 0.65f;
            viewRight.scale = 0.65f;
            AddChild(viewLeft);
            AddChild(viewRight);

            LevelManager.BuildLevelByIndex(this, 2);
            SoundManager.PlaySound(new Sound("Sound/MainMusic.mp3", true, true));
            Heartbeat = SoundManager.PlaySound(new Sound("Sound/Heartbeat.mp3", true, true));
            Heartbeat.Volume = 0;
        }

        void Update()
        {
            Timer -= Time.deltaTime;
            if (Timer < 0)
                SceneManager.EndGame(1);
            UIManager.UpdateTimer(PlayerManager.GetTalismanCount(), Timer);
            float PlayerDistance = Vector2.Distance(PlayerManager.GetPosition(0), PlayerManager.GetPosition(1));
            Heartbeat.Volume = Mathf.Clamp01(500 / (PlayerDistance + 200) - 0.5f);
            //Heartbeat.Volume = Mathf.Clamp(PlayerDistance / 1000f, 0, 1);
            //Console.WriteLine( "Distance: " + PlayerDistance + " --- volume: " + Heartbeat.Volume);
        }

        public Camera[] GetCameras()
        {
            return new Camera[] { viewLeft, viewRight };
        }

        public void AddUI()
        {
            UIManager.AddHiderHealthbar();
            UIManager.SetupTimer();
            UIManager.AddMinimaps();
            UIManager.AddSkillBoxes();
        }
    }
}

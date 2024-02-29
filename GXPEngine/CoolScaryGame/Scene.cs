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

        public Scene()
        {
            SceneManager.SetScene(this);

            viewLeft = new Camera(0, 0, 960, 1080, 0, false);
            viewRight = new Camera(960, 0, 960, 1080, 1, false);
            viewLeft.scale = 0.65f;
            viewRight.scale = 0.65f;
            AddChild(viewLeft);
            AddChild(viewRight);

            LevelManager.BuildLevelByIndex(this, 2);
        }

        void Update()
        {
            Timer -= Time.deltaTime;
            if (Timer < 0)
                SceneManager.EndGame(1);
            UIManager.UpdateTimer(PlayerManager.GetTalismanCount(), Timer);
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

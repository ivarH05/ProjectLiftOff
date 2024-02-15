using GXPEngine.Core;
using GXPEngine;
using System;

namespace CoolScaryGame
{
    public static class SceneManager
    {
        private static MainGame mainGame;
        private static Scene scene;
        public static void SetMainGame(MainGame m)
        {
            mainGame = m;
        }

        public static void LoadScene(bool destroyScene = true)
        {
            if (destroyScene && scene != null)
                scene.LateDestroy();

            scene = new Scene();
            mainGame.AddChild(scene);
            CamManager.SetCameras(scene.GetCameras());
            scene.AddUI();
        }
    }

    public static class CamManager
    {
        private static Camera[] cameras = new Camera[2];

        public static void SetCameras(Camera[] _cameras)
        {
            cameras = _cameras;
        }

        public static void AddUI(GameObject _UI, int index)
        {
            if (index < 0 || index >= cameras.Length)
                return;
            cameras[index].AddChild(_UI);
        }

        public static Vector2 GetPosition(int index)
        {
            if (index < 0 || index >= cameras.Length)
                return new Vector2(0, 0);
            return cameras[index].position;
        }

        public static void SetPosition(int index, Vector2 pos)
        {
            if (index < 0 || index >= cameras.Length)
                return;
            cameras[index].position = pos;
        }
        public static void SetPosition(int index, float x, float y)
        {
            if (index < 0 || index >= cameras.Length)
                return;
            cameras[index].SetXY(x, y);
        }

        public static void LerpToPoint(int index, Vector2 pos, float time)
        {
            if (index < 0 || index >= cameras.Length)
                return;
            cameras[index].position = cameras[index].position.Lerp(pos, time);
        }
    }
}

using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
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
        }
    }

    public static class CamManager
    {
        private static Camera[] cameras = new Camera[2];

        public static void SetCameras(Camera[] _cameras)
        {
            cameras = _cameras;
        }

        public static Vector2 GetPosition(int index)
        {
            return cameras[index].position;
        }

        public static void SetPosition(int index, Vector2 pos)
        {
            cameras[index].position = pos;
        }
        public static void SetPosition(int index, float x, float y)
        {
            cameras[index].SetXY(x, y);
        }

        public static void LerpToPoint(int index, Vector2 pos, float time)
        {
            cameras[index].position = cameras[index].position.Lerp(pos, time);
        }
    }
}

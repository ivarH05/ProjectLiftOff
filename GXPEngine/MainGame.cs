using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using CoolScaryGame;
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.Core;
using GXPEngine.OpenGL;

public class MainGame : Game 
{
	public MainGame() : base(1920, 1080, false, true, 1920 / 2, 1080 / 2, true, enableDepthBuffer:true, gameName: "Haraishi to Yokai")     // Create a window that's 800x600 and NOT fullscreen
	{
		SceneManager.SetMainGame(this);
		SceneManager.MainMenu();
		targetFps = 50;
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		if(Input.GetKeyDown(Key.L))
        {
            Console.WriteLine("fps: " + (int)(1 / Time.deltaTime));
            Console.WriteLine(GetDiagnostics());
        }
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MainGame().Start();                   // Create a "MyGame" and start i
	}
}
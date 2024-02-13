using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.Core;

public class MainGame : Game 
{
	public MainGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		SceneManager.SetMainGame(this);
		SceneManager.LoadScene();
		RenderMain = false;
		targetFps = 50;
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MainGame().Start();                   // Create a "MyGame" and start it
	}
}
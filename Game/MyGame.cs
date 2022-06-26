using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
	int _startSceneNumber = 2;
	int currentScene = 0;
	int totalLevels = 5;

	Canvas _lineContainer = null;

	//public Cannon cannon;

	CollectableSystem CS;
	HUD _hud;

	public List<Ball> _movers;
	public List<LineSegment> _lines;
	//	List<Enemy2Way> _enemy2Ways = new List<Enemy2Way>();
	public Collectable[] _colect = new Collectable[3];

	LevelOne levelOne;

	public EndCircle _endCircle;

	public Dictionary<string, Sound> soundLibrary = new Dictionary<string, Sound>()
	{
		{"Shoot",new Sound("testShoot.wav",false)},
		{"LevelOneBG", new Sound("LevelOneBG.mp3")}
	};

	public int GetNumberOfLines()
	{
		return _lines.Count;
	}

	public LineSegment GetLine(int index)
	{
		if (index >= 0 && index < _lines.Count)
		{
			return _lines[index];
		}
		return null;
	}

	public int GetNumberOfMovers()
	{
		return _movers.Count;
	}

	public Ball GetMover(int index)
	{
		if (index >= 0 && index < _movers.Count)
		{
			return _movers[index];
		}
		return null;
	}

	public void DrawLine(Vec2 start, Vec2 end)
	{
		_lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
	}

	public int Width {
		//get { return width; }
			get { return width / 125 * 100; }
	}

	public int Height
	{
		//get { return height; }
		get { return height / 125 * 100; }
	}
	public MyGame() : base(1920, 1080, false, false)
	{
	//	UnitTesting ut = new UnitTesting();

		_lineContainer = new Canvas(width, height);
		AddChild(_lineContainer);

		targetFps = 60;

		 _movers = new List<Ball>();
		_lines = new List<LineSegment>();

        //Cannon
     //  cannon = new Cannon(height / 2 - 275, width / 2 + 50 - 150, 10);
	//	AddChild(cannon);


		//LoadScene(_startSceneNumber);
		SetUpScenes();
		PrintInfo();


		CS = new CollectableSystem();
		AddChild(CS);
		CS.LoadStars();
		CS.PrintStars();

		_hud = new HUD(new Vec2(200, 200));
		AddChild(_hud);		
	}

	public void SetUpScenes()
    {
		LevelOne levelOne = new LevelOne(soundLibrary);
		SceneManager.instance.AddScene(levelOne);
		LevelTwo levelTwo = new LevelTwo();
		SceneManager.instance.AddScene(levelTwo);
		SceneManager.instance.LoadScene(0);
    }

    void AddLine(Vec2 start, Vec2 end)
	{
		LineSegment line = new LineSegment(start, end, 0xff00ff00, 4);
		AddChild(line);
		_lines.Add(line);
	}

	public int GetLevelCount
	{
		get { return totalLevels; }
	}

	public int GetCurrentScene 
	{ 
	get { return currentScene; }
	}

	public CollectableSystem GetCollectableSystem { 
		get { return CS; }
	}
	public HUD GetHUD { 
		get { return _hud; }
	}

	/****************************************************************************************/

	void PrintInfo()
	{
		Console.WriteLine("Hold tab to slow down the frame rate.");
		Console.WriteLine("Press R to reset scene");
		Console.WriteLine("Press B to toggle high/low bounciness.");
	}

	void HandleInput()
	{


		targetFps = Input.GetKey(Key.TAB) ? 5 : 60;

		if (Input.GetKeyDown(Key.B))
		{
			Ball.bounciness = 1.5f - Ball.bounciness;
		}

        if (Input.GetMouseButtonDown(1)) Console.WriteLine(new Vec2(Input.mouseX, Input.mouseY));
        if (Input.GetKeyDown(Key.SPACE)) Console.WriteLine("###################################################################");

        //Load/reset scenes:
        if (Input.GetKeyDown(Key.R))
        {
			SceneManager.instance.LoadScene(currentScene);
			CS.RestartStarsLevel();
			//cannon.shots = 2;
        }

        if (Input.GetKeyDown(Key.F1))
        {
			SceneManager.instance.LoadScene(0);
			CS.RestartStarsLevel();
		}

        if (Input.GetKeyDown(Key.F2))
        {
			SceneManager.instance.LoadScene(1);		
			CS.RestartStarsLevel();
		}
	}

	public void NextLevel() { 
		
	}


	public void addMover(Ball ball)
	{
		_movers.Add(ball);
	}

	public void addLine(LineSegment line) { 
		_lines.Add(line);
	}

	public void RemoveLine(LineSegment line) {
		_lines.Remove(line);
		line.LateDestroy();
	}


	public void RemoveBalls(Ball remove)
	{

		_movers.Remove(remove);
		remove.LateDestroy();
	}


	void Update()
	{
		HandleInput();
	}

	static void Main()
	{
		new MyGame().Start();
	}
}
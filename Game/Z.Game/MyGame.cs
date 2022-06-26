using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
	int _startSceneNumber = 2;
	int currentScene = 0;
	int currentLevel = 0;
	int totalLevels = 5;
	public bool buttonPressed = false;
	public int nextCount = 0;

	//If the game is paused or not
	public bool frozen = false;
	public bool end = false;
	Canvas _lineContainer = null;

	//public Cannon cannon;

	CollectableSystem CS;
	public Fade fade;
	public ParticleSystem PS;
	public SoundManager SM;
	HUD _hud;

	public List<Ball> _movers;
	public List<LineSegment> _lines;
	//	List<Enemy2Way> _enemy2Ways = new List<Enemy2Way>();
	public Collectable[] _colect = new Collectable[3];

	LevelOne levelOne;

	public EndCircle _endCircle;


	public AnimationSprite idleAni;
	public AnimationSprite shootAni;

	public AnimationSprite door;
	public AnimationSprite door2;

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
			get { return width; }
	}

	public int Height
	{
		//get { return height; }
		get { return height; }
	}


	public MyGame() : base(1920, 1080, false, false)
	{
		//	UnitTesting ut = new UnitTesting();

		idleAni = new AnimationSprite("animation_idle.png", 4, 2);
		shootAni = new AnimationSprite("animation_button.png", 4, 1);
		door = new AnimationSprite("GateB.png", 6, 2);
		door2 = new AnimationSprite("GateB.png", 6, 2);
		_lineContainer = new Canvas(width, height);
		AddChild(_lineContainer);

		targetFps = 60;

		 _movers = new List<Ball>();
		_lines = new List<LineSegment>();


		//_hud = new HUD(new Vec2(-100, -100);
		//AddChild(_hud);
		//Cannon
		//  cannon = new Cannon(height / 2 - 275, width / 2 + 50 - 150, 10);
		//	AddChild(cannon);
		fade = new Fade();
		
		CS = new CollectableSystem();
		AddChild(CS);
		CS.LoadStars();
		CS.PrintStars();


		PS = new ParticleSystem();
		AddChild(PS);

		SM = new SoundManager();
		AddChild(SM);

		//LoadScene(_startSceneNumber);
		SetUpScenes();
		PrintInfo();

		//LevelSelect ls = new LevelSelect(LevelSelect.Worlds.Nephelle);
		//AddChild(ls);

		//	_hud = new HUD(new Vec2(200, 200));
		//	AddChild(_hud);

		AddChild(fade);

	}


	public enum Scenes { Start, WorldSelect, LevelSelectNephelle, LevelSelectBethel, Nephelle_1, Nephelle_2, Nephelle_3, Bethel_1, Bethel_2, Bethel_3 }
	public void SetUpScenes()
    {


		LevelSelect neph = new LevelSelect(LevelSelect.Worlds.Nephelle);

		StartScreen startScreen = new StartScreen();
		SceneManager.instance.AddScene(startScreen);

		SceneManager.instance.AddScene(neph);
		LevelOne levelOne = new LevelOne(soundLibrary);
		SceneManager.instance.AddScene(levelOne);
		LevelTwo levelTwo = new LevelTwo(soundLibrary);
		SceneManager.instance.AddScene(levelTwo);
		LeveThree levelThree = new LeveThree(soundLibrary);
		SceneManager.instance.AddScene(levelThree);
		EndCreditScene endScene = new EndCreditScene();
		SceneManager.instance.AddScene(endScene);

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
		set { currentScene = value; }
	}
	public int GetCurrentLevel
	{ 
		get { return currentLevel; }
		set { currentLevel = value; }
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

	}

	void HandleInput()
	{
		targetFps = 60;
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
		if (idleAni == null) {
			idleAni = new AnimationSprite("animation_idle.png", 4, 2);
			shootAni = new AnimationSprite("animation_button.png", 4, 1);

		}
	}

	static void Main()
	{

		new MyGame().Start();

	}
}
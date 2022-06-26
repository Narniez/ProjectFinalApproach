using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using System.Drawing;
public class LevelOne : Scene
{
   
    public Cannon cannon;
    private Pivot objectOwner;
    private SFXHandler sfxHandler;

    public LevelOne(Dictionary<string, Sound> soundLibrary) : base()
    {
        sfxHandler = new SFXHandler(soundLibrary, .2f);
    }

    protected override void Start()
    {
        sfxHandler.PlaySound("LevelOneBG");
        isActive = true;
        objectOwner = new Pivot();
        MyGame myGame = (MyGame)game;
        //Cannon
<<<<<<< HEAD
        cannon = new Cannon(game.height/2 - 460, game.width / 2 - 650, 10);
=======
        cannon = new Cannon(game.height / 2 - 275, game.height / 2 + 50 - 150, 10);
>>>>>>> main
        AddChild(cannon);

        myGame._lines.Add(new LineSegment(new Vec2(50, 50), new Vec2(750, 50)));
        myGame._lines.Add(new LineSegment(new Vec2(750, 50), new Vec2(750, 550)));
        myGame._lines.Add(new LineSegment(new Vec2(750, 550), new Vec2(50, 550)));
        myGame._lines.Add(new LineSegment(new Vec2(50, 550), new Vec2(50, 50)));

        //_movers.Add(new Ball(10, new Vec2(100, 250), new Vec2(0, 10)));



        //Bottom left part
        myGame._lines.Add(new LineSegment(new Vec2(200, 375), new Vec2(50, 375)));
        myGame._lines.Add(new LineSegment(new Vec2(225, 550), new Vec2(225, 400)));
        myGame._movers.Add(new Ball(25, new Vec2(200, 400), moving: false));

        myGame._lines.Add(new LineSegment(new Vec2(750, 540), new Vec2(225, 480)));



        //Side lines
        myGame._lines.Add(new LineSegment(new Vec2(0, 200), new Vec2(200, 0)));
        myGame._lines.Add(new LineSegment(new Vec2(600, 0), new Vec2(800, 200)));
        myGame._lines.Add(new LineSegment(new Vec2(200, 0), new Vec2(0, 200)));
        myGame._lines.Add(new LineSegment(new Vec2(800, 400), new Vec2(600, 600)));



        //Middle part

        myGame._lines.Add(new LineSegment(new Vec2(590, 420), new Vec2(585, 200)));
        myGame._lines.Add(new LineSegment(new Vec2(515, 200), new Vec2(530, 420)));

        myGame._movers.Add(new Ball(35, new Vec2(550, 200), moving: false));
        myGame._movers.Add(new Ball(30, new Vec2(560, 420), moving: false));

        //Collectables

        myGame._colect[0] = new Collectable(30, new Vec2(460, 123));
        myGame._colect[1] = new Collectable(30, new Vec2(422, 452));
        myGame._colect[2] = new Collectable(30, new Vec2(660, 293));


        EndCircle EC = new EndCircle(new Vec2(300, 300));
        AddChild(EC);
        //Enemy
        myGame._movers.Add(new Enemy2Way(10, new Vec2(627, 220), new Vec2(705, 220)));

        foreach (Ball _ball in myGame._movers)
        {
            AddChild(_ball);
        }

        foreach (LineSegment _line in myGame._lines)
        {
            AddChild(_line);
        }
        foreach (Collectable _col in myGame._colect)
        {
            AddChild(_col);
        }
        AddChild(objectOwner);
    }

    protected override void Update()
    {
        if (!base.isActive) return;

        if (Input.GetKeyDown(Key.F3))
        {
            SceneManager.instance.TryLoadNextScene();
        }
    }

    public override void UnLoadScene()
    {
        base.UnLoadScene();
    }
}


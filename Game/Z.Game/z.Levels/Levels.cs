using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Levels : Scene
{
    public Cannon cannon;
    public EndUI endUI;
    private Pivot objectOwner;
    private SFXHandler sfxHandler;
    protected MyGame myGame;

    public int ballsActive = 0;
    Pause_FailUI pauseMenu;

    bool paused = false;


    bool shoot = false;
    protected AnimationSprite Cillius;
    protected AnimationSprite CilliusPress;

    public Levels(Dictionary<string, Sound> soundLibrary) : base() {
        sfxHandler = new SFXHandler(soundLibrary, .2f);
    }

    protected override void Start()
    {
        isActive = true;
        objectOwner = new Pivot();
        myGame = (MyGame)game;
        ballsActive = 0;
        MakeLevel();
        pauseMenu = new Pause_FailUI(true);

        Cillius = myGame.idleAni;
     //   Cillius.SetOrigin(Cillius.width / 2, Cillius.height / 2);
        Cillius.SetScaleXY(0.2f, 0.2f);
        Cillius.SetXY(15, 771);
        AddChild(Cillius);

        CilliusPress = myGame.shootAni;
    //    CilliusPress.SetOrigin(CilliusPress.width / 2, CilliusPress.height / 2);
        CilliusPress.SetScaleXY(0.2f, 0.2f);
        CilliusPress.SetXY(15, 771);
        AddChild(CilliusPress);


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
        if (Input.GetKeyDown(Key.SPACE)) shoot = true;

      


        if (Input.GetKeyDown(Key.TAB)) Pause();
        if (((MyGame)game).frozen && !((MyGame)game).end) return;


        if (CilliusPress == null || Cillius == null) return;
        if (shoot == true && CilliusPress.currentFrame < 3)
        {

            CilliusPress.alpha = 1;
            CilliusPress.Animate(0.1f);

        }
        else if (shoot) { 
            shoot = false;
            CilliusPress.SetFrame(0);
        }
        else{
            CilliusPress.alpha = 0;
            Cillius.Animate(0.1f);
        }
     
    }

    void Pause() {

        if (((MyGame)game).end) return;
        if (!((MyGame)game).frozen || paused)paused = !paused;


        if (paused)
        {
            AddChild(pauseMenu);
            pauseMenu.paused = true;
            ((MyGame)game).frozen = true;
        }
        else
        {
            ((MyGame)game).frozen = false;
            pauseMenu.paused = false;
        }
    }

    public override void UnLoadScene()
    {
        paused = false;
        RemoveChild(Cillius);
        RemoveChild(CilliusPress);
        base.UnLoadScene();
    }



    protected virtual void MakeLevel() { }

}




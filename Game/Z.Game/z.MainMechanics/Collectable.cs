using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Collectable : CircleMechanic {
    Sound collectStar = new Sound("starsCollectTestSound.wav", false, false);

    Sprite collect;
    float rotatingSpeed;

    bool fade = false;
    public Collectable(Vec2 pPos, int pRadius) : base(pPos, pRadius) {

        collect = new Sprite("collect.png");
        collect.SetOrigin(collect.width/2, collect.height/2);
     //   collect.SetXY(-2, -2);
        collect.width = width;
        collect.height = height;

        collect.rotation = Utils.Random(0, 91);
        rotatingSpeed = Utils.Random(-2.0f, 3.0f);
        rotatingSpeed *= 3;
        AddChild(collect);

        alpha = 0;
    }

    protected override void Update() {

        if (((MyGame)game).frozen && !((MyGame)game).end) return;
        collect.rotation += 3;
        base.Update();
    
    }
    protected override void InCircle()
    {
        if (!fade)
        {
            collectStar.Play();
            CollectableSystem CS = myGame.GetCollectableSystem;
            CS.AddStarsLevel();
        fade = true;
        }
        Fade();
    }

    protected override void OutCircle()
    {
        Fade();
    }

    protected void Fade()
    {

        if (fade)
        {
            collect.alpha -= 0.05f;
        }
        if (collect.alpha <= 0) this.LateDestroy();
    }

}

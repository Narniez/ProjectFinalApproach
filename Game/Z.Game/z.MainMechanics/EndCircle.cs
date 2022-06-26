using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class EndCircle : CircleMechanic {

    //endcircle should be this radius
    int radius = 40;

    EndUI endUI;

    Sprite endCharacter;
    public EndCircle(Vec2 pPos, int pRad, int level) : base(pPos, pRad)
    {
        endCharacter = new Sprite("char" + level + ".png");
        endCharacter.SetScaleXY(0.05f);
        endCharacter.SetOrigin(width/2, height/2);
        endCharacter.SetXY(-55, -35);
        AddChild(endCharacter);
        alpha = 0f;
    }

    protected override void InCircle(Ball pMove, Vec2 pRel)
    {

        Vec2 moreVel = pRel.Normalized();

        pMove.accel = moreVel * 0.03f * (pRel.Length());
        pMove.velocity *= 0.9f;

        ((MyGame)game).end = true;
        if (endUI == null)
        {
           
            CollectableSystem CS = myGame.GetCollectableSystem;
            

            endUI = new EndUI(CS.currentStarsLevel);
            parent.AddChild(endUI);
            ((MyGame)game).frozen = true;
        }
    }

}

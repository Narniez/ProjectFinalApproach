using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Fan : BoxMechanic
{

    Vec2 force;

    public AnimationSprite sprite;

    public Fan(Vec2 Pos, int pWidth, int pHeight, float pRot = -90.0f, float pPower = 1f) : base(Pos, pWidth, pHeight)
    {
        force = Vec2.GetUnitVectorDeg(pRot) * pPower;


        alpha = 0f;
    }

    protected void Update() {
        if (sprite != null && (!((MyGame)game).frozen || ((MyGame)game).end)) sprite.Animate(Time.deltaTime / 100f);
        base.Update();
}
    protected override void InBox(Package pPack) 
    { 
        pPack.accel = force;
      
    }

    protected override void OutBox(Package pPack)
    {
        pPack.accel = new Vec2(0, 0);
    }

}
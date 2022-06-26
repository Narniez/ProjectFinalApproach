using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class CircleMechanic : Sprite
{
    protected MyGame myGame;
    public CircleMechanic(Vec2 pPos, int pRadius) : base("circle.png")
    {
        SetOrigin(width / 2, height / 2);
        width = pRadius * 2;
        height = pRadius * 2;

        x = pPos.x;
        y = pPos.y;

        
    }

    protected virtual void Update()
    {
        if (myGame == null) myGame = ((MyGame)game);

        //if (myGame.frozen) return;
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);
            if (mover.moving)
            {
                Vec2 relPos = new Vec2(x, y) - mover.position;

                if (relPos.Length() < (width / 2) + mover.radius)
                {
   
                    InCircle(mover, relPos);
                    InCircle();
                }
                else OutCircle();
            }
        }
    }


    protected virtual void InCircle(Ball pMove, Vec2 pRel) { }
    protected virtual void InCircle() { }

    protected virtual void OutCircle() { }

}
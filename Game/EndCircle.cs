using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class EndCircle : Sprite {

    int radius = 40;

    public EndCircle(Vec2 pPos) : base("circle.png")
    {
        SetOrigin(width / 2, height / 2);
        width = radius * 2;
        height = radius * 2;

        x = pPos.x;
        y = pPos.y;
        Console.WriteLine(x);
    }

    public void Update() {

        MyGame myGame = ((MyGame)game);

        for (int i = 0; i < myGame.GetNumberOfMovers(); i++) { 
        
            Ball mover = myGame.GetMover(i);
            if (mover.moving)
            {
                Vec2 relPos = new Vec2(x, y) - mover.position;

                if (relPos.Length() <= radius + mover.radius)
                {
                    Vec2 moreVel = relPos.Normalized();

                    mover.accel = moreVel * 0.01f * (relPos.Length());
                    mover.velocity *= 0.9f;
                    //myGame.NextLevel();
                }
            
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Collectable : Sprite {

    public Collectable(int pRadius, Vec2 pPos) : base("circle.png") { 
    
        SetOrigin(width/2, height/2);
        width = pRadius * 2;
        height = pRadius * 2;

        x = pPos.x;
        y = pPos.y;
    }

    void Update() {
        MyGame myGame = ((MyGame)game);

        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);
            if (mover.moving) {

                Vec2 relPos = new Vec2(x, y) - mover.position;

                if (relPos.Length() < (width / 2) + mover.radius)
                {
                    CollectableSystem CS = myGame.GetCollectableSystem;
                    CS.AddStarsLevel();
                    this.LateDestroy();
                }
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class BoxMechanic : Sprite
{
    protected MyGame myGame;
    protected Sprite overLaySprite;
    public BoxMechanic(Vec2 Pos, int pWidth, int pHeight) : base("colors.png")
    {

        SetOrigin(width / 2, height / 2);
        width = pWidth;
        height = pHeight;

        x = Pos.x;
        y = Pos.y;
    }

    protected void Update()
    {
        if (myGame == null) myGame = ((MyGame)game);

        if (myGame.frozen && !myGame.end) return;
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);

            if (mover is Package)
            {
                Package package = (Package)mover;

                //Checks if package inside area of effect
                bool packageInBox = (mover.x <= x + width / 2 && mover.x >= x - width / 2 && mover.y <= y + height / 2 && mover.y >= y - height / 2);

                if (packageInBox) InBox(package);
                else OutBox(package);

            }

        }
    }

    protected virtual void InBox(Package pPack) { }

    protected virtual void OutBox(Package pPack) { }
}
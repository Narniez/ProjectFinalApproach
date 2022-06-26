using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Button : BoxMechanic
{


    LineSegment Wall;
    public AnimationSprite lineSprite;
    public bool pressed = false;
 
    Sprite top;
    Sprite bot;
    public Button(Vec2 pPos, LineSegment pWall, int pWidth, int pHeight, int pRot = 180) : base(pPos, pWidth, pHeight)
    {
        Wall = pWall;

        
        this.alpha = 0;
        top = new Sprite("button_top.png");
        top.SetOrigin(top.width / 2, top.height / 2);
        top.width = pWidth / 2;
        top.height = pHeight * 2;
        top.rotation = pRot;
        top.SetXY(0, 45);
        AddChild(top);

        bot = new Sprite("button_bot.png");
        bot.SetOrigin(bot.width/2, bot.height/2);
        bot.width = pWidth / 2;
        bot.height = pHeight;
        bot.rotation = pRot;
        bot.SetXY(0, 12);
        AddChild(bot);


        if (top.rotation == 90) {
            bot.SetXY(30, 0);
            top.SetXY(30, 0);

        }

    }


    protected override void InBox(Package pPack)
    {
        if (!pressed)
        {
            pressed = true;
            for (int j = 0; j < myGame.GetNumberOfLines(); j++)
            {

                LineSegment line = myGame.GetLine(j);
                if (line == Wall)
                {
                    ((MyGame)game).buttonPressed = true;
                    myGame.RemoveLine(Wall);
                }
            }
        }
        
        if (pressed && top.y >= -3 && top.rotation == 180){
            top.y--;
            



        } else if (pressed && top.x >= 0)
        {
            top.x--;
        }


        if (pressed && lineSprite.currentFrame < 11)
        {
            lineSprite.Animate(0.1f);
        }

    }

    protected override void OutBox(Package pPack)
    {
        if (pressed && top.y >= -3 && top.rotation == 180)
        {
            top.y--;



        }
        else if (pressed && top.x >= 0)
        {
            top.x--;
        }

        if (pressed && lineSprite.currentFrame < 11)
        {
            lineSprite.Animate(0.1f);
        }
    }
}
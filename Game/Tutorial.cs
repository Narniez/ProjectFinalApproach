using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Tutorial : Pivot {


    Sprite leftRight;
    Sprite Space;

    bool pressed1 = false;
    bool pressed2 = false;
    public Tutorial()
    {
        leftRight = new Sprite("tut1.png");
        Space = new Sprite("tut2.png");

        leftRight.SetScaleXY(0.5f);
        Space.SetScaleXY(0.5f);

        AddChild(Space);
        AddChild(leftRight);


    }

    void Update() {

        if (Input.GetKeyDown(Key.LEFT) || Input.GetKeyDown(Key.RIGHT)) pressed1 = true;
        if (Input.GetKeyDown(Key.SPACE)) pressed2 = true;

        if (pressed1 && leftRight.alpha > 0) leftRight.alpha -= 0.05f;
        if (pressed2 && Space.alpha > 0) Space.alpha -= 0.05f;
    }



}
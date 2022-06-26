using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class EndCreditScene : Scene
{
    public EndCreditScene()
    {
       
    }
    protected override void Start()
    {
        ((MyGame)game).GetCurrentScene = 5;
        Sprite background = new Sprite("finalCreditScreen.png");
        background.width = 1920;
        background.height = 1080;

        AddChild(background);
    }

     protected override void Update()
    {
        if (((MyGame)game).GetCurrentScene == 5)
        {
            if ((Input.GetMouseButtonDown(0)) || Input.AnyKeyDown()) ((MyGame)game).fade.SwitchScenes(0);
        }
    }
}



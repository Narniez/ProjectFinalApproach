using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class ButtonMenu : Sprite
{
    int scene;


    float px = 1920 / 2.0f;
    float py = 1080 / 2.0f;
    public bool isActive = false;
    public ButtonMenu(Vec2 pPos, int pWidth, int pHeight, int pScene, float pY = 1080 / 2.0f) : base("colors.png") {
        SetOrigin(width/2, height/2);
        x = pPos.x;
        y = pPos.y;
        width = pWidth;
        height = pHeight;
        scene = pScene;
        alpha = 0;

        py = pY;
    }

    public void Update() {

        Hover();
    
    }

    void Hover() {

        if (!isActive) return;
        if (Input.mouseX < (x + px) + width/2 && Input.mouseX > (x + px) - width/2 && Input.mouseY < (y + py) + height/2 && Input.mouseY > (y + py) - height/2)
        {
            //      _hover.alpha = 1;

            ((MyGame)game).SM.hoverBool = true;
            if (Input.GetMouseButtonDown(0))
            {
                ((MyGame)game).SM.ClickSFX();
                ((MyGame)game).GetCollectableSystem.RestartStarsLevel();
                Console.WriteLine(scene);
                ((MyGame)game).fade.SwitchScenes(scene);

            }
        }
        //else _hover.alpha = 0;
    }

} 
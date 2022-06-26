using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


public class Pause_FailUI : Pivot
{
    EasyDraw grayBG;
    Pivot endBox = new Pivot();

    ButtonMenu Menu;
    ButtonMenu Restart;

    public bool paused;


     bool fail;

    public Pause_FailUI(bool isPaused) {

        paused = false;
        grayBG = new EasyDraw(1920, 1080);
        grayBG.x = -1920 / 2.0f;
        grayBG.y = -1080 / 3.0f - 50;
        grayBG.Fill(0);
        grayBG.Rect(1920 / 2.0f, 1080 / 2.0f, 1920, 1080);
        grayBG.alpha = 0;
        AddChild(grayBG);

        x = 1920 / 2.0f;
        y = 1080 / 3.0f + 50;

        Sprite background;
        if (isPaused)
        {
             background = new Sprite("pause_screen.png");
            fail = false;
        }
        else {
             background = new Sprite("fail_screen.png");
            fail = true;
        }
        background.SetOrigin(background.width / 2, background.height / 2);
        endBox.AddChild(background);

        Menu = new ButtonMenu(new Vec2(-120, 207), 205, 180, 1, y);
        Restart = new ButtonMenu(new Vec2(112, 207), 205, 180, (((MyGame)game).GetCurrentScene), y);

        endBox.AddChild(Menu);
        endBox.AddChild(Restart);


        endBox.SetScaleXY(0, 0);
        Console.WriteLine("paused");
        AddChild(endBox);
    }
    float scale = 0;
    public void Update()
    {
        if (paused) Start();
        else Exit();

    }

    public void Start() {

        
        endBox.visible = true;

        if (((MyGame)game).end) return;
        if (fail) {
            ((MyGame)game).SM.LoseSFX();
            fail = false;
        }
        if (scale >= 0.9f) {
            Menu.isActive = true;
            Restart.isActive = true;

        }
        if (scale >= 0.999f)
        {
            scale = 1f;
            endBox.SetScaleXY(scale, scale);
            if (!Menu.isActive)
            {

                Menu.isActive = true;
                Restart.isActive = true;

            }
        }
        else if (scale < 1)
        {

            scale = scale * 0.95f + 1.01f * 0.05f;
            endBox.SetScaleXY(scale, scale);
        }
        if (grayBG.alpha <= 0.5f) grayBG.alpha += 0.01f;

    }

    public void Exit()
    {

        if (scale < 0f)
        {
            endBox.visible = false;
            this.Remove();


        }
        else if (scale > 0)
        {

            scale = scale * 0.95f  - 0.2f * 0.05f;
            endBox.SetScaleXY(scale, scale);

            if (Menu.isActive)
            {
                Menu.isActive = false;
                Restart.isActive = false;
            }
        }
        if (grayBG.alpha >= 0.05f) grayBG.alpha -= 0.015f;

    }

} 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class EndUI : Pivot
{

    EasyDraw grayBG;
    Pivot endBox = new Pivot();
    EasyDraw Box;

    Sprite star1;
    Sprite star2;
    Sprite star3;
    Sprite star4;
    Sprite star5;
    Sprite star6;

    int stars;

    ButtonMenu Menu;
    ButtonMenu Restart;
    ButtonMenu Next;

   


    //Button should latedestroy this class
    public EndUI(int Stars) {

        ((MyGame)game).SM.WinSFX();
        stars = Stars;
        CollectableSystem CS = ((MyGame)game).GetCollectableSystem;
        CS.CheckStars(((MyGame)game).GetCurrentScene - 2, stars);
     
        grayBG = new EasyDraw(1920, 1080);
        grayBG.x = -1920 / 2.0f;
        grayBG.y = -1080 / 2.0f;
        grayBG.Fill(0);
        grayBG.Rect(1920 / 2.0f, 1080 / 2.0f, 1920, 1080);
        grayBG.alpha = 0;
        AddChild(grayBG);


        x = 1920 / 2.0f;
        y = 1080 / 2.0f;

        
        AddChild(endBox);



        Sprite background = new Sprite("end_screen.png");
        background.SetOrigin(background.width/2, background.height/2);
        endBox.AddChild(background);

       

        endBox.SetScaleXY(0, 0);


         star4 = new Sprite("collect.png");
        star4.SetOrigin(star4.width / 2, star4.height / 2);
        star4.SetScaleXY(2, 2);
        star4.SetXY(-208, -126);
        endBox.AddChild(star4);

         star5 = new Sprite("collect.png");
        star5.SetOrigin(star5.width / 2, star5.height / 2);
        star5.SetScaleXY(2f, 2f);
        star5.SetXY(-4, -196);
        star5.rotation = 45;
        
        endBox.AddChild(star5);

         star6 = new Sprite("collect.png");
        star6.SetOrigin(star6.width / 2, star6.height / 2);
        star6.SetScaleXY(2, 2);
        star6.SetXY(208, -126);
        endBox.AddChild(star6);

        star4.scale = 0;
        star5.scale = 0;
        star6.scale = 0;
        star4.rotation = -45;
        star5.rotation = -45;
        star6.rotation = -45;


        Menu = new ButtonMenu(new Vec2(-235, 205), 205, 180, 1);
        Restart = new ButtonMenu(new Vec2(-5, 205), 205, 180, (((MyGame)game).GetCurrentScene));
        Next = new ButtonMenu(new Vec2(230, 205), 205, 180, ((MyGame)game).GetCurrentScene + 1);
        endBox.AddChild(Menu);
        endBox.AddChild(Restart);
        endBox.AddChild(Next);

    }

    float scale = 0;
   
    public void Update() {

       



        if (scale >= 0.9f)
        {
            Menu.isActive = true;
            Restart.isActive = true;
            Next.isActive = true;
        }
        if (scale >= 0.999f) {
            scale = 1f;
            endBox.SetScaleXY(scale, scale);
            Stars();
            if (!Menu.isActive) { 
            
                Menu.isActive = true;
                Restart.isActive = true;
                Next.isActive = true;
            
            }
        }
        else if (scale < 1)
        {

            scale = scale * 0.95f + 1.01f * 0.05f;
            endBox.SetScaleXY(scale, scale);

        }
        if (grayBG.alpha <= 0.5f) grayBG.alpha += 0.01f;

    }

    bool done1 = false;
    bool done2 = false;
    bool done3 = false;
    void Stars()
    {

        if (star4.scale < 1.99f && stars >= 1)
        {
            star4.scale = star4.scale * 0.85f + 2f * 0.15f;
            star4.rotation = star4.rotation * 0.85f + 0;
            star4.SetScaleXY(star4.scale, star4.scale);



        }
        else if (!done1 && stars >=1)
        {
            done1 = true;

            ((MyGame)game).PS.Boom(new Vec2(x + star4.x, star4.y + y), size: 3);
        }
        else if (star5.scale < 1.99f && stars >= 2)
        {

            star5.scale = star5.scale * 0.85f + 2f * 0.15f;
            star5.rotation = star5.rotation * 0.85f + 0;
            star5.SetScaleXY(star5.scale, star5.scale);


        }
        else if (!done2 && stars >= 2)
        {
            done2 = true;
            ((MyGame)game).PS.Boom(new Vec2(x + star5.x, star5.y + y), size: 3);
        }
        else if (star6.scale < 1.99f && stars >= 3)
        {
            star6.scale = star6.scale * 0.85f + 2f * 0.15f;
            star6.rotation = star6.rotation * 0.85f + 0;
            star6.SetScaleXY(star6.scale, star6.scale);

        }
        else if (!done3 && stars >= 3) {
            done3 = true;
            ((MyGame)game).PS.Boom(new Vec2(x + star6.x, star6.y + y), size: 3);

        }

    }





}

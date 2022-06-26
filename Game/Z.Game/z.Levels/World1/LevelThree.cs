using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using System.Drawing;
public class LeveThree : Levels
{
    public static SoundChannel levelOneBg = new SoundChannel(1);
  //  Sound levelOne = new Sound("cloudsBG.mp3", true, false);


    Sprite movePlatform;
    Sprite cloudSprite;
    AnimationSprite doorSprite;

    Button but;
    
    public LeveThree(Dictionary<string, Sound> soundLibrary) : base(soundLibrary)
    {
    }

    protected override void MakeLevel()
    {
    //    levelOneBg = levelOne.Play();

        ((MyGame)game).GetCurrentLevel = 2;
        ((MyGame)game).GetCurrentScene = 4;

        Sprite BackGround = new Sprite("nepheleBG.png");
        BackGround.width = 1920;
        BackGround.height = 1080;
        AddChild(BackGround);
        Sprite bg = new Sprite("1-3.png");

        bg.width = 1920;
        bg.height = 1080;

        //Sprite BackGround = new Sprite("BGN.jpg");
        //BackGround.width = 1920;
        //BackGround.height = 1080;
        //AddChild(BackGround);
        LineSegment ln = new LineSegment(new Vec2(1461, 691), new Vec2(1867, 691));
        myGame.addLine(ln);
        but = new Button(new Vec2(116, 151), ln, 120, 160, 90);
        AddChild(but);


        doorSprite = ((MyGame)game).door2;
      //  AddChild(doorSprite);
      //  doorSprite.SetOrigin(doorSprite.width / 2, doorSprite.height / 2);
        doorSprite.width = 257;
        doorSprite.height = 268;
        doorSprite.SetXY(824 + doorSprite.height/2, 188 - doorSprite.width/2);
        doorSprite.rotation = 90;
        doorSprite.currentFrame = 0;
        but.lineSprite = doorSprite;
        but.AddChild(doorSprite);

        AddChild(bg);

        Sprite wires = new Sprite("wires.png");

        wires.SetOrigin(wires.width / 2, wires.height / 2);
        wires.SetXY(260, 581);
        wires.rotation = 39;
        wires.width = wires.width / 4;
        wires.height = wires.height / 4;
        AddChild(wires);

        cannon = new Cannon(wires.x - 40, wires.y - 31, 15, -75, 38);
        AddChild(cannon);


        //Walls
        Clouds cloud1 = new Clouds(new Vec2(0, 1080), new Vec2(500, 1080), new Vec2(0, 753), new Vec2(500, 753), pWall: true);
        AddChild(cloud1);
        

        Clouds cloud3 = new Clouds(new Vec2(500, 1080), new Vec2(1920, 1080), new Vec2(500, 1040), new Vec2(1920, 1040), pWall: true);
        AddChild(cloud3);

        Clouds cloud4 = new Clouds(new Vec2(1870, 1080), new Vec2(1920, 1080), new Vec2(1870, 0), new Vec2(1920, 0), pWall: true);
        AddChild(cloud4);
        Clouds cloud5 = new Clouds(new Vec2(0, 40), new Vec2(1920, 40), new Vec2(0, 0), new Vec2(1920, 0), pWall: true);
        AddChild(cloud5);

        Clouds cloud6 = new Clouds(new Vec2(0, 765), new Vec2(60, 770), new Vec2(0, 30), new Vec2(50, 30), pWall: true);
        AddChild(cloud6);

        Clouds cloud7 = new Clouds(new Vec2(742, 1075), new Vec2(805, 1057), new Vec2(437, 814), new Vec2(496, 760), pWall: true);
        AddChild(cloud7);

        Clouds cloud8 = new Clouds(new Vec2(402, 290), new Vec2(851, 290), new Vec2(402, 222), new Vec2(851, 222), pWall: true);
        AddChild(cloud8);

        Clouds cloud9 = new Clouds(new Vec2(1330, 810), new Vec2(1460, 810), new Vec2(1330, 265), new Vec2(1460, 265), pWall: true);
        AddChild(cloud9);

        Clouds cloud10 = new Clouds(new Vec2(1555, 355), new Vec2(1870, 355), new Vec2(1555, 240), new Vec2(1870, 240), pWall: true);
        AddChild(cloud10);

        Clouds cloud11 = new Clouds(new Vec2(1327, 264), new Vec2(1460, 264), new Vec2(1327, 36), new Vec2(1460, 36));
        AddChild(cloud11);

        Clouds cloud12 = new Clouds(new Vec2(1457, 535), new Vec2(1560, 535), new Vec2(1457, 40), new Vec2(1560, 40));
        AddChild(cloud12);

        cloudSprite = new Sprite("break1.png");
        //  cloudSprite.SetOrigin(cloudSprite.width / 2, cloudSprite.height / 2);

        cloudSprite.width = 900;
        cloudSprite.height = 300;
        cloudSprite.SetXY(941 - cloud9.x, 7 - cloud9.y);
  //      cloudSprite.SetScaleXY(0.5f);
        cloud11.AddChild(cloudSprite);
        cloud11.sprite = cloudSprite;

        Sprite cloudSprite2 = new Sprite("break1.png");
        cloudSprite2.width = 718;
        cloudSprite2.height = 537;
        cloudSprite2.SetXY(1240 - cloud9.x, 16 - cloud9.y);
        cloudSprite2.SetScaleXY(0.7f);
        cloud12.AddChild(cloudSprite2);
        cloud12.sprite = cloudSprite2;


        MovablePlatform mp = new MovablePlatform(new Vec2(1065, 552), new Vec2(1152, 552), new Vec2(1065, 255), new Vec2(1152, 255), new Vec2(0, 200), true);
        AddChild(mp);


        movePlatform = new Sprite("cloudHor.png");
        movePlatform.width = 454;
        movePlatform.height = 327;
        
        movePlatform.SetXY(880 - cloud9.x, 235 - cloud9.y);
      
        mp.AddChild(movePlatform);








        //Collectables
        myGame._colect[0] = new Collectable(new Vec2(1238, 169), 33);
        myGame._colect[1] = new Collectable(new Vec2(1663, 493), 33);
        myGame._colect[2] = new Collectable(new Vec2(146, 369), 33);


        //EndCircle
        EndCircle endcircle = new EndCircle(new Vec2(1768, 140), 100, 2);
        AddChild(endcircle);

        Cillius = new AnimationSprite("animation_idle.png", 4, 2);
        Cillius.SetXY(184, 933);
        Cillius.SetOrigin(Cillius.width / 2, Cillius.height / 2);
        Cillius.SetScaleXY(0.2f, 0.2f);
        AddChild(Cillius);
        CilliusPress = new AnimationSprite("animation_button.png", 4, 1);
        CilliusPress.SetXY(184, 933);
        CilliusPress.SetOrigin(CilliusPress.width / 2, CilliusPress.height / 2);
        CilliusPress.SetScaleXY(0.2f, 0.2f);
        AddChild(CilliusPress);
    }

    public override void UnLoadScene()
    {
        but.RemoveChild(doorSprite);
        base.UnLoadScene();
    }





}


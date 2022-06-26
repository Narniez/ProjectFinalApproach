using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using System.Drawing;
public class LevelOne : Levels
{
    public static SoundChannel levelOneBg = new SoundChannel(1);
  //  Sound levelOne = new Sound("cloudsBG.mp3", true, false);

    Sprite wires;

    AnimationSprite wind;
    public LevelOne(Dictionary<string, Sound> soundLibrary) : base(soundLibrary)
    {
    }

    protected override void MakeLevel()
    {
       // levelOneBg = levelOne.Play();

        ((MyGame)game).GetCurrentLevel = 0;
        ((MyGame)game).GetCurrentScene = 2;

        Sprite BackGround = new Sprite("nepheleBG.png");
        BackGround.width = 1920;
        BackGround.height = 1080;
        AddChild(BackGround);
       

        wires = new Sprite("wires.png");

        wires.SetOrigin(wires.width / 2, wires.height / 2);
        wires.SetXY(200, 581);
        

        wires.rotation = 39;
        wires.width = wires.width / 4;
        wires.height = wires.height / 4;
        AddChild(wires);

        cannon = new Cannon(wires.x - 40, wires.y - 31, 15,  -75, 38);
        AddChild(cannon);

        wind = new AnimationSprite("wind.png", 3, 2);
        AddChild(wind);
        wind.SetOrigin(wind.width / 2, wind.height / 2);
        wind.width = 200;
        wind.height = 75;

        //Fan
        Fan fan = new Fan(new Vec2(1322, 826), 120, 500, pPower: 1f);
        AddChild(fan);
        fan.sprite = wind;
        fan.AddChild(wind);


        Sprite bg = new Sprite("1-1.png");

        bg.width = 1920;
        bg.height = 1080;

        AddChild(bg);

        //myGame._endCircle = new EndCircle(new Vec2(400, 300));
        //AddChild(myGame._endCircle);

        //LineSegment ln = new LineSegment(new Vec2(333, 381), new Vec2(290, 441));
        //myGame._lines.Add(ln);


        //Button but = new Button(new Vec2(200, 100), ln);
        //AddChild(but);

        Clouds cloud = new Clouds(new Vec2(518, 1080), new Vec2(725, 1080), new Vec2(400, 958), new Vec2(400, 751), pWall: true);
        AddChild(cloud);

        Clouds cloud1 = new Clouds(new Vec2(882, 693), new Vec2(952, 620), new Vec2(522, 335), new Vec2(610, 257), pWall: true);
        AddChild(cloud1);

        Clouds cloud2 = new Clouds(new Vec2(1440, 276), new Vec2(1600, 253), new Vec2(1155, 0), new Vec2(1346, 0), pWall: true);
        AddChild(cloud2);

        Clouds cloud3 = new Clouds(new Vec2(1551, 660), new Vec2(1920, 663), new Vec2(1552, 595), new Vec2(1920, 594), pWall: true);
        AddChild(cloud3);


        //walls


        Clouds cloud4 = new Clouds(new Vec2(0, 765), new Vec2(30, 770), new Vec2(0, 30), new Vec2(30, 30), pWall: true);
        AddChild(cloud4);

        Clouds cloud6 = new Clouds(new Vec2(500, 1080), new Vec2(1920, 1080), new Vec2(500, 1040), new Vec2(1920, 1040), pWall: true);
        AddChild(cloud6);

        Clouds cloud7 = new Clouds(new Vec2(1890, 1080), new Vec2(1920, 1080), new Vec2(1890, 0), new Vec2(1920, 0), pWall: true);
        AddChild(cloud7);
        Clouds cloud8 = new Clouds(new Vec2(0, 40), new Vec2(1920, 40), new Vec2(0, 0), new Vec2(1920, 0), pWall: true);
        AddChild(cloud8);

        //Outside
        myGame._lines.Add(new LineSegment(new Vec2(0, 1080), new Vec2(0, 0)));
        myGame._lines.Add(new LineSegment(new Vec2(1920, 1080), new Vec2(0, 1080)));
        myGame._lines.Add(new LineSegment(new Vec2(1920, 0), new Vec2(1920, 1080)));
        myGame._lines.Add(new LineSegment(new Vec2(0, 0), new Vec2(1920, 0)));

        //Box bottom left (Make new Class later to clean up
        Clouds cloud9 = new Clouds( new Vec2(0, 1080), new Vec2(400, 1080), new Vec2(0, 742), new Vec2(400, 742), pWall: true);
        AddChild(cloud9);






        //Collectables
        myGame._colect[0] = new Collectable(new Vec2(898, 125), 33);
        myGame._colect[1] = new Collectable(new Vec2(1572, 436), 33);
        myGame._colect[2] = new Collectable(new Vec2(1678, 887), 33);


        EndCircle endCircle = new EndCircle(new Vec2(1712, 231), 100, 1);
        AddChild(endCircle);

        Tutorial tut = new Tutorial();
        tut.SetXY(-200, 0);
        AddChild(tut);



    }

   



}


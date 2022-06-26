using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class MovablePlatform : Pivot
{

    LineSegment[] lines = new LineSegment[4];
    Ball[] caps = new Ball[4];

    bool poof = false;
    MyGame myGame;

    bool wall = false;

    //pRot is in Deg
    Vec2 toVec;
    Vec2 disTo;

    float totalDis;

    Vec2 dis;
    //Make A wall class to clean up

    
    public MovablePlatform(Vec2 pBottomLeft, Vec2 pBottomRight, Vec2 pTopLeft, Vec2 pTopRight, Vec2 disFromMid, bool pWall = false) : base()
    {

        wall = pWall;
        lines[0] = new LineSegment(pTopLeft, pBottomLeft);
        lines[1] = new LineSegment(pBottomLeft, pBottomRight);
        lines[2] = new LineSegment(pBottomRight, pTopRight);
        lines[3] = new LineSegment(pTopRight, pTopLeft);

        caps[0] = new Ball(0, pTopLeft, moving: false);
        caps[1] = new Ball(0, pBottomLeft, moving: false);
        caps[2] = new Ball(0, pBottomRight, moving: false);
        caps[3] = new Ball(0, pTopRight, moving: false);


        disTo = disFromMid;
        toVec = disFromMid.Normalized();

        totalDis = (disTo * 2).Length();
        myGame = ((MyGame)game);
        for (int i = 0; i < lines.Length; i++) myGame.addLine(lines[i]);
        for (int i = 0; i < caps.Length; i++) myGame.addMover(caps[i]);
    }

    public void Update()
    {

        if (((MyGame)game).frozen) return;

        dis += toVec * 1.5f;

        x += toVec.x * 1.5f;
        y += toVec.y * 1.5f;

        Vec2 toStart = dis - disTo;
        Vec2 toEnd = dis + disTo;
        if (toStart.Length() >= totalDis || toEnd.Length() >= totalDis) toVec *= -1;
        for (int i = 0; i < lines.Length; i++)
        {

            //     lines[i].x += toVec.x;
            //     lines[i].y += toVec.y;
            lines[i].start += toVec * 1.5f;
            lines[i].end += toVec * 1.5f;
        }
        for (int i = 0; i < caps.Length; i++)
        {
            caps[i].position += toVec * 1.5f;

        }

        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);
            if (mover.moving)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    if (mover.moving && mover.latestCollision == lines[j])
                    {

                        if (wall)
                        {
                            ((MyGame)game).SM.CloudSFX();
                            mover.latestCollision = null;
                        }
                        else
                        {
                            ((MyGame)game).SM.CloudSFX();
                            poof = true;
                            mover.latestCollision = null;
                        }


                    }
                }
                for (int j = 0; j < caps.Length; j++)
                {


                    if (mover.latestCollision == caps[j])
                        if (wall)
                        {
                            ((MyGame)game).SM.CloudSFX();
                            mover.latestCollision = null;
                        }
                        else
                        {
                            ((MyGame)game).SM.CloudSFX();
                            poof = true;
                            mover.latestCollision = null;
                        }

                }


            }
        }


    }
}

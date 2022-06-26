using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Clouds : Pivot {

    LineSegment[] lines = new LineSegment[4];
    Ball[] caps = new Ball[4];
    bool poof = false;
    MyGame myGame;

    bool wall = false;

    public Sprite sprite;

    //pRot is in Deg

    //Make A wall class to clean up
    public Clouds(Vec2 pBottomLeft, Vec2 pBottomRight, Vec2 pTopLeft, Vec2 pTopRight, float pRot = 0, bool pWall = false) : base()
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




        myGame = ((MyGame)game);
        for (int i = 0; i < lines.Length; i++) myGame.addLine(lines[i]);
        for (int i = 0; i < caps.Length; i++) myGame.addMover(caps[i]);
    }

    public void Update() {

        if (poof) DeleteCloud(); 
        
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++) {

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
                        else {
                            ((MyGame)game).SM.CloudSFX();
                            poof = true;
                            mover.latestCollision = null;
                        }
                    
                }
                

            }

        }
    }


    public void DeleteCloud() {

        for (int i = 0; i < lines.Length; i++)
        {
            myGame.RemoveLine(lines[i]);
        }
        
        for (int i = 0; i < caps.Length; i++) myGame.RemoveBalls(caps[i]);

        if (sprite != null && sprite.alpha > 0) sprite.alpha -= 0.1f;


    }
}
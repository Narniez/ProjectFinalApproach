using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Package : Ball
{





    public bool acid = false;
    //seconds
    public float timer = 5.0f;

    public bool sped = false;
    public bool slow = false;
    public bool normal = true;
    public float spedTimer = 1.0f;
    public Package(Vec2 pPos, Vec2 pVel) : base(10, pPos, pVel)
    {


    }

    void Update()
    {
            Console.WriteLine( velocity.Length());
        if (velocity.Length() > 20) {
            velocity.Normalize();
            velocity *= 20f;
                }
        base.Update();
        accel = new Vec2(0, 0);
        if (timer <= 0 || latestCollision is Enemy2Way)
        {

            ((MyGame)game).PS.Boom(position, 1, 0.1f, 2.0f);
            MyGame myGame = ((MyGame)game);
            for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
            {
                if (myGame.GetMover(i) == this)
                {
                    myGame.RemoveBalls(this);
                    this.LateDestroy();
                }
            }




        }
        else if (latestCollision != null) {

            latestCollision = null;
           
            
            Vec2 returnPos = Vec2.GetUnitVectorDeg(velocity.GetAngleDeg() - 180.0f);
            Vec2 Pos = position + returnPos * 10;


            ((MyGame)game).PS.Cone(Pos, latestNormal.GetAngleDeg(), 60.0f, pRunTime: 0.5f, velocity: 2f);
            latestNormal = new Vec2(0, 0);
        }

        alpha = 0.2f * timer;
        if (acid) timer -= Time.deltaTime / 500.0f;
        else timer -= Time.deltaTime / 1000.0f;
    }

}
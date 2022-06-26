using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class ParticleSystem : Pivot {

    int particles = 50;

    

    public ParticleSystem()
    {
        x = 0;
        y = 0;
    }

    public void Boom(Vec2 pPos, int size = 1, float pRunTime = 1f, float veloctiy = 5.0f, string overlay = null) {

        //for (int i = 0; i < particles; i++) {
        //    Particle ball = new Particle(10, new Vec2(400,300));
        //    ball.velocity = new Vec2(Utils.Random(-3.0f, 3.0f), Utils.Random(-3.0f, 3.0f));
        //    parent.AddChild(ball);
        //}

        for (int i = 0; i < particles; i++)
        {
            Particle ball = new Particle(size, pPos, spriteName: overlay);

            ball.velocity = Vec2.RandomUnitVector();
            ball.velocity *= Utils.Random(0, veloctiy);

            //ball.velocity = new Vec2(Utils.Random(-3.0f, 3.0f), Utils.Random(-3.0f, 3.0f));
            Scene scene = SceneManager.instance.GetActiveScene();
            scene.AddChild(ball);
        }
    }

    public void Cone(Vec2 pPos, float middleDeg, float turnDeg, int size = 1, float pRunTime = 1f, float velocity = 5.0f, Sprite overlay = null) {
        for (int i = 0; i < particles; i++)
        {
            Particle ball = new Particle(size, pPos, RunTime: pRunTime);

            float angle = middleDeg + Utils.Random(-turnDeg, turnDeg);
            ball.velocity = new Vec2(0, 1);
            ball.velocity.SetAngleDeg(angle);
            ball.velocity *= Utils.Random(0, velocity);

            //ball.velocity = new Vec2(Utils.Random(-3.0f, 3.0f), Utils.Random(-3.0f, 3.0f));
            parent.AddChild(ball);
        }


    }

    public void Regen() {
        for (int i = 0; i < particles;  i++)
        {
            Particle ball = new Particle(10, new Vec2(400, 300));

            ball.velocity = Vec2.RandomUnitVector();
            ball.velocity *= Utils.Random(0, 5.0f);

            //ball.velocity = new Vec2(Utils.Random(-3.0f, 3.0f), Utils.Random(-3.0f, 3.0f));
            parent.AddChild(ball);
            ball.regen = true;
        }

    }

    public void SpawnRandom() {
        Particle ball = new Particle(10, new Vec2(400, 300));

        ball.velocity = Vec2.RandomUnitVector();
        ball.velocity *= Utils.Random(0, 5.0f);

        parent.AddChild(ball);
        ball.regen = true;

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Package : Ball
{

    

    public enum PackageSpeed { Slow, Normal, Fast }

    public PackageSpeed speed;

    public bool acid = false;
    //seconds
    public float timer = 5.0f;

    public float spedTimer = 1.0f;


    //Base velocity when the speed is normal
    Vec2 baseVelocity;

    AnimationSprite sprite;

    Explosion breakPackage;
    public Package(Vec2 pPos, Vec2 pVel) : base(30, pPos, pVel)
    {
        baseVelocity = pVel;

        sprite = new AnimationSprite("baller.png", 4, 2, 7);
        sprite.x -= radius - 4;
        sprite.y -= radius - 5;
        sprite.width = width;
        sprite.height = height; 
        sprite.SetOrigin(width / 2, height / 2);
        sprite.alpha = 1;
        AddChild(sprite);
        alpha = 0;
        


   
    
    }

    void Update()
    {
    
        if (((MyGame)game).frozen && !((MyGame)game).end) return;
        base.Update();
        if (((MyGame)game).end) return;

        if (timer <= 0 || latestCollision is Enemy2Way)
        {
         

            MyGame myGame = ((MyGame)game);
            for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
            {

                if (myGame.GetMover(i) == this)
                {
 
                    Scene scene = SceneManager.instance.GetActiveScene();

                    if (scene is Levels)
                    {
                        Levels lev = scene as Levels;
                        if (lev.cannon.shots <= 0 && lev.ballsActive <= 1)
                        {

                            Pause_FailUI failUI = new Pause_FailUI(false);
                            lev.AddChild(failUI);
                            failUI.paused = true;
                        }
                        lev.ballsActive--;
            
                        myGame.RemoveBalls(this);

                        breakPackage = new Explosion(velocity);
                        breakPackage.x -= radius + 9 - position.x;
                        breakPackage.y -= radius + 9 - position.y;
                        breakPackage.width = (int)(sprite.width * 1.45f);
                        breakPackage.height = (int)(sprite.height * 1.45f);
                        breakPackage.SetOrigin(width / 2, height / 2);
                        lev.AddChild(breakPackage);
                        ((MyGame)game).SM.PopSFX();
                        this.LateDestroy();
                     //   ((MyGame)game).PS.Boom(position, 1, 1, 3, "bal.png");
                    }
                    

                }



            }
        } 
        else
        {
            if (acid) timer -= Time.deltaTime / 500.0f;
            else timer -= Time.deltaTime / 1000.0f;

         //   sprite.alpha = 0.2f * timer;
                sprite.Animate(timer / 50);
            
        }
    }



    //Changes the speed after a speedpad has been hit
    public void CheckSpeed() {

        switch (speed)
        { 
            case PackageSpeed.Slow:
                velocity = baseVelocity / 2.0f;
                break;
            case PackageSpeed.Normal:
                velocity = baseVelocity;
                break;
            case PackageSpeed.Fast:
                velocity = baseVelocity * 2.0f;
                break;
            default:
                Console.WriteLine("Speed not in available range!");
                break;  
        }            
    }

}
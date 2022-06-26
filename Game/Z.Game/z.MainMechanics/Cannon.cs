using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class Cannon : Sprite
{
    public int shots = 3;
    //public fields
    float _speed = 1;
    public Vec2 position
    {

        get
        {
            return _position;
        }
    }


    //private fields
    Vec2 _position;
    Vec2 bulletPos;
    Vec2 velocity;

    int left;
    int right;
    float timer;
    int waitTime = 700;

    Sprite sprite;

    Sprite wires;
    Sprite truck;
    Sprite wheel;

    AnimationSprite[] package = new AnimationSprite[3];

    bool first = true;

    
    public Cannon(float pX, float pY, float pSpeed, int leftBound = -47, int rightBound = 56) : base("cannon.png")
    {
        SetOrigin(width / 3, height / 2);
        width = width / 4;
        height = height / 4;
        _position.x = pX;
        _position.y = pY;
        _speed = pSpeed;

        left = leftBound;
        right = rightBound;

        truck = new Sprite("truck.png");
        truck.SetScaleXY(0.7f, 0.7f);
        truck.SetOrigin(truck.width / 2, truck.height / 2);


        //  truck.SetXY(_position.x - 150, _position.y + 101);
        truck.SetXY(_position.x - 150, _position.y + 75);

        wheel = new Sprite("wheel.png");
     
        wheel.SetOrigin(wheel.width / 2, wheel.height / 2);
        wheel.SetXY(_position.x + 25, _position.y + 4);

        wheel.width = wheel.width / 4;
        wheel.height = wheel.height / 4;

        package[0] = new AnimationSprite("baller.png", 4,2,7);
        package[0].SetScaleXY(0.2f, 0.2f);
        package[0].SetXY(_position.x, _position.y + 125);

        package[2] = new AnimationSprite("baller.png", 4, 2, 7);
        package[2].SetScaleXY(0.2f, 0.2f);
        package[2].SetXY(_position.x - 50, _position.y + 50);

        package[1] = new AnimationSprite("baller.png", 4, 2, 7);
        package[1].SetScaleXY(0.2f, 0.2f);
        package[1].SetXY(_position.x - 100, _position.y + 125);


    }

    void Controls()
    {

        
        float angleRotation = rotation * Mathf.PI / 180;

        if (Input.GetKey(Key.LEFT))
        {
            rotation--;
          //  ((MyGame)game).SM.Rotate();
        }
        else if (Input.GetKey(Key.RIGHT))
        {
            rotation++;
          //  ((MyGame)game).SM.Rotate();
        } 

        if(rotation < left)
        {

            rotation = left;
        }

        if(rotation > right)
        {
            rotation = right;
        }

        velocity = Vec2.GetUnitVectorDeg(rotation - 14) * _speed;
        bulletPos = Vec2.GetUnitVectorDeg(rotation - 14) * 100 + _position;

      
    }

    void Shoot()
    {
        if ((Input.GetKeyDown(Key.SPACE)) && shots > 0)
        {
           
            Package ball = new Package(bulletPos, velocity);
            ((MyGame)game)._movers.Add(ball);
            parent.AddChild(ball);
            ball.rotation = rotation ;
            if (parent is Levels)
            {
                Levels level = (Levels)parent;
                level.ballsActive++;
            }
        //   HUD _hud = ((MyGame)game).GetHUD;
            ((MyGame)game).SM.ShootSFX();

        //   HUD _hud = ((MyGame)game).GetHUD;
            shots--;
        //    _hud.UpdateShots();
        }

    }

    void UpdateSceenPosition()
    {
        x = _position.x;
        y = _position.y;

    }

    public void FollowMouse()
    {
       

    }

    Ball[] aimLine = new Ball[8];
    public void GEQOLL() {

        int balls = 4;
        float alpha = 1.0f;
        if (aimLine[0] == null) {
            for (int i = 0; i < aimLine.Length; i++) {
                Ball b = new Ball(20 - (2 * i), new Vec2(0, 0), new Vec2(0, 0), moving: false, tr: true);
                b.alpha -= i * 0.1f;
                aimLine[i] = b;
                parent.AddChild(b);
            }
        }

        if (aimLine[0] != null) {
            for (int i = 0; i < aimLine.Length; i++) { 
                aimLine[i].position = Vec2.GetUnitVectorDeg(rotation - 14) * (225 + 50 * i) + _position;
            }
         
        }
    }



    public void Update()
    {


        if (((MyGame)game).frozen && !((MyGame)game).end) return;
        for (int i = 0; i < package.Length; i++)
        {
            package[i].Animate(0.1f);
            if (shots - i <= 0 && package[i].alpha > 0) package[i].alpha -= 0.05f;
        }
        if (((MyGame)game).end) return;

        if (parent != null && first)
        {
            first = false;
            parent.AddChild(wheel);
            parent.AddChild(truck);
            parent.AddChild(package[0]);
            parent.AddChild(package[1]);
            parent.AddChild(package[2]);
       //     parent.AddChild(alienCharacter);
        }


        Controls();
        GEQOLL();
        Shoot();
        UpdateSceenPosition();

    }
}

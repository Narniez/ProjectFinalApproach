using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class LevelSelect : Scene {


    public enum Worlds { Nephelle, Bethel };

    Worlds _world;

    Sprite _backGround;
    Sprite _bottom;
    Sprite _hover;
    Sprite starEmpty = new Sprite("star_empty.png");
    Sprite starEmpty2 = new Sprite("star_empty.png");
    Sprite starFull = new Sprite("Star_Full.png");

    Vec2 stabalise = new Vec2(580, 515);
    public LevelSelect(Worlds pWorld) {

       _world = pWorld;
    }

    protected override void Start()
    {
        isActive = true;
        ((MyGame)game).GetCurrentScene = 1;

        CheckWorld();

        ImportSprites();
        MakeWorld();

    }
    public void CheckWorld() {


        switch (_world) {
            case Worlds.Nephelle:
                Console.WriteLine("World is Nephelle");
                break;
            case Worlds.Bethel:
                Console.WriteLine("World is Bethel");
                break;
            default:
                Console.WriteLine("World doesn't exist!");
               break;
        }
    }

    public void ImportSprites() {

        switch (_world)
        {
            case Worlds.Nephelle:
                _backGround = new Sprite("nepheleLevels.png");
                _hover = new Sprite("backgroundHover.png");
                _bottom = new Sprite("level1.png");
                break;
            case Worlds.Bethel:
                Console.WriteLine("World Bethel is not implemented yet!");
                break;
            default:
                Console.WriteLine("World doesn't exist!");
                break;
        }
    }
    
    void MakeWorld() {

       AddChild(_backGround);

        for (int i = 0; i < 3; i++)
        {
            BottonLevel level = new BottonLevel(new Vec2(577 + 379 * i, 526), Worlds.Nephelle, i);
            AddChild(level);
        }
    }

    public override void UnLoadScene()
    {
        base.UnLoadScene();
    }






}
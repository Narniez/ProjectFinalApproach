using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;


public class BottonLevel : Pivot
{

    Sprite _bottom;
    Sprite _hover;
    Sprite starEmpty = new Sprite("star_empty.png");
    Sprite starFull = new Sprite("Star_Full.png");
    Sprite number = new Sprite("number1.png");

    LevelSelect.Worlds _world;

    public static SoundChannel buttonScreenChannel = new SoundChannel(1);
    //Sound buttonScreenMusic = new Sound("", true, false);

    Sound buttonPress = new Sound("Sounds/Buttons/click.wav", false, false);
    Sound hoverButton = new Sound("", false, false);

    int _level;
    int levelInTotal;

    int stars;

    int radius = 130;
    public BottonLevel(Vec2 pPos, LevelSelect.Worlds pWorld, int pLevel) : base() {

        _level = pLevel;
        x = pPos.x;
        y = pPos.y;
        _world = pWorld;
        ImportSprites();
        ImportStars();
        ImportNumber();
        MakeButton();
    }


    public void ImportSprites() {

        switch (_world)
        {
            case LevelSelect.Worlds.Nephelle:
                _hover = new Sprite("backgroundHover.png");
                _bottom = new Sprite("level1.png");
                break;
            case LevelSelect.Worlds.Bethel:
                Console.WriteLine("World Bethel is not implemented yet!");
                break;
            default:
                Console.WriteLine("World doesn't exist!");
                break;
        }
    }

    public void MakeButton() {




        _hover.SetOrigin(_hover.width / 2, _hover.height / 2);
        _hover.SetXY(-4, 2);
        _hover.alpha = 0;
        AddChild(_hover);

        _bottom.SetOrigin(_bottom.width / 2, _bottom.height / 2);
        _bottom.SetXY(0, 0);
        AddChild(_bottom);

        number.SetOrigin(number.width / 2, number.height / 2);
        number.SetXY(-1, -8);
        AddChild(number);


        int totalStars = 3;
        for (int i = 0; i < stars; i++) {

            totalStars--;
            Sprite starFull = new Sprite("Star_Full.png");
            starFull.SetOrigin(starEmpty.width / 2, starEmpty.height / 2);
            starFull.SetXY(-67 + 67 * i, 137);
            AddChild(starFull);
        }
        for (int i = 0; i < totalStars; i++)
        {
            Sprite starEmpty = new Sprite("star_empty.png");
            starEmpty.SetOrigin(starEmpty.width /2, starEmpty.height / 2);
            starEmpty.SetXY(67 - 67 * i, 137);
            AddChild(starEmpty);
        }


    }

    public void ImportStars()
    {

        //-1 to make it start at level 0 as the collectable system works with it
        levelInTotal = _level + (int) _world * 3;

        CollectableSystem CS = ((MyGame)game).GetCollectableSystem;
        stars = CS.GetStars(levelInTotal);
    }

    

    public void ImportNumber() {

        switch (_level)
        {

            case 0:
                number = new Sprite("number1.png");
                break;
            case 1:
                number = new Sprite("number2.png");
                break;
            case 2:
                number = new Sprite("number3.png");
                break;
            default:
                Console.WriteLine("number is not available");
                break;
        }
    
    }

    void Update()
    {
        Hover();
    }

    bool hover = false;

    public void Hover() {

        if (Input.mouseX < x + radius && Input.mouseX > x - radius && Input.mouseY < y + radius && Input.mouseY > y - radius)
        {
            ((MyGame)game).SM.hoverBool = true;

            hover = true;
            _hover.alpha = 1;

            if (Input.GetMouseButtonDown(0))
            {
             
                ((MyGame)game).SM.ClickSFX();

                ((MyGame)game).fade.SwitchScenes(levelInTotal + 2);
 
            }
           
        }
        else 
        {
     
            _hover.alpha = 0;
        }
    }
} 
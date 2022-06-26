using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class StartScreen : Scene
{

    EasyDraw exitButton;
    AnimationSprite startScreen;

    int animatedRow = 1;
    public StartScreen()
    {
        
    }

    protected override void Start()
    {
        ((MyGame)game).GetCurrentScene = 0;
        ((MyGame)game).GetCurrentLevel = 0;
        Sprite background = new Sprite("BGN.jpg");
        background.width = 1920;
        background.height = 1080;

        startScreen = new AnimationSprite("newSheeet.png", 4, 6);

        exitButton = new EasyDraw(300, 200, false);
        exitButton.SetXY(860, 600);
        exitButton.TextSize(50);
        exitButton.Text("EXIT");


        AddChild(startScreen);
        AddChild(exitButton);
    }

    private void updateAnimation()
    {
        if(animatedRow == 1)
        {
            startScreen.SetCycle(1, 3);
            animatedRow = 2;
        }

        else if (animatedRow == 2)
        {
            startScreen.SetCycle(1, 21);

        }
    }
    
    protected override void Update()
    {
        
        if (((MyGame)game).GetCurrentScene == 0)
        {
            if ((Input.GetMouseButtonDown(0)) || Input.AnyKeyDown()) ((MyGame)game).fade.SwitchScenes(((MyGame)game).GetCurrentScene + 1);
              //  SceneManager.instance.LoadScene(((MyGame)game).GetCurrentScene + 1);
            

            if(Input.mouseX >= exitButton.x && Input.mouseX <= exitButton.x  + exitButton.width - 130 && Input.mouseY >= exitButton.y + 120 && Input.mouseY <= exitButton.y + exitButton.height - 20)
            {
                Console.WriteLine("Over Button");
                if (Input.GetMouseButtonDown(0)) game.Destroy();
            }
        }

        updateAnimation();
        startScreen.Animate(0.1f);
    }
}

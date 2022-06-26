using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class SoundManager : Pivot
{
    Sound bgSound = new Sound("Sounds/Background/MainBG.mp3");
    public Sound backgroundSound = new Sound("Sounds/Background/CloudsBG.mp3");
    public Sound menuSound = new Sound("Sounds/Background/MainBG.mp3");
    SoundChannel channel1;
    SoundChannel channel2;

    SoundChannel hover;
    bool switched = false;

    public bool switching = true;
    public Sound switchingSound;
    public bool mainMenu = true;
    public bool hoverBool = false;
    Sound[] cloud = new Sound[8];

    Sound click;
    Sound pop;
    Sound win;
    Sound lose;

    Sound rotate;
    SoundChannel rot;
    public SoundManager() {

        //       backgroundSound = new Sound("cloudsBG.mp3", true);
        //      backgroundSound.Play();

        click = new Sound("Sounds/Buttons/click.wav");
        LoadSounds();
        Sound hoverSound = new Sound("Sounds/Buttons/hover.wav", true);

       
       // hoverSound.Play();
        if (hover == null) hover = hoverSound.Play();
        hover.Volume = 0;



    }


    void LoadSounds() {
        for (int i = 0; i < 8; i++) cloud[i] = new Sound("Sounds/CloudCol/collision_" + i.ToString() + ".wav");
        pop = new Sound("Sounds/Ball/pop.wav");
        win = new Sound("Sounds/End/win.wav");
        lose = new Sound("Sounds/End/fail.wav");

        channel1 = backgroundSound.Play();
        channel2 = menuSound.Play();
        channel2.Volume = 1;
        channel1.Volume = 0;
    }




    


    void Update() {
        if (hoverBool) HoverButtonStart();
        else HoverButtonEnd();

        if (switching)
        {
            
            if (channel1.Volume > 0)
            {
                channel1.Volume -= 0.05f;
            }
            else if (channel2.Volume < 1)
            {
                channel2.Volume += 0.05f;
            }

        }
        else if (!switching)
        {
            if (channel2.Volume > 0)
            {
                channel2.Volume -= 0.05f;
            }
            else if (channel1.Volume < 1)
            {
                channel1.Volume += 0.05f;
            }

        }
    }

    public void WinSFX() { 
    win.Play();
    }

    public void LoseSFX() { 
    lose.Play();
    }
    public void PopSFX() { 
    pop.Play();
    }



    public void ShootSFX() {

        int i = Utils.Random(1, 7);

        string sound = "Sounds/Shoot/click" + i.ToString() + ".wav";

        Console.WriteLine(sound);
        Sound shoot = new Sound(sound);
        

        int j = Utils.Random(1, 3);

        string sound2 = "Sounds/Shoot/canon_" + j.ToString() + ".wav";
      //  Console.WriteLine(sound2);
        Sound shoot2 = new Sound(sound2);    
        shoot2.Play();
      //  shoot.Play();
    }

    public void CloudSFX()
    {

        int i = Utils.Random(0, 8);

        

        cloud[i].Play();

    }

    public void RockSFX() {

        int i = Utils.Random(1, 8);

        string sound = "Sounds/RockCol/collision " + i.ToString() + ".wav";

        Console.WriteLine(sound);
        Sound shoot = new Sound(sound);

        shoot.Play();
        
    }

   
    public void Rotate() {
        int i = Utils.Random(1, 5);

        string sound = "Sounds/Shoot/turning" + i.ToString() + ".wav";

        rotate = new Sound(sound);

     
            rot = rotate.Play();
        
        rot.Volume = 1;
        
    
    }

    public void StopRotate() {
        if (rot != null)
        {
            rot.Mute = true;
            rot.Stop();
        }
    }

    public void ClickSFX() {

        click.Play();
    }

    public void Shoot() { 
    
    }

    
    public void HoverButtonStart() {

        if (hover.Volume < 2) hover.Volume += 0.1f;
        hoverBool = false;
        Console.WriteLine(hover.Volume);
    }
    public void HoverButtonEnd() {
        if (hover.Volume > 0) hover.Volume -= 0.1f;
    
    }

}
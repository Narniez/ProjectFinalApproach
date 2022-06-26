using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class WorldSelectButton : Sprite
{

    int radius = 30;
    
    public WorldSelectButton(int pRadius) : base("circle.png")
    {
        SetOrigin(width / 2, height / 2);
        radius = pRadius;
        width = radius * 2;
        height = radius * 2;

    }

    public void Update() { 
    
    //calculates the distance from middle circle to mouse to see if it's inside the circle and thus clickable
    float DisToMouse = (new Vec2(x, y) - new Vec2(Input.mouseX, Input.mouseY)).Length();

        if (Input.GetMouseButtonDown(1) && DisToMouse <= radius) { 
        
            //Switch to level select of screen
        
        }
    }

}

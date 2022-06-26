using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class HUD : Pivot {

    EasyDraw shots;
    EasyDraw collectables;

    public HUD(Vec2 pPos)
    {
        x = pPos.x;
        y = pPos.y;

        EasyDraw canvas = new EasyDraw(200, 100);
        canvas.Fill(122);
        canvas.Rect(50, 50, 200, 100);  
        AddChild(canvas);
        shots = new EasyDraw(200, 50);
        shots.x = -25;
        shots.Fill(0);
     //   shots.Text("Shots left: " + ((MyGame)game).cannon.shots, 50, 50);
        AddChild(shots);

        collectables = new EasyDraw(200, 50);       
        collectables.y = 50;
        AddChild(collectables);
    }


    public void UpdateShots() {
        shots.ClearTransparent();
      //  shots.Text("Shots left: " + ((MyGame)game).cannon.shots, 50, 50);
    }

    public void UpdateCol(int col) {
        collectables.ClearTransparent();

        for (int i = 0; i < col; i++)
        {
            collectables.Ellipse(25 + i * 50, 25, 25, 25);
        }
    
    }
}
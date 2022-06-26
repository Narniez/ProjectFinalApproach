using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class ChangeSpeedPad : BoxMechanic {

    float speedChange;

    public ChangeSpeedPad(Vec2 Pos, int pWidth, int pHeight, float pRot = -90.0f, float pSpeed = 1f) : base(Pos, pWidth, pHeight) {
        speedChange = pSpeed;
    }

    protected override void InBox(Package pPack)
    {
        if (speedChange > 1.0f)
        {
            if (pPack.speed < Package.PackageSpeed.Fast)
            {
                pPack.speed++;
                pPack.CheckSpeed();
            }
        }
        else if (speedChange < 1.0f)
        {
            if (pPack.speed > Package.PackageSpeed.Slow)
            { 
                pPack.speed--;
                pPack.CheckSpeed();
            }
        }

    }
}
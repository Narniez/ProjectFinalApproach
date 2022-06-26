using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class AcidRain : BoxMechanic {


    public AcidRain(Vec2 Pos, int pWidth, int pHeight) : base(Pos, pWidth,  pHeight) {

    }
    protected override void InBox(Package pPack)
    {
       pPack.acid = true;
    }

    protected override void OutBox(Package pPack)
    {
      pPack.acid = false;
    }
}
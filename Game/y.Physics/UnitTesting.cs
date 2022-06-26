using GXPEngine;
using System;


public class UnitTesting
{



    public UnitTesting()
    {

        DoTests();

    }

    static void DoTests()
    {


        Vec2 myVec = new Vec2(2, 3);
        Vec2 myVec2 = new Vec2(1, 5);
        Vec2 result = myVec - myVec2;
        Console.WriteLine("Minus left good?: " + (result.x == 1 && result.y == -2 && myVec.x == 2 && myVec.y == 3 && myVec2.x == 1 && myVec2.y == 5));
        Vec2 result2 = myVec2 - myVec;
        Console.WriteLine("Minus right good?: " + (result2.x == -1 && result2.y == 2 && myVec.x == 2 && myVec.y == 3 && myVec2.x == 1 && myVec2.y == 5));


        Vec2 result3 = myVec * 3;
        Console.WriteLine("Scalar multiplication right ok ?: " +
        (result3.x == 6 && result3.y == 9 && myVec.x == 2 && myVec.y == 3));
        Vec2 result4 = 4 * myVec2;
        Console.WriteLine("Scalar multiplication left ok ?: " +
        (result4.x == 4 && result4.y == 20 && myVec2.x == 1 && myVec2.y == 5));


        myVec.SetXY(-3, 4);
        Console.WriteLine("SetXY ok? : {0} (Should be (-3, 4) ", myVec);
        myVec.Normalize();
        myVec2.Normalize();
        Console.WriteLine("Normalize ok? : {0}  (should be (-0.6,0.8))  ", myVec);

        Vec2 test = myVec.Normalized();
        Console.WriteLine("Normalized ok? : {0} Should be (-0.6, 0.8))  ", test);



        Console.ReadLine();


        //Little of bc PI is not excactly PI
        Vec2 myVec3 = Vec2.GetUnitVectorDeg(90);
        Console.WriteLine("GetUnitVectorDeg ok? : {0} (Should be (0,1))", myVec3);

        Vec2 myVec4 = Vec2.GetUnitVectorRad(Mathf.PI);
        Console.WriteLine("GetUnitVectorRad ok? : {0} (Should be (-1,0))", myVec4);
        Console.WriteLine(Mathf.Sin(Mathf.PI));

        Vec2 myVec5 = Vec2.RandomUnitVector();
        Vec2 myVec6 = Vec2.RandomUnitVector();
        Console.WriteLine("RandomUnitVector ok? : {0} & {1}  (Both should be different)", myVec5, myVec6);


        Vec2 myVec7 = new Vec2(0, 2);
        myVec7.SetAngleRad(-(Mathf.PI * 0.5f));
        Console.WriteLine("SetAngleRad ok? : {0} (Should be (0,-2) ", myVec7);
        Vec2 myVec8 = new Vec2(2, 0);
        myVec8.SetAngleDeg(90);
        Console.WriteLine("SetAngleDeg ok? : {0} (Should be (0,2) ", myVec8);

        float f1 = myVec8.GetAngleDeg();
        Console.WriteLine("GetAngleDeg ok? : {0}  (Should be 90) ", f1);

        float f2 = myVec7.GetAngleRad();
        Console.WriteLine("GetAngleRad ok? : {0} (Should be -0.5f PI)", f2);


        Vec2 myVec9 = new Vec2(1, 0);
        myVec9.RotateDeg(-45);
        myVec9.RotateDeg(-45);
        Console.WriteLine("RotateDeg ok? : {0} (Should be (0,-1)", myVec9);

        Vec2 myVec10 = new Vec2(1, 0);
        myVec10.RotateRad(0.25f * Mathf.PI);
        myVec10.RotateRad(0.25f * Mathf.PI);
        Console.WriteLine("RotateRad ok? : {0} (Should be (0,1)", myVec10);

        Vec2 myVec11 = new Vec2(2, 2);
        Vec2 myVec12 = new Vec2(1, 2);

        myVec12.RotateAroundDeg(myVec11, 90);
        Console.WriteLine("RotateAroundDeg ok? : {0} (Should be (2,1))", myVec12);
        myVec12.SetXY(1, 2);
        myVec12.RotateAroundRad(myVec11, Mathf.PI);
        Console.WriteLine("RotateAroundRad ok? : {0} (Should be (3,2))", myVec12);



        Console.ReadLine();

        Vec2 myVec13 = new Vec2(3, 4);
        myVec13.Length();
        Console.WriteLine("Lenght ok?: {0} (Should be 5)", myVec13);

        Vec2 myVec14 = new Vec2(3, 6);
        Vec2 myVec15 = new Vec2(2, 4);
        float testDot = myVec14.Dot(myVec15);
        Console.WriteLine("Dot ok? : {0} (Should be 30)", testDot);

        Vec2 myVec16 = new Vec2(14, 7);
        myVec16 /= 7;
        Console.WriteLine("Divider ok? : {0} (Should be (2,1) )", myVec16);

        Console.WriteLine("Deg2Rad ok? : {0} (Should be around 3,14...)", Vec2.Deg2Rad(180));

        Console.WriteLine("Rad2Deg ok? : {0} (Should be around -90)", Vec2.Rad2Deg(-(0.5f * Mathf.PI)));

        Vec2 myVec18 = new Vec2(-1, 0);
        Vec2 myVec19 = myVec18.Normal();
        Console.WriteLine("Normal ok? : {0} Should be (0,-1) )", myVec19);

        Vec2 myVec17 = new Vec2(1, 1);
        Vec2 myNormal = new Vec2(0, 1);
        myVec17.Reflect(myNormal);

        Console.WriteLine("Reflect ok? : {0} Should be (1,-1) )", myVec17);

        Console.ReadLine();

    }

}
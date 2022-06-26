using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2
{
	public float x;
	public float y;


	public Vec2 MousePosition()
    {
		Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
		return mousePos; 
    }

	public Vec2(float pX = 0, float pY = 0)
	{
		x = pX;
		y = pY;
	}

	// TODO: SetXY methods (see Assignment 1)

	public float Length()
	{
		return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
	}

	public void Normalize()
	{
		float devider = Length();
		if (devider == 0) return;
		x = x / devider;
		y = y / devider;
	}

	public Vec2 Normalized()
	{
		float devider = Length();
		if (devider == 0) return this;
		return new Vec2(x / devider, y / devider);
	}
	public void SetXY(float pX, float pY)
	{
		x = pX;
		y = pY;
	}

	public float Dot(Vec2 right)
	{

		return ((this.x * right.x) + (this.y * right.y));
	}

	public Vec2 Normal()
	{

		Vec2 vector = new Vec2(-y, x);
		vector.Normalize();
		return vector;

	}

	public void Reflect(Vec2 normalLine, float bounciness = 1.0f)
	{

		this = this - (1 + bounciness) * (this.Dot(normalLine)) * normalLine;
	}

	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}

	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}

	public static Vec2 operator *(Vec2 left, float right)
	{
		return new Vec2(left.x * right, left.y * right);
	}
	public static Vec2 operator *(float left, Vec2 right)
	{
		return new Vec2(left * right.x, left * right.y);
	}

	//Write Unit Test
	public static Vec2 operator /(Vec2 left, float right)
	{
		return new Vec2(left.x / right, left.y / right);
	}

	public static float Deg2Rad(float degree)
	{
		return (degree / (180.0f / Mathf.PI));
	}
	public static float Rad2Deg(float rad)
	{
		return (rad * (180.0f / Mathf.PI));
	}

	public static Vec2 GetUnitVectorDeg(float degrees)
	{
		return GetUnitVectorRad(Deg2Rad(degrees));
	}

	public static Vec2 GetUnitVectorRad(float rad)
	{
		Vec2 vectorRad = new Vec2();
		vectorRad.x = Mathf.Cos(rad);

		vectorRad.y = Mathf.Sin(rad);
		return vectorRad;

	}

	public static Vec2 RandomUnitVector()
	{
		float RandomDeg = Utils.Random(0, 361);
		return GetUnitVectorDeg(RandomDeg);

	}

	public void SetAngleDeg(float degrees)
	{

		SetAngleRad(Deg2Rad(degrees));
	}
	public void SetAngleRad(float rad)
	{
		float lenght = Length();

		x = Mathf.Cos(rad) * lenght;
		y = Mathf.Sin(rad) * lenght;
	}

	public float GetAngleDeg()
	{

		return Rad2Deg(GetAngleRad());

	}
	public float GetAngleRad()
	{
		return Mathf.Atan2(y, x);
	}
	public void RotateDeg(float degrees)
	{
		RotateRad(Deg2Rad(degrees));
	}

	public void RotateRad(float rad)
	{


		float cosR = Mathf.Cos(rad);
		float sinR = Mathf.Sin(rad);
	
		SetXY(x * cosR - y * sinR, x * sinR + y * cosR);
	}

	public void RotateAroundDeg(Vec2 vector, float deg)
	{
		RotateAroundRad(vector, Deg2Rad(deg));
	}
	public void RotateAroundRad(Vec2 vector, float rad)
	{
		this -= vector;
		RotateRad(rad);
		this += vector;

	}


	public override string ToString()
	{
		return String.Format("({0},{1})", x, y);
	}
}


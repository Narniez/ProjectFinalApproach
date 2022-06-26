using System;
using GXPEngine;

public class Ball : EasyDraw
{
	// These four public static fields are changed from MyGame, based on key input (see Console):
	public static bool drawDebugLine = false;
	public static bool wordy = false;
	public static float bounciness = 1.00f;


	bool trigger = false;
	public Vec2 accel;
	public Vec2 velocity;
	public Vec2 position;
	protected Vec2 _oldPosition;

	public readonly int radius;
	public readonly bool moving;

	//Makes so I can check which collision happened and with which element.
	public GameObject latestCollision = null;
	public Vec2 latestNormal = new Vec2(0,0);

	// Mass = density * volume.
	// In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
	public float Mass {
		get {
			return radius * radius * _density;
		}
	}

	Arrow _velocityIndicator;

	protected float _density = 1;

	public Ball (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), bool moving=true, bool tr = false) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		radius = pRadius;
		position = pPosition;
		velocity = pVelocity;
		this.moving = moving;
		trigger = tr;

		position = pPosition;
		UpdateScreenPosition ();
		SetOrigin (radius, radius);

		Draw (230, 200, 0);

		_velocityIndicator = new Arrow(position, new Vec2(0,0), 10);
		AddChild(_velocityIndicator);
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (radius, radius, 2*radius, 2*radius);
	}

	void UpdateScreenPosition() {
		x = position.x;
		y = position.y;
	}


	public float TOIBall(Ball move, Ball other) {

		float a = Mathf.Pow(Mathf.Abs(move.velocity.Length()),2);

		Vec2 u = (move._oldPosition - other.position);
		float b = u.Dot(move.velocity) * 2;
		float c = Mathf.Pow(Mathf.Abs(u.Length()),2) - Mathf.Pow((move.radius + other.radius), 2);

		if (a <= 0) return -10;

		float D = Mathf.Pow(b, 2) - (4 * a * c);
		if (D < 0) return -10;

		float TOI1 = ((-b - Mathf.Pow(D, (1.0f / 2.0f))) / (2 * a));
		float TOI2 = ((-b + Mathf.Pow(D, (1/0f / 2.0f))) / (2 * a));

		if (TOI1 < 0 || TOI2 < 0) return -10;

		if (TOI1 > TOI2) return TOI2; 
		else return TOI1;
	}

	public float TOIBallLine(Ball move, LineSegment line, Vec2 difference, bool top)
	{


		float t = 10;
		Vec2 lineLenght = (line.end - line.start);
		Vec2 lineNormal = lineLenght.Normal();
		Vec2 lineNormalized = lineLenght.Normalized();

		float a = difference.Dot(lineNormal) - move.radius;
		float b = -lineNormal.Dot(move.velocity);

		if (b <= 0) return 10;
		

		if (a >= 0)
		{
			t = a / b;
		}
		else if (a >= -move.radius)
		{
			t = 0;
		}
		else return 10;


		if (t < 1)
		{
			Vec2 POI = _oldPosition + velocity * t;
			Vec2 newDif = POI - line.start;

			float d = newDif.Dot(lineNormalized);

			if (d >= 0 && d <= lineLenght.Length()) return t;
			else return 10;

		}
		else return 10;


	}
	public void Update() {
		Step();
	}


	public void Step () {
		velocity += accel;
		_oldPosition = position;
		position += velocity;

		x = position.x;
		y = position.y;
		if (!trigger) CheckCollision();

	}

	public void CheckCollision() {


		CollisionInfo firstCollision = FindEarliestCollision();
		if (firstCollision != null)
		{
			ResolveCollision(firstCollision);
        }
        else 

		UpdateScreenPosition();
	}

	//Difference of line/ball need for resolve line
	public float difline;
	
	CollisionInfo FindEarliestCollision()
	{
		MyGame myGame = (MyGame)game;

		GameObject otherCol = null;

		Vec2 firstnormal = new Vec2(0,1);
		float FirstTOI = 2;
		// Check other movers:			
		for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
		{
			Ball mover = myGame.GetMover(i);
			if (mover != this)
			{
				Vec2 relativePosition = position - mover.position;

				if (relativePosition.Length() < radius + mover.radius)
				{
					float Time = TOIBall(this, mover);
					if (Time < 0) Time = 3;

					if (Time < FirstTOI)
					{
						FirstTOI = Time;
						otherCol = mover;
						

					}
				}
			}
		}


		for (int i = 0; i < myGame.GetNumberOfLines(); i++)
		{
			LineSegment _line = myGame.GetLine(i);

			Vec2 point = _line.start;
			Vec2 line = _line.start - _line.end;

			Vec2 normalLine = line.Normal();
			Vec2 difference = point - position;

			Vec2 oldDif = _oldPosition - point;

			float ballDistance = difference.Dot(normalLine);

			if (Mathf.Abs(ballDistance) < radius)
			{

				float t = TOIBallLine(this, _line, oldDif, false);

				if (t < FirstTOI)
				{
					firstnormal = normalLine;
					otherCol = _line;
					FirstTOI=t;
					difline = ballDistance;
				}
			}
			else
			{
				SetColor(0, 1, 0);
			}
		}

		if (FirstTOI < 1)
		{
			return new CollisionInfo(firstnormal, otherCol, FirstTOI);
		}
		else return null;
	}



	void ResolveCollision(CollisionInfo col) {

		latestCollision = col.other;
		latestNormal = col.normal;
		
		if (col.other is Ball) {

			position = _oldPosition + col.timeOfImpact * velocity;
			Ball otherBall = (Ball)col.other;

			Vec2 relPos = position - otherBall.position;
			Vec2 relVel = velocity - otherBall.velocity;

			float angle = relVel.Dot(relPos);

		
			if (angle >= 0) return;

			Vec2 CenterOfMass = (velocity * Mass + otherBall.velocity * otherBall.Mass) / (Mass + otherBall.Mass);
			Vec2 firstNormal = (position - otherBall.position).Normalized();



			if (otherBall.moving == true)
			{
				velocity = velocity - (1 + bounciness) * (firstNormal.Dot(velocity - CenterOfMass)) * firstNormal;
				otherBall.velocity = otherBall.velocity - (1 + bounciness) * (firstNormal.Dot(otherBall.velocity - CenterOfMass)) * firstNormal;
			}
			else velocity *= bounciness;

			velocity.Reflect(firstNormal);

		}

		if (col.other is LineSegment) {
			position -= (-difline + radius) * col.normal;
			velocity.Reflect(col.normal);
		}
	}

	void ShowDebugInfo() {
		if (drawDebugLine) {
			// ((MyGame)game).DrawLine (_oldPosition, position);
		}
		_velocityIndicator.startPoint = position;
		_velocityIndicator.vector = velocity;
	}
}


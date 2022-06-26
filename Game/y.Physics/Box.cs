using System;
using GXPEngine;


public class Block : EasyDraw
{
	/******* PUBLIC FIELDS AND PROPERTIES *********************************************************/

	// These four public static fields are changed from MyGame, based on key input (see Console):
	public static bool drawDebugLine = false;
	public static bool wordy = false;
	public static float bounciness = 0.98f;
	// For ease of testing / changing, we assume every block has the same acceleration (gravity):
	public static Vec2 acceleration = new Vec2(0, 0);

	public readonly int radius;

	// Mass = density * volume.
	// In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
	public float Mass
	{
		get
		{
			return 4 * radius * radius * _density;
		}
	}

	public Vec2 position
	{
		get
		{
			return _position;
		}
	}

	public Vec2 velocity;

	public int side = 0;
	public float firstTOI = 0;
	public Block firstColBlock = null;

	/******* PRIVATE FIELDS *******************************************************************/

	Vec2 _position;
	Vec2 _oldPosition;

	float _red = 1;
	float _green = 1;
	float _blue = 1;

	float _density = 1;

	const float _colorFadeSpeed = 0.025f;

	/******* PUBLIC METHODS *******************************************************************/

	public Block(int pRadius, Vec2 pPosition, Vec2 pVelocity) : base(pRadius * 2, pRadius * 2)
	{
		radius = pRadius;
		_position = pPosition;
		velocity = pVelocity;

		SetOrigin(radius, radius);
		Draw();
		UpdateScreenPosition();
		_oldPosition = new Vec2(0, 0);
		//	bounciness = 1.0f;
		acceleration = new Vec2(0, (100f * 9.81f) / Mass);


	}


	bool graf = false;
	public void Gravity()
	{

		if (Input.GetKeyDown(Key.E)) graf = !graf;

		if (graf)
		{

			if (Input.GetKeyDown(Key.UP)) acceleration.SetXY(0, -((100f * 9.81f) / Mass));
			if (Input.GetKeyDown(Key.DOWN)) acceleration.SetXY(0, ((100f * 9.81f) / Mass));
			if (Input.GetKeyDown(Key.LEFT)) acceleration.SetXY(-((100f * 9.81f) / Mass), 0);
			if (Input.GetKeyDown(Key.RIGHT)) acceleration.SetXY(((100f * 9.81f) / Mass), 0);

		}
		else acceleration.SetXY(0, 0);
	}

	public void SetFadeColor(float pRed, float pGreen, float pBlue)
	{
		_red = pRed;
		_green = pGreen;
		_blue = pBlue;
	}

	public void Update()
	{
		// For extra testing flexibility, we call the Step method from MyGame instead:
		
		Gizmos.DrawArrow(_position.x, _position.y, velocity.x * 10, velocity.y * 10);
	}

	public void Step()
	{
		_oldPosition = _position;

		Gravity();
		Move();
		UpdateColor();
		UpdateScreenPosition();
		ShowDebugInfo();
	}



	void Move()
	{
		firstTOI = 1.0f;
		//side 1 is left, 2 is right, 3 is top, 4 is down;
		side = 0;
		firstColBlock = null;

		velocity += acceleration;
		_position += velocity;

		CheckBlockOverlaps();
		ResolveCollision(side, firstTOI, firstColBlock);

	}



	float POI(float Impact, bool y)
	{
		float timeOfInpact;

		if (y)
		{
			if (position.y - _oldPosition.y == 0) return 10;
			timeOfInpact = (Impact - _oldPosition.y) / (_position.y - _oldPosition.y);
		}
		else
		{
			if (position.x - _oldPosition.x == 0) return 10;
			timeOfInpact = (Impact - _oldPosition.x) / (_position.x - _oldPosition.x);
		}
		return timeOfInpact;



	}




	void CheckBlockOverlaps()
	{

		MyGame myGame = (MyGame)game;
		for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
		{
			Block other = new Block(30, new Vec2(2,2), new Vec2(2, 2));
			if (other != this)
			{
				// TODO: improve hit test, move to method:
				if ((this._position.x + this.radius > other._position.x - other.radius) &&
					(this._position.x - this.radius < other._position.x + other.radius) &&
					(this._position.y + this.radius > other._position.y - other.radius) &&
					(this._position.y - this.radius < other._position.y + other.radius))
				{

					//LEFT COLLISSION
					if (_position.x - radius < other._position.x + other.radius)
					{

						float impact = (other._position.x + other.radius + this.radius);
						float TOI = POI(impact, false);

						if (TOI >= 0 && TOI < firstTOI)
						{
							float newy = _oldPosition.y + TOI * velocity.y;
							float newy2 = _oldPosition.y + TOI * velocity.y;

							newy += radius;
							newy2 -= radius;

							if (!((newy < (other.position.y - other.radius) && newy2 < (other.position.y - other.radius)) || newy > (other.position.y + other.radius) && newy2 > (other.position.y + other.radius)))
							{
								firstColBlock = other;
								firstTOI = TOI;
								side = 1;
							}
						}

					}
					//RIGHT COLLISION
					if (_position.x + radius > other._position.x - other.radius)
					{
						float impact = (other._position.x - other.radius - this.radius);
						float TOI = POI(impact, false);
						if (TOI >= 0 && TOI < firstTOI)
						{
							float newy = _oldPosition.y + TOI * velocity.y;
							float newy2 = _oldPosition.y + TOI * velocity.y;
							newy += radius;
							newy2 -= radius;

							if (!((newy < (other.position.y - other.radius) && newy2 < (other.position.y - other.radius)) || newy > (other.position.y + other.radius) && newy2 > (other.position.y + other.radius)))
							{
								firstColBlock = other;
								firstTOI = TOI;
								side = 2;
							}
						}
					}
					//TOP COLLISION
					if (_position.y - radius < other._position.y + other.radius)
					{
						float impact = (other._position.y + other.radius + this.radius);
						float TOI = POI(impact, true);
						if (TOI >= 0 && TOI < firstTOI)
						{
							float newx = _oldPosition.x + TOI * velocity.x;
							float newx2 = _oldPosition.x + TOI * velocity.x;

							newx += radius;
							newx2 -= radius;

							if (!((newx < (other.position.x - other.radius) && newx2 < (other.position.x - other.radius)) || newx > (other.position.x + other.radius) && newx2 > (other.position.x + other.radius)))
							{
								firstColBlock = other;
								firstTOI = TOI;
								side = 3;
							}
						}
					}
					//BOTTOM COLLISION
					if (_position.y + radius > other._position.y - other.radius)
					{
						float impact = (other._position.y - other.radius - this.radius);

						float TOI = POI(impact, true);
						if (TOI >= 0 && TOI < firstTOI)
						{
							float newx = _oldPosition.x + TOI * velocity.x;
							float newx2 = _oldPosition.x + TOI * velocity.x;

							newx += radius;
							newx2 -= radius;

							//checks if both y's are outside the other cube
							if (!((newx < (other.position.x - other.radius) && newx2 < (other.position.x - other.radius)) || newx > (other.position.x + other.radius) && newx2 > (other.position.x + other.radius)))
							{
								firstColBlock = other;
								firstTOI = TOI;
								side = 4;
							}
						}
					}

					SetFadeColor(0.2f, 0.2f, 1);
					other.SetFadeColor(0.2f, 0.2f, 1);
					if (wordy)
					{
						Console.WriteLine("Block-block overlap detected.");
					}
				}
			}
		}
	}

	void ResolveCollision(int side, float TOI, Block other = null)
	{
		if (side == 0) return;
		

		if (other == null)
		{
			if (side == 1 || side == 2)
			{
				_position = _oldPosition + firstTOI * velocity;
				velocity.x *= -bounciness;
			}
			else if (side == 3 || side == 4)
			{
				_position = _oldPosition + firstTOI * velocity;
				velocity.y *= -bounciness;
			}
		}
		else if (other != null)
		{

			Vec2 relPos = _oldPosition - other.position;
			Vec2 relVel = velocity - other.velocity;

			float angle = relVel.Dot(relPos);

			if (angle >= 0) return;

			if (side == 1 || side == 2)
			{
				_position = _oldPosition + firstTOI * velocity;
				other._position = other._oldPosition + firstTOI * other.velocity;

				float CenterOfMass = ((this.velocity.x * this.Mass + other.velocity.x * other.Mass) / (this.Mass + firstColBlock.Mass));
				velocity.x = CenterOfMass - bounciness * (velocity.x - CenterOfMass);
				other.velocity.x = CenterOfMass - bounciness * (other.velocity.x - CenterOfMass);

			}
			else if (side == 3 || side == 4)
			{
				_position = _oldPosition + firstTOI * velocity;
				firstColBlock._position = firstColBlock._oldPosition + firstTOI * firstColBlock.velocity;

				float CenterOfMass = ((this.velocity.y * this.Mass + firstColBlock.velocity.y * firstColBlock.Mass) / (this.Mass + firstColBlock.Mass));
				velocity.y = CenterOfMass - bounciness * (velocity.y - CenterOfMass);
				firstColBlock.velocity.y = CenterOfMass - bounciness * (firstColBlock.velocity.y - CenterOfMass);
			}

		}
	}









	/******* NO NEED TO CHANGE ANY OF THE CODE BELOW: **********************************************/

	void UpdateColor()
	{
		if (_red < 1)
		{
			_red = Mathf.Min(1, _red + _colorFadeSpeed);
		}
		if (_green < 1)
		{
			_green = Mathf.Min(1, _green + _colorFadeSpeed);
		}
		if (_blue < 1)
		{
			_blue = Mathf.Min(1, _blue + _colorFadeSpeed);
		}
		SetColor(_red, _green, _blue);
	}

	void ShowDebugInfo()
	{
		if (drawDebugLine)
		{
			((MyGame)game).DrawLine(_oldPosition, _position);
		}
	}

	void UpdateScreenPosition()
	{
		x = _position.x;
		y = _position.y;
	}

	void Draw()
	{
		Fill(200);
		NoStroke();
		ShapeAlign(CenterMode.Min, CenterMode.Min);
		Rect(0, 0, width, height);
	}
}
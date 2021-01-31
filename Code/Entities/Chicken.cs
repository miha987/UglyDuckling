using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using UglyDuckling.Code.ChickenControl;

namespace UglyDuckling.Code.Entities
{
	class Chicken : Entity
	{
		private const double OFF_TRACK_CHANCE = 0.50;
		// Putting off-track deviation over 0.5 produces weird behaviour.
		private const double OFF_TRACK_DEVIATION = 0.40;

		private Random random = new Random();

		public float MoveSpeed { get; set; } = 3f; // default
		private double freezeTime = 0; // in seconds
		public Vector2 TargetPosition { get; set; }

		private Vector2? OffTrackDirection = null;
		private double OffTrackTime = 0;

		private bool PerformingInitialMovementProcedure = false;
		private bool PerformingFinalMovementProcedure = false;

		public Chicken() : base(Vector2.Zero)
		{
			SetTexture("yellow_duck");
			//Rotate(MathHelper.Pi);

			InitializeAnimations();
		}

		public void InitializeAnimations()
		{
			EnableAnimator(6, 10);

			Animation blobAnimation = new Animation("blob", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, 0.055556, true, true);
			Animation idleAnimation = new Animation("idle", new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, 0.0833, true, true);
			Animation leftAnimation = new Animation("left", new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 }, 0.055556, true, true);
			Animation rightAnimation = new Animation("right", new int[] { 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48 }, 0.055556, true, true);
			Animation upAnimation = new Animation("up", new int[] { 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 }, 0.055556, true, true);

			this.AddAnimation(blobAnimation);
			this.AddAnimation(idleAnimation);
			this.AddAnimation(leftAnimation);
			this.AddAnimation(rightAnimation);
			this.AddAnimation(upAnimation);

			this.SetAnimation("idle");
		}

		public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			CheckBeat();

			InitialMovementProcedure(gameTime);
			FinalMovementProcedure(gameTime);

			if (freezeTime > 0)
            {
				freezeTime -= gameTime.ElapsedGameTime.TotalSeconds;
            }

			double errorDist = Vector2.Distance(GetPosition(), TargetPosition);
			if (errorDist > 2 && freezeTime <= 0)
            {
				MoveTowardsTarget(gameTime);
            }

			UpdateZ();
		}

		private void CheckBeat()
		{
			bool isBeat = GameState.Instance.GetVar<bool>("is_beat");

			if (isBeat)
			{
				Beat beat = GameState.Instance.GetVar<Beat>("current_beat");
				//Trace.WriteLine(beat.Type);
				if (beat.Type == 0)
					SetAnimation("up", false, true);
				if (beat.Type == 1)
					SetAnimation("right", false, true);
				if (beat.Type == 2)
					SetAnimation("blob", false, true);
				if (beat.Type == 3)
					SetAnimation("left", false, true);
			}
			else
			{
				SetAnimation("idle");

			}
		}

		public void UpdateZ()
		{
			int backgroundHeight = GameState.Instance.GetVar<int>("background_height");
			int backgroundY = GameState.Instance.GetVar<int>("background_y");

			if (backgroundHeight == 0)
				return;

			float factor = (GetPosition().Y - (backgroundY - backgroundHeight)) / backgroundHeight;
			int newZ = (int)(factor * 800);

			if (newZ > 800)
				newZ = 800;

			if (newZ < 0)
				newZ = 0;

			SetZ(newZ);
		}

		public void PerformInitialMovementProcedure(double delaySeconds)
		{
			freezeTime = delaySeconds;
			PerformingInitialMovementProcedure = true;
			SetPosition(NamedPositions.ChickenCoopInside);
			TargetPosition = NamedPositions.ChickenCoopDoor;
		}

		public void PerformFinalMovementProcedure(double delaySeconds)
        {
			freezeTime = delaySeconds;
			PerformingFinalMovementProcedure = true;
			TargetPosition = NamedPositions.ChickenCoopDoor;
        }

		public bool IsPerformingInitialMovementProcedure()
        {
			return PerformingInitialMovementProcedure;
		}

		public bool IsPerformingFinalMovementProcedure()
        {
			return PerformingFinalMovementProcedure;
        }
		
		public bool IsMoving()
		{
			double errorDist = Vector2.Distance(GetPosition(), TargetPosition);
			return errorDist >= 2;
		}

		private void MoveTowardsTarget(GameTime gameTime)
        {
			Vector2 moveTowards = Vector2.Normalize(Vector2.Subtract(TargetPosition, GetPosition()));
			if (OffTrackDirection != null)
			{
				// off-track direction is added after normalization - thus affecting movement speed as well
				// (i.e. when off-tracking, chickens can go slower/faster, depends on deviation).
				moveTowards += (Vector2)OffTrackDirection;
				OffTrackTime += gameTime.ElapsedGameTime.TotalSeconds;

				if (OffTrackTime > 2)
				{
					OffTrackDirection = null;
					OffTrackTime = 0;
				}
			}
			else if (OffTrackDirection == null && random.NextDouble() < OFF_TRACK_CHANCE)
			{
				float dx = (float)(random.NextDouble() * (OFF_TRACK_DEVIATION * 2) - OFF_TRACK_DEVIATION);
				float dy = (float)(random.NextDouble() * (OFF_TRACK_DEVIATION * 2) - OFF_TRACK_DEVIATION);
				OffTrackDirection = new Vector2(dx, dy);
			}

			this.horizontalFlip = moveTowards.X > 0;

			moveTowards *= MoveSpeed;
			Move(moveTowards);
		}

		private void InitialMovementProcedure(GameTime gameTime)
        {
			if (PerformingInitialMovementProcedure)
            {
				bool nearDoor = Vector2.Distance(GetPosition(), NamedPositions.ChickenCoopDoor) < 2;
				if (nearDoor)
				{
					// chicken is at the door, update target to outside chicken coop
					TargetPosition = CheckpointGenerator.RandomOffset(NamedPositions.ChickenCoopOutside);
				} 
				else if (Vector2.Distance(GetPosition(), TargetPosition) < 2 && !nearDoor)
                {
					// Initial movement procedure done.
					PerformingInitialMovementProcedure = false;
                }

			}
		}

		private void FinalMovementProcedure(GameTime gameTime)
        {
			if (PerformingFinalMovementProcedure)
            {
				bool nearDoor = Vector2.Distance(GetPosition(), NamedPositions.ChickenCoopDoor) < 2;
				if (nearDoor)
                {
					TargetPosition = NamedPositions.ChickenCoopInside;
                }
				else if (Vector2.Distance(GetPosition(), TargetPosition) < 2 && !nearDoor)
                {
					// PerformingFinalMovementProcedure = false;
                }
			}
        }
	}
}

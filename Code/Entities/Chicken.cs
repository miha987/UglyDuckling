using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace UglyDuckling.Code.Entities
{
	class Chicken : Entity
	{
		private const double OFF_TRACK_CHANCE = 0.50;
		// Putting off-track deviation over 0.5 produces weird behaviour.
		private const double OFF_TRACK_DEVIATION = 0.40;

		private Random random = new Random();

		public float MoveSpeed { get; set; } = 4f; // default
		public Vector2 TargetPosition { get; set; }

		private Vector2? OffTrackDirection = null;
		private double OffTrackTime = 0;

		public Chicken() : base(Vector2.Zero)
		{
			SetTexture("player_idle");
			//Rotate(MathHelper.Pi);

			InitializeAnimations();
		}

		public void InitializeAnimations()
		{
			EnableAnimator(12, 1);

			Animation idleAnimation = new Animation("idle", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, 0.0833);

			this.AddAnimation(idleAnimation);

			this.SetAnimation("idle");
		}

		public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			// TODO: move towards target if not within error distance
			double errorDist = Vector2.Distance(GetPosition(), TargetPosition);
			if (errorDist > 2)
            {
				MoveTowardsTarget(gameTime);
            }
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

			moveTowards *= MoveSpeed;
			Move(moveTowards);
		}
	}
}

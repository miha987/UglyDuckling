using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace UglyDuckling.Code.Entities
{
	class Chicken : Entity
	{

		private float moveSpeed = 4f; // default
		public Vector2 TargetPosition { get; set; }

		public Chicken() : base(Vector2.Zero)
		{
			SetTexture("walk1_test");
			Rotate(MathHelper.Pi);
		}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

			// TODO: move towards target if not within error distance
			double errorDist = Vector2.Distance(GetPosition(), TargetPosition);
			if (errorDist > 2)
            {
				// move towards the target with at the moveSpeed rate
				Vector2 moveTowards = Vector2.Normalize(Vector2.Subtract(TargetPosition, GetPosition()));
				moveTowards *= moveSpeed;
				Move(moveTowards);
				// Debug.WriteLine("Target: " + TargetPosition);
            }
        }

		//
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class Player : Entity
	{

		private const float MOVE_SPEED = 4.5f;

		public Player(Vector2 position) : base(position)
		{
			SetTexture("chicken");

			InitializeAnimations();
		}

		public void InitializeAnimations()
		{
			EnableAnimator(18, 1);

			Animation idleAnimation = new Animation("idle", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, 0.0833, true, true);
			Animation downAnimation = new Animation("down", new int[] { 1, 2, 3, 13, 14, 15, 16, 17, 18, 10, 11, 12 }, 0.0416, true, true);

			this.AddAnimation(idleAnimation);
			this.AddAnimation(downAnimation);

			this.SetAnimation("idle");
		}

		public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
			//Debug.WriteLine("Player position: " + GetPosition());

			CheckKeyboard(gameTime);
        }

		//

		public void CheckKeyboard(GameTime gameTime)
		{
			KeyboardState keyState = Keyboard.GetState();
			KeyboardState prevKeyState = GameState.Instance.GetPrevKeyboardState();

			SetAnimation("idle");

			Vector2 movement = new Vector2(0, 0);
			if (keyState.IsKeyDown(Keys.S))
			{
				movement.Y += 1;
			}

			if (keyState.IsKeyDown(Keys.W))
			{
				movement.Y -= 1;
			}

			if (keyState.IsKeyDown(Keys.A))
			{
				this.horizontalFlip = false;
				movement.X -= 1;
			}

			if (keyState.IsKeyDown(Keys.D))
			{
				this.horizontalFlip = true;
				movement.X += 1;

			}

			if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
			{
				SetAnimation("down", true, true);
			}

			if (movement.X != 0 && movement.Y != 0)
            {
				movement.Normalize();
			}
			
			movement *= MOVE_SPEED;
			movement.Round();
			this.Move(movement);
		}
	}
}

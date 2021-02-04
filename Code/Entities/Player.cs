using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

		public Player(Vector2 position, double idleTimePerFrame=0.0833, double danceTimePerFrame=0.055556) : base(position)
		{
			SetTexture("brown_duck");
			SetZ(50);
			SetCheckCollisions(true);
			SetCollidable(true);

			InitializeAnimations(idleTimePerFrame, danceTimePerFrame);
		}

		public override void LoadContent()
		{
			base.LoadContent();
			// 115, 90 are magic numbers that seem to work well enough.
			SetBoundingRectangle(new Rectangle(115, 130, 90, 125));
		}

		public void InitializeAnimations(double idleTimePerFrame, double danceTimePerFrame)
		{
			EnableAnimator(6, 10);

			Animation blobAnimation = new Animation("blob", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, danceTimePerFrame, true, true);
			Animation idleAnimation = new Animation("idle", new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }, idleTimePerFrame, true, true);
			Animation leftAnimation = new Animation("left", new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 }, danceTimePerFrame, true, true);
			Animation rightAnimation = new Animation("right", new int[] { 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48 }, danceTimePerFrame, true, true);
			Animation upAnimation = new Animation("up", new int[] { 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 }, danceTimePerFrame, true, true);

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
			//Debug.WriteLine("Player position: " + GetPosition());

			CheckKeyboard(gameTime);
			UpdateZ();
		}

		public override void PostUpdate(GameTime gameTime)
		{
			base.PostUpdate(gameTime);

			CheckSeedPickups(gameTime);
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

		private void CheckKeyboard(GameTime gameTime)
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
				SetAnimation("blob", true, true);
			}

			if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
			{
				SetAnimation("up", true, true);
			}

			if (keyState.IsKeyDown(Keys.Left) && !prevKeyState.IsKeyDown(Keys.Left))
			{
				SetAnimation("left", true, true);
			}

			if (keyState.IsKeyDown(Keys.Right) && !prevKeyState.IsKeyDown(Keys.Right))
			{
				SetAnimation("right", true, true);
			}

			if (movement.X != 0 && movement.Y != 0)
			{
				movement.Normalize();
			}

			movement *= MOVE_SPEED;
			movement.Round();
			this.Move(movement);
		}

		private void CheckSeedPickups(GameTime gameTime)
		{
			List<Entity> seeds = GetMyCollisionsWithTag("seed");
			foreach (Seed s in seeds)
			{
				int seedsCount = GameState.Instance.GetVar<int>("seeds");
				GameState.Instance.SetVar<int>("seeds", seedsCount + 1);
				s.Remove();
			}
		}
    }
}

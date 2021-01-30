using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class BeatBar : Entity
	{
		public BeatBar(Vector2 position) : base(position)
		{
			SetTexture("bar");
			AddTag("bar");
			//SetCheckCollisions(true);
			SetCollidable(false);
			SetStatic(true);
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));
		}

		public void CheckKeyboard()
		{
			KeyboardState keyState = Keyboard.GetState();
			KeyboardState prevKeyState = GameState.Instance.GetPrevKeyboardState();

			List<Entity> beats = GetMyCollisionsWithTag("beat");

			List<Beat> beatList = GameState.Instance.GetVar<List<Beat>>("beat_list");


			if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
			{
				foreach (Beat beat in beatList)
				{
					if (beat.Type == 0 && beat.GetRectangle().Intersects(GetRectangle()))
					{
						beat.RemoveBeat();
						break;
					}
				}
			}

			if (keyState.IsKeyDown(Keys.Right) && !prevKeyState.IsKeyDown(Keys.Right))
			{
				foreach (Beat beat in beatList)
				{
					if (beat.Type == 1 && beat.GetRectangle().Intersects(GetRectangle()))
					{
						beat.RemoveBeat();
						break;
					}
				}
			}

			if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
			{
				foreach (Beat beat in beatList)
				{
					if (beat.Type == 2 && beat.GetRectangle().Intersects(GetRectangle()))
					{
						beat.RemoveBeat();
						break;
					}
				}
			}

			if (keyState.IsKeyDown(Keys.Left) && !prevKeyState.IsKeyDown(Keys.Left))
			{
				foreach (Beat beat in beatList)
				{
					if (beat.Type == 3 && beat.GetRectangle().Intersects(GetRectangle()))
					{
						beat.RemoveBeat();
						break;
					}
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			CheckKeyboard();
		}
	}
}

﻿using Microsoft.Xna.Framework;
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
			SetTexture("footprint");
			AddTag("bar");
			//SetCheckCollisions(true);
			SetCollidable(false);
			SetStatic(true);
			SetZ(95);
		}

		public override void Initialize()
		{
			base.Initialize();

			int bannerHeight = GameState.Instance.GetVar<int>("banner_height");

			SetOriginPoint(new Vector2(0, 0));
			SetScale((float)bannerHeight/2 / (float)GetHeight()); // 4850 = BANNER WIDTH
			SetPosition(new Vector2(GetProjectedPosition().X - GetWidth()/2, GameState.Instance.GetCurrentScene().GetWindowHeight() - (int)(GetHeight()*1.5)));
			SetBoundingRectangle(new Rectangle(0, 0, GetWidth(), GetHeight()));
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
					Rectangle bRect = beat.GetRectangle();
					Rectangle mRect = GetRectangle();
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

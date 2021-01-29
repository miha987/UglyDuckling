﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class Beat : Entity
	{
		private const int SIZE = 64;
		public double StartTime;
		public int Type; // 0 - UP, 1 - RIGHT, 2 - DOWN, 3 - LEFT

		public static double SPEED_RATE = 0.4;

		public Beat(Vector2 position, double startTime, int type) : base(position)
		{
			SetTexture("arrows");
			SetStatic(true);
			SetZ(103);
			AddTag("beat");
			

			StartTime = startTime;
			Type = type;
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));
			SetSize(SIZE, SIZE);
			SetBoundingRectangle(new Rectangle(0, 0, SIZE, SIZE));
		}

		public void RemoveBeat()
		{
			List<Beat> beatList = GameState.Instance.GetVar<List<Beat>>("beat_list");
			beatList.Remove(this);
			GameState.Instance.SetVar<List<Beat>>("beat_list", beatList);

			Remove();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (GetProjectedPosition().X < 0)
				RemoveBeat();

			float dX = -(float)SPEED_RATE * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			Move((int) dX, 0);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Rectangle rect = new Rectangle(Type * SIZE, 0, SIZE, SIZE);
			spriteBatch.Draw(GetTexture(), new Rectangle((int)GetProjectedPosition().X, (int)GetProjectedPosition().Y, rect.Width, rect.Height), rect, Color.White, GetRotationAngle(), GetOriginPoint(), SpriteEffects.None, 0);
		}
	}
}
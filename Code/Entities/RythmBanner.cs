using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class RythmBanner : Entity
	{
		public RythmBanner(Vector2 position) : base(position)
		{
			SetTexture("banner");
			SetStatic(true);
			SetCollidable(false);
			SetZ(90);
		}

		public override void Initialize()
		{
			base.Initialize();

			SetScale((float)GameState.Instance.GetCurrentScene().GetWindowWidth() / (float)GetWidth());
			SetOriginPoint(new Vector2(0, 0));
			SetPosition(new Vector2(0, GameState.Instance.GetCurrentScene().GetWindowHeight() - GetHeight()));
			GameState.Instance.SetVar<int>("banner_height", GetHeight());
		}
	}
}

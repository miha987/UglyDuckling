using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class GameOverScreen : Entity
	{
		public GameOverScreen(Vector2 position) : base(position)
		{
			SetTexture("game_over_screen");
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));
			SetScale((float)GameState.Instance.GetCurrentScene().GetWindowWidth() / (float)GetWidth());
		}
	}
}

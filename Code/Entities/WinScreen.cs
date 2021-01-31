using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class WinScreen : Entity
	{
		public WinScreen(Vector2 position) : base(position)
		{
			SetTexture("win_screen");
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));
			SetScale((float)GameState.Instance.GetCurrentScene().GetWindowWidth() / (float)GetWidth());
		}
	}
}

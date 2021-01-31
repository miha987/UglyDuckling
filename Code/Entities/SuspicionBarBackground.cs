using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class SuspicionBarBackground : Entity
	{
		SuspicionBar SuspicionBar;

		public SuspicionBarBackground(SuspicionBar suspicionBar) : base(new Vector2(0, 0))
		{
			SetColor(Color.White);
			SetSize(30, 300);
			SetCollidable(false);
			SetStatic(true);
			//SetTexture("suspicion_bar");
			SetZ(1000);

			SuspicionBar = suspicionBar;
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));

			SetPosition(new Vector2(SuspicionBar.GetProjectedPosition().X + SuspicionBar.GetWidth() * 0.05f, SuspicionBar.GetProjectedPosition().Y + SuspicionBar.GetHeight() * 0.61f));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Rectangle drawRect = new Rectangle((int)GetProjectedPosition().X, (int)GetProjectedPosition().Y, (int)(SuspicionBar.GetWidth() * 0.9f), (int)(SuspicionBar.GetHeight() * 0.20f));
			spriteBatch.Draw(GetTexture(), drawRect, drawRect, Color.White, GetRotationAngle(), GetOriginPoint(), SpriteEffects.None, 0);
		}
	}
}

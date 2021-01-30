using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class SuspicionBarOverlay : Entity
	{
		SuspicionBar SuspicionBar;

		public SuspicionBarOverlay(SuspicionBar suspicionBar) : base(new Vector2(0, 0))
		{
			SetColor(Color.White);
			SetSize(30, 300);
			SetCollidable(false);
			SetStatic(true);
			//SetTexture("suspicion_bar");
			SetZ(100);

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

			int suspicion = GameState.Instance.GetVar<int>("suspicion");
			int maxSuspicion = GameState.Instance.GetVar<int>("max_suspicion");

			double suspicionWidth = (suspicion * SuspicionBar.GetWidth() * 0.9f) / maxSuspicion;
			//int suspicionY = (int)GetProjectedPosition().Y + GetHeight() - (int)suspicionHeight;
			Rectangle drawSuspicionRect = new Rectangle((int)GetProjectedPosition().X, (int)GetProjectedPosition().Y, (int)suspicionWidth, (int)(SuspicionBar.GetHeight() * 0.20f));
			spriteBatch.Draw(GetTexture(), drawSuspicionRect, drawSuspicionRect, Color.Red, GetRotationAngle(), GetOriginPoint(), SpriteEffects.None, 0);
		}
	}
}

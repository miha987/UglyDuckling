using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class SuspicionBar : Entity
	{
		private SuspicionBarOverlay Overlay;

		public SuspicionBar(Vector2 position) : base(position)
		{
			SetColor(Color.White);
			//SetSize(30, 300);
			SetCollidable(false);
			SetStatic(true);
			SetTexture("suspicion_bar");
			SetZ(100);
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));

			SetScale(0.4f);

			int bannerHeight = GameState.Instance.GetVar<int>("banner_height");
			SetPosition(new Vector2(50, GameState.Instance.GetCurrentScene().GetWindowHeight() - bannerHeight - GetHeight() - 50));


			Overlay = new SuspicionBarOverlay(this);
			Overlay.LoadContent();
			Overlay.Initialize();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			Overlay.Draw(spriteBatch);

			//int suspicion = GameState.Instance.GetVar<int>("suspicion");
			//int maxSuspicion = GameState.Instance.GetVar<int>("max_suspicion");


			//Rectangle drawRect = new Rectangle((int)GetProjectedPosition().X, (int)GetProjectedPosition().Y, GetWidth(), GetHeight());
			//spriteBatch.Draw(GetTexture(), drawRect, drawRect, Color.Cyan, GetRotationAngle(), GetOriginPoint(), SpriteEffects.None, 0);


			//double suspicionHeight = (suspicion * GetHeight()) / maxSuspicion;
			//int suspicionY = (int)GetProjectedPosition().Y + GetHeight() - (int)suspicionHeight;
			//Rectangle drawSuspicionRect = new Rectangle((int)GetProjectedPosition().X, suspicionY, GetWidth(), (int)suspicionHeight);
			//spriteBatch.Draw(GetTexture(), drawSuspicionRect, drawSuspicionRect, Color.Red, GetRotationAngle(), GetOriginPoint(), SpriteEffects.None, 0);
		}
	}
}

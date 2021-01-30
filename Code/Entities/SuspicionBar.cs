using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class SuspicionBar : Entity
	{
		private SuspicionBarBackground Background;
		private SuspicionBarOverlay Overlay;

		public SuspicionBar(Vector2 position) : base(position)
		{
			SetColor(Color.White);
			//SetSize(30, 300);
			SetCollidable(false);
			SetStatic(true);
			SetTexture("suspicion_bar");
			SetZ(110);
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));

			SetScale(0.4f);

			int bannerHeight = GameState.Instance.GetVar<int>("banner_height");
			SetPosition(new Vector2(50, GameState.Instance.GetCurrentScene().GetWindowHeight() - bannerHeight - GetHeight() - 50));


			Background = new SuspicionBarBackground(this);
			Background.LoadContent();
			Background.Initialize();

			Overlay = new SuspicionBarOverlay(this);
			Overlay.LoadContent();
			Overlay.Initialize();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Background.Draw(spriteBatch);
			Overlay.Draw(spriteBatch);
			
			base.Draw(spriteBatch);
		}
	}
}

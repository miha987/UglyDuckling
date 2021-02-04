using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.HUDs
{
	class BeatHUD : Entity
	{
		BeatBar Bar;

		public BeatHUD() : base(new Vector2(0, 0))
		{
			SetZ(1009);
			SetPausable(false);
			SetStatic(true);
		}

		public override void Initialize()
		{
			base.Initialize();

			RythmBanner rythmBanner = new RythmBanner(new Vector2(0, 0));
			GameState.Instance.GetCurrentScene().AddEntity(rythmBanner);

			Bar = new BeatBar(new Vector2(GameState.Instance.GetCurrentScene().GetWindowWidth() / 2, GameState.Instance.GetVar<int>("BEAT_Y")));
			GameState.Instance.GetCurrentScene().AddEntity(Bar);
			//Bar.LoadContent();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			//Rectangle barRectangle = new Rectangle((int)Bar.GetPosition().X, (int)Bar.GetPosition().Y, Bar.GetWidth(), Bar.GetHeight());
			//spriteBatch.Draw(Bar.GetTexture(), barRectangle, barRectangle, Color.White, Bar.GetRotationAngle(), Bar.GetOriginPoint(), SpriteEffects.None, 0);
		}
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class SuspicionBar : Entity
	{
		public SuspicionBar(Vector2 position) : base(position)
		{
			SetColor(Color.White);
			SetSize(30, 300);
			SetCollidable(false);
			SetStatic(true);
			SetZ(100);
		}

		public override void Initialize()
		{
			base.Initialize();

			SetOriginPoint(new Vector2(0, 0));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			int suspicion = GameState.Instance.GetVar<int>("suspicion");
			int maxSuspicion = GameState.Instance.GetVar<int>("max_suspicion");


			Rectangle drawRect = new Rectangle((int)GetProjectedPosition().X, (int)GetProjectedPosition().Y, GetWidth(), GetHeight());
			spriteBatch.Draw(GetTexture(), drawRect, drawRect, Color.Cyan, GetRotationAngle(), GetOriginPoint(), SpriteEffects.None, 0);


			double suspicionHeight = (suspicion * GetHeight()) / maxSuspicion;
			int suspicionY = (int)GetProjectedPosition().Y + GetHeight() - (int)suspicionHeight;
			Rectangle drawSuspicionRect = new Rectangle((int)GetProjectedPosition().X, suspicionY, GetWidth(), (int)suspicionHeight);
			spriteBatch.Draw(GetTexture(), drawSuspicionRect, drawSuspicionRect, Color.Red, GetRotationAngle(), GetOriginPoint(), SpriteEffects.None, 0);
		}
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class BeatStatic : Entity
	{
		private const int SIZE = 128;
		
		public int Type; // 0 - UP, 1 - RIGHT, 2 - DOWN, 3 - LEFT

		public bool Visible { get; set; } = true;

		public BeatStatic(Vector2 position, int type) : base(position)
		{
			SetTexture("arrows");
			SetStatic(true);
			SetZ(1030);
			SetCollidable(false);
			
			Type = type;
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Visible)
            {
				Rectangle rect = new Rectangle(Type * SIZE, 0, SIZE, SIZE);
				spriteBatch.Draw(
					GetTexture(),
					new Rectangle((int)GetProjectedPosition().X, (int)GetProjectedPosition().Y, rect.Width, rect.Height),
					rect,
					Color.White,
					GetRotationAngle(),
					GetOriginPoint(),
					SpriteEffects.None, 0
				);
			}
		}
	}
}

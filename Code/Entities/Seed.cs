using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class Seed : Entity
	{
		public Seed(Vector2 position) : base(position)
		{
			SetTexture("seed1");
			SetZ(9);

			AddTag("seed");
			// SetCheckCollisions(true);
			SetCollidable(true);
		}
    }
}

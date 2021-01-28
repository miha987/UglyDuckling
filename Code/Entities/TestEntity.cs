using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.Entities
{
	class TestEntity : Entity
	{
		public TestEntity(Vector2 position) : base(position)
		{
			SetTexture("test");
		}
	}
}

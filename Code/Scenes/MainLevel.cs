using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.Scenes
{
	class MainLevel : Scene
	{
		public override void LoadTextures()
		{
			base.LoadTextures();

			AddTexture("TestImage", "test");
		}

		public override void Initialize()
		{
			base.Initialize();

			TestEntity entity = new TestEntity(new Vector2(200, 200));
			AddEntity(entity);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				GameState.Instance.GetGameReference().Exit();
		}
	}
}

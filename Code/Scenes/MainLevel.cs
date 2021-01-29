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
			AddTexture("walk1_test", "walk1_test");
		}

		public override void Initialize()
		{
			base.Initialize();

			TestEntity[] corners = new TestEntity[] {
				// center
				new TestEntity(new Vector2(    0,    0)),
				// corners
				new TestEntity(new Vector2(-1500, -800)),
				new TestEntity(new Vector2(+1500, +800)),
				new TestEntity(new Vector2(+1500, -800)),
				new TestEntity(new Vector2(-1500, +800)),
				// mid-points
				new TestEntity(new Vector2(    0, -800)),
				new TestEntity(new Vector2(    0, +800)),
				new TestEntity(new Vector2(+1500,    0)),
				new TestEntity(new Vector2(-1500,    0)),
			};
			foreach (TestEntity corner in corners)
            {
				corner.SetScale(0.12f);
				AddEntity(corner);
			}

			Player player = new Player(new Vector2(0, 0));
			AddEntity(player);

			Camera.FollowEntity(player);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				GameState.Instance.GetGameReference().Exit();
		}
	}
}

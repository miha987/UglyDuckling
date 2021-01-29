using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.ChickenControl;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.Scenes
{
	class MainLevel : Scene
	{
		private ChickenController ChickenController;

		public MainLevel()
        {
			this.ChickenController = new ChickenController(EntityManager, 4);
		}

		public override void LoadTextures()
		{
			base.LoadTextures();

			AddTexture("TestImage", "test");
			AddTexture("walk1_test", "walk1_test");
		}

		public override void Initialize()
		{
			base.Initialize();

			// just to help with positioning on an empty map
			for (int i = -1500; i < +1500; i+= 200)
            {
				for (int j = -800; j < +800; j += 200)
                {
					TestEntity e = new TestEntity(new Vector2(i, j));
					e.SetZ(-2);
					e.SetScale(0.12f);
					EntityManager.AddEntity(e);
                }

			}
			/*TestEntity[] corners = new TestEntity[] {
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
			}*/

			Player player = new Player(new Vector2(0, 0));
			AddEntity(player);

			Camera.FollowEntity(player);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			ChickenController.Update(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				GameState.Instance.GetGameReference().Exit();
		}
	}
}

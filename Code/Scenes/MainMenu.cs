using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.Scenes
{
	class MainMenu : Scene
	{
		public override void LoadTextures()
		{
			base.LoadTextures();

			AddTexture("bg_test_1", "background");
		}

		public override void Initialize()
		{
			base.Initialize();


			Background background = new Background(new Vector2(1000, 1000));
			// AddEntity(background);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				GameState.Instance.GetGameReference().Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.Enter))
				GameState.Instance.SetScene(new MainLevel());
		}
	}
}

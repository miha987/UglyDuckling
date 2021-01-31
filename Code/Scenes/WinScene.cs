using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.Scenes
{
	class WinScene : Scene
	{
		public override void LoadTextures()
		{
			base.LoadTextures();

			//AddTexture("bg_test_1", "background");
			AddTexture("win_screen", "win_screen");
		}

		public override void Initialize()
		{
			base.Initialize();

			//Background background = new Background(new Vector2(1000, 1000));

			WinScreen background = new WinScreen(new Vector2(0, 0));
			AddEntity(background);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			KeyboardState keyState = Keyboard.GetState();
			KeyboardState prevKeyState = GameState.Instance.GetPrevKeyboardState();

			if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !prevKeyState.IsKeyDown(Keys.Escape))
				GameState.Instance.SetScene(new MainMenu());

			if (keyState.IsKeyDown(Keys.Enter) && !prevKeyState.IsKeyDown(Keys.Enter))
				GameState.Instance.SetScene(new MainMenu());
		} 
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.Scenes
{
	class GameOverScene : Scene
	{
		public override void LoadTextures()
		{
			base.LoadTextures();

			AddTexture("game_over_screen", "game_over_screen");
		}

		public override void Initialize()
		{
			base.Initialize();


			GameOverScreen background = new GameOverScreen(new Vector2(0, 0));
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
				GameState.Instance.SetScene((Scene)Activator.CreateInstance(GameState.Instance.GetVar<Scene>("current_level").GetType()));
				//GameState.Instance.SetScene(new MainMenu());

			//if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			//	GameState.Instance.GetGameReference().Exit();

			//if (Keyboard.GetState().GetPressedKeys().Length > 0)
			//	GameState.Instance.SetScene(new MainMenu());
		}
	}
}

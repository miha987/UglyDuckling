using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.Entities;
using UglyDuckling.Code.HUDs;

namespace UglyDuckling.Code.Scenes
{
	class MainMenu : Scene
	{
		private Player Player;

		private bool drawCredits = false;

		private List<TextEntity> instructions = new List<TextEntity>();
		private List<TextEntity> credits = new List<TextEntity>();
		private List<BeatStatic> beats = new List<BeatStatic>();

		public override void LoadTextures()
		{
			base.LoadTextures();

			AddTexture("bg_test_2", "background");
			AddTexture("arrows3", "arrows");
			AddTexture("brown_duck_spritesheet_FINAL", "brown_duck");
		}

		public override void Initialize()
		{
			base.Initialize();

			GameState.Instance.SetVar<int>("background_height", 0); // SET IN BACKGROUND CLASS
			GameState.Instance.SetVar<int>("background_y", 0); // SET IN BACKGROUND CLASS
			Background background = new Background(new Vector2(1000, 500));
			AddEntity(background);

			int centerX = GetWindowWidth() / 2;

			beats.Add(new BeatStatic(new Vector2(centerX + 192 - 300, 500), 0));
			beats.Add(new BeatStatic(new Vector2(centerX + 192 - 100, 500), 1));
			beats.Add(new BeatStatic(new Vector2(centerX + 192 + 100, 500), 2));
			beats.Add(new BeatStatic(new Vector2(centerX + 192 + 300, 500), 3));

			instructions.Add(new TextEntity(new Vector2(centerX - 210, GetWindowHeight() / 2 + 200), "Use WASD to move around."));
			instructions.Add(new TextEntity(new Vector2(centerX - 210, GetWindowHeight() / 2 + 240), "Use Arrow keys to dance!"));
			instructions.Add(new TextEntity(new Vector2(centerX - 260, GetWindowHeight() / 2 + 310), "Press ENTER to start the game"));
			instructions.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 + 370), "(Hold SHIFT for credits)"));

			credits.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 +   0), "A game by:"));
			credits.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 +  50), "Ana AF \"Deka\""));
			credits.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 + 150), "Martin Cebular \"Silver\""));
			credits.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 + 100), "Miha Zadravec"));
			credits.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 + 200), "Tanita Fabjan Demsar \"Firey\""));
			credits.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 + 260), "Music by:"));
			credits.Add(new TextEntity(new Vector2(centerX - 200, GetWindowHeight() / 2 + 310), "Maja Salamon \"AQUAGON\""));

			instructions.ForEach((o) => AddEntity(o));
			credits.ForEach((o) => AddEntity(o));
			beats.ForEach((o) => AddEntity(o));

			Player = new Player(new Vector2(GetWindowWidth() / 2, GetWindowHeight() / 2 - 200));
			AddEntity(Player);

		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			KeyboardState keyState = Keyboard.GetState();
			KeyboardState prevKeyState = GameState.Instance.GetPrevKeyboardState();

			drawCredits = Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift);

			instructions.ForEach((o) => o.Visible = !drawCredits);
			beats.ForEach((o) => o.Visible = !drawCredits);
			credits.ForEach((o) => o.Visible = drawCredits);

			if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !prevKeyState.IsKeyDown(Keys.Escape))
				GameState.Instance.GetGameReference().Exit();

			if (keyState.IsKeyDown(Keys.Enter) && !prevKeyState.IsKeyDown(Keys.Enter))
				GameState.Instance.SetScene(new MainLevel());
		}
	}
}

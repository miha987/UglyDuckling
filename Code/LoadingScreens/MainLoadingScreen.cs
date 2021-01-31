using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.Engine;
using UglyDuckling.Code.HUDs;

namespace UglyDuckling.Code.LoadingScreens
{
	class MainLoadingScreen : LoadingScreen
	{
		private TextEntity LoadingText;
		private Scene Scene;

		public MainLoadingScreen(Scene scene) : base(scene)
		{
			Scene = scene;
		}

		public override void LoadTextures()
		{
			base.LoadTextures();

		}

		public override void Initialize()
		{
			base.Initialize();

			LoadingText = new TextEntity(new Vector2(Scene.GetWindowWidth()/2 - 80, Scene.GetWindowHeight()/2 - 10), "Loading...");
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			LoadingText.Draw(spriteBatch);
		}
	}
}

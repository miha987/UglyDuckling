using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
			AddTexture("bg_test_1", "background");
		}

		public override void Initialize()
		{
			base.Initialize();

			Player player = new Player(new Vector2(-444, -344));
			AddEntity(player);

			AddEntity(new Background(new Vector2(0, 0)));

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

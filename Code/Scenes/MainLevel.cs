using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using UglyDuckling.Code.ChickenControl;
using UglyDuckling.Code.Entities;
using UglyDuckling.Code.HUDs;
using UglyDuckling.Code.Mechanics;

namespace UglyDuckling.Code.Scenes
{
	class MainLevel : Scene
	{
		private ChickenController ChickenController;
		private SeedGenerator SeedGenerator;

		public MainLevel()
		{
			GameState.Instance.SetVar<int>("suspicion", 0);
			GameState.Instance.SetVar<int>("max_suspicion", 100);

			this.ChickenController = new ChickenController(EntityManager, 5);
			this.SeedGenerator = new SeedGenerator(EntityManager);
			SeedGenerator.GenerateSeeds();
		}

		public override void LoadTextures()
		{
			base.LoadTextures();

			AddTexture("TestImage", "test");
			AddTexture("bg_test_2", "background");
			AddTexture("arrows1", "arrows");
			AddTexture("bar_placeholder", "bar");
			//AddTexture("temporary_chicken", "chicken");
			AddTexture("brown_duck_spritesheet_FINAL", "brown_duck");
			AddTexture("yellow_duck_spritesheet", "yellow_duck");
			//AddTexture("idle_animation_3", "player_idle");
			AddTexture("banner_2", "banner");
			AddTexture("footprint_1", "footprint");
			AddTexture("suspicion_bar_1_transparent", "suspicion_bar");
			AddTexture("seed1", "seed1");
		}

		public override void LoadSounds()
		{
			base.LoadSounds();

			AddSong("song1_wip4", "main_theme");
			//AddSong("song1_wip3", "main_theme");
			//AddSong("song2_wip1", "main_theme");
		}

		public override void Initialize()
		{
			base.Initialize();

			AddEntity(new SeedCountHud());

			Player player = new Player(NamedPositions.ChickenCoopDoor);
			AddEntity(player);

			GameState.Instance.SetVar<int>("background_height", 0); // SET IN BACKGROUND CLASS
			GameState.Instance.SetVar<int>("background_y", 0); // SET IN BACKGROUND CLASS
			AddEntity(new Background(new Vector2(0, 0)));
			GameState.Instance.SetVar<int>("BEAT_Y", GetWindowHeight() - 150);

			BeatHUD beatHUD = new BeatHUD();
			AddEntity(beatHUD);

			SuspicionBar suspicionBar = new SuspicionBar(new Vector2(GetWindowWidth() - 100, 200));
			AddEntity(suspicionBar);

			Camera.FollowEntity(player);

			GameState.Instance.SetVar<Player>("player", player);
			GameState.Instance.SetVar<int>("seeds", 0);

			GameState.Instance.SetVar<bool>("is_beat", false);
			GameState.Instance.SetVar<Beat>("current_beat", null);
			GameState.Instance.SetVar<List<Beat>>("beat_list", new List<Beat>());

			GameState.Instance.SetVar<int>("max_distance_to_chicken", 600);
			GameState.Instance.SetVar<float>("distance_to_chicken_percent", 1f);

			// BANNER HARDCODED STUFF
			float bannerScaleFactor = (float)GameState.Instance.GetCurrentScene().GetWindowWidth() / 4850;
			GameState.Instance.SetVar<int>("banner_height", (int)(bannerScaleFactor * 590));


			BeatManager beatManager = new BeatManager(GetSoundManager(), "main_theme");
			beatManager.PlaySong();
			AddSpawnController(beatManager);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			ChickenController.Update(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				GameState.Instance.GetGameReference().Exit();

			int suspicion = GameState.Instance.GetVar<int>("suspicion");
			int maxSuspicion = GameState.Instance.GetVar<int>("max_suspicion");

			if (suspicion >= maxSuspicion)
			{
				StopSong();
				GameState.Instance.SetScene(new GameOverScene());
			}

		}
	}
}

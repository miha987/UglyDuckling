using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.ChickenControl;
using UglyDuckling.Code.Entities;
using UglyDuckling.Code.HUDs;
using UglyDuckling.Code.Mechanics;

namespace UglyDuckling.Code.Scenes
{
	class MainLevel2 : Scene
	{
		private ChickenController ChickenController;
		private SeedGenerator SeedGenerator;

		public MainLevel2()
		{
			GameState.Instance.SetVar<int>("suspicion", 0);
			GameState.Instance.SetVar<int>("max_suspicion", 100);

			GameState.Instance.SetVar<bool>("player_won", false);

			ChickenController = new ChickenController(EntityManager, 5, 0.0833, 0.050000);
			SeedGenerator = new SeedGenerator(EntityManager);
			SeedGenerator.GenerateSeeds();
		}

		public override void LoadTextures()
		{
			base.LoadTextures();

			AddTexture("TestImage", "test");
			AddTexture("bg_wip_3", "background");
			AddTexture("fg_test_1", "foreground");
			AddTexture("arrows3", "arrows");
			AddTexture("bar_placeholder", "bar");
			//AddTexture("temporary_chicken", "chicken");
			AddTexture("brown_duck_spritesheet_FINAL", "brown_duck");
			AddTexture("yellow_duck_spritesheet", "yellow_duck");
			//AddTexture("idle_animation_3", "player_idle");
			AddTexture("banner_2", "banner");
			AddTexture("footprint_1", "footprint");
			AddTexture("suspicion_bar_1_transparent", "suspicion_bar");
			AddTexture("seme", "seed1");
		}

		public override void LoadSounds()
		{
			base.LoadSounds();

			AddSong("song2_wip1", "main_theme");
			//AddSong("song1_wip3", "main_theme");
			//AddSong("song2_wip1", "main_theme");
		}

		public override void Initialize()
		{
			base.Initialize();

			AddEntity(new SeedCountHud());
			AddEntity(new MainHUD());

			Player player = new Player(NamedPositions.ChickenCoopDoor, 0.0833, 0.050000);
			AddEntity(player);

			GameState.Instance.SetVar<bool>("immediately_finish", false);

			GameState.Instance.SetVar<int>("background_height", 0); // SET IN BACKGROUND CLASS
			GameState.Instance.SetVar<int>("background_y", 0); // SET IN BACKGROUND CLASS
			AddEntity(new Background(new Vector2(0, 0)));
			AddEntity(new Foreground(new Vector2(0, 0)));
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

			GameState.Instance.SetVar<string>("current_level_name", "Level 3");
			GameState.Instance.SetVar<Scene>("current_level", this);

			// BANNER HARDCODED STUFF
			float bannerScaleFactor = (float)GameState.Instance.GetCurrentScene().GetWindowWidth() / 4850;
			GameState.Instance.SetVar<int>("banner_height", (int)(bannerScaleFactor * 590));


			BeatManager beatManager = new BeatManager(GetSoundManager(), "main_theme", 420, 88000, 100000);
			beatManager.PlaySong();
			AddSpawnController(beatManager);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			ChickenController.Update(gameTime);

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				GameState.Instance.SetScene(new MainMenu());

			int suspicion = GameState.Instance.GetVar<int>("suspicion");
			int maxSuspicion = GameState.Instance.GetVar<int>("max_suspicion");

			if (suspicion >= maxSuspicion)
			{
				StopSong();
				GameState.Instance.SetScene(new GameOverScene());
			}
			
			if (GameState.Instance.GetVar<bool>("player_won"))
			{
				GameState.Instance.SetScene(new WinScene());
			}
		}
	}
}

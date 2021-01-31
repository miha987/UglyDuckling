using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UglyDuckling.Code.Engine;
using UglyDuckling.Code.Entities;
using UglyDuckling.Code.Scenes;

namespace UglyDuckling.Code.Mechanics
{
	class BeatManager : SpawnController
	{

		private SoundManager SoundManager;
		private string BeatSongName;
		private int BeatStep;

		private double TimePassedOffset;
		private double TimePassedTotal;
		private bool Playing;

		private Random BeatRandom;

		private double LastBeatTime; // local, needed for positioning beats
		private double LastStartTime; // global, needed for detection if beat is current

		private const double BEAT_TIME_THRESHOLD = 100;
		private const double END_BEAT_TIME = 110000;
		private const double WIN_TIME = 122000;

		private List<Beat> BeatList;

		private bool FirstBeat;


		public BeatManager(SoundManager soundManager, string beatSongName, int beatStep=500)
		{
			SoundManager = soundManager;
			BeatSongName = beatSongName;
			BeatStep = beatStep;

			TimePassedOffset = 0;

			LastBeatTime = 0;
			LastStartTime = 0;
			//LastBeatTime = 0;

			BeatRandom = new Random();

			BeatList = new List<Beat>();

			MediaPlayer.Volume = 0.7f;

			FirstBeat = true;

			Initialize();
		}

		public void Initialize()
		{
			List<Beat> beatList = new List<Beat>();

			for (int i = 0; i < 6; i++)
			{
				Beat beat = NewBeat(false);
				beatList.Add(beat);
				BeatList.Add(beat);
			}

			GameState.Instance.SetVar<List<Beat>>("beat_list", beatList);
		}

		public Beat NewBeat(bool visible=true)
		{
			//Trace.WriteLine("BEAT!!");

			int ARROW_OFFSET = 124;

			int type = BeatRandom.Next(0, 4);
			//double time = LastBeatTime + BeatStep;
			double startTime = LastStartTime + BeatStep;
			//float beatX = (float)((time - ARROW_OFFSET) * Beat.SPEED_RATE) + GameState.Instance.GetCurrentScene().GetWindowWidth() / 2;
			float beatX = (float)((BeatStep * (BeatList.Count + 1) - ARROW_OFFSET - BeatStep/2) * Beat.SPEED_RATE) + GameState.Instance.GetCurrentScene().GetWindowWidth() / 2;

			Beat beat = new Beat(new Vector2(beatX, GameState.Instance.GetVar<int>("BEAT_Y")), startTime, type, visible);
			//beat.LoadContent();
			GameState.Instance.GetCurrentScene().AddEntity(beat);

			//if (updateLastBeatTime)
			//	LastBeatTime += BeatStep;

			LastStartTime += BeatStep;

			return beat;
		}

		public void PlaySong()
		{
			SoundManager.PlaySong(BeatSongName);
			Playing = true;
		}

		//public Beat GetCurrentBeat()
		//{
		//	//GameState.Instance.SetVar<Beat>("current_beat", beat);
		//	List<Beat> beatList =  GameState.Instance.GetVar<List<Beat>>("beat_list");

		//	foreach (Beat beat in beatList)
		//	{
		//		if (beat.StartTime > TimePassedTotal - BEAT_TIME_THRESHOLD && beat.StartTime < TimePassedTotal + BEAT_TIME_THRESHOLD)
		//			return beat;
		//	}

		//	return null;
		//}

		

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			//Trace.WriteLine("tuki");
			TimePassedOffset += gameTime.ElapsedGameTime.TotalMilliseconds;
			TimePassedTotal += gameTime.ElapsedGameTime.TotalMilliseconds;

			if (Playing)
			{
				GameState.Instance.SetVar<bool>("is_beat", false);

				List<Beat> beatList = GameState.Instance.GetVar<List<Beat>>("beat_list");

				//if (TimePassedOffset > BeatStep)
				if (beatList.Count > 0)
				{
					double startTime = BeatList[0].StartTime;
					if (TimePassedTotal >= BeatList[0].StartTime)
					{
						TimePassedOffset = TimePassedOffset % BeatStep;

						//Beat beat = beatList[0];
						Beat beat = BeatList[0];
						GameState.Instance.SetVar<bool>("is_beat", true);
						GameState.Instance.SetVar<Beat>("current_beat", beat);
						//Trace.WriteLine(beat.Type);

						//if (beat.Type == 3)
						//{
						//	int a = 1;
						//}

						// DEBUG!!!!!!!!!!!
						//beat.RemoveBeat();

						//beatList.RemoveAt(0);
						BeatList.RemoveAt(0);


						if (TimePassedTotal < END_BEAT_TIME)
						{
							Beat newBeat = NewBeat();
							beatList.Add(newBeat);
							BeatList.Add(newBeat);
						}


						GameState.Instance.SetVar<List<Beat>>("beat_list", beatList);
					}
				}
			}

			if (TimePassedTotal > WIN_TIME)
			{
				SoundManager.StopSong();
				GameState.Instance.SetScene(new WinScene());
			}
		}
	}
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.ChickenControl
{
    /// <summary>
    /// Controls the NPC chicken behaviour.
    /// </summary>
    class ChickenController
    {
        private readonly Random random = new Random();
        /// <summary>
        /// Reference to the EntityManager chickens will be added to
        /// </summary>
        private readonly EntityManager EntityManager;
        private readonly CheckpointGenerator CheckpointGenerator;

        private double TimeElapsedTotal = 0;
        private double TimeElapsedAtCurrentCheckpoint = 0;
        private bool IsPaused { get; set; } = false;

        private readonly List<Chicken> Chickens = new List<Chicken>();

        private readonly List<Checkpoint> Checkpoints = new List<Checkpoint>();
        private int CurrentCheckpointIndex = 0;
        

        public ChickenController(EntityManager entityManager, int chickenCount)
        {
            this.EntityManager = entityManager;
            this.CheckpointGenerator = new CheckpointGenerator();

            for (int i = 0; i < chickenCount; i++)
            {
                Chicken c = new Chicken();
                c.SetZ(10 + i);
                Chickens.Add(c);
                EntityManager.AddEntity(c);
            }

            Reset();
        }

        /// <summary>
        /// Resets the Controller:
        /// </summary>
        private void Reset()
        {
            ToTheInitialProcedure();
            Checkpoints.AddRange(CheckpointGenerator.Generate());
            CurrentCheckpointIndex = 0;
        }

        private bool AllChickensOnCheckpoint()
        {
            Checkpoint currentCheckpoint = Checkpoints[CurrentCheckpointIndex];
            
            foreach (Chicken c in Chickens)
            {
                double dist = Vector2.Distance(c.GetPosition(), currentCheckpoint.Position);
                if (dist > CheckpointGenerator.MAX_ERROR_DIST)
                {
                    return false;
                }
            }
            return true;
        }

        private bool AllChickensNotMoving()
        {
            foreach (Chicken c in Chickens)
            {
                if (c.IsMoving())
                {
                    return false;
                }
            }
            return true;
        }

        private bool AnyChickenInInitialMovementProcedure()
        {
            foreach (Chicken c in Chickens)
            {
                if (c.IsPerformingInitialMovementProcedure())
                {
                    return true;
                }
            }

            return false;
        }

        private bool AnyChickenInFinalMovementProcedure()
        {
            foreach (Chicken c in Chickens)
            {
                if (c.IsPerformingFinalMovementProcedure())
                {
                    return true;
                }
            }

            return false;
        }

        private void ToTheNextCheckpoint()
        {
            if (CurrentCheckpointIndex + 1 >= Checkpoints.Count)
            {
                if (!AnyChickenInFinalMovementProcedure())
                {
                    ToTheFinalProcedure();
                }
                // There is no next checkpoint, don't do anything else.
                return;
            }
            
            Debug.WriteLine("To the next checkpoint!");
            CurrentCheckpointIndex++;

            TimeElapsedAtCurrentCheckpoint = 0;
            
            // Let the chickens know about the new target!
            foreach(Chicken c in Chickens)
            {
                c.TargetPosition = CheckpointGenerator.RandomOffset(Checkpoints[CurrentCheckpointIndex].Position);
            }
        }

        private void ToTheInitialProcedure()
        {
            // start procedure to move chickens out of coop.
            int startingDelay = 3;
            for (int i = 0; i < Chickens.Count; i++)
            {
                Chickens[i].PerformInitialMovementProcedure(startingDelay + i * (random.NextDouble() / 2));
            }
        }

        private void ToTheFinalProcedure()
        {

            // start procedure to move chickens to coop.
            int startingDelay = 2;
            for (int i = 0; i < Chickens.Count; i++)
            {
                Debug.WriteLine("Performing final procedure...");
                Chickens[i].PerformFinalMovementProcedure(startingDelay + i * (0.5 + random.NextDouble()));
            }
        }

        private void CheckDistanceToPlayer()
		{
            if (!GameState.Instance.HasVar("player"))
                return;

            Player player = GameState.Instance.GetVar<Player>("player");

            float minDist = 9999999999;

            foreach (Chicken c in Chickens)
			{
                float distToPlayer = (c.GetProjectedPosition() - player.GetProjectedPosition()).Length();
                
                if (distToPlayer < minDist)
                    minDist = distToPlayer;
			}


            int maxDistToChicken = GameState.Instance.GetVar<int>("max_distance_to_chicken");

            if (minDist >= maxDistToChicken)
			{
                GameState.Instance.SetVar<float>("distance_to_chicken_percent", 0f);
			} else
			{
                float distToChickenPercent = 1 - (minDist /(float)maxDistToChicken);
                GameState.Instance.SetVar<float>("distance_to_chicken_percent", distToChickenPercent);
			}
        }

        /// <summary>
        /// Should be called in the Update method of the scene to work!
        /// </summary>
        /// <param name="gameTime"></param>
        internal void Update(GameTime gameTime)
        {
            if (IsPaused)
            {
                // Do not progress the logic of the controller if paused.
                return;
            }

            TimeElapsedTotal += gameTime.ElapsedGameTime.TotalSeconds;

            if (AnyChickenInInitialMovementProcedure())
            {
                // Debug.WriteLine("Initial procedure...");
            } 
            else
            {
                if (AllChickensOnCheckpoint() && AllChickensNotMoving())
                {
                    TimeElapsedAtCurrentCheckpoint += gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (TimeElapsedAtCurrentCheckpoint > Checkpoints[CurrentCheckpointIndex].WaitTime)
                {
                    ToTheNextCheckpoint();
                }
            }

            CheckDistanceToPlayer();
        }

    }
}

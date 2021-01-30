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
            int initialDelay = 3;
            for (int i = 0; i < Chickens.Count; i++)
            {
                Chickens[i].PerformInitialMovementProcedure(initialDelay + i * (1 + new Random().NextDouble()));
            }

            CheckpointGenerator.Generate();

            // prepare new points
            Checkpoints.AddRange(new Checkpoint[] {
                // in front of coop
                new Checkpoint(NamedPositions.ChickenCoopOutside, 5),
                // sandbox
                new Checkpoint(NamedPositions.Sandbox, 5),
                // near the water
                new Checkpoint(NamedPositions.Lake, 5),
                // bottom-left corner grass
                new Checkpoint(NamedPositions.Grass, 5),
                // back to 1st point. Wait time doesnt matter as theres no "next" to go to.
                new Checkpoint(NamedPositions.ChickenCoopOutside, 0),
            });

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

        private void ToTheNextCheckpoint()
        {
            if (CurrentCheckpointIndex + 1 >= Checkpoints.Count)
            {
                // There is no next checkpoint
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
                if (AllChickensOnCheckpoint())
                {
                    TimeElapsedAtCurrentCheckpoint += gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (TimeElapsedAtCurrentCheckpoint > Checkpoints[CurrentCheckpointIndex].WaitTime)
                {
                    ToTheNextCheckpoint();
                }
            }
        }

    }
}

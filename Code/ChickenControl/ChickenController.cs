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

        private double TimeElapsedTotal = 0;
        private double TimeElapsedAtCurrentCheckpoint = 0;
        private bool IsPaused { get; set; } = false;

        private readonly List<Chicken> Chickens = new List<Chicken>();

        private readonly List<Checkpoint> Checkpoints = new List<Checkpoint>();
        private int CurrentCheckpointIndex = 0;
        private const int MAX_ERROR_DIST = 250;

        public ChickenController(EntityManager entityManager, int chickenCount)
        {
            this.EntityManager = entityManager;

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
            // prepare new points
            Checkpoints.AddRange(new Checkpoint[] {
                // in front of coop
                new Checkpoint(new Vector2(-320, -110), 5),
                // sandbox
                new Checkpoint(new Vector2(+270, -270), 5),
                // near the water
                new Checkpoint(new Vector2(140, +340), 5),
                // bottom-left corner grass
                new Checkpoint(new Vector2(-510, +360), 5),
                // back to 1st point. Wait time doesnt matter as theres no "next" to go to.
                new Checkpoint(new Vector2(-320, -110), 0),
            });

            // reset chicken positions by teleporting them to 1st point
            foreach (Chicken c in Chickens)
            {
                c.SetPosition(Checkpoints[0].Position);
            }

            // index to -1 as ToTheNextCheckpoint immediately increments it
            CurrentCheckpointIndex = -1;
            ToTheNextCheckpoint();
        }

        private bool AllChickensOnCheckpoint()
        {
            Checkpoint currentCheckpoint = Checkpoints[CurrentCheckpointIndex];
            
            foreach (Chicken c in Chickens)
            {
                double dist = Vector2.Distance(c.GetPosition(), currentCheckpoint.Position);
                if (dist > MAX_ERROR_DIST)
                {
                    return false;
                }
            }
            return true;
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
                Vector2 targetPos = Checkpoints[CurrentCheckpointIndex].Position;

                // testing randomness
                Random r = new Random();
                int randomOffset = (int) Math.Floor((double) (MAX_ERROR_DIST * 7 / 10));
                targetPos.X += r.Next(-randomOffset, +randomOffset);
                targetPos.Y += r.Next(-randomOffset, +randomOffset);

                // Debug.WriteLine("New target is " + targetPos);
                c.TargetPosition = targetPos;
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

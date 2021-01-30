using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.ChickenControl
{
    class CheckpointGenerator
    {
        public static int MAX_ERROR_DIST { get; } = 250;

        private static readonly Random random = new Random();

        public CheckpointGenerator()
        {
            
        }

        public List<Checkpoint> Generate()
        {
            List<Checkpoint> result = new List<Checkpoint>();
            // 1st point
            result.Add(new Checkpoint(NamedPositions.ChickenCoopOutside, 5));

            // around the sandbox
            result.AddRange(GenerateSub(NamedPositions.Sandbox, 400, 5, 5));

            // near the lake
            result.AddRange(GenerateSub(NamedPositions.Lake, 100, 3, 1));

            // in the grass
            result.AddRange(GenerateSub(NamedPositions.Grass, 1200, 10, 5));

            // back to 1st point. After wait time, final movement procedure begins (chickens going back to the coop)
            result.Add(new Checkpoint(NamedPositions.ChickenCoopOutside, 3));

            return result;
        }

        /// <summary>
        /// Generates given amount of checkpoints randomly around the center position.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<Checkpoint> GenerateSub(Vector2 center, int distance, int count, int totalWaitTime)
        {
            List<Checkpoint> result = new List<Checkpoint>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Checkpoint(RandomOffset(center, distance), totalWaitTime / count));
            }
            return result;
        }

        public static Vector2 RandomOffset(Vector2 position)
        {
            return RandomOffset(position, MAX_ERROR_DIST);
        }

        public static Vector2 RandomOffset(Vector2 position, int distance)
        {
            int randomOffset = (int)Math.Floor((double)(distance * 7 / 10));
            position.X += random.Next(-randomOffset, +randomOffset);
            position.Y += random.Next(-randomOffset, +randomOffset);
            return position;
        }
    }
}

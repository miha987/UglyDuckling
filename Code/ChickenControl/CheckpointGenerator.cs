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
            return null;
        }

        public static Vector2 RandomOffset(Vector2 position)
        {
            int randomOffset = (int)Math.Floor((double)(MAX_ERROR_DIST * 7 / 10));
            position.X += random.Next(-randomOffset, +randomOffset);
            position.Y += random.Next(-randomOffset, +randomOffset);
            return position;
        }
    }
}

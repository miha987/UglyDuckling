using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.ChickenControl
{
    class Checkpoint
    {
        public readonly Vector2 Position;
        public readonly int WaitTime; // in seconds
        public Checkpoint(Vector2 position, int waitTime)
        {
            this.Position = position;
            this.WaitTime = waitTime;
        }
    }
}

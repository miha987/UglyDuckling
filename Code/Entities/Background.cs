﻿using Microsoft.Xna.Framework;

namespace UglyDuckling.Code.Entities
{
    class Background : Entity
    {

        public Background(Vector2 position) : base(position)
        {
            SetTexture("background");
            SetScale(0.35f);
            SetZ(-20);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.HUDs
{
    class SeedCountHud : Entity
    {
        private SpriteFont font;

        public SeedCountHud() : base(new Vector2(0, 0))
        {
            font = GameState.Instance.GetContent().Load<SpriteFont>("Londrina");

            SetStatic(true);
            SetZ(1009);
        }

        public override void Initialize()
        {
            base.Initialize();

            int bannerHeight = GameState.Instance.GetVar<int>("banner_height");
            int x = GameState.Instance.GetCurrentScene().GetWindowWidth() - 220;
            int y = GameState.Instance.GetCurrentScene().GetWindowHeight() - bannerHeight - 50;
            SetPosition(new Vector2(x, y));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            int currentSeeds = GameState.Instance.GetVar<int>("seeds");
            int totalSeeds = GameState.Instance.GetVar<int>("totalSeeds");
            spriteBatch.DrawString(font, "Seeds: " + currentSeeds + "/" + totalSeeds + "", GetPosition(), Color.White);
        }
    }
}

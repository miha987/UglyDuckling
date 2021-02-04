using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.HUDs
{
	class MainHUD : Entity
	{
        private TextEntity CurrentLevelText;

        public MainHUD() : base(new Vector2(0, 0))
        {

            SetStatic(true);
            SetZ(1009);
        }

        public override void Initialize()
        {
            base.Initialize();

            int bannerHeight = GameState.Instance.GetVar<int>("banner_height");
            int x = GameState.Instance.GetCurrentScene().GetWindowWidth() - 260;
            int y = GameState.Instance.GetCurrentScene().GetWindowHeight() - bannerHeight - 160;
            
            string CurrentLevelName = GameState.Instance.GetVar<string>("current_level_name");
            
            CurrentLevelText = new TextEntity(new Vector2(x, y), CurrentLevelName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CurrentLevelText.Draw(spriteBatch);
        }
    }
}

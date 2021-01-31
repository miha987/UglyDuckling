using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace UglyDuckling.Code.HUDs
{
    class TextEntity : Entity
    {
        private SpriteFont font;
        private string text;
        public bool Visible { get; set; } = true;

        public TextEntity(Vector2 position, string text) : base(position)
        {
            this.text = text;
            font = GameState.Instance.GetContent().Load<SpriteFont>("Londrina");

            SetStatic(true);
            SetZ(1009);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Visible)
            {
                spriteBatch.DrawString(font, text, GetPosition(), Color.White);
            }
        }
    }
}

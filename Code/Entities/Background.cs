using Microsoft.Xna.Framework;

namespace UglyDuckling.Code.Entities
{
    class Background : Entity
    {

        public Background(Vector2 position) : base(position)
        {
            SetTexture("background");
            SetZ(-20);
        }

		public override void Initialize()
		{
			base.Initialize();

            SetScale(0.7f);
            GameState.Instance.SetVar<int>("background_height", GetHeight());
            GameState.Instance.SetVar<int>("background_y", (int)GetProjectedPosition().Y);
        }
	}
}

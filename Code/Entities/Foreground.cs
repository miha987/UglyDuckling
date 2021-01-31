using Microsoft.Xna.Framework;

namespace UglyDuckling.Code.Entities
{
    class Foreground : Entity
    {

        public Foreground(Vector2 position) : base(position)
        {
            SetTexture("foreground");
            SetZ(+1000);
        }

		public override void Initialize()
		{
			base.Initialize();

            SetScale(0.7f);
        }
	}
}

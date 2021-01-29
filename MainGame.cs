using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UglyDuckling.Code.Scenes;

namespace UglyDuckling
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //_graphics.PreferredBackBufferWidth = 1200; // GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = 675; // GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            
            _graphics.ApplyChanges();

            GameState.Instance.SetGameReference(this);

            GameState.Instance.SetGraphics(this._graphics);
            GameState.Instance.SetContent(this.Content);

            GameState.Instance.SetScene(new MainLevel());

            this.IsFixedTimeStep = true;
            this._graphics.SynchronizeWithVerticalRetrace = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            if (GameState.Instance.GetCurrentScene() != null)
            {
                GameState.Instance.GetCurrentScene().LoadContent();
            }
        }

        protected override void Update(GameTime gameTime)
        {

            if (GameState.Instance.GetCurrentScene() != null)
            {
                GameState.Instance.GetCurrentScene().Update(gameTime);
            }

            GameState.Instance.UpdatePrevKeyboardState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(163, 173, 113));

            _spriteBatch.Begin();

            if (GameState.Instance.GetCurrentScene() != null)
            {
                GameState.Instance.GetCurrentScene().Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameCubeTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BasicEffect _basicEffect;


        private VertexPositionNormalTexture[] _cubeVertices;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _basicEffect = new BasicEffect(GraphicsDevice)
            {
                AmbientLightColor = Vector3.One,
                LightingEnabled = true,
                DiffuseColor = Vector3.One,
                TextureEnabled = true,
                Texture = Content.Load<Texture2D>("square-star")
            };
            _cubeVertices = CubeFactory.Create();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _rotation += 1f;

            base.Update(gameTime);
        }

        private float _rotation = 0;
        private Vector3 _modelPosition = new Vector3(0,2,0);

        private readonly RasterizerState _rasterizerState = new RasterizerState
        {
            CullMode = CullMode.CullClockwiseFace
        };
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.RasterizerState = _rasterizerState;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _basicEffect.World = Matrix.CreateRotationY(MathHelper.ToRadians(_rotation))
                                 //*Matrix.CreateTranslation(_modelPosition)
                                 * Matrix.CreateRotationX(MathHelper.ToRadians(_rotation/4f));
                                 

            _basicEffect.View = Matrix.CreateTranslation(0f, 0f, -5f);

            _basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                GraphicsDevice.Viewport.AspectRatio,
                1f,
                100f);

            _basicEffect.EnableDefaultLighting();

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _cubeVertices, 0, 12);
            }

            base.Draw(gameTime);
        }
    }
}
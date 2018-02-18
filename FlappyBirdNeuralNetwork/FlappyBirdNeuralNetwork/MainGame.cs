using System.Collections.Generic;
using FlappyBirdNeuralNetwork.FlappyBird;
using FlappyBirdNeuralNetwork.InterfaceHandler;
using FlappyBirdNeuralNetwork.NeuralNetwork;
using FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdNeuralNetwork
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        private GraphicsDeviceManager _Graphics;
        private SpriteBatch _SpriteBatch;
        private GameController _Controller;
        private InterfaceController _InterfaceController;
        private bool _KeyDown;

        public MainGame()
        {
            _Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _Controller = new GameController(Content);
            _InterfaceController = new InterfaceController(Content, _Controller, this);


            //Neural Network Variables to Populate
            GlobalVariables._TrainingData = new TrainingData();        
            GlobalVariables._NetworkController = new NeuralNetworkController();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GlobalVariables._InGame)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && GlobalVariables._InGame && !_KeyDown)
                {
                    _Controller.JumpPressed();
                    _KeyDown = true;
                }

                if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                    _KeyDown = false;

                _Controller.Update();
                GlobalVariables._FrameCount++;

                if (GlobalVariables._FrameCount >= 10) //60 frames is equivilant to around 1 second in game
                {
                    if (GlobalVariables._Flapped == false) //every second, save whether there was a flap or not
                    {
                        GlobalVariables._NetworkController.SaveFlap(_Controller.GetHorizontalDistance(), _Controller.GetVerticalDistance(), 0);
                        GlobalVariables._FrameCount = 0;
                    }
                    else
                    {
                        GlobalVariables._Flapped = false;
                        GlobalVariables._FrameCount = 0;
                    }
                }
            }

            if (GlobalVariables._NeuralNetworkGame)
            {
                _Controller.Update();

                int horiDistance = _Controller.GetHorizontalDistance();
                int verDistance = _Controller.GetVerticalDistance();
                List<double> arr = new List<double> {horiDistance, verDistance};

                var shouldFlap = GlobalVariables._NetworkController.GetNetworkOutput(arr);

                if (shouldFlap[0] > 0.5)
                {
                    _Controller.JumpPressed();
                }
            }

            _InterfaceController.Update(Keyboard.GetState());

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _SpriteBatch.Begin();
            if (GlobalVariables._InGame || GlobalVariables._NeuralNetworkGame)
                _Controller.Draw(_SpriteBatch);

            _InterfaceController.Draw(_SpriteBatch);
            _SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

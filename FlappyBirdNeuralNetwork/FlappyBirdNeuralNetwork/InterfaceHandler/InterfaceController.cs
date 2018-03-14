using FlappyBirdNeuralNetwork.FlappyBird;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdNeuralNetwork.InterfaceHandler
{
    internal class InterfaceController
    {
        private readonly SpriteFont _BigText;
        private readonly SpriteFont _SmallText;
        private readonly GameController _MainGame;
        private readonly MainGame _Game;

        private readonly Texture2D _Background;

        private int _SelectedIndex;

        private bool _KeyPressed;

        internal InterfaceController(ContentManager manager, GameController main, MainGame game)
        {
            _BigText = manager.Load<SpriteFont>("MainText");
            _SmallText = manager.Load<SpriteFont>("OtherText");
            _MainGame = main;
            _Game = game;
            _Background = manager.Load<Texture2D>("FlappyBackground");
        }

        internal void Update(KeyboardState state)
        {
            if (GlobalVariables._InGame == false && GlobalVariables._NeuralNetworkGame == false)
            {
                if ((state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Up)) && _KeyPressed == false)
                {
                    _KeyPressed = true;
                    _SelectedIndex = state.IsKeyDown(Keys.Down) ? _SelectedIndex + 1 : _SelectedIndex - 1;

                    if (_SelectedIndex == 5)
                    {
                        _SelectedIndex = 0;
                    }
                    if (_SelectedIndex == -1)
                    {
                        _SelectedIndex = 4;
                    }
                }

                if (state.IsKeyDown(Keys.Space) && _KeyPressed == false)
                {
                    _KeyPressed = true;
                    if (_SelectedIndex == 0)
                    {
                        GlobalVariables._InGame = true;
                        _MainGame.ResetMap();
                    }
                    else if (_SelectedIndex == 1)
                    {
                        _Game.Exit();
                    }
                    else if (_SelectedIndex == 2)
                    {
                        switch (GlobalVariables._GapDifficulty)
                        {
                            case 0:
                                GlobalVariables._GapDifficulty = 1;
                                break;
                            case 1:
                                GlobalVariables._GapDifficulty = 2;
                                break;
                            case 2:
                                GlobalVariables._GapDifficulty = 0;
                                break;
                        }
                    }
                    else if (_SelectedIndex == 3)
                    {
                        switch (GlobalVariables._ObstacleDifficulty)
                        {
                            case 0:
                                GlobalVariables._ObstacleDifficulty = 1;
                                break;
                            case 1:
                                GlobalVariables._ObstacleDifficulty = 2;
                                break;
                            case 2:
                                GlobalVariables._ObstacleDifficulty = 0;
                                break;
                        }
                    }
                    else
                    {
                        _MainGame.ResetMap();
                        GlobalVariables._NetworkController.TrainNeuralNetwork();
                        GlobalVariables._NeuralNetworkGame = true;
                    }
                }

                if (state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.Down) && state.IsKeyUp(Keys.Space))
                {
                    _KeyPressed = false;
                }
            }
            else
            {
                if (!GlobalVariables._Dead) return;
                if (!state.IsKeyDown(Keys.Enter)) return;

                GlobalVariables._InGame = false;
                GlobalVariables._Dead = false;
                GlobalVariables._NeuralNetworkGame = false;
                GlobalVariables._Score = 0;
                _MainGame.ResetMap();
            }
        }

        internal void Draw(SpriteBatch main)
        {
            main.Draw(_Background, new Rectangle(0, 0, 800, 500), Color.White);
            if (GlobalVariables._InGame || GlobalVariables._NeuralNetworkGame)
            {
                //show score
                if (GlobalVariables._Dead)
                {
                    main.DrawString(_SmallText, "You are dead.", new Vector2(20, 200), Color.Red);
                    main.DrawString(_SmallText, "You scored: " + GlobalVariables._Score, new Vector2(20, 250), Color.Green);
                    main.DrawString(_SmallText, "Press [ENTER] to return to main menu.", new Vector2(20, 300), Color.Red);
                }
                else
                {
                    main.DrawString(_SmallText, "Score: " + GlobalVariables._Score, new Vector2(20, 20), Color.Yellow);
                }
            }
            else
            {
                //Show main menu
                string gapDif = "Gap Difficulty: " + (GlobalVariables._GapDifficulty == 0 ? "Easy" : GlobalVariables._GapDifficulty == 1 ? "Medium" : "Hard");
                string obsDif = "Obstacle Difficulty: " + (GlobalVariables._ObstacleDifficulty == 0 ? "Easy" : GlobalVariables._ObstacleDifficulty == 1 ? "Medium" : "Hard");

                main.DrawString(_BigText, "Flappy Bird: Neural Network", new Vector2(20, 0), Color.Red);

                main.DrawString(_SmallText, "Play Game", new Vector2(20, 100), _SelectedIndex == 0 ? Color.Yellow : Color.White);
                main.DrawString(_SmallText, "Neural Network Game", new Vector2(20, 300), _SelectedIndex == 4 ? Color.Yellow : Color.White);
                main.DrawString(_SmallText, "Exit Game", new Vector2(20, 150), _SelectedIndex == 1 ? Color.Yellow : Color.White);
                main.DrawString(_SmallText, gapDif, new Vector2(20, 200), _SelectedIndex == 2 ? Color.Yellow : Color.White);
                main.DrawString(_SmallText, obsDif, new Vector2(20, 250), _SelectedIndex == 3 ? Color.Yellow : Color.White);
            }
        }
    }
}

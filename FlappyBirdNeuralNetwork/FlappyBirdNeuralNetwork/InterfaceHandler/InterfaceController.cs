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

        private int _SelectedIndex;

        private bool _KeyPressed;

        internal InterfaceController(ContentManager manager, GameController main, MainGame game)
        {
            _BigText = manager.Load<SpriteFont>("MainText");
            _SmallText = manager.Load<SpriteFont>("OtherText");
            _MainGame = main;
            _Game = game;
        }

        internal void Update(KeyboardState state)
        {
            if (GlobalVariables._InGame == false)
            {
                if ((state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Up)) && _KeyPressed == false)
                {
                    _KeyPressed = true;
                    _SelectedIndex = _SelectedIndex == 0 ? 1 : 0;
                }

                if (state.IsKeyDown(Keys.Space) && _KeyPressed == false)
                {
                    if (_SelectedIndex == 0)
                    {
                        GlobalVariables._InGame = true;
                    }
                    else
                    {
                        _Game.Exit();
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
                GlobalVariables._Score = 0;
                _MainGame.ResetMap();
            }
        }

        internal void Draw(SpriteBatch main)
        {
            if (GlobalVariables._InGame)
            {
                //show score
                if (GlobalVariables._Dead)
                {
                    main.DrawString(_SmallText, "You are dead. Press [ENTER] to return to main menu.", new Vector2(20, 200), Color.Red);
                    main.DrawString(_SmallText, "You scored: " + GlobalVariables._Score, new Vector2(20, 250), Color.Green);
                }
                else
                {
                    main.DrawString(_SmallText, "Score: " + GlobalVariables._Score, new Vector2(20, 20), Color.Yellow);
                }
            }
            else
            {
                //Show main menu
                main.DrawString(_BigText, "Flappy Bird: Neural Network", new Vector2(20, 20), Color.Red);

                if (_SelectedIndex == 0)
                {
                    main.DrawString(_SmallText, "Play Game", new Vector2(20, 200), Color.Yellow);
                    main.DrawString(_SmallText, "Exit Game", new Vector2(20, 300), Color.White);
                }
                else
                {
                    main.DrawString(_SmallText, "Play Game", new Vector2(20, 200), Color.White);
                    main.DrawString(_SmallText, "Exit Game", new Vector2(20, 300), Color.Yellow);
                }
            }
        }
    }
}

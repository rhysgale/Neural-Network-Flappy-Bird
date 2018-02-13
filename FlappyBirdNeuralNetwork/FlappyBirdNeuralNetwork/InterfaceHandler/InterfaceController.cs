using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBirdNeuralNetwork.InterfaceHandler
{
    internal class InterfaceController
    {
        private SpriteFont _BigText;
        private SpriteFont _SmallText;

        private int _SelectedIndex = 0;

        private bool _KeyPressed;

        internal InterfaceController(ContentManager manager)
        {
            _BigText = manager.Load<SpriteFont>("MainText");
            _SmallText = manager.Load<SpriteFont>("OtherText");
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
                        //quit game
                    }
                }

                if (state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.Down) && state.IsKeyUp(Keys.Space))
                {
                    _KeyPressed = false;
                }
            }
            else
            {
                
            }
        }

        internal void Draw(SpriteBatch main)
        {
            if (GlobalVariables._InGame)
            {
                //show score
                main.DrawString(_SmallText, "Score: " + GlobalVariables._Score, new Vector2(100, 20), Color.Yellow);
            }
            else
            {
                //Show main menu
                main.DrawString(_BigText, "Flappy Bird: Neural Network", new Vector2(100, 20), Color.Red);

                if (_SelectedIndex == 0)
                {
                    main.DrawString(_SmallText, "Play Game", new Vector2(200, 200), Color.Yellow);
                    main.DrawString(_SmallText, "Exit Game", new Vector2(200, 300), Color.White);
                }
                else
                {
                    main.DrawString(_SmallText, "Play Game", new Vector2(200, 200), Color.White);
                    main.DrawString(_SmallText, "Exit Game", new Vector2(200, 300), Color.Yellow);
                }
            }
        }
    }
}

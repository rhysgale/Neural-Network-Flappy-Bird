using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdNeuralNetwork.Assets
{
    internal class NeuralNetworkDictionary
    {
        private Dictionary<TextureType, Texture2D> _TextureDitionary;

        internal NeuralNetworkDictionary(ContentManager manager)
        {
            _TextureDitionary = new Dictionary<TextureType, Texture2D>();
            LoadTextures(manager);
        }

        internal void LoadTextures(ContentManager manager)
        {
            _TextureDitionary.Add(TextureType.Bird, manager.Load<Texture2D>("Bird"));
            _TextureDitionary.Add(TextureType.TopPipe, manager.Load<Texture2D>("Top"));
            _TextureDitionary.Add(TextureType.BottomPipe, manager.Load<Texture2D>("Bottom"));
        }

        internal Texture2D GetTexture(TextureType type)
        {
            return _TextureDitionary[type];
        }
    }
}

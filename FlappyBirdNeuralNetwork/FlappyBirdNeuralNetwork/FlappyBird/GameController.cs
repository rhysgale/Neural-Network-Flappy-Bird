using System;
using System.Collections.Generic;
using System.Xml.Schema;
using FlappyBirdNeuralNetwork.Assets;
using FlappyBirdNeuralNetwork.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBirdNeuralNetwork.FlappyBird
{
    internal class GameController
    {
        private NeuralNetworkDictionary _TextureDictionary;
        private List<Asset> _AssetList;
        private PhysicsController _Physics;
        private Asset _Bird;
        private bool _Dead = false;

        internal GameController(ContentManager manager)
        {
            _Physics = new PhysicsController();
            _TextureDictionary = new NeuralNetworkDictionary(manager); //Loads textures
            _AssetList = new List<Asset>();
            _Bird = new Asset(50, 300, 50, 50,TextureType.Bird, true);

            _AssetList.Add(new Asset(800, -150, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(800, 400, 50, 250, TextureType.TopPipe, false));

            _AssetList.Add(new Asset(1200, -150, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(1200, 400, 50, 250, TextureType.TopPipe, false));

            _AssetList.Add(new Asset(1600, -150, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(1600, 400, 50, 250, TextureType.TopPipe, false));
        }

        internal void Update()
        {
            if (_Dead) return;

            _Physics.ApplyPhysics(_Bird);
            foreach (var asset in _AssetList)
            {
                _Physics.ApplyPhysics(asset);
                bool hit = _Physics.CheckCollision(_Bird, asset);

                if (hit)
                {
                    _Dead = true;
                }
            }

            PipeUpdate();
        }

        internal void Draw(SpriteBatch main)
        {
            if (_Dead)
                return;

            foreach (var asset in _AssetList)
            {
                main.Draw(_TextureDictionary.GetTexture(asset.GetTextureType()),
                        new Rectangle((int)asset.GetPosition().X, (int)asset.GetPosition().Y, (int)asset.GetSize().X, (int)asset.GetSize().Y),
                        Color.White);
            }

            main.Draw(_TextureDictionary.GetTexture(_Bird.GetTextureType()),
                        new Rectangle((int)_Bird.GetPosition().X, (int)_Bird.GetPosition().Y, (int)_Bird.GetSize().X, (int)_Bird.GetSize().Y),
                        Color.White);
        }

        internal void JumpPressed()
        {
            _Bird.UpdateVelocity(new Vector2(0, -50));
        }

        internal void PipeUpdate()
        {
            if (_AssetList[0].GetPosition().X < -50)
            {
                GlobalVariables._Score++;

                _AssetList.Remove(_AssetList[0]);
                _AssetList.Remove(_AssetList[0]);

                int xPos = (int)_AssetList[2].GetPosition().X + 400;

                _AssetList.Add(new Asset(xPos, -150, 50, 250, TextureType.BottomPipe, false));
                _AssetList.Add(new Asset(xPos, 400, 50, 250, TextureType.TopPipe, false));
            }
        }
    }
}

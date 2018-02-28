﻿using System;
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
        private NeuralNetworkDictionary _TextureDictionary; //Textures stored here
        private List<Asset> _AssetList;
        private PhysicsController _Physics;
        private Asset _Bird;
        private Random _RandomGen;

        internal GameController(ContentManager manager)
        {
            _Physics = new PhysicsController();
            _TextureDictionary = new NeuralNetworkDictionary(manager); //Loads textures
            _AssetList = new List<Asset>();
            _RandomGen = new Random();
            ResetMap();
        }

        internal void ResetMap()
        {
            _AssetList.Clear();

            _Bird = new Asset(50, 300, 50, 50, TextureType.Bird, true);

            int gap;

            if (GlobalVariables._GapDifficulty == 0)
            {
                gap = 300;
            }
            else if (GlobalVariables._GapDifficulty == 1)
            {
                gap = 250;
            }
            else
            {
                gap = 200;
            }

            int obsgap;

            switch (GlobalVariables._ObstacleDifficulty)
            {
                case 0:
                    obsgap = 400;
                    break;
                case 1:
                    obsgap = 300;
                    break;
                default:
                    obsgap = 200;
                    break;
            }

            int y = _RandomGen.Next(300, 500);

            _AssetList.Add(new Asset(800, y - 250 - gap, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(800, y, 50, 250, TextureType.TopPipe, false));

            y = _RandomGen.Next(300, 400);

            _AssetList.Add(new Asset(800 + obsgap, y - 250 - gap, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(800 + obsgap, y, 50, 250, TextureType.TopPipe, false));

            y = _RandomGen.Next(300, 400);

            _AssetList.Add(new Asset(800 + (obsgap*2), y - 250 - gap, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(800 + obsgap * 2, y, 50, 250, TextureType.TopPipe, false));

            y = _RandomGen.Next(300, 400);

            _AssetList.Add(new Asset(800 + obsgap * 3, y - 250 - gap, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(800 + obsgap * 3, y, 50, 250, TextureType.TopPipe, false));

            y = _RandomGen.Next(300, 400);

            _AssetList.Add(new Asset(800+obsgap * 4, y - 250 - gap, 50, 250, TextureType.BottomPipe, false));
            _AssetList.Add(new Asset(800 + obsgap * 4, y, 50, 250, TextureType.TopPipe, false));
        }

        internal void Update()
        {
            if (GlobalVariables._Dead) return;

            if (GlobalVariables._NeuralNetworkGame)
            {
                if (_AssetList[0].GetPosition().X > 300)
                {
                    if (_Bird.GetPosition().Y > 400)
                    {
                        JumpPressed();
                    }
                }
            }

            _Physics.ApplyPhysics(_Bird);
            foreach (var asset in _AssetList)
            {
                _Physics.ApplyPhysics(asset);
                bool hit = _Physics.CheckCollision(_Bird, asset);

                if (hit)
                {
                    GlobalVariables._Dead = true;
                }
            }

            if (_Bird.GetPosition().Y > 600 || _Bird.GetPosition().Y < -50)
                GlobalVariables._Dead = true;

            if (GlobalVariables._Dead)
            {
                 var stuff = GlobalVariables._TrainingData.GetTrainingData();
            }

            PipeUpdate();
        }

        internal void Draw(SpriteBatch main)
        {
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
            if (!GlobalVariables._NeuralNetworkGame)
                GlobalVariables._NetworkController.SaveFlap(GetHorizontalDistance(), GetVerticalDistance(), 1); //save the two distances, and whether we flapped

            _Bird.UpdateVelocity(new Vector2(0, -50));
            GlobalVariables._Flapped = true;
        }

        internal void PipeUpdate()
        {
            if (_AssetList[0].GetPosition().X < -50)
            {
                GlobalVariables._Score++;

                _AssetList.Remove(_AssetList[0]);
                _AssetList.Remove(_AssetList[0]);

                int obsgap;

                switch (GlobalVariables._ObstacleDifficulty)
                {
                    case 0:
                        obsgap = 400;
                        break;
                    case 1:
                        obsgap = 300;
                        break;
                    default:
                        obsgap = 200;
                        break;
                }

                int xPos = (int)_AssetList[6].GetPosition().X + obsgap;

                int gap;

                switch (GlobalVariables._GapDifficulty)
                {
                    case 0:
                        gap = 300;
                        break;
                    case 1:
                        gap = 250;
                        break;
                    default:
                        gap = 200;
                        break;
                }

                int y = _RandomGen.Next(300, 400);

                _AssetList.Add(new Asset(xPos, y - 250 - gap, 50, 250, TextureType.BottomPipe, false));
                _AssetList.Add(new Asset(xPos, y, 50, 250, TextureType.TopPipe, false));
            }
        }

        internal int GetHorizontalDistance()
        {
            int birdX = (int) _Bird.GetPosition().X;
            int pipeX = (int) _AssetList[1].GetPosition().X;
            int pipeX2 = (int) _AssetList[2].GetPosition().X;

            if (pipeX - (birdX - 85) <= 0)
                return pipeX2 - birdX;
            
            return pipeX - birdX;
        }

        internal int GetVerticalDistance()
        {
            int pipeX = (int)_AssetList[1].GetPosition().X - 50;
            int birdX = (int)_Bird.GetPosition().X;

            int birdY = (int)_Bird.GetPosition().Y;
            int pipeY = (int)_AssetList[1].GetPosition().Y  - 100;
            int pipeY2 = (int)_AssetList[3].GetPosition().Y - 100;

            if (pipeX - (birdX - 85) <= 0)
                return birdY - pipeY2;

            return birdY - pipeY;
        }
    }
}

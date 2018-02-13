using System;
using System.Security.Cryptography;

namespace FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork
{
    internal class RandomNumberGen
    {
        internal double _RandomNumber;

        internal RandomNumberGen()
        {
            using (RNGCryptoServiceProvider p = new RNGCryptoServiceProvider())
            {
                Random number = new Random(p.GetHashCode());
                _RandomNumber = number.NextDouble();
            }
        }
    }
}

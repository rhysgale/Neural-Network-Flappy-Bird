using FlappyBirdNeuralNetwork.NeuralNetwork;
using FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork;

namespace FlappyBirdNeuralNetwork
{
    internal static class GlobalVariables
    {
        internal static int _Score = 0;
        internal static bool _InGame = false;
        internal static bool _Dead = false;

        //Neural network based variables
        internal static NeuralNetworkMain _ArtificialNeuralNetwork;
        internal static TrainingData _TrainingData;
    }
}

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
        internal static TrainingData _TrainingData;
        internal static NeuralNetworkController _NetworkController;
        internal static bool _NeuralNetworkGame = false;


        //Neural network no flap saving
        internal static int _FrameCount = 0;
        internal static bool _Flapped = false;
    }
}

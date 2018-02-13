using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork;

namespace FlappyBirdNeuralNetwork.NeuralNetwork
{
    internal class NeuralNetworkController
    {
        private NeuralNetworkMain _ArtificialNeuralNetwork;

        internal NeuralNetworkController()
        {
            _ArtificialNeuralNetwork = new NeuralNetworkMain(0.9, new [] {2, 3, 1});
        }

        internal void TrainNeuralNetwork()
        {
            //Train the nerual network with all the gathered training data. 
            List<DataPiece> trainingData = GlobalVariables._TrainingData.GetTrainingData();

            foreach (var dataPiece in trainingData)
            {
                List<double> inputs = new List<double>();
                List<double> output = new List<double>();
                inputs.Add(dataPiece._InputA);
                inputs.Add(dataPiece._InputB);
                output.Add(dataPiece._Output);
                _ArtificialNeuralNetwork.TrainNeuralNetwork(inputs, output);
            }
        }

        internal double[] GetNetworkOutput(List<double> input)
        {
            //Run the neural network with the 2 inputs, to retreive an output
            return _ArtificialNeuralNetwork.RunNeuralNetwork(input);
        }
    }
}

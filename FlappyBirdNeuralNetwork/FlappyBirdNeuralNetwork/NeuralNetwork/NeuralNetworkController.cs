using System.Collections.Generic;
using FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork;

namespace FlappyBirdNeuralNetwork.NeuralNetwork
{
    internal class NeuralNetworkController
    {
        private NeuralNetworkMain _ArtificialNeuralNetwork;

        internal NeuralNetworkController()
        {
            _ArtificialNeuralNetwork = new NeuralNetworkMain(0.1, new [] {2, 9, 1});
        }

        internal void TrainNeuralNetwork()
        {
            //Train the nerual network with all the gathered training data. 
            List<DataPiece> trainingData = GlobalVariables._TrainingData.GetTrainingData();

            //for (int i = 0; i < 100; i++)
            {
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
        }

        internal double[] GetNetworkOutput(List<double> input)
        {
            //Run the neural network with the 2 inputs, to retreive an output
            return _ArtificialNeuralNetwork.RunNeuralNetwork(input);
        }

        internal void SaveFlap(int ina, int inb, int op)
        {
            GlobalVariables._TrainingData.CreateTrainingData(ina, inb, op); //1 is for flap, 0 for no flap
        }
    }
}

using System.Collections.Generic;

namespace FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork
{
    internal class Layer
    {
        internal List<Neuron> _Neurons { get; set; }

        internal int _NeuronCount
        {
            get { return _Neurons.Count; }
        }

        internal Layer(int Neurons)
        {
            _Neurons = new List<Neuron>(Neurons);
        }
    }
}

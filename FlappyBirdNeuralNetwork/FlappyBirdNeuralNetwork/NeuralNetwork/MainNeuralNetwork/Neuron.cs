using System;
using System.Collections.Generic;

namespace FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork
{
    internal class Neuron
    {
        internal List<Dendrite> _Dendrites;
        internal double _Bias;
        internal double _Delta;
        internal double _Value;

        public int _DendriteCount
        {
            get { return _Dendrites.Count; }
        }

        public Neuron()
        {
            Random number = new Random(Environment.TickCount);
            _Bias = number.NextDouble();
            _Dendrites = new List<Dendrite>();
        }
    }
}

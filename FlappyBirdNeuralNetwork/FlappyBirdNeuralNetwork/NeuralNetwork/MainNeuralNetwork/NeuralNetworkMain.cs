using System;
using System.Collections.Generic;

namespace FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork
{
    internal class NeuralNetworkMain
    {
        internal List<Layer> _Layers { get; set; }
        internal double _LearningRate { get; set; }

        internal int _LayerCount
        {
            get { return _Layers.Count; }
        }

        internal NeuralNetworkMain(double learningRate, int[] layers)
        {
            //We need at least 3 layers. 
            if (layers.Length < 2) return;

            //Setup variables
            _LearningRate = learningRate;
            _Layers = new List<Layer>();

            for (int i = 0; i < layers.Length; i++)
            {
                Layer layer = new Layer(layers[i]);
                _Layers.Add(layer);

                for (int n = 0; n < layers[i]; n++)
                    layer._Neurons.Add(new Neuron());

                layer._Neurons.ForEach((nn) =>
                {
                    if (i == 0)
                        nn._Bias = 0;
                    else
                        for (int d = 0; d < layers[i - 1]; d++)
                            nn._Dendrites.Add(new Dendrite());
                });
            }
        }

        private double SigmoidFunction(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        public double[] RunNeuralNetwork(List<double> input)
        {
            if (input.Count != _Layers[0]._NeuronCount) return null;

            for (int l = 0; l < _Layers.Count; l++)
            {
                Layer layer = _Layers[l];

                for (int n = 0; n < layer._Neurons.Count; n++)
                {
                    Neuron neuron = layer._Neurons[n];

                    if (l == 0)
                        neuron._Value = input[n];
                    else
                    {
                        neuron._Value = 0;
                        for (int np = 0; np < _Layers[l - 1]._Neurons.Count; np++)
                            neuron._Value = neuron._Value + _Layers[l - 1]._Neurons[np]._Value * neuron._Dendrites[np]._Weight;

                        neuron._Value = SigmoidFunction(neuron._Value + neuron._Bias);
                    }
                }
            }

            Layer last = _Layers[_Layers.Count - 1];
            int numOutput = last._Neurons.Count;
            double[] output = new double[numOutput];
            for (int i = 0; i < last._Neurons.Count; i++)
                output[i] = last._Neurons[i]._Value;

            return output;
        }


        internal bool TrainNeuralNetwork(List<double> input, List<Double> output)
        {
            if ((input.Count != _Layers[0]._Neurons.Count) || (output.Count != _Layers[_Layers.Count - 1]._Neurons.Count)) return false;

            RunNeuralNetwork(input);

            for (int i = 0; i < _Layers[_Layers.Count - 1]._Neurons.Count; i++)
            {
                Neuron neuron = _Layers[_Layers.Count - 1]._Neurons[i];

                neuron._Delta = neuron._Value * (1 - neuron._Value) * (output[i] - neuron._Value);

                for (int j = _Layers.Count - 2; j >= 1; j--)
                {
                    for (int k = 0; k < _Layers[j]._Neurons.Count; k++)
                    {
                        Neuron n = _Layers[j]._Neurons[k];

                        n._Delta = n._Value *
                                  (1 - n._Value) *
                                  _Layers[j + 1]._Neurons[i]._Dendrites[k]._Weight *
                                  _Layers[j + 1]._Neurons[i]._Delta;
                    }
                }
            }

            for (int i = _Layers.Count - 1; i >= 1; i--)
            {
                for (int j = 0; j < _Layers[i]._Neurons.Count; j++)
                {
                    Neuron n = _Layers[i]._Neurons[j];
                    n._Bias = n._Bias + (_LearningRate * n._Delta);

                    for (int k = 0; k < n._Dendrites.Count; k++)
                        n._Dendrites[k]._Weight = n._Dendrites[k]._Weight + (_LearningRate * _Layers[i - 1]._Neurons[k]._Value * n._Delta);
                }
            }

            return true;
        }
    }
}


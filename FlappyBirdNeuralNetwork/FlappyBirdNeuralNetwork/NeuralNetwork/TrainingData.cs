using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBirdNeuralNetwork.NeuralNetwork
{
    //Class used for saving training data
    internal class TrainingData
    {
        private List<DataPiece> _TrainingData;

        internal TrainingData()
        {
            _TrainingData = new List<DataPiece>();
        }

        internal void CreateTrainingData(int in1, int in2, int op)
        {
            _TrainingData.Add(new DataPiece
            {
                _InputA = in1,
                _InputB = in2,
                _Output = op
            });
        }

        internal List<DataPiece> GetTrainingData()
        {
            return _TrainingData;
        }
    }
}

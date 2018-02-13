namespace FlappyBirdNeuralNetwork.NeuralNetwork.MainNeuralNetwork
{
    internal class Dendrite
    {
        internal double _Weight;

        internal Dendrite()
        {
            _Weight = new RandomNumberGen()._RandomNumber;
        }
    }
}

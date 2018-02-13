using Microsoft.Xna.Framework;

namespace FlappyBirdNeuralNetwork.Assets
{
    internal class Asset
    {
        private Vector2 _Position;
        private Vector2 _Size;
        private Vector2 _Velocity;
        private TextureType _Type;
        private bool _GravityEffected;

        internal Asset(int x, int y, int sizeX, int sizeY, TextureType type, bool gravityEffected)
        {
            _Position = new Vector2(x, y);
            _Size = new Vector2(sizeX, sizeY);
            _Type = type;
            _Velocity = new Vector2(0);
            _GravityEffected = gravityEffected;
            if (!_GravityEffected)
            {
                _Velocity = new Vector2(-10, 0);
            }
        }

        internal Vector2 GetPosition()
        {
            return _Position;
        }

        internal void UpdatePosition(int x, int y)
        {
            _Position = new Vector2(x, y);
        }

        internal Vector2 GetSize()
        {
            return _Size;
        }

        internal TextureType GetTextureType()
        {
            return _Type;
        }

        internal Vector2 GetVelocity()
        {
            return _Velocity;
        }

        internal bool IsEffectedByGravity()
        {
            return _GravityEffected;
        }

        internal void UpdateVelocity(Vector2 velo)
        {
            _Velocity = velo;
        }
    }
}

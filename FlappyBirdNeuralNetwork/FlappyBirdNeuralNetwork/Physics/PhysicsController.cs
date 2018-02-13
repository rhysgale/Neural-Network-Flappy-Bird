using FlappyBirdNeuralNetwork.Assets;
using Microsoft.Xna.Framework;

namespace FlappyBirdNeuralNetwork.Physics
{
    internal class PhysicsController
    {
        private float _AverageTime = 0.25f;

        internal bool CheckCollision(Asset item1, Asset item2)
        {
            Vector2 pos = item1.GetPosition();
            Vector2 size = item1.GetSize();
            int right = (int) (pos.X + size.X);
            int left = (int) pos.X;
            int top = (int) pos.Y;
            int bottom = (int) (pos.Y + size.Y);

            Vector2 posB = item2.GetPosition();
            Vector2 sizeB = item2.GetSize();
            int rightB = (int)(posB.X + sizeB.X);
            int leftB = (int)posB.X;
            int topB = (int)posB.Y;
            int bottomB = (int)(posB.Y + sizeB.Y);

            if (right <= leftB || left >= rightB || top >= bottomB || bottom <= topB)
                return false;

            return true;
        }

        internal void ApplyPhysics(Asset item)
        {
            Vector2 velo = item.GetVelocity();
            Vector2 gravity = new Vector2(0, 9.8f);
            Vector2 currentPos = item.GetPosition();

            if (item.IsEffectedByGravity())
                velo += (gravity * _AverageTime);

            currentPos += (velo * _AverageTime);

            item.UpdatePosition((int)currentPos.X, (int)currentPos.Y);
            item.UpdateVelocity(velo);
        }
    }
}

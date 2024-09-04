using UnityEngine;

namespace PuzzlePlatformer
{
    public interface IPushable
    {
        void Push(Vector3 direction, float force);
    }
}
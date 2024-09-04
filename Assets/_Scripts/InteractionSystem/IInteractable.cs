using UnityEngine;

namespace PuzzlePlatformer
{
    public interface IInteractable
    {
        void Interact();

        Transform GetTransform();
    }
}
using UnityEngine;

namespace PuzzlePlatformer
{
    public class UnlockDoorButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private Door _door;
        private PuzzleManager _puzzleManager;

        private void Start()
        {
            _puzzleManager = PuzzleManager.Instance;
        }

        public Transform GetTransform() => transform;

        public void Interact()
        {
            _puzzleManager.UnlockDoor(_door);
        }
    }
}
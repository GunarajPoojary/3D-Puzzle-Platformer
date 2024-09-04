using UnityEngine;

namespace PuzzlePlatformer
{
    public class Door : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void UnlockDoor()
        {
            _animator.SetTrigger(OpenDoor);
        }
    }
}
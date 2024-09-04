using UnityEngine;

namespace PuzzlePlatformer
{
    public class PressurePlate : MonoBehaviour
    {
        private bool _isActivated = false;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col != null)
            {
                _animator.SetBool("IsPressed", true);
            }
        }

        private void OnCollisionExit(Collision col)
        {
            if (col != null)
            {
                _animator.SetBool("IsPressed", false);
            }
        }
    }
}

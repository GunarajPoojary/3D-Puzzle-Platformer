using UnityEngine;

namespace PuzzlePlatformer
{
    public class GravityBlock : MonoBehaviour
    {
        [SerializeField] private LayerMask _environmentLayer;
        [SerializeField] private Transform _targetBlock;
        private bool _pathBlocked = false;

        private void Awake()
        {
            transform.localPosition = -_targetBlock.localPosition;
        }

        private void Update()
        {
            if (_pathBlocked)
            {
                return; // Don't move if the path is blocked
            }

            // Get the target block's local position relative to its parent
            Vector3 targetLocalPosition = _targetBlock.localPosition;

            // Invert the local axes (inverting all three axes here)
            Vector3 invertedLocalPosition = new Vector3(-targetLocalPosition.x, -targetLocalPosition.y, -targetLocalPosition.z);

            // Set this block's local position to the inverted position
            transform.localPosition = invertedLocalPosition;
        }

        private void OnCollisionEnter(Collision col)
        {
            if (((1 << col.gameObject.layer) & _environmentLayer) != 0)
            {
                _pathBlocked = true;
            }
        }

        private void OnCollisionExit(Collision col)
        {
            if (((1 << col.gameObject.layer) & _environmentLayer) != 0)
            {
                _pathBlocked = false;
            }
        }
    }
}
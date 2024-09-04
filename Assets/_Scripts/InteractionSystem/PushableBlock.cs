using UnityEngine;

namespace PuzzlePlatformer
{
    public class PushableBlock : MonoBehaviour, IPushable
    {
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.constraints = RigidbodyConstraints.FreezeRotation; // Prevent unwanted rotations
        }

        public void Push(Vector3 direction, float force)
        {
            // Apply force to the block in the push direction
            _rb.AddForce(direction * force, ForceMode.Force);
        }
    }
}
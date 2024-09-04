using UnityEngine;

namespace PuzzlePlatformer
{
    public class RigidBodyPush : MonoBehaviour
    {
        public float pushForce = 5f;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Check if the object hit by the player is the block
            Rigidbody rb = hit.collider.attachedRigidbody;

            // Ensure the object has a Rigidbody and is not kinematic
            if (rb != null && !rb.isKinematic)
            {
                // Calculate the push direction and apply force
                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}
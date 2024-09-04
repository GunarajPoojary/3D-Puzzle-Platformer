using UnityEngine;

namespace PuzzlePlatformer
{
    public class PlayerRigidBodyPush : MonoBehaviour
    {
        [SerializeField] private float _forceMagnitude = 5f; // Adjust this for realism
        [SerializeField] private float _pushRadius = 1f;     // Radius for detecting pushable objects
        private PlayerInputs _playerInputs;
        private Vector3 _pushDirection;
        private Animator _animator;
        private CharacterMovement _characterMovement;
        [SerializeField] private Transform _pushCheckPoint;

        [SerializeField] private LayerMask _pushableLayer;

        private static readonly int IsPushingHash = Animator.StringToHash("IsPushing");

        public bool IsPushing { get; private set; } = false;
        private bool _foundPushable = false;

        private void Awake()
        {
            _playerInputs = GetComponent<PlayerInputs>();
            _animator = GetComponentInChildren<Animator>();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            DetectPushableObjects();

            if (_characterMovement.MovementInput == Vector3.forward && _foundPushable)
            {
                PushObject();
            }
            else
            {
                IsPushing = false;
            }

            UpdateAnimation();
        }

        private void PushObject()
        {
            RaycastHit hit;
            if (Physics.Raycast(_pushCheckPoint.position, _pushCheckPoint.forward, out hit, 1f))
            {
                if (hit.transform.TryGetComponent<IPushable>(out var pushable))
                {
                    pushable.Push(_pushDirection, _forceMagnitude);
                    IsPushing = true;
                }
                else
                {
                    IsPushing = false;
                }
            }
        }

        private void DetectPushableObjects()
        {
            Collider[] colliders = Physics.OverlapSphere(_pushCheckPoint.position, _pushRadius, _pushableLayer);

            _foundPushable = false;

            foreach (var collider in colliders)
            {
                if (collider.transform.TryGetComponent<IPushable>(out var pushable))
                {
                    _pushDirection = _pushCheckPoint.forward;
                    _foundPushable = true;
                    break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_pushCheckPoint.position, _pushRadius);
        }

        private void UpdateAnimation()
        {
            _animator.SetBool(IsPushingHash, IsPushing);
        }
    }
}
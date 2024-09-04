using UnityEngine;
using UnityEngine.InputSystem;

namespace PuzzlePlatformer
{
    [RequireComponent(typeof(PlayerInputs), typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Gravity Variables")]
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _groundCheckRadius = 0.4f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Movement Variables")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        [SerializeField] private float _jumpHeight = 5f;

        private float _turnSmoothVelocity;
        private PlayerInputs _playerInputs;
        private CharacterController _characterController;
        private Animator _animator;
        private Transform _mainCam;

        public Vector3 MovementInput { get; set; }
        private Vector3 _velocity;
        private bool _isGrounded;
        //private bool _isJumping;

        private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
        private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");

        private void Awake()
        {
            _mainCam = Camera.main.transform;

            _playerInputs = GetComponent<PlayerInputs>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();

            ResetMovementInput();
        }

        private void OnEnable()
        {
            _playerInputs.PlayerActions.Movement.performed += OnMovementPerformed;
            _playerInputs.PlayerActions.Movement.canceled += OnMovementCanceled;
            _playerInputs.PlayerActions.Jump.started += OnJumpPerformed;
        }

        private void OnDisable()
        {
            _playerInputs.PlayerActions.Movement.performed -= OnMovementPerformed;
            _playerInputs.PlayerActions.Movement.canceled -= OnMovementCanceled;
            _playerInputs.PlayerActions.Jump.started -= OnJumpPerformed;
        }

        private void Update()
        {
            CheckForGrounded();
            ApplyDownwardVelocity();

            Move();
            UpdateAnimation();
        }

        #region Main Methods
        private void Move()
        {
            if (MovementInput.magnitude >= 0.1f)
            {
                HandleRotation();

                // Move the character in the desired direction based on the calculated rotation
                Vector3 moveDirection = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward;
                _characterController.Move(moveDirection.normalized * _moveSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            // Calculate the target angle based on the camera's forward direction and player input
            float targetAngle = Mathf.Atan2(MovementInput.x, MovementInput.z) * Mathf.Rad2Deg + _mainCam.eulerAngles.y;

            // Smoothly interpolate between the current angle and the target angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

            // Apply the interpolated angle as the character's rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        private void Jump()
        {
            if (_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
                //_isJumping = true;
            }
        }

        private void CheckForGrounded()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer);
            //_isJumping = false;
        }

        private void ResetMovementInput()
        {
            MovementInput = Vector3.zero;
        }

        private void ApplyDownwardVelocity()
        {
            if (_isGrounded && _velocity.y < 0f)
            {
                _velocity.y = -2f;
            }
            else
            {
                _velocity.y += _gravity * Time.deltaTime;
            }

            _characterController.Move(_velocity * Time.deltaTime);
        }
        #endregion

        private void UpdateAnimation()
        {
            bool isWalking = MovementInput.magnitude >= 0.1f && _isGrounded;
            _animator.SetBool(IsWalkingHash, isWalking);
            _animator.SetBool(IsJumpingHash, !_isGrounded);
        }

        #region Input Methods
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            MovementInput = new Vector3(inputVector.x, 0f, inputVector.y);
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            ResetMovementInput();
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            Jump();
        }
        #endregion
    }
}
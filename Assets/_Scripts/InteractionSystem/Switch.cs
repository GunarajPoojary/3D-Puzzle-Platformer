using UnityEngine;

namespace PuzzlePlatformer
{
    public enum SwitchState { Off, On };

    public class Switch : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _parameterName = "IsSwitchedOn"; // Parameter name in Animator
        private Animator _switchAnimator; // Animator component

        public SwitchState _switchState;

        public int SwitchIndex { get; set; }

        private void Awake()
        {
            _switchAnimator = GetComponentInChildren<Animator>();

            if (_switchAnimator == null)
            {
                Debug.LogError("Animator is not assigned!");
            }
        }

        // Method to toggle the switch
        public void ToggleSwitch()
        {
            bool isSwitchOn = _switchState == SwitchState.Off;

            if (isSwitchOn)
            {
                SwitchOn();
            }
            else
            {
                SwitchOff();
            }

            // Pass the switch state to PuzzleManager
            PuzzleManager.Instance.ToggleSwitch(SwitchIndex, isSwitchOn);
        }

        public void SwitchOff()
        {
            _switchState = SwitchState.Off;
            _switchAnimator.SetBool(_parameterName, false);
        }

        public void SwitchOn()
        {
            _switchState = SwitchState.On;
            _switchAnimator.SetBool(_parameterName, true);
        }

        public void Interact()
        {
            ToggleSwitch();
        }

        public Transform GetTransform() => transform;
    }
}
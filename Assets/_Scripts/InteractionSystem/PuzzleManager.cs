using System.Collections.Generic;
using UnityEngine;

namespace PuzzlePlatformer
{
    public class PuzzleManager : MonoBehaviour
    {
        public static PuzzleManager Instance;

        [SerializeField] private Switch[] _switches;

        public List<int> correctSequence = new List<int> { 2, 3, 1 };  // Desired sequence
        private List<int> playerSequence = new List<int>();  // Player's sequence
        private bool _isSequenceCorrect = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Optional: Persist between scenes
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SetSwitchIndex();
            ResetPuzzle();
        }

        private void SetSwitchIndex()
        {
            for (int i = 0; i < _switches.Length; i++)
            {
                _switches[i].SwitchIndex = i + 1;
            }
        }

        public void ToggleSwitch(int switchID, bool isSwitchOn)
        {
            if (isSwitchOn)
            {
                // If the switch is turned on, add it to the player sequence
                if (playerSequence.Count < correctSequence.Count)
                {
                    playerSequence.Add(switchID);

                    if (playerSequence.Count == correctSequence.Count)
                    {
                        CheckSequence();
                    }
                }
            }
            else
            {
                ResetPuzzle();  // Improved: Reset the puzzle instead of just clearing the sequence
            }
        }

        private void CheckSequence()
        {
            _isSequenceCorrect = true;

            for (int i = 0; i < correctSequence.Count; i++)
            {
                if (playerSequence[i] != correctSequence[i])
                {
                    _isSequenceCorrect = false;
                    break;
                }
            }

            if (!_isSequenceCorrect)
            {
                ResetPuzzle();  // Reset the puzzle if the sequence is incorrect
            }
        }

        public void ResetPuzzle()
        {
            foreach (var puzzleSwitch in _switches)
            {
                puzzleSwitch.SwitchOff(); // Ensure all switches are turned off
            }

            _isSequenceCorrect = false;
            playerSequence.Clear(); // Clear player sequence on reset
        }

        public void UnlockDoor(Door door)
        {
            if (_isSequenceCorrect)
            {
                door.UnlockDoor();
            }
            else
            {
                ResetPuzzle();
            }
        }
    }
}
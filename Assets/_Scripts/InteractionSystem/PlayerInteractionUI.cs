using UnityEngine;

namespace PuzzlePlatformer
{
    public class PlayerInteractionUI : MonoBehaviour
    {
        [SerializeField] private GameObject _interactionUI;
        [SerializeField] private Interact _playerInteract;

        private void Awake()
        {
            _interactionUI.SetActive(false);
        }

        private void OnEnable()
        {
            _playerInteract.OnGetInteractable += OnGetInteractable;
        }

        private void OnDisable()
        {
            _playerInteract.OnGetInteractable -= OnGetInteractable;
        }

        private void OnGetInteractable(IInteractable interactable)
        {
            bool hasInteractable = interactable != null;

            _interactionUI.SetActive(hasInteractable);

            if (hasInteractable)
            {
                Vector3 interatcablePos = interactable.GetTransform().position;

                _interactionUI.transform.position = interatcablePos + new Vector3(0, 0.5f, -0.5f);
            }
        }
    }
}
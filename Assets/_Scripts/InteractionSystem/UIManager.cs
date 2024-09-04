using TMPro;
using UnityEngine;

namespace PuzzlePlatformer
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI keysText;
        public TextMeshProUGUI hintsText;

        private void Update()
        {
            //keysText.text = "Keys: " + InventoryManager.instance.GetKeyCount();
            // Update hints based on the player's progress or current puzzle
        }
    }
}
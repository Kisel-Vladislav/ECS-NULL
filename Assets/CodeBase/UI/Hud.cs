using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _ammoText;

        public void SetAmmo(int current, int total)
        {
            _ammoText.text = $"{current}/{total}";
        }
    }
}

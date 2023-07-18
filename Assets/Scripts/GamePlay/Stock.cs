using TMPro;
using UnityEngine;

namespace GamePlay
{
    public class Stock: MonoBehaviour, IAddItems
    {
        [SerializeField] private TextMeshProUGUI _swordCount;
        private int _swords;

        private void Start()
        {
           HideDisplay();
        }

        public void AddItem()
        {
            _swords += 1;
            _swordCount.text = _swords+ "x";
        }

        public void ShowDisplay()
        {
            _swordCount.gameObject.SetActive(true);
        } 
        public void HideDisplay()
        {
            _swordCount.gameObject.SetActive(false);
        }
    }
}
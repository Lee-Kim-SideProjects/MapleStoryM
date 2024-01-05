using UnityEngine;
using TMPro;

namespace XEntity.InventoryItemSystem
{
    public class ShowMoney : MonoBehaviour
    {
        private TextMeshProUGUI moneyText;

        void Start()
        {
            moneyText = GetComponent<TextMeshProUGUI>();
        }

        void LateUpdate()
        {
            moneyText.text = ItemManager.Instance.haveMoney.ToString();
        }

    }
}
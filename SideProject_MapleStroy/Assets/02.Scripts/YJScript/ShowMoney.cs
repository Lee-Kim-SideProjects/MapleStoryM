using UnityEngine;
using TMPro;

namespace XEntity.InventoryItemSystem
{
    //인벤토리 창의 메소 보여주는 텍스트에 넣기
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
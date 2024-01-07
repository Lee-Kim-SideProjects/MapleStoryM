using UnityEngine;
using TMPro;

namespace XEntity.InventoryItemSystem
{
    //�κ��丮 â�� �޼� �����ִ� �ؽ�Ʈ�� �ֱ�
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
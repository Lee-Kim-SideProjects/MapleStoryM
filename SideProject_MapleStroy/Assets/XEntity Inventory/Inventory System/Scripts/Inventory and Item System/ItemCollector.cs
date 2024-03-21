using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //이 스크립트는 아이템 수집기 오브젝트에 첨부됩니다.
    //아이템 수집기는 씬에 있는 부유하는 아이템 프리팹으로, 상호작용자가 충돌하여 가져갈 수 있습니다.
    public class ItemCollector : MonoBehaviour
    {
        private Item item;

        //이 축을 따라 아이템 수집기를 계속 회전시킵니다.
        private readonly Vector3 rotAxis = new Vector3(0.1f, 1, 0.1f);

        private void Update()
        {
            //수집기 오브젝트를 회전시킵니다.
            transform.Rotate(rotAxis, Time.deltaTime * 200);
        }

        //아이템 수집기가 오브젝트에 첨부되었을 때, 이 메서드를 호출하고 이 수집기의 아이템을 전달해야 합니다.
        public void Create(Item item)
        {
            this.item = item;
        }

        //상호작용자와 트리거되면, 이 수집기의 아이템을 상호작용자의 인벤토리에 추가하려고 시도합니다.
        private void OnTriggerEnter(Collider other)
        {
            Interactor interactor = other.GetComponent<Interactor>();
            if (interactor != null) interactor.AddToInventory(item, gameObject);
        }
    }
}

using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //이 스크립트는 나무와 같은 HP 기반으로 수확되는 수확 가능한 개체에 첨부됩니다.
    public class Harvestable : MonoBehaviour, IInteractable
    {
        //이 HP가 0이되면 객체가 수확됩니다.
        public float HP;

        //이 객체가 수확되면이 수확 드롭을 기반으로 한 항목이 플레이어가 픽업하도록 삭제됩니다.
        public HarvestDrop[] harvestDrops;

        //이 방법은 상호 작용을위한 범위 내에있는 상호 작용자가이 개체를 클릭 할 때 호출됩니다.
        public void OnClickInteract(Interactor interactor)
        {
            HP--;
            if (HP <= 0) Harvest(interactor);
        }

        //HP가 제로이고 항목을 수확해야하는 경우이 메서드가 호출됩니다.
        private void Harvest(Interactor interactor) 
        {
            System.Random prng = new System.Random(GetHashCode());

            //드롭 확률에 따라 항목이 삭제됩니다.
            for (int i = 0; i < harvestDrops.Length; i++) 
            {
                HarvestDrop drop = harvestDrops[i];
                if (prng.NextDouble() <= drop.chance)
                    Utils.InstantiateItemCollector(drop.itemToDrop, transform.position + (transform.forward / 2));
            }

            //수확 된 모든 항목이 삭제되면 수확 가능한 객체가 파괴됩니다.
            StartCoroutine(Utils.TweenScaleOut(gameObject, 40, true));
        }
    }


    //이 구조물에는 수확한 아이템 드롭과 드롭 찬스가 포함되어 있습니다.
    [System.Serializable]
    public struct HarvestDrop
    {
        public Item itemToDrop;
        [Range(0, 1)]
        public float chance;
    }
}

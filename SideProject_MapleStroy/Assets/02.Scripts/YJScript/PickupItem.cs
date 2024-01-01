using NPOI.SS.Formula.Functions;
using UnityEngine;
using XEntity.InventoryItemSystem;

namespace XEntity.InventoryItemSystem
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        private bool isHarvested = false;

        public Item harvestItem;

        public LayerMask layer;

        void SearchItem()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                var Grounditem = Physics2D.OverlapCircle(this.transform.position, 1f, layer);

                if (Grounditem != null)
                {
                    Interactor interactor = Grounditem.GetComponent<Interactor>();
                    AttemptHarvest(interactor);
                }
            }

        }

        public void OnClickInteract(Interactor interactor)
        {
            //아직 수확하지 않은 경우 수확 시도
            AttemptHarvest(interactor);
        }

        public void AttemptHarvest(Interactor harvestor)
        {
            if (!isHarvested)
            {
                if (harvestor.AddToInventory(harvestItem, gameObject))
                {
                    isHarvested = true;
                }
            }
        }
    }
}

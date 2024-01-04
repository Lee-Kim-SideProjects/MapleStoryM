using NPOI.SS.Formula.Functions;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace XEntity.InventoryItemSystem
{
    //This script is attached to any item that is picked up by the interactor on a single click such as small rocks and sticks.
    //NOTE: The item is only added if the interactor is within the interaction range.
    public class InstantHarvest : MonoBehaviour, IInteractable
    {
        private bool isHarvested = false;

        //The item that will be harvested on click.
        public Item harvestItem;

        private Animator anim;
        public LayerMask layer;


        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            PressGetHarvester();
        }

        //The item is instantly added to the inventory of the interactor on interact.
        public void OnClickInteract(Interactor interactor)
        {
            //Attempt to harvest if not harvested already
            AttemptHarvest(interactor);
        }

        public void AttemptHarvest(Interactor harvestor) 
        {
            if (harvestItem != null && gameObject != null)
            {
                if (!isHarvested)
                {
                    if (harvestor.AddToInventory(harvestItem, gameObject))
                    {
                        isHarvested = true;
                    }
                }
            }
            else
            {
                Debug.LogError("harvestItem or gameObject is null.");
            }
        }
        void PressGetHarvester()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                var Grounditem = Physics2D.OverlapCircle(this.transform.position, 1f, layer);

                if (Grounditem != null)
                {
                    if (Grounditem.CompareTag("Player")) { }
                    Interactor interactor = Grounditem.GetComponent<Interactor>();
                    if (interactor != null)
                    {
                        AttemptHarvest(interactor);
                        anim.SetTrigger("Get");
                    }
                }
            }
        }
    }
}

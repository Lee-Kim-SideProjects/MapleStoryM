using System;
using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //이 클래스는 플레이어에 첨부됩니다.
    //이 클래스는 다양한 종류의 상호작용 이벤트와 상호작용 트리거 메서드를 보유합니다.
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public ItemContainer inventory;

        private InteractionTarget interactionTarget;

        //아이템을 드롭할 위치입니다. (이 상호작용자의 앞)
        public Vector3 ItemDropPosition { get { return transform.position + transform.forward; } }

        public LayerMask layer;
        private Item groundItemInfo;

        private int plusMoney;

        private void Update()
        {
            HandleInteractions();
            SearchItem();
        }

        //이 메서드는 에디터에서 gizmo를 그립니다.
        private void OnDrawGizmos()
        {
            if (InteractionSettings.Current.drawRangeIndicators)
            {
                Gizmos.DrawWireSphere(transform.position, InteractionSettings.Current.interactionRange);
            }
        }

        //이 메서드는 상호작용 가능한 객체 탐지, 상호작용 트리거 및 상호작용 이벤트 콜백을 처리합니다.
        private void HandleInteractions()
        {
            //Raycast 쏘기
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (interactionTarget?.gameObject != null) 
                Utils.UnhighlightObject(interactionTarget.gameObject);

            //InteractionSettings에 Range 범위 안에 있으면 InterationTarget 으로 GameObject를 지정해주고 Utils의 오브젝트 하이라이트 실행
            if (Physics.Raycast(ray, out hit) && InRange(hit.transform.position))
            {
                IInteractable target = hit.transform.GetComponent<IInteractable>();
                if (target != null)
                {
                    interactionTarget = new InteractionTarget(target, hit.transform.gameObject);
                    Utils.HighlightObject(interactionTarget.gameObject);
                }
                else interactionTarget = null;
            }
            else
            {
                interactionTarget = null;
            }

            if (Input.GetMouseButtonDown(0)) 
                InitInteraction();
        }

        //대상 위치가 상호작용 범위 내에 있는 경우 true를 반환합니다.
        private bool InRange(Vector3 targetPosition)
        {
            return Vector3.Distance(targetPosition, transform.position) <= InteractionSettings.Current.interactionRange;
        }

        //이 메서드는 유효한 상호작용 대상이 존재하는 경우 이 상호작용자와의 상호작용을 초기화합니다.
        private void InitInteraction()
        {
            if (interactionTarget == null) return;
            interactionTarget.interactable.OnClickInteract(this);
        }

        //이 메서드는 아이템을 이 상호작용자의 인벤토리에 추가하고, 필요한 경우 아이템의 물리적 인스턴스를 파괴합니다.
        public bool AddToInventory(Item item, GameObject instance)
        {
            if (inventory.AddItem(item))
            {
                if (instance)
                {
                    Destroy(instance, 1f);
                }
                return true;
            }
            return false;
        }

        internal class InteractionTarget
        {
            internal IInteractable interactable;
            internal GameObject gameObject;
            public InteractionTarget(IInteractable interactable, GameObject gameObject)
            {
                this.interactable = interactable;
                this.gameObject = gameObject;
            }
        }

        private void SearchItem()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                var GroundItem = Physics2D.OverlapCircle(this.transform.position, 1f, layer);

                if (GroundItem != null && GroundItem.CompareTag("Item"))
                {
                    //인벤토리에 추가
                    GameObject groundItemObject = GroundItem.gameObject;
                    groundItemInfo = GroundItem.GetComponent<InstantHarvest>().harvestItem;

                    //돈을 주우면 인벤에 추가X
                    if (groundItemInfo.type != ItemType.Money)
                    {
                        AddToInventory(groundItemInfo, groundItemObject);
                    }
                    else
                    {
                        string itemName = groundItemInfo.ItemName;
                        switch (itemName)
                        {
                            case "Bronze":
                                plusMoney = UnityEngine.Random.Range(1, 10);
                                break;

                            case "Gold":
                                plusMoney = UnityEngine.Random.Range(10, 100);
                                break;

                            case "Money":
                                plusMoney = UnityEngine.Random.Range(100, 1000);
                                break;

                            case "Flex":
                                plusMoney = UnityEngine.Random.Range(1000, 1000);
                                break;
                        }
                        ItemManager.Instance.haveMoney += plusMoney;
                        Destroy(groundItemObject, 1f);
                    }

                    //줍는 모션
                    GroundItem.gameObject.GetComponent<ItemFollowPlayer>().enabled = true;
                }
            }
        }
    }
}

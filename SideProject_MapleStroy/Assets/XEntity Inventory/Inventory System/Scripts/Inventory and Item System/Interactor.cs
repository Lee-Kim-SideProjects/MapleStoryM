using System;
using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //�� Ŭ������ �÷��̾ ÷�ε˴ϴ�.
    //�� Ŭ������ �پ��� ������ ��ȣ�ۿ� �̺�Ʈ�� ��ȣ�ۿ� Ʈ���� �޼��带 �����մϴ�.
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public ItemContainer inventory;

        private InteractionTarget interactionTarget;

        //�������� ����� ��ġ�Դϴ�. (�� ��ȣ�ۿ����� ��)
        public Vector3 ItemDropPosition { get { return transform.position + transform.forward; } }

        public LayerMask layer;
        private Item groundItemInfo;

        private int plusMoney;

        private void Update()
        {
            HandleInteractions();
            SearchItem();
        }

        //�� �޼���� �����Ϳ��� gizmo�� �׸��ϴ�.
        private void OnDrawGizmos()
        {
            if (InteractionSettings.Current.drawRangeIndicators)
            {
                Gizmos.DrawWireSphere(transform.position, InteractionSettings.Current.interactionRange);
            }
        }

        //�� �޼���� ��ȣ�ۿ� ������ ��ü Ž��, ��ȣ�ۿ� Ʈ���� �� ��ȣ�ۿ� �̺�Ʈ �ݹ��� ó���մϴ�.
        private void HandleInteractions()
        {
            //Raycast ���
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (interactionTarget?.gameObject != null) 
                Utils.UnhighlightObject(interactionTarget.gameObject);

            //InteractionSettings�� Range ���� �ȿ� ������ InterationTarget ���� GameObject�� �������ְ� Utils�� ������Ʈ ���̶���Ʈ ����
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

        //��� ��ġ�� ��ȣ�ۿ� ���� ���� �ִ� ��� true�� ��ȯ�մϴ�.
        private bool InRange(Vector3 targetPosition)
        {
            return Vector3.Distance(targetPosition, transform.position) <= InteractionSettings.Current.interactionRange;
        }

        //�� �޼���� ��ȿ�� ��ȣ�ۿ� ����� �����ϴ� ��� �� ��ȣ�ۿ��ڿ��� ��ȣ�ۿ��� �ʱ�ȭ�մϴ�.
        private void InitInteraction()
        {
            if (interactionTarget == null) return;
            interactionTarget.interactable.OnClickInteract(this);
        }

        //�� �޼���� �������� �� ��ȣ�ۿ����� �κ��丮�� �߰��ϰ�, �ʿ��� ��� �������� ������ �ν��Ͻ��� �ı��մϴ�.
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
                    //�κ��丮�� �߰�
                    GameObject groundItemObject = GroundItem.gameObject;
                    groundItemInfo = GroundItem.GetComponent<InstantHarvest>().harvestItem;

                    //���� �ֿ�� �κ��� �߰�X
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

                    //�ݴ� ���
                    GroundItem.gameObject.GetComponent<ItemFollowPlayer>().enabled = true;
                }
            }
        }
    }
}

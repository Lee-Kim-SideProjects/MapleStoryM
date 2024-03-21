using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //�� ��ũ��Ʈ�� ������ ������ ������Ʈ�� ÷�ε˴ϴ�.
    //������ ������� ���� �ִ� �����ϴ� ������ ����������, ��ȣ�ۿ��ڰ� �浹�Ͽ� ������ �� �ֽ��ϴ�.
    public class ItemCollector : MonoBehaviour
    {
        private Item item;

        //�� ���� ���� ������ �����⸦ ��� ȸ����ŵ�ϴ�.
        private readonly Vector3 rotAxis = new Vector3(0.1f, 1, 0.1f);

        private void Update()
        {
            //������ ������Ʈ�� ȸ����ŵ�ϴ�.
            transform.Rotate(rotAxis, Time.deltaTime * 200);
        }

        //������ �����Ⱑ ������Ʈ�� ÷�εǾ��� ��, �� �޼��带 ȣ���ϰ� �� �������� �������� �����ؾ� �մϴ�.
        public void Create(Item item)
        {
            this.item = item;
        }

        //��ȣ�ۿ��ڿ� Ʈ���ŵǸ�, �� �������� �������� ��ȣ�ۿ����� �κ��丮�� �߰��Ϸ��� �õ��մϴ�.
        private void OnTriggerEnter(Collider other)
        {
            Interactor interactor = other.GetComponent<Interactor>();
            if (interactor != null) interactor.AddToInventory(item, gameObject);
        }
    }
}

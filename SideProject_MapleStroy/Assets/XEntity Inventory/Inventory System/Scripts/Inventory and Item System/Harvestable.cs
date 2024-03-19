using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //�� ��ũ��Ʈ�� ������ ���� HP ������� ��Ȯ�Ǵ� ��Ȯ ������ ��ü�� ÷�ε˴ϴ�.
    public class Harvestable : MonoBehaviour, IInteractable
    {
        //�� HP�� 0�̵Ǹ� ��ü�� ��Ȯ�˴ϴ�.
        public float HP;

        //�� ��ü�� ��Ȯ�Ǹ��� ��Ȯ ����� ������� �� �׸��� �÷��̾ �Ⱦ��ϵ��� �����˴ϴ�.
        public HarvestDrop[] harvestDrops;

        //�� ����� ��ȣ �ۿ������� ���� �����ִ� ��ȣ �ۿ��ڰ��� ��ü�� Ŭ�� �� �� ȣ��˴ϴ�.
        public void OnClickInteract(Interactor interactor)
        {
            HP--;
            if (HP <= 0) Harvest(interactor);
        }

        //HP�� �����̰� �׸��� ��Ȯ�ؾ��ϴ� ����� �޼��尡 ȣ��˴ϴ�.
        private void Harvest(Interactor interactor) 
        {
            System.Random prng = new System.Random(GetHashCode());

            //��� Ȯ���� ���� �׸��� �����˴ϴ�.
            for (int i = 0; i < harvestDrops.Length; i++) 
            {
                HarvestDrop drop = harvestDrops[i];
                if (prng.NextDouble() <= drop.chance)
                    Utils.InstantiateItemCollector(drop.itemToDrop, transform.position + (transform.forward / 2));
            }

            //��Ȯ �� ��� �׸��� �����Ǹ� ��Ȯ ������ ��ü�� �ı��˴ϴ�.
            StartCoroutine(Utils.TweenScaleOut(gameObject, 40, true));
        }
    }


    //�� ���������� ��Ȯ�� ������ ��Ӱ� ��� ������ ���ԵǾ� �ֽ��ϴ�.
    [System.Serializable]
    public struct HarvestDrop
    {
        public Item itemToDrop;
        [Range(0, 1)]
        public float chance;
    }
}

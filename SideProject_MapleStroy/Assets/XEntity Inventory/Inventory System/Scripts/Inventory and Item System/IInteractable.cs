namespace XEntity.InventoryItemSystem
{
    //���ͷ��Ͱ� ��ȣ �ۿ��� �� �ִ� ��� ��ü�� ���� �⺻ Ŭ�����Դϴ�.
    public interface IInteractable
    {
        //���ͷ��Ͱ� ��ü�� ��ȣ�ۿ��� �õ��� �� �� �޼��带 ȣ���մϴ�.
        //Interactor���� ���޵� ���� ��ȣ�ۿ��� �õ��ϴ� ��ȣ�ۿ����Դϴ�.
        public void OnClickInteract(Interactor interactor);
    }
}

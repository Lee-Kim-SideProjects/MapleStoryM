namespace XEntity.InventoryItemSystem
{
    //인터랙터가 상호 작용할 수 있는 모든 개체에 대한 기본 클래스입니다.
    public interface IInteractable
    {
        //인터랙터가 개체와 상호작용을 시도할 때 이 메서드를 호출합니다.
        //Interactor에서 전달된 것은 상호작용을 시도하는 상호작용자입니다.
        public void OnClickInteract(Interactor interactor);
    }
}

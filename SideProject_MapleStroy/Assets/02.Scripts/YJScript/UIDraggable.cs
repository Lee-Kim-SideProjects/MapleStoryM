using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    //UI �巡�� �����ϰ� ���ִ� ��ũ��Ʈ
    
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 offset;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // ���콺 Ŭ�� �� UI�� ���� ��ġ�� ���콺 ��ġ�� ���̸� ����Ͽ� offset���� ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform == null || canvas == null)
            return;

        // ���콺 �巡�� �� UI�� �̵�
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out newPos);
        rectTransform.position = rectTransform.parent.TransformPoint(newPos - offset);
    }
}

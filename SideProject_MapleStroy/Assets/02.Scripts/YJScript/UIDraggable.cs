using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    //UI 드래그 가능하게 해주는 스크립트
    
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
        // 마우스 클릭 시 UI의 현재 위치와 마우스 위치의 차이를 계산하여 offset으로 저장
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform == null || canvas == null)
            return;

        // 마우스 드래그 시 UI를 이동
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out newPos);
        rectTransform.position = rectTransform.parent.TransformPoint(newPos - offset);
    }
}

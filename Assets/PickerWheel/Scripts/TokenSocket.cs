using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenSocket : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그된 오브젝트를 가져옴
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            // 드래그된 오브젝트를 DropZone의 하위 오브젝트로 설정
            draggedObject.transform.SetParent(transform);

            // 드래그된 오브젝트의 위치를 부모에 맞게 초기화
            RectTransform draggedRectTransform = draggedObject.GetComponent<RectTransform>();
            /*draggedRectTransform.anchoredPosition = Vector2.zero;*/
        }
    }
}

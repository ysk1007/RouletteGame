using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenSocket : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // �巡�׵� ������Ʈ�� ������
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            // �巡�׵� ������Ʈ�� DropZone�� ���� ������Ʈ�� ����
            draggedObject.transform.SetParent(transform);

            // �巡�׵� ������Ʈ�� ��ġ�� �θ� �°� �ʱ�ȭ
            RectTransform draggedRectTransform = draggedObject.GetComponent<RectTransform>();
            /*draggedRectTransform.anchoredPosition = Vector2.zero;*/
        }
    }
}

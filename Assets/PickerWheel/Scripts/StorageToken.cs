using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageToken : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // �巡�׵� ������Ʈ�� ������
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            switch (draggedObject.tag)
            {
                case "outsideToken":
                    if (draggedObject.GetComponent<TokenUi>().Piece != null)
                        draggedObject.GetComponent<TokenUi>().Piece.outside_token.tokenType = OutSideToken.Type.EmptyToken;
                    break;
                case "insideToken":
                    if (draggedObject.GetComponent<TokenUi>().Piece != null)
                        draggedObject.GetComponent<TokenUi>().Piece.inside_token.tokenType = InSideToken.Type.EmptyToken;
                    break;
            }
            GameManager.instance.CostCalculation();
        }
    }
}

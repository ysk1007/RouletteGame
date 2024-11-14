using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StorageToken : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그된 오브젝트를 가져옴
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            switch (draggedObject.tag)
            {
                case "outsideToken":
                    if (draggedObject.GetComponent<TokenUi>().Piece != null)
                    {
                        // 휠에서 뺐을 때
                        if (draggedObject.GetComponent<TokenUi>().CountText == null)
                            // 토큰 추가
                            GameManager.instance.GetSetToken(draggedObject.GetComponent<TokenUi>().Piece.outside_token.tokenType, 1);

                        draggedObject.GetComponent<TokenUi>().Piece.outside_token.tokenType = OutSideToken.Type.EmptyToken;
                    }
                    break;
                case "insideToken":
                    if (draggedObject.GetComponent<TokenUi>().Piece != null)
                    {
                        // 휠에서 뺐을 때
                        if (draggedObject.GetComponent<TokenUi>().CountText == null)
                            // 토큰 추가
                            GameManager.instance.GetSetToken(draggedObject.GetComponent<TokenUi>().Piece.inside_token.tokenType, 1);

                        draggedObject.GetComponent<TokenUi>().Piece.inside_token.tokenType = InSideToken.Type.EmptyToken;
                    }
                    break;
            }
            GameManager.instance.CostCalculation();
        }
    }
}

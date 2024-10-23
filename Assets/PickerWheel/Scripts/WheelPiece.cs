using TMPro;
using UnityEngine ;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EasyUI.PickerWheelUI {

    [System.Serializable]
   public class WheelPiece : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        [Tooltip("Token Type")] public OutSideToken outside_token;

        [SerializeField] private Image outside_token_image;
        [SerializeField] private TextMeshProUGUI outside_token_label;
        [SerializeField] private TokenUi outside_token_ui;

        [SerializeField]
        [Tooltip("Token Type")] public InSideToken inside_token;

        [SerializeField] private Image inside_token_image;
        [SerializeField] private TextMeshProUGUI inside_token_label;
        [SerializeField] private TokenUi inside_token_ui;

        [Tooltip ("Probability in %")] 
        [Range (0f, 100f)]
        [HideInInspector] public float Chance = 100f ;

        [SerializeField] public int Index ;
        [HideInInspector] public double _weight = 0f ;

        private void LateUpdate()
        {
            TokenSetting();
        }

        private void OnValidate()
        {
            //TokenSetting();
        }

        public void OnDrop(PointerEventData eventData)
        {
            // 드래그된 오브젝트를 가져옴
            GameObject draggedObject = eventData.pointerDrag;

            if (draggedObject != null)
            {
                /*// 드래그된 오브젝트를 DropZone의 하위 오브젝트로 설정
                draggedObject.transform.SetParent(transform);*/

                // 드래그된 오브젝트의 위치를 부모에 맞게 초기화
                switch (draggedObject.tag)
                {
                    case "outsideToken":
                        if (draggedObject.GetComponent<TokenUi>().Piece != null)
                            draggedObject.GetComponent<TokenUi>().Piece.outside_token.tokenType = outside_token.tokenType;
                        outside_token.tokenType = FollowingToken.instance.OutSideToken.tokenType;
                        break;
                    case "insideToken":
                        if (draggedObject.GetComponent<TokenUi>().Piece != null)
                            draggedObject.GetComponent<TokenUi>().Piece.inside_token.tokenType = inside_token.tokenType;
                        inside_token.tokenType = FollowingToken.instance.InSideToken.tokenType;
                        break;
                }
                if (draggedObject.GetComponent<TokenUi>().Piece != null)
                    draggedObject.GetComponent<TokenUi>().Piece.TokenSetting();
                TokenSetting();
                GameManager.instance.CostCalculation();
            }
        }

        public void TokenSetting()
        {
            outside_token.TokenSetting(outside_token_image, outside_token_label);
            outside_token_ui.OutSideToken = outside_token.tokenType;

            inside_token.TokenSetting(inside_token_image, inside_token_label);
            inside_token_ui.InSideToken = inside_token.tokenType;
        }
    }
}

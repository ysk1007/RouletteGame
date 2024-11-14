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
                // 드래그된 오브젝트의 위치를 부모에 맞게 초기화
                switch (draggedObject.tag)
                {
                    case "outsideToken":
                        // 드래그 시작 오브젝트가 이미 토큰이 있으면
                        if (draggedObject.GetComponent<TokenUi>().Piece != null)
                            // 현재 가지고 있는 토큰을 주고
                            draggedObject.GetComponent<TokenUi>().Piece.outside_token.tokenType = outside_token.tokenType;
                        // 토큰을 받음
                        outside_token.tokenType = FollowingToken.instance.OutSideToken.tokenType;

                        // 인벤토리에서 꺼냈을 때
                        if (draggedObject.GetComponent<TokenUi>().CountText != null)
                            GameManager.instance.GetSetToken(outside_token.tokenType, -1);
                        break;
                    case "insideToken":
                        // 드래그 시작 오브젝트가 이미 토큰이 있으면
                        if (draggedObject.GetComponent<TokenUi>().Piece != null)
                            // 현재 가지고 있는 토큰을 주고
                            draggedObject.GetComponent<TokenUi>().Piece.inside_token.tokenType = inside_token.tokenType;
                        // 토큰을 받음
                        inside_token.tokenType = FollowingToken.instance.InSideToken.tokenType;

                        // 인벤토리에서 꺼냈을 때
                        if (draggedObject.GetComponent<TokenUi>().CountText != null)
                            GameManager.instance.GetSetToken(inside_token.tokenType, -1);
                        break;
                }
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

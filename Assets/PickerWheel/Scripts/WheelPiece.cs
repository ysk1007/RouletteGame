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

        [SerializeField]
        [Tooltip("Token Type")] public InSideToken inside_token;

        [SerializeField] private Image inside_token_image;
        [SerializeField] private TextMeshProUGUI inside_token_label;

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
                        outside_token.tokenType = draggedObject.GetComponent<Draggable>().GetOutSideToken;
                        break;
                    case "insideToken":
                        inside_token.tokenType = draggedObject.GetComponent<Draggable>().GetInSideToken;
                        break;
                }
            }
        }

        public void TokenSetting()
        {
            outside_token.TokenSetting(outside_token_image, outside_token_label);
            inside_token.TokenSetting(inside_token_image, inside_token_label);
        }
    }
}

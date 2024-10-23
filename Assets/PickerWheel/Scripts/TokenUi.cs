using EasyUI.PickerWheelUI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas; // 캔버스 참조 (UI 드래그 시 필요)

    [SerializeField]
    private WheelPiece piece;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private OutSideToken.Type outside_token;

    [SerializeField]
    private InSideToken.Type inside_token;

    public OutSideToken.Type OutSideToken
    {
        get => outside_token;
        set => outside_token = value;
    }

    public InSideToken.Type InSideToken
    {
        get => inside_token;
        set => inside_token = value;
    }

    public WheelPiece Piece
    {
        get => piece;
    }


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // 캔버스 가져오기 (드래그 대상이 UI일 경우 필요)
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(gameObject.tag == "outsideToken" && outside_token.GetHashCode() == 0) return;
        if(gameObject.tag == "insideToken" && inside_token.GetHashCode() == 0) return;

        canvasGroup.alpha = 0.6f;  // UI의 투명도를 살짝 낮춤

        canvasGroup.blocksRaycasts = false; // 드래그 시작 시 오브젝트가 상호작용할 수 없도록 설정

        FollowingToken.instance.gameObject.tag = gameObject.tag;
        FollowingToken.instance.OutSideToken.tokenType = outside_token;
        FollowingToken.instance.InSideToken.tokenType = inside_token;
        FollowingToken.instance.Setting(transform.position);
        FollowingToken.instance.transform.localScale = Vector3.one;
    }

    public void OnDrag(PointerEventData eventData)
    {
        FollowingToken.instance.FollowingMouse(eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;  // 투명도를 원래대로 되돌림
        // 드래그 종료 시 다시 상호작용할 수 있도록 설정
        canvasGroup.blocksRaycasts = true;
        FollowingToken.instance.init();
    }
}

using EasyUI.PickerWheelUI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas; // ĵ���� ���� (UI �巡�� �� �ʿ�)

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

        // ĵ���� �������� (�巡�� ����� UI�� ��� �ʿ�)
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(gameObject.tag == "outsideToken" && outside_token.GetHashCode() == 0) return;
        if(gameObject.tag == "insideToken" && inside_token.GetHashCode() == 0) return;

        canvasGroup.alpha = 0.6f;  // UI�� ������ ��¦ ����

        canvasGroup.blocksRaycasts = false; // �巡�� ���� �� ������Ʈ�� ��ȣ�ۿ��� �� ������ ����

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
        canvasGroup.alpha = 1f;  // ������ ������� �ǵ���
        // �巡�� ���� �� �ٽ� ��ȣ�ۿ��� �� �ֵ��� ����
        canvasGroup.blocksRaycasts = true;
        FollowingToken.instance.init();
    }
}

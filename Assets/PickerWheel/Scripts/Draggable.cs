using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas; // ĵ���� ���� (UI �巡�� �� �ʿ�)
    [SerializeField] private Vector3 startPos;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private OutSideToken.Type outside_token;

    [SerializeField]
    private InSideToken.Type inside_token;

    public OutSideToken.Type GetOutSideToken
    {
        get => outside_token;
    }

    public InSideToken.Type GetInSideToken
    {
        get => inside_token;
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
        // �巡�� ���� �� ������Ʈ�� ��ȣ�ۿ��� �� ������ ����
        canvasGroup.blocksRaycasts = false;
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� ������Ʈ �̵�
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� �ٽ� ��ȣ�ۿ��� �� �ֵ��� ����
        canvasGroup.blocksRaycasts = true;
        transform.position = startPos;
    }
}

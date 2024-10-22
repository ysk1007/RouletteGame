using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas; // 캔버스 참조 (UI 드래그 시 필요)
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

        // 캔버스 가져오기 (드래그 대상이 UI일 경우 필요)
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 오브젝트가 상호작용할 수 없도록 설정
        canvasGroup.blocksRaycasts = false;
        startPos = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중일 때 오브젝트 이동
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 다시 상호작용할 수 있도록 설정
        canvasGroup.blocksRaycasts = true;
        transform.position = startPos;
    }
}

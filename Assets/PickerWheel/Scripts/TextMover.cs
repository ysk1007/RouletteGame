using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMover : MonoBehaviour
{
    public ObjectPool objectPool; // 오브젝트 풀 참조

    private void OnEnable()
    {
        if(!objectPool)
            objectPool = ObjectPool.instance;
    }

    public void AnimateScore(Vector3 startPosition, Vector3 targetPosition,int score)
    {
        TextMeshProUGUI textComponent = this.GetComponent<TextMeshProUGUI>();
        textComponent.text = score.ToString();
        textComponent.color = (score > 0) ? Color.green : Color.red;
        this.transform.position = startPosition;

        // 애니메이션
        this.transform.DOMove(targetPosition, 1f)
            .OnComplete(() =>
            {
                Debug.Log("도착");
                // 애니메이션 완료 후 텍스트 오브젝트를 풀로 반환
                objectPool.ReturnObject(gameObject);
            });
        
    }
}

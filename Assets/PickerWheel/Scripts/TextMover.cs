using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMover : MonoBehaviour
{
    public ObjectPool objectPool; // ������Ʈ Ǯ ����

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

        // �ִϸ��̼�
        this.transform.DOMove(targetPosition, 1f)
            .OnComplete(() =>
            {
                Debug.Log("����");
                // �ִϸ��̼� �Ϸ� �� �ؽ�Ʈ ������Ʈ�� Ǯ�� ��ȯ
                objectPool.ReturnObject(gameObject);
            });
        
    }
}

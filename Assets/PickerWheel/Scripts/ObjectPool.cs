using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject textPrefab; // 텍스트 프리팹
    public int poolSize = 10; // 초기 풀 크기

    [SerializeField]
    private Transform[] scoreText;

    [SerializeField]
    private List<GameObject> pool;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 풀 초기화
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewTextObject(); // 초기 텍스트 오브젝트 생성
        }
    }

    public GameObject GetObject(Vector3 startPosition, int targetIndex, int score)
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true); // 활성화
                obj.transform.GetComponent<TextMover>().AnimateScore(startPosition, scoreText[targetIndex].transform.position, score);
                return obj;
            }
        }

        // 사용할 수 있는 오브젝트가 없으면 새로 생성
        return CreateNewTextObject();
    }

    private GameObject CreateNewTextObject()
    {
        GameObject obj = Instantiate(textPrefab, transform); // 부모 아래에 생성
        obj.SetActive(false); // 비활성화 상태로 시작
        pool.Add(obj);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 비활성화
        obj.transform.SetParent(transform); // 부모를 ObjectPool로 설정
    }
}

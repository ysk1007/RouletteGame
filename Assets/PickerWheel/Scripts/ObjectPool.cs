using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject textPrefab; // �ؽ�Ʈ ������
    public int poolSize = 10; // �ʱ� Ǯ ũ��

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
        // Ǯ �ʱ�ȭ
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewTextObject(); // �ʱ� �ؽ�Ʈ ������Ʈ ����
        }
    }

    public GameObject GetObject(Vector3 startPosition, int targetIndex, int score)
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true); // Ȱ��ȭ
                obj.transform.GetComponent<TextMover>().AnimateScore(startPosition, scoreText[targetIndex].transform.position, score);
                return obj;
            }
        }

        // ����� �� �ִ� ������Ʈ�� ������ ���� ����
        return CreateNewTextObject();
    }

    private GameObject CreateNewTextObject()
    {
        GameObject obj = Instantiate(textPrefab, transform); // �θ� �Ʒ��� ����
        obj.SetActive(false); // ��Ȱ��ȭ ���·� ����
        pool.Add(obj);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // ��Ȱ��ȭ
        obj.transform.SetParent(transform); // �θ� ObjectPool�� ����
    }
}

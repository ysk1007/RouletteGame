using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

public class FollowingToken : MonoBehaviour
{
    public static FollowingToken instance;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TextMeshProUGUI token_label;

    [SerializeField]
    private Image token_image;

    [SerializeField] private OutSideToken outSideToken;

    [SerializeField] private InSideToken inSideToken;

    [SerializeField] private Animator descAnimator;
    [SerializeField] private TextMeshProUGUI cost_text;
    [SerializeField] private TextMeshProUGUI desc_text;

    [SerializeField] private GameObject storageTokenZone;

    public OutSideToken OutSideToken
    {
        get => outSideToken;
    }

    public InSideToken InSideToken
    {
        get => inSideToken;
    }


    private void Awake()
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FollowingMouse(Vector2 vc2)
    {
        rectTransform.anchoredPosition += vc2;
    }

    public void Setting(Vector3 pos)
    {
        transform.position = pos;

        if (gameObject.tag == "insideToken")
        {
            inSideToken.TokenSetting(token_image, token_label);
            inSideToken.TokenDesc(cost_text,desc_text);
        }
        else if (gameObject.tag == "outsideToken")
        {
            outSideToken.TokenSetting(token_image, token_label);
            outSideToken.TokenDesc(cost_text, desc_text);
        }

        descAnimator.SetTrigger("Show");

        storageTokenZone.SetActive(true);
    }

    public void init()
    {
        transform.localScale = Vector3.zero;
        outSideToken.tokenType = OutSideToken.Type.EmptyToken;
        inSideToken.tokenType = InSideToken.Type.EmptyToken;

        storageTokenZone.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        GameScore, CoinCount, addScore, multiScore, subScore, diviScore
    }

    public InfoType type;

    TextMeshProUGUI thisText;
    Image thisimage;

    private void Awake()
    {
        thisText = GetComponent<TextMeshProUGUI>();
        thisimage = GetComponent<Image>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.GameScore:
                thisText.text = GameManager.instance.GameScore.ToString();
                break;
            case InfoType.CoinCount:
                break;
            case InfoType.addScore:
                thisText.text = "+" + GameManager.instance.AddScore.ToString();
                break;
            case InfoType.multiScore:
                thisText.text = "x" + GameManager.instance.MultiScore.ToString();
                break;
            case InfoType.subScore:
                thisText.text = "-" + GameManager.instance.SubScore.ToString();
                break;
            case InfoType.diviScore:
                thisText.text = "/" + GameManager.instance.DiviScore.ToString();
                break;
            default:
                break;
        }
    }
}

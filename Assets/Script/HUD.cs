using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        GameScore, CoinCount, addScore, multiScore, subScore, diviScore, spinPower
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
                thisText.text = GameManager.instance.GameScore.ToString("F0");
                break;
            case InfoType.CoinCount:
                break;
            case InfoType.addScore:
                thisText.text = (GameManager.instance.AddScore > 0) ? " + " + GameManager.instance.AddScore.ToString("F0") : "";
                break;
            case InfoType.multiScore:
                thisText.text = (GameManager.instance.MultiScore > 1) ? " x " + GameManager.instance.MultiScore.ToString("F1") : "";
                break;
            case InfoType.subScore:
                thisText.text = (GameManager.instance.SubScore < 0) ? " - " + (GameManager.instance.SubScore * -1).ToString("F0") : "";
                break;
            case InfoType.diviScore:
                thisText.text = (GameManager.instance.DiviScore > 1) ? " ¡À " + GameManager.instance.DiviScore.ToString("F1") : "";
                break;
            case InfoType.spinPower:
                thisText.text = string.Format("{0} ~ {1}", PickerWheel.instance.spinPower/ 20 - 1, PickerWheel.instance.spinPower / 20 + 1);
                break;
            default:
                break;
        }
    }
}

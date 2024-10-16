using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Token
{
    public enum OutSideToken
    {
        nBetterScoreToken = -3, nSpeedToken, nScoreToken,
        EmptyToken = 0,
        pScoreToken, pSpeedToken, pSideToken, pBetterScoreToken
    }

    public enum InSideToken
    {
        nBonusSpinToken = -4, nInvalidityToken, nPercentToken, nScoreToken,
        EmptyToken = 0,
        pScoreToken, pPercentToken, pCopyToken, pInvalidityToken, pBonusSpinToken
    }

    public OutSideToken outSideToken;
    public InSideToken inSideToken;

    public void TokenSetting(Image outSideTokenImage,Image inSideTokenImage, TextMeshProUGUI outSideLabel, TextMeshProUGUI inSideLabel)
    {
        outSideTokenImage.sprite = (outSideToken.GetHashCode() == 0) ? PickerWheel.instance.tokenSprites[0] :  (outSideToken.GetHashCode() < 0 ? PickerWheel.instance.tokenSprites[1] : PickerWheel.instance.tokenSprites[2]);
        inSideTokenImage.sprite = (inSideToken.GetHashCode() == 0) ? PickerWheel.instance.tokenSprites[0] : (outSideToken.GetHashCode() < 0 ? PickerWheel.instance.tokenSprites[1] : PickerWheel.instance.tokenSprites[2]);
        switch (outSideToken)
        {
            case OutSideToken.nBetterScoreToken:
            case OutSideToken.pBetterScoreToken:
                outSideLabel.text = "0~9";
                break;
            case OutSideToken.nSpeedToken:
            case OutSideToken.pSpeedToken:
                outSideLabel.text = "<<";
                break;
            case OutSideToken.nScoreToken:
            case OutSideToken.pScoreToken:
                outSideLabel.text = "10~30";
                break;
            case OutSideToken.EmptyToken:
                outSideLabel.text = "";
                break;
            case OutSideToken.pSideToken:
                outSideLabel.text = "<!>";
                break;
            default:
                break;
        }

        switch (inSideToken)
        {
            case InSideToken.nBonusSpinToken:
                inSideLabel.text = "-$";
                break;
            case InSideToken.nInvalidityToken:
                inSideLabel.text = "NULL";
                break;
            case InSideToken.nPercentToken:
            case InSideToken.pPercentToken:
                inSideLabel.text = "1.0~9.0";
                break;
            case InSideToken.nScoreToken:
            case InSideToken.pScoreToken:
                inSideLabel.text = "100~999";
                break;
            case InSideToken.EmptyToken:
                inSideLabel.text = "";
                break;
            case InSideToken.pCopyToken:
                inSideLabel.text = "#=>";
                break;
            case InSideToken.pInvalidityToken:
                inSideLabel.text = "!N";
                break;
            case InSideToken.pBonusSpinToken:
                inSideLabel.text = "+$";
                break;
            default:
                break;
        }
    }

    public void OutSideTokenSocre()
    {
        switch (outSideToken)
        {
            case OutSideToken.nBetterScoreToken:
                GameManager.instance.AddScore -= Random.Range(-10, -31);
                break;
            case OutSideToken.nSpeedToken:
                break;
            case OutSideToken.nScoreToken:
                GameManager.instance.AddScore -= Random.Range(0, 10);
                break;
            case OutSideToken.EmptyToken:
                break;
            case OutSideToken.pScoreToken:
                GameManager.instance.AddScore += Random.Range(0, 10);
                break;
            case OutSideToken.pSpeedToken:
                break;
            case OutSideToken.pSideToken:
                break;
            case OutSideToken.pBetterScoreToken:
                GameManager.instance.AddScore += Random.Range(10, 31);
                break;
            default:
                break;
        }
    }
}

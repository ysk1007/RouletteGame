using EasyUI.PickerWheelUI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

[System.Serializable]
public class InSideToken : Token
{
    public enum Type
    {
        nBonusSpinToken = -4, nInvalidityToken, nPercentToken, nScoreToken,
        EmptyToken = 0,
        pScoreToken, pPercentToken, pCopyToken, pInvalidityToken, pBonusSpinToken
    }

    public Type tokenType;

    public override void TokenSetting(Image tokenImage, TextMeshProUGUI tokenLabel)
    {
        tokenImage.sprite = (tokenType.GetHashCode() == 0) ? PickerWheel.instance.inSide_tokenSprites[0] :
                            (tokenType.GetHashCode() < 0 ? PickerWheel.instance.inSide_tokenSprites[1] : PickerWheel.instance.inSide_tokenSprites[2]);

        switch (tokenType)
        {
            case Type.nBonusSpinToken:
                tokenLabel.text = "-$";
                break;
            case Type.nInvalidityToken:
                tokenLabel.text = "NULL";
                break;
            case Type.nPercentToken:
            case Type.pPercentToken:
                tokenLabel.text = "1.0~9.0";
                break;
            case Type.nScoreToken:
            case Type.pScoreToken:
                tokenLabel.text = "100~999";
                break;
            case Type.EmptyToken:
                tokenLabel.text = "";
                break;
            case Type.pCopyToken:
                tokenLabel.text = "#=>";
                break;
            case Type.pInvalidityToken:
                tokenLabel.text = "!N";
                break;
            case Type.pBonusSpinToken:
                tokenLabel.text = "+$";
                break;
            default:
                break;
        }
    }

    public override ScoreCalculationType CalculateScore()
    {
        float score = 0;
        ScoreType scoreType = ScoreType.Add;
        ScoreCalculationType calculationType = ScoreCalculationType.BasicCalculation;

        switch (tokenType)
        {
            case Type.nBonusSpinToken:
                // Decrease spin opportunities
                break;
            case Type.nInvalidityToken:
                return ScoreCalculationType.invalidityScore;
            case Type.nPercentToken:
                score = Mathf.Round(Random.Range(1f, 9f) * 10f) / 10f;
                scoreType = ScoreType.Division;
                GameManager.instance.DiviScore = score;
                break;
            case Type.nScoreToken:
                score = Random.Range(-100, -999);
                GameManager.instance.SubScore = score;
                scoreType = ScoreType.Sub;
                break;
            case Type.EmptyToken:
                break;
            case Type.pScoreToken:
                score = Random.Range(100, 999);
                GameManager.instance.AddScore = score;
                scoreType = ScoreType.Add;
                break;
            case Type.pPercentToken:
                score = Mathf.Round(Random.Range(1f, 9f) * 10f) / 10f;
                GameManager.instance.MultiScore = score;
                scoreType = ScoreType.Multiple;
                break;
            case Type.pCopyToken:
                // Copy left token effect
                break;
            case Type.pInvalidityToken:
                calculationType = ScoreCalculationType.disregardNegativity;
                break;
            case Type.pBonusSpinToken:
                // Increase spin opportunities
                break;
            default:
                break;
        }

        ObjectPool.instance.GetObject(PickerWheel.instance.transform.position, scoreType, score);
        return calculationType;
    }

    public override void TokenScore(int currentPieceIndex = default, HashSet<int> visitedIndexes = null)
    {
        // Implement if necessary for InSideToken
    }
}
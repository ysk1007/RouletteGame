using EasyUI.PickerWheelUI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

[System.Serializable]
public class OutSideToken : Token
{
    public enum Type
    {
        nBetterScoreToken = -3, nSpeedToken, nScoreToken,
        EmptyToken = 0,
        pScoreToken, pSpeedToken, pSideToken, pBetterScoreToken
    }

    public Type tokenType;

    public override void TokenSetting(Image tokenImage, TextMeshProUGUI tokenLabel)
    {
        tokenImage.sprite = (tokenType.GetHashCode() == 0) ? PickerWheel.instance.outSide_tokenSprites[0] :
                            (tokenType.GetHashCode() < 0 ? PickerWheel.instance.outSide_tokenSprites[1] : PickerWheel.instance.outSide_tokenSprites[2]);

        switch (tokenType)
        {
            case Type.nBetterScoreToken:
            case Type.pBetterScoreToken:
                tokenLabel.text = "10~30";
                break;
            case Type.nSpeedToken:
            case Type.pSpeedToken:
                tokenLabel.text = "<<";
                break;
            case Type.nScoreToken:
            case Type.pScoreToken:
                tokenLabel.text = "1~9";
                break;
            case Type.EmptyToken:
                tokenLabel.text = "";
                break;
            case Type.pSideToken:
                tokenLabel.text = "<!>";
                break;
            default:
                break;
        }
    }

    public override void TokenScore(int currentPieceIndex = default, HashSet<int> visitedIndexes = null)
    {
        if (visitedIndexes == null)
        {
            visitedIndexes = new HashSet<int>();
        }

        if (visitedIndexes.Contains(currentPieceIndex))
        {
            return;
        }

        visitedIndexes.Add(currentPieceIndex);
        int score = 0;

        switch (tokenType)
        {
            case Type.nBetterScoreToken:
                score = Random.Range(-10, -31);
                GameManager.instance.SubScore = score;
                break;
            case Type.nScoreToken:
                score = Random.Range(-1, -10);
                GameManager.instance.SubScore = score;
                break;
            case Type.pScoreToken:
                score = Random.Range(1, 10);
                GameManager.instance.AddScore = score;
                break;
            case Type.pSideToken:
                // Handle side tokens (recursive logic for adjacent tokens)
                int leftIndex = currentPieceIndex - 1 >= 0 ? currentPieceIndex - 1 : PickerWheel.instance.wheelPieces.Length - 1;
                int rightIndex = currentPieceIndex + 1 > PickerWheel.instance.wheelPieces.Length - 1 ? 0 : currentPieceIndex + 1;

                PickerWheel.instance.wheelPieces[leftIndex].outside_token.TokenScore(leftIndex, visitedIndexes);
                PickerWheel.instance.wheelPieces[rightIndex].outside_token.TokenScore(rightIndex, visitedIndexes);

                PickerWheel.instance.scoreTexts[currentPieceIndex].text = "< ! >";
                PickerWheel.instance.scoreAnimators[currentPieceIndex].SetTrigger("Show");
                return;
            case Type.pBetterScoreToken:
                score = Random.Range(10, 31);
                GameManager.instance.AddScore = score;
                break;
            default:
                break;
        }

        PickerWheel.instance.scoreTexts[currentPieceIndex].text = score.ToString();
        PickerWheel.instance.scoreTexts[currentPieceIndex].color = (score >= 0) ? Color.green : Color.red;
        PickerWheel.instance.scoreAnimators[currentPieceIndex].SetTrigger("Show");
    }

    public override ScoreCalculationType CalculateScore()
    {
        // Implement if necessary for OutSideToken
        return ScoreCalculationType.BasicCalculation;
    }
}

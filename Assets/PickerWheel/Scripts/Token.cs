using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static GameManager;

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
        outSideTokenImage.sprite = (outSideToken.GetHashCode() == 0) ? PickerWheel.instance.outSide_tokenSprites[0] :  (outSideToken.GetHashCode() < 0 ? PickerWheel.instance.outSide_tokenSprites[1] : PickerWheel.instance.outSide_tokenSprites[2]);
        inSideTokenImage.sprite = (inSideToken.GetHashCode() == 0) ? PickerWheel.instance.inSide_tokenSprites[0] : (inSideToken.GetHashCode() < 0 ? PickerWheel.instance.inSide_tokenSprites[1] : PickerWheel.instance.inSide_tokenSprites[2]);
        switch (outSideToken)
        {
            case OutSideToken.nBetterScoreToken:
            case OutSideToken.pBetterScoreToken:
                outSideLabel.text = "10~30";
                break;
            case OutSideToken.nSpeedToken:
            case OutSideToken.pSpeedToken:
                outSideLabel.text = "<<";
                break;
            case OutSideToken.nScoreToken:
            case OutSideToken.pScoreToken:
                outSideLabel.text = "1~9";
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

    public void OutSideTokenSocre(int currentPieceIndex = default(int), HashSet<int> visitedIndexes = null)
    {
        if (visitedIndexes == null)
        {
            visitedIndexes = new HashSet<int>();
        }

        // �̹� �湮�� �ε������� Ȯ��
        if (visitedIndexes.Contains(currentPieceIndex))
        {
            return; // �̹� �湮�� �ε����� ����
        }

        visitedIndexes.Add(currentPieceIndex); // ���� �ε��� �߰�

        int score = 0;

        // ������ ��ġ ( 0 : ���ϱ� , 1 : ���ϱ� , 2 : ���� , 3 : ������ )
        int targetIndex = 0;
        switch (outSideToken)
        {
            case OutSideToken.nBetterScoreToken:
                score = Random.Range(-10, -31);
                targetIndex = 2;
                GameManager.instance.SubScore = score;
                break;
            case OutSideToken.nSpeedToken:
                break;
            case OutSideToken.nScoreToken:
                targetIndex = 2;
                score = Random.Range(-1, -10);
                GameManager.instance.SubScore = score;
                break;
            case OutSideToken.EmptyToken:
                break;
            case OutSideToken.pScoreToken:
                score = Random.Range(1, 10);
                GameManager.instance.AddScore = score;
                break;
            case OutSideToken.pSpeedToken:
                break;
            case OutSideToken.pSideToken:
                int leftIndex = currentPieceIndex - 1 >= 0 ? currentPieceIndex - 1 : PickerWheel.instance.wheelPieces.Length - 1;
                int rightIndex = currentPieceIndex + 1 > PickerWheel.instance.wheelPieces.Length - 1 ? 0 : currentPieceIndex + 1;

                // �� �� ��ū�� ���� ��� ȣ��
                PickerWheel.instance.wheelPieces[leftIndex].token.OutSideTokenSocre(leftIndex, visitedIndexes);
                PickerWheel.instance.wheelPieces[rightIndex].token.OutSideTokenSocre(rightIndex, visitedIndexes);
                PickerWheel.instance.scoreTexts[currentPieceIndex].text = "< ! >";
                PickerWheel.instance.scoreAnimators[currentPieceIndex].SetTrigger("Show");
                return;
            case OutSideToken.pBetterScoreToken:
                score = Random.Range(10, 31);
                GameManager.instance.AddScore = score;
                break;
            default:
                break;
        }

        PickerWheel.instance.scoreTexts[currentPieceIndex].text = score.ToString();
        PickerWheel.instance.scoreTexts[currentPieceIndex].color = (score >= 0) ? Color.green : Color.red;
        PickerWheel.instance.scoreAnimators[currentPieceIndex].SetTrigger("Show");
        ObjectPool.instance.GetObject(PickerWheel.instance.transform.position, targetIndex, score);
    }

    public ScoreCalculationType InSideTokenScore()
    {
        float score = 0;
        ScoreCalculationType calculationType = ScoreCalculationType.BasicCalculation;
        switch (inSideToken)
        {
            // ���� ��ȸ -1
            case InSideToken.nBonusSpinToken:
                break;

            // ���� ��ȿ
            case InSideToken.nInvalidityToken:
                return ScoreCalculationType.invalidityScore;

            // 1.0 ~ 9.0 (������)
            case InSideToken.nPercentToken:
                // 1.0���� 9.0 ������ ���� �� ����
                // �Ҽ��� ù° �ڸ��� �ݿø�
                score = Mathf.Round(Random.Range(1f, 9f) * 10f) / 10f;
                GameManager.instance.DiviScore = score;
                break;

            // -100 ~ -999
            case InSideToken.nScoreToken:
                score = Random.Range(-100, -999);
                GameManager.instance.SubScore = score;
                break;

            // �� ��ū
            case InSideToken.EmptyToken:
                break;

            // +100 ~ +999
            case InSideToken.pScoreToken:
                score = Random.Range(100, 999);
                GameManager.instance.AddScore = score;
                break;

            // +1.0 ~ +9.0 (���ϱ�)
            case InSideToken.pPercentToken:
                // 1.0���� 9.0 ������ ���� �� ����
                // �Ҽ��� ù° �ڸ��� �ݿø�
                score = Mathf.Round(Random.Range(1f, 9f) * 10f) / 10f;
                GameManager.instance.MultiScore = score;
                break;

            // ���� ��ū ȿ�� ����
            case InSideToken.pCopyToken:
                break;

            // ���� ��ȿ
            case InSideToken.pInvalidityToken:
                calculationType = ScoreCalculationType.disregardNegativity;
                break;

            // ���� ��ȸ +1
            case InSideToken.pBonusSpinToken:
                break;
            default:
                break;
        }
        return calculationType;
    }

}

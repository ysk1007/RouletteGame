using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 점수 계산 방식
    public enum ScoreCalculationType
    {
        // 점수 무효 ( 0점 처리 )
        invalidityScore = -1,

        // 기본 계산
        BasicCalculation = 0,

        // 부정 무시
        disregardNegativity = 1
    }

    public static GameManager instance;

    [Header("GameScore :")]
    [Space]
    [SerializeField] private float gameScore;
    [Space]
    [SerializeField] private float addScore;
    [SerializeField] private float multiScore;
    [SerializeField] private float subScore;
    [SerializeField] private float diviScore;

    private int spinChance; // 남은 스핀 횟수

    //[Header("PieceCost :")]
    private int positiveCost;
    private int negativeCost;

    [Header("Ui :")]
    [Space]
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private TextMeshProUGUI uiSpinButtonText;

    [Header("PickerWheel :")]
    [Space]
    [SerializeField] private PickerWheel pickerWheel;

    [Space]
    [Header("User Tokens")]
    [Header("inside_Token :")]
    [SerializeField] private int nBonusSpin_inside_Token;
    [SerializeField] private int nInvalidity_inside_Token;
    [SerializeField] private int nPercent_inside_Token;
    [SerializeField] private int nScore_inside_Token;
    [SerializeField] private int pScore_inside_Token;
    [SerializeField] private int pPercent_inside_Token;
    [SerializeField] private int pCopy_inside_Token;
    [SerializeField] private int pInvalidity_inside_Token;
    [SerializeField] private int pBonusSpin_inside_Token;

    [Space]
    [Header("outside_Token :")]
    [SerializeField] private int nBetterScore_outside_Token;
    [SerializeField] private int nSpeed_outside_Token;
    [SerializeField] private int nScore_outside_Token;
    [SerializeField] private int pScore_outside_Token;
    [SerializeField] private int pSpeed_outside_Token;
    [SerializeField] private int pSide_outside_Token;
    [SerializeField] private int pBetterScore_outside_Token;

    // get set 프로퍼티

    public int SpinChance
    {
        get => spinChance;
        set => spinChance = value;
    }

    public int NBonusSpinInsideToken
    {
        get => nBonusSpin_inside_Token;
        set => nBonusSpin_inside_Token = value;
    }

    public int NInvalidityInsideToken
    {
        get => nInvalidity_inside_Token;
        set => nInvalidity_inside_Token = value;
    }

    public int NPercentInsideToken
    {
        get => nPercent_inside_Token;
        set => nPercent_inside_Token = value;
    }

    public int NScoreInsideToken
    {
        get => nScore_inside_Token;
        set => nScore_inside_Token = value;
    }

    public int PScoreInsideToken
    {
        get => pScore_inside_Token;
        set => pScore_inside_Token = value;
    }

    public int PPercentInsideToken
    {
        get => pPercent_inside_Token;
        set => pPercent_inside_Token = value;
    }

    public int PCopyInsideToken
    {
        get => pCopy_inside_Token;
        set => pCopy_inside_Token = value;
    }

    public int PInvalidityInsideToken
    {
        get => pInvalidity_inside_Token;
        set => pInvalidity_inside_Token = value;
    }

    public int PBonusSpinInsideToken
    {
        get => pBonusSpin_inside_Token;
        set => pBonusSpin_inside_Token = value;
    }

    public int NBetterScoreOutsideToken
    {
        get => nBetterScore_outside_Token;
        set => nBetterScore_outside_Token = value;
    }

    public int NSpeedOutsideToken
    {
        get => nSpeed_outside_Token;
        set => nSpeed_outside_Token = value;
    }

    public int NScoreOutsideToken
    {
        get => nScore_outside_Token;
        set => nScore_outside_Token = value;
    }

    public int PScoreOutsideToken
    {
        get => pScore_outside_Token;
        set => pScore_outside_Token = value;
    }

    public int PSpeedOutsideToken
    {
        get => pSpeed_outside_Token;
        set => pSpeed_outside_Token = value;
    }

    public int PSideOutsideToken
    {
        get => pSide_outside_Token;
        set => pSide_outside_Token = value;
    }

    public int PBetterScoreOutsideToken
    {
        get => pBetterScore_outside_Token;
        set => pBetterScore_outside_Token = value;
    }


    public float GameScore
    {
        get => gameScore;
        set => gameScore = value;
    }

    public float AddScore
    {
        get => addScore;
        set => addScore += value;
    }

    public float MultiScore
    {
        get => multiScore;
        set => multiScore += value;
    }

    public float SubScore
    {
        get => subScore;
        set => subScore += value;
    }

    public float DiviScore
    {
        get => diviScore;
        set => diviScore += value;
    }

    public int PositiveCost
    {
        get => positiveCost;
        set => positiveCost += value;
    }

    public int NegativeCost
    {
        get => negativeCost;
        set => negativeCost += value;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiSpinButton.onClick.AddListener(() => {
            uiSpinButton.interactable = false;
            uiSpinButtonText.text = "Spinning!";

            pickerWheel.OnSpinStart(() =>
            {
                ResetScore();
                Debug.Log("Spin started..");
            });

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                Debug.Log("Spin end: Amount:" + wheelPiece.Index);
                gameScore += ScoreCalculation(wheelPiece.inside_token.CalculateScore());
                uiSpinButton.interactable = true;
                uiSpinButtonText.text = "Spin";
            });

            pickerWheel.Spin();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetScore()
    {
        addScore = subScore = 0;
        multiScore = diviScore = 1;
    }

    float ScoreCalculation(ScoreCalculationType type)
    {
        float score = 0;

        switch (type)
        {
            case ScoreCalculationType.invalidityScore:
                break;
            case ScoreCalculationType.BasicCalculation:
                score = (addScore - subScore) * multiScore / diviScore;
                break;
            case ScoreCalculationType.disregardNegativity:
                score = addScore * multiScore;
                break;
        }

        return score;
    }

    public void CostCalculation()
    {
        pickerWheel.spinPower = 120;
        positiveCost = 0;
        negativeCost = 0;

        for (int i = 0; i < pickerWheel.wheelPieces.Length; i++)
        {
            if (pickerWheel.wheelPieces[i]?.outside_token.tokenType.GetHashCode() >= 0)
            {
                positiveCost += pickerWheel.wheelPieces[i].outside_token.tokenType.GetHashCode();
                // 스피드 토큰에 따른 휠 속도 조절
                if (pickerWheel.wheelPieces[i].outside_token.tokenType == OutSideToken.Type.pSpeedToken) pickerWheel.spinPower += 60;
            }
            else
            {
                negativeCost += -1 * pickerWheel.wheelPieces[i].outside_token.tokenType.GetHashCode();
                // 스피드 토큰에 따른 휠 속도 조절
                if (pickerWheel.wheelPieces[i].outside_token.tokenType == OutSideToken.Type.nSpeedToken) pickerWheel.spinPower -= 60;
            }
        }

        for (int i = 0; i < pickerWheel.wheelPieces.Length; i++)
        {
            if (pickerWheel.wheelPieces[i]?.inside_token.tokenType.GetHashCode() >= 0)
            {
                positiveCost += pickerWheel.wheelPieces[i].inside_token.tokenType.GetHashCode();
            }
            else
            {
                negativeCost += -1 * pickerWheel.wheelPieces[i].inside_token.tokenType.GetHashCode();
            }
        }
    }

    public void GetSetToken(OutSideToken.Type type , int count)
    {
        switch (type)
        {
            case OutSideToken.Type.nBetterScoreToken:
                nBetterScore_outside_Token += count;
                break;
            case OutSideToken.Type.nSpeedToken:
                nSpeed_outside_Token += count;
                break;
            case OutSideToken.Type.nScoreToken:
                nScore_outside_Token += count;
                break;
            case OutSideToken.Type.EmptyToken:
                break;
            case OutSideToken.Type.pScoreToken:
                pScore_outside_Token += count;
                break;
            case OutSideToken.Type.pSpeedToken:
                pSpeed_outside_Token += count;
                break;
            case OutSideToken.Type.pSideToken:
                pSide_outside_Token += count;
                break;
            case OutSideToken.Type.pBetterScoreToken:
                pBetterScore_outside_Token += count;
                break;
            default:
                break;
        }
    }

    public void GetSetToken(InSideToken.Type type, int count)
    {
        switch (type)
        {
            case InSideToken.Type.nBonusSpinToken:
                nBonusSpin_inside_Token += count;
                break;
            case InSideToken.Type.nInvalidityToken:
                nInvalidity_inside_Token += count;
                break;
            case InSideToken.Type.nPercentToken:
                nPercent_inside_Token += count;
                break;
            case InSideToken.Type.nScoreToken:
                nScore_inside_Token += count;
                break;
            case InSideToken.Type.EmptyToken:
                break;
            case InSideToken.Type.pScoreToken:
                pScore_inside_Token += count;
                break;
            case InSideToken.Type.pPercentToken:
                pPercent_inside_Token += count;
                break;
            case InSideToken.Type.pCopyToken:
                pCopy_inside_Token += count;
                break;
            case InSideToken.Type.pInvalidityToken:
                pInvalidity_inside_Token += count;
                break;
            case InSideToken.Type.pBonusSpinToken:
                pBonusSpin_inside_Token += count;
                break;
            default:
                break;
        }
    }
}

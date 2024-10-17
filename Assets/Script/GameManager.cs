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
        invalidityScore = -1 , 

        // 기본 계산
        BasicCalculation = 0 , 

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

    [Header("Ui :")]
    [Space]
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private TextMeshProUGUI uiSpinButtonText;

    [Header("PickerWheel :")]
    [Space]
    [SerializeField] private PickerWheel pickerWheel;

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
                Debug.Log("Spin end: Amount:"+ wheelPiece.Index);
                gameScore += ScoreCalculation(wheelPiece.token.InSideTokenScore());
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
                score = (addScore - subScore) *multiScore / diviScore;
                break;
            case ScoreCalculationType.disregardNegativity:
                score = addScore * multiScore;
                break;
        }

        return score;
    }
}

using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
                Debug.Log("Spin started..");
            });

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                Debug.Log("Spin end: Amount:"+ wheelPiece.inside_Amount);
                gameScore = (gameScore + addScore - subScore) * multiScore / diviScore;
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


}

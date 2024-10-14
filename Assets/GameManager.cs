using EasyUI.PickerWheelUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button uiSpinButton;
    [SerializeField] private TextMeshProUGUI uiSpinButtonText;
    [SerializeField] private PickerWheel pickerWheel;

    private void Awake()
    {

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
                Debug.Log("Spin end: Label:"+wheelPiece.Label+ ", Amount:"+ wheelPiece.Amount);
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

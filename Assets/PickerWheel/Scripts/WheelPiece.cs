using UnityEngine ;

namespace EasyUI.PickerWheelUI {

    [System.Serializable]
   public class WheelPiece {

        [SerializeField]
        [Tooltip("Token Type")] public Token token;

        [Tooltip ("Probability in %")] 
        [Range (0f, 100f)]
        [HideInInspector] public float Chance = 100f ;

        [SerializeField] public int Index ;
        [HideInInspector] public double _weight = 0f ;
   }
}

using UnityEngine ;

namespace EasyUI.PickerWheelUI {

    [System.Serializable]
   public class WheelPiece {

        [SerializeField]
        public Token token;

        [Tooltip ("Reward amount")] public int inside_Amount;
        [Tooltip("Reward amount")]  public int outside_Amount;

        [Tooltip ("Probability in %")] 
      [Range (0f, 100f)] 
      public float Chance = 100f ;

      [HideInInspector] public int Index ;
      [HideInInspector] public double _weight = 0f ;
   }
}

using UnityEngine ;

namespace EasyUI.PickerWheelUI {
    [System.Serializable]
   public class WheelPiece {
       public enum PieceType
       {
           AddPiece, MultiPiece, SubPiece, DiviPiece
       }

      public PieceType pieceType;
      public UnityEngine.Sprite Icon ;
      public string Label ;

      [Tooltip ("Reward amount")] public int Amount ;

      [Tooltip ("Probability in %")] 
      [Range (0f, 100f)] 
      public float Chance = 100f ;

      [HideInInspector] public int Index ;
      [HideInInspector] public double _weight = 0f ;

      public void PieceScore()
        {
            switch (pieceType)
            {
                case PieceType.AddPiece:
                    GameManager.instance.AddScore = Amount;
                    break;
                case PieceType.MultiPiece:
                    GameManager.instance.MultiScore = Amount;
                    break;
                case PieceType.SubPiece:
                    GameManager.instance.SubScore = Amount;
                    break;
                case PieceType.DiviPiece:
                    GameManager.instance.DiviScore = Amount;
                    break;
                default:
                    break;
            }
        }
   }
}

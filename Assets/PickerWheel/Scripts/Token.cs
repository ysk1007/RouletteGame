using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public abstract class Token
{
    public enum ScoreType
    {
        Add = 0, Multiple, Sub, Division
    }

    public abstract void TokenSetting(Image tokenImage, TextMeshProUGUI tokenLabel);
    public abstract void TokenScore(int currentPieceIndex = default, HashSet<int> visitedIndexes = null);

    public abstract void TokenDesc(TextMeshProUGUI cost, TextMeshProUGUI desc);

    public abstract ScoreCalculationType CalculateScore();
}
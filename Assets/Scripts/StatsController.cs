using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class StatsController : MonoBehaviour
{
    public Image level;
    public TMP_Text levelText;

    public int playerLevel = 1;
    private float fillAmountNow = 0;

    public void LevelFill(float fill)
    {
        fillAmountNow += fill;

        if (fillAmountNow >= 1)
        {
            float overflow = fillAmountNow - 1;
            LevelUp();
            fillAmountNow = overflow;
        }

        level.fillAmount = fillAmountNow;
    }

    private void LevelUp()
    {
        playerLevel++;
        levelText.text = playerLevel.ToString();
    }
}

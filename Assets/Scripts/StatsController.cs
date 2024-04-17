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

    private float fill = 1.5f;
    private int playerLevel = 1;
    private bool countNow = false;
    private float fillAmountNow;
    private float fillNow;

    public void LevelFill()
    {
        if (level.fillAmount + fill >= 1)
        {
            fill = fill - level.fillAmount;
            LevelUp();
            level.fillAmount += fill;
        } else
        {
            level.fillAmount += fill;
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        levelText.text = playerLevel.ToString();
        level.fillAmount = 0;
    }
}

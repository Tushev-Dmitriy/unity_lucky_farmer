using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsController : MonoBehaviour
{
    public Image level;
    public TMP_Text levelText;

    private float fill = 0.1f;
    private int playerLevel = 1;

    public void LevelFill()
    {
        if (level.fillAmount != 1)
        {
            level.fillAmount += fill;
        }
        else
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        levelText.text = playerLevel.ToString();
    }
}

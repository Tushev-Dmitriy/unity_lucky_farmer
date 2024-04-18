using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnnounceUsing : MonoBehaviour
{
    public GameObject announceUI;
    public GameObject playerInGame;
    public PlantsUsing plantsUsing;
    public AnnouncesBlocks announcesBlocks;

    private Animation announceAnim;
    private bool announceRunning = false;

    public int[,] announceRequireAndReward = new int[2, 5] { { 3, 6, 9, 12, 15 }, { 15, 30, 45, 60, 75 } };
    public int[] rewardsNow = new int[6];
    public int[] requireNow = new int[6];

    private void Start()
    {
        announceAnim = announceUI.GetComponent<Animation>();
    }
    public void ShowAnnounceUI()
    {
        if (announceRunning)
        {
            announceAnim.Play("AnnounceBtnReverse");
            announceRunning = false;
        }
        else
        {
            announceAnim.Play("AnnounceBtn");
            announceRunning = true;
        }
    }

    public void RewardAdd()
    {
        for (int i = 0; i < 6; i++)
        {
            rewardsNow[i] = announceRequireAndReward[1, Random.Range(0, 5)];
            requireNow[i] = announceRequireAndReward[0, Random.Range(0, 5)];
        }
        announcesBlocks.SwapRequireText();
        announcesBlocks.SwapRewardText();
    }
}

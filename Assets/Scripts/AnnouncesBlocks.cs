using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnnouncesBlocks : MonoBehaviour
{
    public AnnounceUsing announceUsing;
    public GameObject requireTextPrefab;
    public GameObject rewardTextPrefab;


    private void Start()
    {
        announceUsing.RewardAdd();
    }

    public void SwapRequireText()
    {
        requireTextPrefab.GetComponent<TextMeshProUGUI>().text = $"����������: {announceUsing.requireNow[1]}";
    }

    public void SwapRewardText()
    {
        rewardTextPrefab.GetComponent<TextMeshProUGUI>().text = $"�������: {announceUsing.rewardsNow[1]}";
    }
}

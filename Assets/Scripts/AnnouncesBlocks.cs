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
        requireTextPrefab.GetComponent<TextMeshProUGUI>().text = $"Требования: {announceUsing.requireNow[1]}";
    }

    public void SwapRewardText()
    {
        rewardTextPrefab.GetComponent<TextMeshProUGUI>().text = $"Награда: {announceUsing.rewardsNow[1]}";
    }
}

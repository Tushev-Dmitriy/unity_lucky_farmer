using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnnounceUsing : MonoBehaviour
{
    public GameObject announceUI;
    public GameObject playerInGame;
    public PlantsUsing plantsUsing;
    public AnnouncesBlocks announcesBlocks;
    public GameObject BgInGame;
    public StatsController statsController;
    public GameObject announceBlockPrefab;

    private GameObject tomatoImg;
    private GameObject cabbageImg;
    private Animation announceAnim;
    private bool announceRunning = false;

    public int[] rewardsNow = new int[5];
    public int[] requireNow = new int[5];
    public char[] infoAboutItem;
    public int[] randomIndex;

    private void Start()
    {
        announceAnim = announceUI.GetComponent<Animation>();
        for (int i = 0; i < rewardsNow.Length; i++)
        {
            Debug.Log(rewardsNow[i] + " " + requireNow[i]);
        }
        int f = BgInGame.transform.childCount;
        infoAboutItem = new char[f];
        randomIndex = new int[f];   
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
        if (BgInGame.transform.childCount < 6)
        {
            Instantiate(announceBlockPrefab);
        }
        int a = BgInGame.transform.childCount;
        for (int z = 0; z < a; z++)
        {
            GameObject imgNow = BgInGame.transform.GetChild(z).gameObject;
            AnnouncesBlocks imgAnnounce = imgNow.GetComponent<AnnouncesBlocks>();
            announcesBlocks = imgAnnounce;
            int b = Random.Range(0, 5);
            randomIndex[z] = b;
            announcesBlocks.SwapRequireText(b);
            announcesBlocks.SwapRewardText(b);
            tomatoImg = imgNow.transform.GetChild(3).gameObject;
            cabbageImg = imgNow.transform.GetChild(4).gameObject;
            char aboutItem;
            if (statsController.playerLevel < 2)
            {
                tomatoImg.SetActive(true);
                aboutItem = 't';
            } else
            {
                int c = Random.Range(0, 2);
                if (c == 0)
                {
                    tomatoImg.SetActive(true);
                    aboutItem = 't';
                } else
                {
                    cabbageImg.SetActive(true);
                    aboutItem = 'c';
                }
            }
            infoAboutItem[z] = aboutItem;
        }
    }
}

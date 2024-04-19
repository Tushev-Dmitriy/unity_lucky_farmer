using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AnnounceUsing : MonoBehaviour
{
    public GameObject announceUI;
    public GameObject playerInGame;
    public PlantsUsing plantsUsing;
    public AnnouncesBlocks announcesBlocks;
    public GameObject BgInGame;
    public StatsController statsController;
    public GameObject announceBlockPrefab;
    public GameObject inventoryObj;

    private GameObject tomatoImg;
    private GameObject cabbageImg;
    private Animation announceAnim;
    private bool announceRunning = false;
    private int f = 0;

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
        f = BgInGame.transform.childCount;
        infoAboutItem = new char[f];
        randomIndex = new int[f];
        SetValue();
    }

    public void SetValue()
    {
        int bgImgChildCount = BgInGame.transform.childCount;
        for (int i = 0;i < bgImgChildCount;i++)
        {
            BgInGame.transform.GetChild(i).gameObject.GetComponent<AnnouncesBlocks>().imageController = i;
        }
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
            GameObject newImgBlock = Instantiate(announceBlockPrefab, BgInGame.transform);
            newImgBlock.GetComponent<AnnouncesBlocks>().announceUsing = gameObject.GetComponent<AnnounceUsing>();
            newImgBlock.GetComponent<AnnouncesBlocks>().inventoryManager = inventoryObj.GetComponent<InventoryManager>();
            newImgBlock.GetComponent<AnnouncesBlocks>().inventoryScript = inventoryObj.GetComponent<InventoryScript>();
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

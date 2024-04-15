using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnnounceUsing : MonoBehaviour
{
    public GameObject announceUI;
    public GameObject playerInGame;
    public PlantsUsing plantsUsing;

    private Animation announceAnim;
    private bool announceRunning = false;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnBed : MonoBehaviour
{
    private bool onEmptyBed = false;
    private bool onGrowBed = false;
    private bool onReadyBed = false;
    private bool onPlowBed = false;

    private Material bedMaterial;
    private Color whiteRed = new Color(255, 118, 118);
    private Color whiteGreen = new Color(187, 255, 187);
    private Color whiteBlue = new Color(139, 146, 255);
    
    public PlantsUsing plantUsing;

    private void Start()
    {
        GameObject terrain = GameObject.FindGameObjectWithTag("emptyBed");
        Renderer renderer = terrain.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "emptyBed")
        {
            onEmptyBed = true;
        } else if (other.gameObject.tag == "growBed")
        {
            onGrowBed = true;
        } else if (other.gameObject.tag == "readyBed")
        {
            onReadyBed = true;
        } else if (other.gameObject.tag == "plowBed")
        {
            onPlowBed = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "emptyBed")
        {
            if (plantUsing.GetBedStatus(2))
            {
                other.gameObject.tag = "growBed";
            }
        }
        else if (other.gameObject.tag == "growBed")
        {
            if (plantUsing.GetBedStatus(3))
            {
                other.gameObject.tag = "readyBed";
            }
        }
        else if (other.gameObject.tag == "readyBed")
        {
            if (plantUsing.GetBedStatus(4))
            {
                other.gameObject.tag = "plowBed";
            }
        }
        else if (other.gameObject.tag == "plowBed")
        {
            if (plantUsing.GetBedStatus(1))
            {
                other.gameObject.tag = "emptyBed";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bedMaterial = other.gameObject.GetComponent<Material>();
        if (other.gameObject.tag == "emptyBed")
        {
            onEmptyBed = false;
            bedMaterial.color = whiteBlue;
        }
        else if (other.gameObject.tag == "growBed")
        {
            onGrowBed = false;
        }
        else if (other.gameObject.tag == "readyBed")
        {
            onReadyBed = false;
        }
        else if (other.gameObject.tag == "plowBed")
        {
            onPlowBed = false;
        }
    }

    public bool GetBedStatus(int i)
    {
        switch (i)
        {
            case 1:
                return onEmptyBed;
            case 2:
                return onGrowBed;
            case 3:
                return onReadyBed;
            case 4:
                return onPlowBed;
            default:
                return false;
        }
    }
}

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
    private Color whiteOrange = new Color(1, 0.91f, 0.65f);
    private Color stockColor = new Color(0.52f, 0.39f, 0.26f);

    public PlantsUsing plantUsing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "emptyBed")
        {
            onEmptyBed = true;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = whiteOrange;
        }
        else if (other.tag == "growBed")
        {
            onGrowBed = true;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = whiteOrange;
        }
        else if (other.tag == "readyBed")
        {
            onReadyBed = true;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = whiteOrange;
        }
        else if (other.tag == "plowBed")
        {
            onPlowBed = true;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = whiteOrange;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "emptyBed")
        {
            other.tag = "growBed";
        }
        else if (other.tag == "growBed")
        {
            other.tag = "readyBed";
        }
        else if (other.tag == "readyBed")
        {
            other.tag = "plowBed";
        }
        else if (other.tag == "plowBed")
        {
            other.tag = "emptyBed";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "emptyBed")
        {
            onEmptyBed = false;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = stockColor;
        }
        else if (other.tag == "growBed")
        {
            onGrowBed = false;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = stockColor;
        }
        else if (other.tag == "readyBed")
        {
            onReadyBed = false;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = stockColor;
        }
        else if (other.tag == "plowBed")
        {
            onPlowBed = false;
            bedMaterial = other.gameObject.GetComponent<Renderer>().material;
            bedMaterial.color = stockColor;
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

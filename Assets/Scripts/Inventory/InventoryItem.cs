using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour /*IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    public Image image;
    public TMP_Text countText;

    [HideInInspector] public string title;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
        title = newItem.title;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    image.raycastTarget = false;
    //    parentAfterDrag = transform.parent;
    //    transform.SetParent(transform.root);
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.position = Input.mousePosition;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    image.raycastTarget = true;
    //    transform.SetParent(parentAfterDrag);
    //}
}

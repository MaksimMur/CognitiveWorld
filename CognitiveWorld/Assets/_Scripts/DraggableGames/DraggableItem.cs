using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentAfterDrag;
    public Image img;
    public Image Flag;
    public Text CountryName;
    public Country country;
    public void SetCountry(Country c)
    { 
        country = c;
        Flag.sprite = c.Flag;
        CountryName.text = c.CountryName;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        img.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position =Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        img.raycastTarget = true;
    }

}

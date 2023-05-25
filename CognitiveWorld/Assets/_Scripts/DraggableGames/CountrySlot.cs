using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CountrySlot : MonoBehaviour, IDropHandler
{
    public Transform slotTransform;
    public int ChildCountMax = 1;
    public Country country;
    public GameObject Result;
    public GameObject Answer;

    public Sprite right, lie;
    public void OnDrop(PointerEventData eventData)
    {
        if (slotTransform.childCount < ChildCountMax)
        { 
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = slotTransform;
            country = draggableItem.country;
        }
        else
        {
            DraggableItem itemKepped= GetComponentInChildren<DraggableItem>();

            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            itemKepped.parentAfterDrag = draggableItem.parentAfterDrag;
            itemKepped.transform.SetParent(draggableItem.parentAfterDrag);
            itemKepped.parentAfterDrag.GetComponentInParent<CountrySlot>().country = itemKepped.country;

            draggableItem.parentAfterDrag = slotTransform;
            draggableItem.transform.SetParent(slotTransform);
            country = draggableItem.country;
        }
    }
    public void SetLie()
    {
        Result.SetActive(true);
        Result.GetComponent<Image>().sprite = lie;
    }

    public void SetRight()
    {
        Result.SetActive(true);
        Result.GetComponent<Image>().sprite = right;
    }

    public void SetDefault()
    {
        Result.SetActive(false);
        country = null;
    }

    public void SetActiveAnswer(bool active, ChooseModeGame mode =ChooseModeGame.None)
    {
        if (active)
        {
            SetAnswer(mode);
        }
        else
        {
            Answer.SetActive(false);
        }
    }

    private void SetAnswer(ChooseModeGame mode)
    {
        Answer.SetActive(true);
        if (mode == ChooseModeGame.SequenceGeneration)
        {
            if (country is null)
            {
                Answer.GetComponentInChildren<Text>().text = "???";
                return;
            }
            else
            {
                Answer.GetComponentInChildren<Text>().text = country.Generation;
            }
        }
        else
        {
            if (country is null)
            {
                Answer.GetComponentInChildren<Text>().text = "???";
                return;
            }
            else
            {
                Answer.GetComponentInChildren<Text>().text = country.Square;
            }  
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCardDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Description;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Description.SetActive(false);
    }

    private void Start()
    {
       Description.SetActive(false);
    }
}

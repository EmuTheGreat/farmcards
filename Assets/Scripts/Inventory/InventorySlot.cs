using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty;
    public GameObject icon;
    public TMP_Text textItemAmount;

    private void Awake()
    {
        isEmpty = true;
        icon = transform.GetChild(0).gameObject;
        textItemAmount = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite icon)
    {
        this.icon.GetComponent<Image>().color = new Color(1, 1, 1);
        this.icon.GetComponent<Image>().sprite = icon;
    }

    public void DeleteIcon()
    {
        icon.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        icon.GetComponent<Image>().sprite = null;
    }
}

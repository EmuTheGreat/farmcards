using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryPanel;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public ItemScriptableObject itemScriptableObject;

    private void Start()
    {
        for(int i = 0; i < inventoryPanel.childCount; i++)
        {
            inventorySlots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    public void AddItem(ItemScriptableObject item)
    {
        foreach(InventorySlot slot in inventorySlots)
        {
            if (item == slot.item)
            {
                slot.amount += 1;
                return;
            }
        }
        foreach(InventorySlot slot in inventorySlots)
        {
            if (slot.isEmpty)
            {
                slot.item = item;
                slot.amount += 1;
                slot.isEmpty = false;
                return;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InterfaceManager InterfaceManager;
    public Transform inventoryPanel;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

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
                slot.textItemAmount.text = slot.amount.ToString();
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
                slot.textItemAmount.text = slot.amount.ToString();
                slot.SetIcon(item.sprite);
                return;
            }
        }
    }

    public void SellItems()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (!slot.isEmpty)
            {
                int sellPrice = slot.item.Price * slot.amount;
                InterfaceManager.SetBalance(sellPrice);
                DeleteItem(slot);
            }
        }
    }

    private void DeleteItem(InventorySlot slot)
    {
        slot.amount = 0;
        slot.item = null;
        slot.isEmpty = true;
        slot.textItemAmount.text = string.Empty;
        slot.DeleteIcon();
    }
}

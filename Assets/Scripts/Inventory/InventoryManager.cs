using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, ISaveState
{
    public InterfaceManager InterfaceManager;
    public Transform inventoryPanel;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    [SerializeField]
    private ObjectsDatabaseSO dataBase;

    private void Start()
    {
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            inventorySlots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        if (PlayerPrefs.HasKey("Inventory"))
        {
            Load();
        }
    }

    public void AddItem(ItemScriptableObject item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (item == slot.item)
            {
                slot.amount += 1;
                slot.textItemAmount.text = slot.amount.ToString();
                return;
            }
        }
        foreach (InventorySlot slot in inventorySlots)
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

    public void Save()
    {
        List<SaveItem> items = new();
        foreach (var slot in inventorySlots)
        {
            if (!slot.isEmpty)
            {
                items.Add(new(slot.item.Id, slot.amount));
            }
        }

        string inventory = JsonUtility.ToJson(new ListContainer<SaveItem>(items));
        PlayerPrefs.SetString("Inventory", inventory);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            ListContainer<SaveItem> inventory = JsonUtility.FromJson<ListContainer<SaveItem>>(PlayerPrefs.GetString("Inventory"));
            for (int i = 0; i < inventory.list.Count; i++)
            {
                var item = inventory.list[i];
                var slot = inventorySlots[i];
                var scrItem = dataBase.objectsData[item.id].Item;
                slot.item = scrItem;
                slot.amount = item.amount;
                slot.isEmpty = false;
                slot.textItemAmount.text = slot.amount.ToString();
                slot.SetIcon(scrItem.sprite);
            }
        }
    }
}

[Serializable]
public class SaveItem
{
    public int id;
    public int amount;

    public SaveItem(int id, int amount)
    {
        this.id = id;
        this.amount = amount;
    }
}
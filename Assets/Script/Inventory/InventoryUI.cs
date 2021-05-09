using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static bool GameInventoryOn = false;
    public GameObject InventoryPanel;
    Inventory inventory;
    public Transform itemParent;
    InventorySlot[] slots;
    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        slots = itemParent.GetComponentsInChildren<InventorySlot>();
    }
  
    void UpdateUI()
    {
        for(int i=0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count) //Kiem Soat Xem item co bao nhieu de dua vao slots
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
                slots[i].ClearSlot();
        }
    }
    public void ChangeInventory() //Dong Hoac Mo UI Inventory
    {
        if (GameInventoryOn)
        {
            OffInventoryPanel();
        }
        else
        {
            OnInventoryPanel();
        }
    }

    void OffInventoryPanel() //Thuc Hien Dong UI
    {
        InventoryPanel.SetActive(false);
        GameInventoryOn = false;
    }
    void OnInventoryPanel() //Thuc Hien Mo UI
    {
        InventoryPanel.SetActive(true);
        GameInventoryOn = true;
    }
}

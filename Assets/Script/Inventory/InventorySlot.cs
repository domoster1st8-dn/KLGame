using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Item item;
    public SoundPlayer soundPlayer;
    public void AddItem(Item newitem)
    {
        item = newitem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    public void OnRemoveBtn()
    {
        Inventory.instance.Remove(item);
    }
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            item.RemoveFromInventory();
            soundPlayer.OnSoundItem();
        }
    }
}

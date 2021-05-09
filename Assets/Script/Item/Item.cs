using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool type;
    public int Hp_;
    public int Mp_;
    public bool Forever = false;
    public Material material;
    public virtual void Use()
    {
        ItemManager.intance.Equip(this);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}


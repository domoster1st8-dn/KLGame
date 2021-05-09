using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public void PickUp()
    {
        bool isAddd = Inventory.instance.Add(item);
        if(isAddd)
            Destroy(gameObject);    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public int space = 8;
    public List<Item> items = new List<Item>();
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            { 
                return false;
            }
            items.Add(item);

            if (onItemChangedCallBack != null)
                onItemChangedCallBack.Invoke();
        }
        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}

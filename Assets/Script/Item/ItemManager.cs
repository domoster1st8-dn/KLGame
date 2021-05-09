using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region Singleton
    public static ItemManager intance;
    public PlayerStatus PlayerStatus;
    private void Awake()
    {
        intance = this;  
    }
    #endregion
    public void Equip(Item item)
    {
        switch (item.type)
        {
            case true:
                {
                    if (item.Forever)
                        PlayerStatus.SendMessage("AddMaxHealth", item.Hp_);
                    else
                        PlayerStatus.SendMessage("setHealth", item.Hp_);

                    break;
                }
            case false:
                {
                    if (item.Forever)
                        PlayerStatus.SendMessage("AddMaxMana", item.Mp_);
                    else
                        PlayerStatus.SendMessage("setMana", item.Mp_);

                    break;
                }
        }   
    }
}

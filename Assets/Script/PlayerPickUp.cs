using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPickUp : MonoBehaviour
{
    [Header("PickItem")]
    public float radius = 3f;
    public LayerMask itemMask;
    public SoundUI SoundUI;
    [Header("PickNPC")]
    public Transform PointAttackToRayPickNPC;
    public LayerMask NPCMask;

    public void Nhat()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,radius,itemMask);
        
        if(colliders.Length > 0)
        {
            colliders[0].SendMessageUpwards("PickUp");
            SoundUI.OnSound();
        }
        else
        {
            PickNPC(); 
        }
    }
    public void PickNPC()
    {
        Collider[] colliders = Physics.OverlapSphere(PointAttackToRayPickNPC.position, radius, NPCMask);
   
        if (colliders.Length > 0)
        {
            colliders[0].SendMessageUpwards("OnDialog");
            SoundUI.OnSound();
        }
    }
    

}
//            //int index = 0;
//float DistanceMin = Vector3.Distance(transform.position,colliders[0].transform.position);
//for (int i = 1; i < colliders.Length; i++)
//{
//    float DistanceTemp = Vector3.Distance(transform.position, colliders[i].transform.position);
//    if (DistanceMin > DistanceTemp)
//    {
//        DistanceMin = DistanceTemp;
//        index = i;
//    }
//}

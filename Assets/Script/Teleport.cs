using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Info Teleport")]
    bool isTeleport = false;
    public float radius = 0.4f;
    public int IndexMap = 2;
    public LoadingMap LoadingMap;
    public LayerMask playerMask;
    private void Update()
    {
        if(!isTeleport && Physics.CheckSphere(transform.position, radius,playerMask))
        {
            LoadingMap.LoadMap(IndexMap);
            isTeleport = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleTap : MonoBehaviour, IPointerDownHandler
{
    public ThirdMovement Player;
    public float tapSpeed = 0.5f; //in seconds

    private float lastTapTime = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        if ((Time.time - lastTapTime) < tapSpeed)
        {

            Debug.Log("Double tap");

        }

        lastTapTime = Time.time;
    }
}

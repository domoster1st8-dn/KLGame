using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public RectTransform compass,playerpoint;
    public Camera mapCam;


    [Range(1,5)]
    public int zoomlevel = 1;
    public bool pointNorth = true;


    void Update()
    {
        
        transform.position = player.position;

        if (pointNorth)
        {
            if(transform.forward != Vector3.forward)
            {
                transform.forward = Vector3.forward;
                compass.eulerAngles = Vector3.zero;
            }
            playerpoint.eulerAngles = new Vector3(0, 0, 180 - player.eulerAngles.y );
        }
        else
        {
            if (playerpoint.eulerAngles != new Vector3(0, 0, 180))
                playerpoint.eulerAngles = new Vector3(0, 0, 180);

            transform.forward = player.forward;
            compass.eulerAngles = new Vector3(0, 0, player.eulerAngles.y);
        }

        if(mapCam.orthographicSize != zoomlevel * 5)
        {
            mapCam.orthographicSize = zoomlevel * 5;
        }
    }
    public void Zoom(int intlevel)
    {
        zoomlevel  = Mathf.Clamp(zoomlevel + intlevel,1,5);
    }
}

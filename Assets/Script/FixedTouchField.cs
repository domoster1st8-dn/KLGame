using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public List<int> lst = new List<int>();
    public float zoomValue;
    public Vector2 LookValue = new Vector2();
    public Cinemachine.CinemachineFreeLook cinemachine;
    public int Count;


    // Update is called once per frame
    void Update()
    {
       
        if (Input.touches.Length - Count == 1)
        {
            int i;
            Touch touch = new Touch();
            for (i = 0; i < Input.touches.Length; i++)
            {
                if(lst.Count > 0)
                if (Input.touches[i].fingerId == lst[0])
                {
                    touch = Input.touches[i];
                    break;
                }
            }
            if (touch.phase == TouchPhase.Moved)
            {
                LookValue = new Vector2(touch.deltaPosition.x * 3f * Time.deltaTime, touch.deltaPosition.y * .005f * Time.deltaTime);
                cinemachine.m_XAxis.Value += LookValue.x;
                cinemachine.m_YAxis.Value += LookValue.y;
            }
        }
        if (Input.touchCount - Count == 2)
        {
            int i, j;
            Touch touch1 = new Touch(), touch2 = new Touch();
            for (i = 0; i < Input.touches.Length; i++)
            {
                if (Input.touches[i].fingerId == lst[0])
                {
                    touch1 = Input.touches[i];
                    break;
                }
            }
            for (j = 0; j < Input.touches.Length; j++)
            {
                if (Input.touches[j].fingerId == lst[1])
                {
                    touch2 = Input.touches[j];
                    break;
                }
            }
            if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                Vector2 touchoneprepos = touch1.position - touch1.deltaPosition;
                Vector2 touchtwoprepos = touch2.position - touch2.deltaPosition;

                float preMagnitude = (touchoneprepos - touchtwoprepos).magnitude;
                float currentMagnitude = (touch1.position - touch2.position).magnitude;

                float difference = currentMagnitude - preMagnitude;
                zoomValue = difference * 0.03f;
                cinemachine.m_Lens.FieldOfView = Mathf.Clamp(cinemachine.m_Lens.FieldOfView - zoomValue, 50, 80);
            }
            else
            {
                zoomValue = 0;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.pointerId >= 0)
            lst.Add(eventData.pointerId);
       
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId >= 0)
            lst.Remove(eventData.pointerId);
    }



    public void OnBtnUI()
    {
        Count++;
    }
    public void OffBtnUI()
    {
        Count--;
    }

    
}
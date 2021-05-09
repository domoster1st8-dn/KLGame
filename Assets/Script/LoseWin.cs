using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseWin : MonoBehaviour
{
    public GameObject UI_LoseWin;
    public GameObject EnemyObj;
    public TextMeshProUGUI TextLoseWin;
    public TextMeshProUGUI TextTimeLose;
    
    public bool DoiThoai = false;
    public bool Chuyen = false;
    bool Activity = false;

    [Header("TimeLose")]
    float timevalue = 90;

    private void Start()
    {
        TextLoseWin.text = "";
        UI_LoseWin.SetActive(false);
        if (TextTimeLose != null)
        {
            TextTimeLose.text = timevalue.ToString();
        }
    }
    void Update()
    {
        if (!Chuyen)
        {
            if (EnemyObj.GetComponentsInChildren<EnemyStats>().Length <= 0 && DoiThoai)
            {
                UI_LoseWin.SetActive(true);
                TextLoseWin.text = "YOU WIN";
                Activity = true;
            }
        }
        UpdateTime();
    }
    public void Lose()
    {
        UI_LoseWin.SetActive(true);
        TextLoseWin.text = "YOU LOSE";
        Activity = true;
    }
    public void UpdateTime()
    {
        

        if(TextTimeLose != null)
        {
            if (timevalue > 0)
                timevalue -= Time.deltaTime;
            else
                timevalue = 0;
            if (timevalue <= 0 && !Activity)
            {
                Lose();
                return;
            }
            if(!Activity)
            {
                TextTimeLose.SetText("{0:00}:{1:00}", (timevalue < 60) ? 0 : timevalue /60,timevalue%60);
            }
                
        }
    }
}

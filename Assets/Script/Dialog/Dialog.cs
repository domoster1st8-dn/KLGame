using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [Header("Dialog")]
    public TextMeshProUGUI textDialog;
    public string[] listText = null;
    int indexT = 0;
    public LoseWin loseWin;
    [Header("Doi Tuong Can Setting")]
    public GameObject BtnNext;
    public GameObject DialogUIManager;
    TextMeshProUGUI TextBtnNext;

    [Header("Sound UI")]
    public SoundUI soundUI;
    public void NextTextDialog()
    {
        soundUI.OnSound();
        if(indexT >= listText.Length)
        {
            if (!loseWin.DoiThoai)
                loseWin.DoiThoai = true;
            TextBtnNext.text = "NEXT";
            DialogUIManager.SetActive(false);
            return;
        }
        if(indexT == listText.Length - 1)
        {
            TextBtnNext.text = "OK";
        }
        ShowText();
    }
    public void ShowText()
    {
        textDialog.text = listText[indexT];
        indexT++;
    }
    public void OnDialogNPC(string[] NpcDialog)
    {
        listText = NpcDialog;
        indexT = 0;
        DialogUIManager.SetActive(true);
        TextBtnNext = BtnNext.GetComponentInChildren<TextMeshProUGUI>();
        ShowText();
    }
}

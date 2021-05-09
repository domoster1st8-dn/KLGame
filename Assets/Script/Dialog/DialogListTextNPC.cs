using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogListTextNPC : MonoBehaviour
{
    public string[] lstTextDialogForNPC;
    public Dialog dialogFromGameManager;

    public void OnDialog()
    {
        dialogFromGameManager.OnDialogNPC(lstTextDialogForNPC);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingMap : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image Amount;
    public TextMeshProUGUI text;
    public void LoadMap(int indexMap)
    {
        StartCoroutine(LoadAsynOperation(indexMap));
    }
    IEnumerator LoadAsynOperation(int indexMap)
    {
        LoadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(indexMap);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Amount.fillAmount = progress;
            text.text = (int)(progress * 100f)+"%";
            yield return null;
        }
    }
}

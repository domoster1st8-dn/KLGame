using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatText : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    TextMeshProUGUI Text;
    [SerializeField]
    float lifeTime;


    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public IEnumerator FadeOut()
    {
        float Alpha = Text.color.a;
        float rate = 1f / lifeTime;
        float progress = 0f;
        while (progress < 1f)
        {
            Color tmp = Text.color;
            tmp.a = Mathf.Lerp(Alpha,0,progress);
            Text.color = tmp;
            progress += rate * Time.deltaTime;
            yield return null;
        }
        Destroy(this);
    }
}

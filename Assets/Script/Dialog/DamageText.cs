using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] Vector3 offset;
    [SerializeField] float speed;
   

    TextMeshPro damageText;
    // Start is called before the first frame update
    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
        transform.localPosition += offset;
        Destroy(gameObject, destroyTime); 
    }
    

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    public void Init(int damage)
    {
        damageText.text = damage + "";
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : MonoBehaviour
{
    [Header("Player Movement")]
    public ThirdMovement thirdMovement;

    [Header("VFX JUMP")]
    public ParticleSystem TayTrai;
    public ParticleSystem TayPhai;



    // Start is called before the first frame update
    void Start()
    {
        thirdMovement = GetComponentInChildren<ThirdMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (thirdMovement.controller.isGrounded)
        {
            
            if (TayPhai.isPlaying)
            {
                TayPhai.Stop();
                TayTrai.Stop();
            }
            
        }
        else
        {
            if (!TayPhai.isPlaying)
            {
                TayPhai.Play();
                TayTrai.Play();
            }
        }
        
       
    }
}

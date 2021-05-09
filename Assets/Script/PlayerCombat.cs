using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   
    public ThirdMovement ThirdMovement;
    public Animator anim;
    public bool isCombo = false;
    public int AttackNum = 0;
    public ParticleSystem ParticleSystem;

    public PlayerStatus playerStatus;
    float TimeMana;

    [Header("Event Attack Point")]
    public Transform PointAttack;
    public ParticleSystem PointAtk;
    public float radius = 0.4f;
    public LayerMask EnemyMask;

    [Header("Sound Attack")]
    public SoundPlayer soundPlayer;
    void Start()
    {
        ThirdMovement = GetComponent<ThirdMovement>();
        anim = GetComponentInChildren<Animator>();
        playerStatus = GetComponentInParent<PlayerStatus>();
        PointAtk = PointAttack.GetComponentInChildren<ParticleSystem>();
        PointAtk.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (ThirdMovement.isAttack)
        {
            ParticleSystem.Play();
            TimeMana = Time.time;
        }
        else
        {
            ParticleSystem.Stop();
            if(Time.time - TimeMana >= 2f)
            {
                playerStatus.UseMana(1);
                TimeMana = Time.time;
            }
        }
    }
    public void Attack()
    {
            if (ThirdMovement.CanAttack && playerStatus.currentSecondary.Mana >= 5)
            {
                if (AttackNum == 0)
                {
                    anim.Play("Attack 1");
                    AttackNum++;
                    playerStatus.UseMana(-5);
                    return;
                }
                if (AttackNum != 0)
                {
                    if (isCombo)
                    {
                        isCombo = false;
                        AttackNum++;
                        anim.SetBool("Combo", true);
                    }
                }
            }
    }
    public void OnCombo()
    {
        isCombo = true;
    }
  
    public void ResetCombo()
    {
        isCombo = false;
        AttackNum = 0;
        ThirdMovement.CanRotation = true;
    }
    public void AttackEvent()
    {
        soundPlayer.OnSoundSword();
        Collider[] Enemys = Physics.OverlapSphere(PointAttack.position,radius,EnemyMask);
        if (Enemys.Length > 0)
        {
            int dmg = playerStatus.currentPrimary.PowerAttack;
            dmg += AttackNum;
            foreach (Collider d in Enemys)
            {
                d.SendMessageUpwards("TakeDamage", -Random.Range(dmg - 1, dmg + 3));
            }
            PointAtk.Play();
        }
    }
    public void UseMana()
    {
        playerStatus.UseMana(-5);
    }

}

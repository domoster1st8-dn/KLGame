using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float radiusAttack = 4f;
    public float distanceSendDamage = 3f;

    [Header("Target")]
    public PlayerStatus PlayerStatus;
    public Transform Target;

    public EnemyStats enemyStats;
    NavMeshAgent agent;
    public Animator anim;
    public bool isAttack = false;
    public bool CanMove = true;

    [SerializeField]
    float AttackDelay = 5f;
    float LastTimeAttack;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
        anim = GetComponentInChildren<Animator>();
        Target = PlayerStatus.transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
      
        float dictance = Vector3.Distance(Target.position, transform.position);
        if (dictance <= lookRadius)
        {
           anim.SetBool("Run",true);
           agent.SetDestination(Target.position);
            if (isAttack || !CanMove)
            {
                agent.SetDestination(transform.position);
            }
           if (dictance <= agent.stoppingDistance)
           {
                anim.SetBool("Run", false);
                FaceTarget();
               if(Time.time - LastTimeAttack >= AttackDelay)
               {
                  Attack();
               }    
           }
        }
        else
        {
            anim.SetBool("Run", false);
            agent.SetDestination(transform.position);
        }
    }
    void Attack()
    {
        anim.SetTrigger("Attack");
        LastTimeAttack = Time.time;
    }
    public void SendDamage()
    {
        if (!enemyStats.isTake)
        {
            int dmg = enemyStats.currentPrimary.PowerAttack;
            if(Vector3.Distance(PlayerStatus.transform.position,transform.position)<=distanceSendDamage)
                PlayerStatus.TakeDamage(-Random.Range(dmg - 1, dmg + 2));
        } 
    }
    void FaceTarget()
    {
        if (!isAttack && CanMove)
        {
            Vector3 direction = (Target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}

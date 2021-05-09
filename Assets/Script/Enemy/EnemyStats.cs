using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyStats : MonoBehaviour
{
    [Header("DamageText")]
    [SerializeField] GameObject damageTextPrefab;

    [Header("Stats Enemy")]
    public string EnemyName;
    [HideInInspector]
    public SecondaryStats currentSecondary;
    public SecondaryStats maxSecondary;
    [HideInInspector]
    public PrimaryStats currentPrimary;
    public PrimaryStats basePrimary;

    [Header("Input Info")]
    public int Hp_ = 200;
    public int Damage_ = 2;
    public int Armor_ = 2;

    [Header("Animator")]
    public Animator anim;

    [Header("UI")]
    //Name
    public TextMeshProUGUI nameText;

    //Health
    public Image healthIMG;
    public bool isTake = false;

    public EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        StatsSetup(Hp_, Damage_, Armor_);
        setEnemyName();
        anim = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();
        limitStatValue();
        UpdateStatus();
    }
  

    void StatsSetup(int Hp,int dmg, int amr) //Khoi tao mac dinh cho player
    {
        basePrimary = new PrimaryStats(dmg, amr);
        currentPrimary = new PrimaryStats(basePrimary.PowerAttack, basePrimary.Armor);

        maxSecondary = new SecondaryStats(Hp + (int)(basePrimary.Armor * 0.5f), 100 + (int)(basePrimary.PowerAttack * 0.5f));
        currentSecondary = new SecondaryStats(maxSecondary.Hearth, maxSecondary.Mana);

    }
    float valueSlider(int max, int currentStat) //dua ve % gia tri
    {
        return ((float)currentStat / (float)max);
    }
    void UpdateStatus() //Cap nhan UI cho Mau va Mana
    {
        healthIMG.fillAmount = valueSlider(maxSecondary.Hearth, currentSecondary.Hearth);
     
    }
    void limitStatValue()
    {
        currentSecondary.Hearth = Mathf.Clamp(currentSecondary.Hearth, 0, maxSecondary.Hearth);
        currentSecondary.Mana = Mathf.Clamp(currentSecondary.Mana, 0, maxSecondary.Mana);
    }
    void setEnemyName()
    {
        nameText.SetText(EnemyName);
    }
    void AddMaxHealth(int Health)
    {
        maxSecondary.Hearth += Health;
        if (Health >= 0)
        {
            currentSecondary.Hearth += Health;
        }

    }
    void setHealth(int Health)
    {
        currentSecondary.Hearth = Mathf.Clamp(currentSecondary.Hearth + Health, 0, maxSecondary.Hearth);
        UpdateStatus();
    }
    public void TakeDamage(int damage)
    {
        setHealth(damage);
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<DamageText>().Init(damage);
        if (currentSecondary.Hearth <= 0)
        {
            Die();
        }
        isTake = true;
        anim.SetTrigger("Takedmg");
        
    }
    public void Die()
    {
        

        enemyController.CanMove = false; // Khoa di chuyen
        anim.SetBool("Die",true);
        StartCoroutine(WaitDie(3f)); //Ham Can thiet De Chay IEnumerator
        
    }
    
    
    IEnumerator WaitDie(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  
}

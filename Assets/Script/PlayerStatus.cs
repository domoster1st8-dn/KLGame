using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatus : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] GameObject HealthTextTakeDamagePrefab;

    [Header("Player Name")]
    public string playername;
    public Animator anim;
    

    [Header("Stats Player")]
    [HideInInspector]
    public SecondaryStats currentSecondary;
    public SecondaryStats maxSecondary;
    [HideInInspector]
    public PrimaryStats currentPrimary;
    public PrimaryStats basePrimary;

    [Header("Sound TakeDamage")]
    public SoundPlayer soundPlayer;

    [Header("UI")]
    //Name
    public TextMeshProUGUI nameText;

    //Health
    public Image healthIMG;
    public TextMeshProUGUI healthText;

    //Mana
    public Image ManaIMG;
    public TextMeshProUGUI ManaText;

    //Panel LOSE
    public LoseWin loseWin;

    // Start is called before the first frame update
    void Start()
    {
        StatsSetup(10,10);
        setPlayerName();
        anim = GetComponentInChildren<Animator>();
        limitStatValue();
        UpdateStatus();
    }
    void StatsSetup(int atk,int amr) //Khoi tao mac dinh cho player
    {
        basePrimary = new PrimaryStats(atk,amr);
        currentPrimary = new PrimaryStats(basePrimary.PowerAttack,basePrimary.Armor);
        maxSecondary = new SecondaryStats(100 + (int)(basePrimary.Armor * 0.5f), 100 + (int)(basePrimary.PowerAttack * 0.5f));
        currentSecondary = new SecondaryStats(maxSecondary.Hearth,maxSecondary.Mana);
    }

    float valueSlider(int max,int currentStat) //dua ve % gia tri
    {
        return ((float)currentStat / (float)max);
    }

    void UpdateStatus() //Cap nhan UI cho Mau va Mana
    {
        healthIMG.fillAmount = valueSlider(maxSecondary.Hearth,currentSecondary.Hearth);
        healthText.SetText(currentSecondary.Hearth.ToString() + "/" + maxSecondary.Hearth.ToString());

        ManaIMG.fillAmount = valueSlider(maxSecondary.Mana, currentSecondary.Mana);
        ManaText.SetText(currentSecondary.Mana.ToString() + "/" + maxSecondary.Mana.ToString());
    }
    void limitStatValue()
    {
        currentSecondary.Hearth = Mathf.Clamp(currentSecondary.Hearth, 0 ,maxSecondary.Hearth);
        currentSecondary.Mana = Mathf.Clamp(currentSecondary.Mana, 0, maxSecondary.Mana);
    }
    void setPlayerName()
    {
        nameText.SetText(playername);
    }
    void AddMaxHealth(int Health)
    {
        maxSecondary.Hearth += Health;
        if (Health >= 0)
        {
            currentSecondary.Hearth += Health;
        }
        limitStatValue();
        UpdateStatus();
    }
    void AddMaxMana(int Mana)
    {
        maxSecondary.Mana += Mana;
        if (Mana >= 0)
        {
            currentSecondary.Mana += Mana;
        }
        limitStatValue();
        UpdateStatus();
    }
    void setHealth(int Health)
    {
        currentSecondary.Hearth = Mathf.Clamp(currentSecondary.Hearth + Health, 0, maxSecondary.Hearth);
        UpdateStatus();
    }
    void setMana(int Mana)
    {
        currentSecondary.Mana = Mathf.Clamp(currentSecondary.Mana + Mana, 0, maxSecondary.Mana);
        UpdateStatus();
    }
    public void TakeDamage(int damage)
    {
        setHealth(damage);
        Instantiate(HealthTextTakeDamagePrefab, transform.position, Quaternion.identity,this.transform).GetComponent<DamageText>().Init(damage);
        //Text hien thi Mat mau khi bi enemy tan cong
        if (currentSecondary.Hearth <= 0)
        {
            Die();
            anim.SetTrigger("TakeDamage");
            return;
        }
        if(currentSecondary.Hearth > 0)
        {
            anim.SetTrigger("TakeDamage");
            soundPlayer.OnSoundTakeDmg();
        }
    }
    public void UseMana(int Mana)
    {
        setMana(Mana);
    }
    public void Die()
    {
        loseWin.Lose();
        anim.SetBool("isDie",true);
    }
}
[System.Serializable] //Tu thuan hoa, duoc chuyen doi cau tru du lieu thanh 1 doi tuong co the luu tru
public class PrimaryStats
{
    public int PowerAttack, Armor; //co the them nhieu thuoc tinh khac

    public PrimaryStats(int pAtk, int Am)
    {
        PowerAttack = pAtk;
        Armor = Am;
    }
}
[System.Serializable]
public class SecondaryStats
{
    public int Hearth, Mana;

    public SecondaryStats(int her, int ma)
    {
        Hearth = her;
        Mana = ma;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsDamage : MonoBehaviour
{
    [Header("UI")]
    public Image HpBar;
    public Text HpText;
    public int hp = 0;
    public int maxHp = 100;
    public string attackTag = "ATTACK_HITBOX";
    void Start()
    {
        hp = maxHp;
        HpBar.color = Color.green;
        HpInfo();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(attackTag))
        {
            hp -= 10;
            HpInfo();
        }
    }
    private void HpInfo()
    {
        HpBar.fillAmount = (float)hp / (float)maxHp;
        HpText.text = $"HP : <color=#FFAAAA>{hp.ToString()}</color>";
    }



    
}

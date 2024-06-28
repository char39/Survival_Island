using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDamage : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;
    public GameObject bloodEffect;
    [Header("Vars")]
    public string playerTag = "Player";
    public string bulletTag = "BULLET";
    public string hitStr = "HitTrigger";
    public string dieStr = "DieTrigger";
    //public int hitCount = 0;
    public bool IsDie = false;
    [Header("UI")]
    public Image hpBar;
    public Text hpText;
    public int maxHp = 100;
    public int hpInit = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        hpInit = maxHp;
        hpBar.color = Color.green;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.mass = 75f;
        }
        else if (col.gameObject.CompareTag(bulletTag))
        {
            HitInfo(col);
            hpInit -= col.gameObject.GetComponent<BulletCtrl>().damage;
            hpBar.fillAmount = (float)hpInit / (float)maxHp;
            hpText.text = $"HP : <color=#FF0000>{hpInit.ToString()}</color>";
            if (hpInit <= 0)
                MonsterDie();
        }
    }

    private void HitInfo(Collision col)
    {
        Destroy(col.gameObject);
        animator.SetTrigger(hitStr);

        //Vector3 hitPos = col.transform.position;          //���� ��ġ
        Vector3 hitPos = col.contacts[0].point;             //���� ��ġ

        //Quaternion hitRot = Quaternion.Euler(0, 90, 0);                                       //���� ����
        //Quaternion hitRot = Quaternion.FromToRotation(-Vector3.forward, hitPos.normalized);    //���� ����2
        Quaternion hitRot = Quaternion.LookRotation(-(col.contacts[0].normal));                 //���� ����

        var blood = Instantiate(bloodEffect, hitPos, hitRot);
        Destroy(blood, Random.Range(0.8f, 1.2f));
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.mass = 75f;
        }
    }

    public void MonsterDie()
    {
        animator.SetTrigger(dieStr);
        capCol.enabled = false;
        rb.isKinematic = true;
        IsDie = true;
    }

    void Update()
    {
        if (hpBar.fillAmount <= 0.3f)
            hpBar.color = Color.red;
        else if (hpBar.fillAmount <= 0.5f)
            hpBar.color = Color.yellow;
        else if (hpBar.fillAmount <= 1f)
            hpBar.color = Color.green;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonDamage : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;
    public GameObject bloodEffect;
    public BoxCollider boxCol;
    public MeshRenderer meshRenderer;
    [Header("Vars")]
    public string playerTag = "Player";
    public string bulletTag = "BULLET";
    public string hitStr = "HitTrigger";
    public string dieStr = "DieTrigger";
    public bool IsDie = false;
    [Header("UI")]
    public Image hpBar;
    public Text hpText;
    public int maxHp = 100;
    public int hpInit = 0;
    FireCtrl firectrl;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        hpInit = maxHp;
        hpBar.color = Color.green;
        firectrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>();
    }
    public void BoxColEnable()
    {
        boxCol.enabled = true;
        meshRenderer.enabled = true;
    }
    public void BoxColDisable()
    {
        boxCol.enabled = false;
        meshRenderer.enabled = false;
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(bulletTag))
        {
            HitInfo(col);
            hpInit -= col.gameObject.GetComponent<BulletCtrl>().damage;
            hpBar.fillAmount = (float)hpInit / (float)maxHp;
            hpText.text = $"HP : <color=#FF0000>{hpInit}</color>";
            if (hpInit <= 0)
            {
                SkeletonDie();
            }
        }
    }
    public void SkeletonDie()
    {
        animator.SetTrigger(dieStr);
        capCol.enabled = false;
        rb.isKinematic = true;
        IsDie = true;
        Destroy(gameObject, 5.0f);
    }
    public void HitInfo(Collision col)
    {
        Destroy(col.gameObject);
        animator.SetTrigger(hitStr);

        Vector3 hitPos = col.transform.position;          //기존 위치
        //Vector3 hitPos = col.contacts[0].point;             //코파일럿 수정 위치
        Vector3 fireNormal = (col.transform.position - firectrl.firePos.position);      //기존 위치3
        fireNormal = -fireNormal.normalized; //벡터단위 방향 정규화

        //Quaternion hitRot = Quaternion.Euler(0, 90, 0);                                       //기존 방향
        //Quaternion hitRot = Quaternion.FromToRotation(-Vector3.forward, hitPos.normalized);    //기존 방향2
        //Quaternion hitRot = Quaternion.LookRotation(-(col.contacts[0].normal));                 //코파일럿 수정 방향
        Quaternion hitRot = Quaternion.LookRotation(fireNormal);                                //기존 방향3
             //LookRotation 함수는 벡터값을 받아서 회전으로 바꾸어 주는 기능을 가짐
        
        var blood = Instantiate(bloodEffect, hitPos, hitRot); 
        Destroy(blood, Random.Range(0.8f, 1.2f));
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

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
        firectrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>();
    }

    void OnEnable()
    {
        UIUpdate();
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
            UIUpdate();
            hpText.text = $"HP : <color=#FF0000>{hpInit}</color>";
            if (hpInit <= 0)
            {
                SkeletonDie();
            }
        }
    }

    public void UIUpdate()
    {
        hpInit = Mathf.Clamp(hpInit, 0, maxHp);
        hpBar.fillAmount = (float)hpInit / (float)maxHp;
        hpText.text = $"HP : <color=#FF0000>{hpInit.ToString()}</color>"; //tostring 안해도 크게 문제는 없음.
    }

    public void SkeletonDie()
    {
        animator.SetTrigger(dieStr);
        capCol.enabled = false;
        rb.isKinematic = true;
        IsDie = true;
        GetComponent<EnemyOnDisable>().DisableTime();
        GameManager.Instance.KillScore(1);
    }
    public void ExplosionDie()
    {
        hpInit = 0;
        UIUpdate();
        animator.SetTrigger(dieStr);
        capCol.enabled = false;     //콜라이더[충돌감지 기능] 비활성화
        rb.isKinematic = true;      //물리기능 true일때 일시 제거
        IsDie = true;
        GetComponent<EnemyOnDisable>().DisableTime();
        GameManager.Instance.KillScore(1);
    }
    public void HitInfo(Collision col)
    {
        col.gameObject.SetActive(false); // 총알 비활성화
        animator.SetTrigger(hitStr);

        Vector3 hitPos = col.transform.position;
        Vector3 fireNormal = (col.transform.position - firectrl.firePos.position);
        fireNormal = -fireNormal.normalized;

        Quaternion hitRot = Quaternion.LookRotation(fireNormal);
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

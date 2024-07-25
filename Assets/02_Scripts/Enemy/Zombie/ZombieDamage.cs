using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieDamage : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;
    public GameObject bloodEffect;      // 너무 빈번하게 실행시킬거면 파티클로 직접 쓰는게 나음
    public BoxCollider boxCol;
    public MeshRenderer meshRenderer;
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
    public int hpInit = 0;  //Init 초기값의 약자
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        hpInit = maxHp;
        hpBar.color = Color.green;
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
    private void OnCollisionEnter(Collision col)
    {
        // col.gameObject.tag == "Player"            < 동적할당 + 비교    속도가 느림
        if (col.gameObject.CompareTag(playerTag))  //< 동적할당은 위에서 미리 되었고, 비교만 함
        {
            rb.mass = 75f;             //무게값 변경
            //rb.freezeRotation = true;   //회전멈추기 true
            //rb.isKinematic = false;     //물리력 끄기를 false
            //          is Kinematic 아래의 Interpolate는 물리력을 받으면 부드럽게 재생, extra는 예측
        }
        else if (col.gameObject.CompareTag(bulletTag))
        {
            HitInfo(col);
            hpInit -= col.gameObject.GetComponent<BulletCtrl>().damage;
            UIUpdate();
            if (hpInit <= 0)
                ZombieDie();
        }
    }

    public void UIUpdate()
    {
        hpInit = Mathf.Clamp(hpInit, 0, maxHp);
        hpBar.fillAmount = (float)hpInit / (float)maxHp;
        hpText.text = $"HP : <color=#FF0000>{hpInit.ToString()}</color>"; //tostring 안해도 크게 문제는 없음.
    }

    private void HitInfo(Collision col)
    {
        //Destroy(col.gameObject);    //총알 제거
        col.gameObject.SetActive(false);    //총알 제거
                                    //print("맞았나?");
        animator.SetTrigger(hitStr);

        //Vector3 hitPos = col.transform.position;          //기존 위치
        Vector3 hitPos = col.contacts[0].point;             //수정 위치

        //Quaternion hitRot = Quaternion.Euler(0, 90, 0);                                       //기존 방향
        //Quaternion hitRot = Quaternion.FromToRotation(-Vector3.forward, hitPos.normalized);    //기존 방향2
                                                        //절대좌표
        Quaternion hitRot = Quaternion.LookRotation(-(col.contacts[0].normal));                 //수정 방향


        ////- 붙인 이유는 이펙트를 반전시켜서 보이기 위함    앞쪽 방향에서     충돌한 방향으로
        ////Quaternion hitRot = Quaternion.FromToRotation(-Vector3.forward, hitPos.normalized);
        ////Quaternion hitRot = Quaternion.LookRotation(-(col.contacts[0].normal));

        var blood = Instantiate(bloodEffect, hitPos, hitRot);

        Destroy(blood, Random.Range(0.8f, 1.2f));
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.mass = 75f;
            //rb.freezeRotation = true;
        }
    }
    void ZombieDie()
    {
        animator.SetTrigger(dieStr);
        capCol.enabled = false;     //콜라이더[충돌감지 기능] 비활성화
        rb.isKinematic = true;      //물리기능 true일때 일시 제거
        IsDie = true;
        //Destroy(gameObject, 5.0f);
        GetComponent<EnemyOnDisable>().Disable();
        GameManager.Instance.KillScore(1);
    }
    void Update()
    {
        //  체력 변수의 값으로 확인해도 돼고, 체력 UI 바의 값을 확인해도 좋다.
        if (hpBar.fillAmount <= 0.3f)
            hpBar.color = Color.red;
        else if (hpBar.fillAmount <= 0.5f)
            hpBar.color = Color.yellow;
        else if (hpBar.fillAmount <= 1f)
            hpBar.color = Color.green;
    }

}

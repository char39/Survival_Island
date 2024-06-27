using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;
    [Header("Vars")]
    public string playerTag = "Player";
    public string bulletTag = "BULLET";
    public string hitStr = "HitTrigger";
    public string dieStr = "DieTrigger";
    public int hitCount = 0;
    public bool IsDie = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }
    
    private void OnCollisionEnter(Collision col)
    {
        // col.gameObject.tag == "Player"            < 동적할당 + 비교    속도가 느림
        if (col.gameObject.CompareTag(playerTag))  //< 동적할당은 위에서 미리 되었고, 비교만 함
        {
            rb.mass = 75f;              //무게값 변경
            rb.freezeRotation = false;  //회전멈추기 false
        }
        else if (col.gameObject.CompareTag(bulletTag))
        {
            Destroy(col.gameObject);    //총알 제거
            //print("맞았나?");
            animator.SetTrigger(hitStr);
            if (++hitCount == 5)
                ZombieDie();
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.mass = 75f;
        }
    }
    

    void ZombieDie()
    {
        animator.SetTrigger(dieStr);
        capCol.enabled = false;     //콜라이더[충돌감지 기능] 비활성화
        rb.isKinematic = true;      //물리기능 true일때 일시 제거
        IsDie = true;
    }
}

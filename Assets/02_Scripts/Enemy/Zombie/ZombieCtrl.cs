using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCtrl : MonoBehaviour
{
    //Attribute : 속성
    [Header("Component")]
    public NavMeshAgent agent;          //네비게이션 [추적할 대상을 찾는 네비 컴포넌]
    public Transform Player;            //플레이어랑 거리를 재기 위함
    public Transform thisZombie;        //누구랑? 이 좀비랑
    public Animator animator;
    public ZombieDamage damage;         //ZombieDamage.cs 스크립트 컴포넌트 불러오기 : 해당 클래스 불러오기 
    [Header("Vars")]
    public float attackDist = 3.0f;     //공격 거리
    public float traceDist = 20.0f;     //추적 범위

    void Start()
    {
            //유니티엔진 없이 C#으로는 아래처럼 할당을 해줌
            //agent = new NavMeshAgent();
            //  자기자신 옵젝 안에 있는        NavMeshAgent 컴포넌을 대입
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        thisZombie = this.gameObject.GetComponent<Transform>();
            // thisZombie = transform;으로 써도됨
            //      Hierarchy안에 있는 겜옵젝의 태그를 읽어서 가져온다.
        Player = GameObject.FindWithTag("Player").transform;
            //transform을 GetComponent<Transform>()으로 써도 괜찮다.
            //transform은 항상 있으니까 괜찮은것.
        animator = GetComponent<Animator>();
        damage = GetComponent<ZombieDamage>();
    }

    void OnEnable()
    {
        FpsDamage.PlayerDie_Event += PlayerDeath_Dance;
    }

    void Update()
    {
        if (damage.IsDie || Player.GetComponent<FpsDamage>().isPlayerDie)
            return;   //update 함수 바로 null을 반환하고 끝냄. 다만 무한 반복이라 다음엔 이렇게 짜면 안됨.
            // 거리를 잰다.
            //player이랑 thisZombie를 transform으로 잡았기에 position으로 거리를잼.
            //gameobject로 잡으면 transform으로 거리를 잼
        float distance = Vector3.Distance(thisZombie.position, Player.position);
        if (distance <= attackDist)
        {
            //Debug.Log("공격");
            animator.SetBool("IsAttack", true);
            agent.isStopped = true;
            Quaternion rot = Quaternion.LookRotation(Player.position - thisZombie.position);
            thisZombie.rotation = Quaternion.Slerp(thisZombie.rotation, rot, Time.deltaTime * 3.0f);
                        //Slerp 부드럽게 곡면(원형방향으로) 보간하는 함수
                        //Quaternion.Slerp(자기자신 로테이션에서, 플레이어 방향으로, 주어진시간만큼)
        }
        else if (distance <= traceDist)
        {
            //Debug.Log("추적");
            animator.SetBool("IsTrace", true);
            animator.SetBool("IsAttack", false);
            agent.isStopped = false;
            agent.destination = Player.position;    //추적 대상은 플레이어 위치
        }
        else
        {
            //Debug.Log("추적범위벗어남");
            animator.SetBool("IsTrace", false);
            agent.isStopped = true;
        }

    }
    public void PlayerDeath()
    {
        GetComponent<Animator>().SetTrigger("PlayerDie");
    }
    public void PlayerDeath_Dance()
    {
        GetComponent<Animator>().SetTrigger("PlayerDie_Dance");
    }
}

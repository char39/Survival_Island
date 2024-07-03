using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterCtrl : MonoBehaviour
{
    [Header("Component")]
    public NavMeshAgent agent2;
    public Transform Player;
    public Transform thisMonster;
    public Animator animator;
    public MonsterDamage damage;
    [Header("Vars")]
    public float attackDist = 3.0f;
    public float traceDist = 20.0f;

    void Start()
    {
        agent2 = this.gameObject.GetComponent<NavMeshAgent>();
        thisMonster = transform;
        Player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        damage = GetComponent<MonsterDamage>();
    }

    void Update()
    {
        if (damage.IsDie || Player.GetComponent<FpsDamage>().isPlayerDie)
            return;
        float distance = Vector3.Distance(thisMonster.position, Player.position);
        if(distance <= attackDist)
        {
            //Debug.Log("공격");
            animator.SetBool("IsAttack", true);
            agent2.isStopped = true;
            Vector3 PlayerPos = Player.position - transform.position;
            PlayerPos = PlayerPos.normalized;
            Quaternion rot = Quaternion.LookRotation(PlayerPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 3.0f);
        }
        else if(distance <= traceDist)
        {
            //Debug.Log("추적");
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsTrace", true);
            agent2.isStopped = false;
            agent2.destination = Player.position;
        }
        else
        {
            //Debug.Log("추적하지 않음");
            animator.SetBool("IsTrace", false);
            agent2.isStopped = true;
        }
    }
    public void PlayerDeath()
    {
        GetComponent<Animator>().SetTrigger("PlayerDie");
    }
}

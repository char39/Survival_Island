using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SkeletonCtrl : MonoBehaviour
{
    [Header("Component")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform thisSkeleton;
    public Animator animator;
    public SkeletonDamage damage;
    public AudioSource audioSource;
    public AudioClip swordclip;
    [Header("Vars")]
    public float attackDist = 3.0f;
    public float traceDist = 20.0f;
    public string findTag = "Player";

    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        thisSkeleton = transform;
        player = GameObject.FindWithTag(findTag).transform;
        animator = GetComponent<Animator>();
        damage = GetComponent<SkeletonDamage>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        FpsDamage.PlayerDie_Event += PlayerDeath_Dance;
    }

    void Update()
    {
        if (damage.IsDie || player.GetComponent<FpsDamage>().isPlayerDie)
            return;
        float distance = Vector3.Distance(thisSkeleton.position, player.position);
        if (distance <= attackDist)
        {
            animator.SetBool("IsAttack", true);
            agent.isStopped = true;

            Vector3 playerPos = (player.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(playerPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 3.0f);
        }
        else if (distance <= traceDist)
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsTrace", true);
            agent.isStopped = false;
            agent.destination = player.position;
        }
        else
        {
            animator.SetBool("IsTrace", false);
            agent.isStopped = true;
        }
        
    }
    public void SwordSfx()
    {
        audioSource.clip = swordclip;   //사운드 클립을 받아서 아래에서 1회 재생
        audioSource.PlayDelayed(0.1f);  //0.1초 딜레이 후 재생
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

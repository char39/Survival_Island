using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonCtrl : MonoBehaviour
{
    [Header("Component")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform thisSkeleton;
    public Animator animator;
    public SkeletonDamage damage;
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
    }

    void Update()
    {
        if (damage.IsDie) return;
        float distance = Vector3.Distance(thisSkeleton.position, player.position);
        if (distance <= attackDist)
        {
            animator.SetBool("IsAttack", true);
            agent.isStopped = true;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDisable : MonoBehaviour
{
    private Rigidbody rb;
    public Transform[] Points;


    void OnEnable() // 켜질 때
    {

    }

    void OnDisable()// 꺼질 때
    {
        rb.Sleep();
        rb.velocity = Vector3.zero;
        gameObject.transform.position = GameObject.Find("EnemyGroup").transform.position;
        gameObject.SetActive(false);
    }

    public void Disable()  // 호출을 위한 메서드
    {
        Invoke("StartDisable", 5.0f);        
    }
    void StartDisable()
    {
        rb.Sleep();
        rb.velocity = Vector3.zero;
        gameObject.transform.position = GameObject.Find("EnemyGroup").transform.position;
        if (gameObject.TryGetComponent<SkeletonDamage>(out var skeletonDamage))
        {
            skeletonDamage.IsDie = false;
            skeletonDamage.capCol.enabled = true;
            skeletonDamage.rb.isKinematic = false;
        }
        if (gameObject.TryGetComponent<MonsterDamage>(out var monsterDamage))
        {
            monsterDamage.IsDie = false;
        }
        if (gameObject.TryGetComponent<ZombieDamage>(out var zombieDamage))
        {
            zombieDamage.IsDie = false;
        }
        gameObject.SetActive(false);
    }
}

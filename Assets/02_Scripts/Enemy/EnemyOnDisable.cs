using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDisable : MonoBehaviour
{
    public Transform[] Points;

    void OnEnable() // 켜질 때
    {
        if (gameObject.TryGetComponent<SkeletonDamage>(out var skeletonDamage))
        {
            skeletonDamage.hpInit = skeletonDamage.maxHp;
        }

        if (gameObject.TryGetComponent<MonsterDamage>(out var monsterDamage))
        {
            monsterDamage.hpInit = monsterDamage.maxHp;
        }
    
        if (gameObject.TryGetComponent<ZombieDamage>(out var zombieDamage))
        {
            zombieDamage.hpInit = zombieDamage.maxHp;
        }
    }

    void OnDisable()// 꺼질 때
    {
        StartDisable();
    }

    public void Disable()  // 호출을 위한 메서드
    {
        Invoke("StartDisable", 5.0f);        
    }

    public void StartDisable()
    {
        if (gameObject.TryGetComponent<SkeletonDamage>(out var skeletonDamage))
        {
            skeletonDamage.IsDie = false;
            skeletonDamage.capCol.enabled = true;
            skeletonDamage.rb.isKinematic = false;
            skeletonDamage.rb.Sleep();
            skeletonDamage.rb.velocity = Vector3.zero;
            skeletonDamage.gameObject.transform.position = GameObject.Find("EnemyGroup").transform.position;
        }

        if (gameObject.TryGetComponent<MonsterDamage>(out var monsterDamage))
        {
            monsterDamage.IsDie = false;
            monsterDamage.capCol.enabled = true;
            monsterDamage.rb.isKinematic = false;
            monsterDamage.rb.Sleep();
            monsterDamage.rb.velocity = Vector3.zero;
            monsterDamage.gameObject.transform.position = GameObject.Find("EnemyGroup").transform.position;
        }
    
        if (gameObject.TryGetComponent<ZombieDamage>(out var zombieDamage))
        {
            zombieDamage.IsDie = false;
            zombieDamage.capCol.enabled = true;
            zombieDamage.rb.isKinematic = false;
            zombieDamage.rb.Sleep();
            zombieDamage.rb.velocity = Vector3.zero;
            zombieDamage.gameObject.transform.position = GameObject.Find("EnemyGroup").transform.position;
        }

        gameObject.SetActive(false);
    }
}

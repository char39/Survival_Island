using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDisable : MonoBehaviour
{
    public Transform[] Points;

    void OnEnable() // 켜질 때
    {

    }

    void OnDisable()// 꺼질 때
    {
        StartDisable();
    }

    public void DisableTime()  // 호출을 위한 메서드
    {
        Invoke("StartDisable", 5.0f);        
    }

    public void StartDisable()
    {
        if (gameObject.TryGetComponent<SkeletonDamage>(out var skeletonDamage))
        {
            skeletonDamage.IsDie = false;
            skeletonDamage.hpInit = skeletonDamage.maxHp;
            skeletonDamage.capCol.enabled = true;
            skeletonDamage.rb.isKinematic = false;
            skeletonDamage.rb.Sleep();
            skeletonDamage.rb.velocity = Vector3.zero;
            skeletonDamage.gameObject.SetActive(false);
        }

        else if (gameObject.TryGetComponent<MonsterDamage>(out var monsterDamage))
        {
            monsterDamage.IsDie = false;
            monsterDamage.hpInit = monsterDamage.maxHp;
            monsterDamage.capCol.enabled = true;
            monsterDamage.rb.isKinematic = false;
            monsterDamage.rb.Sleep();
            monsterDamage.rb.velocity = Vector3.zero;
            monsterDamage.gameObject.SetActive(false);
        }
    
        else if (gameObject.TryGetComponent<ZombieDamage>(out var zombieDamage))
        {
            zombieDamage.IsDie = false;
            zombieDamage.hpInit = zombieDamage.maxHp;
            zombieDamage.capCol.enabled = true;
            zombieDamage.rb.isKinematic = false;
            zombieDamage.rb.Sleep();
            zombieDamage.rb.velocity = Vector3.zero;
            zombieDamage.gameObject.SetActive(false);
        }
    }
}

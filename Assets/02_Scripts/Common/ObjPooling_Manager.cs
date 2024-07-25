using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPooling_Manager : MonoBehaviour
{
    public static ObjPooling_Manager instance;
    public GameObject bulletPrefab;
    public int maxBulletPool = 10;
    public List<GameObject> bulletPoolList;

    public GameObject[] EnemyPrefabs;
    private int maxEnemyPool = 10;
    public List<GameObject> enemyPoolList;


    void Awake()
    {
        instance = this;

        CreateBulletPool();
        CreateEnemyPool();
    }

    void CreateBulletPool()
    {
        GameObject BulletGroup = new GameObject("BulletGroup");
        for (int i = 0; i < 10; i++)
        {
            var bullets = Instantiate(bulletPrefab, BulletGroup.transform);
            bullets.name = $"bullet_{i+1}";
            bullets.SetActive(false);
            bulletPoolList.Add(bullets);
        }
    }
    public GameObject GetBulletPool()
    {
        for (int i = 0; i < bulletPoolList.Count; i++)
        {
            if (!bulletPoolList[i].activeSelf)
            {
                return bulletPoolList[i];
            }
        }
        return null;
    }

    void CreateEnemyPool()
    {
        GameObject EnemyGroup = new GameObject("EnemyGroup");
        for (int i = 0; i < maxEnemyPool; i++)
        {
            var enemy = Instantiate(EnemyPrefabs[Random.Range(0, 2)], EnemyGroup.transform);
            enemy.name = $"enemy_{i+1}";
            enemy.SetActive(false);
            enemyPoolList.Add(enemy);
        }
    }
    public GameObject GetEnemyPool()
    {
        for (int i = 0; i < enemyPoolList.Count; i++)
        {
            if (!enemyPoolList[i].activeSelf)
            {
                return enemyPoolList[i];
            }
        }
        return null;
    }





}

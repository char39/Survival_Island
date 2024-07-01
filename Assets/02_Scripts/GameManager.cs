using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy가 생성되는 로직, 게임 전체를 관리하는 클래스

// 1. Enemy Prefabs     2. 스폰위치    3. 스폰간격    4. 몇마리 스폰할지 

public class GameManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject monsterPrefab;
    public GameObject skeletonPrefab;
    public Transform[] Points;
    #region private_Vars
    private float ZombieTimePreV; //시간을 담는 변수로, 딜레이 구현을 위함
    private float MonsterTimePreV;
    private float SkeletonTimePreV;
    private int ZombieMaxCount = 10;
    private int MonsterMaxCount = 3;
    private int SkeletonMaxCount = 5;
    private float ZombieSpawnTime = 3.0f;
    private float MonsterSpawnTime = 8.0f;
    private float SkeletonSpawnTime = 5.0f;
    #endregion
    void Start()
    {
        //Hierarchy에서 SpawnPoints 옵젝 이름을 찾는다
        // 자기 자신 포함, 하위 옵젝들의 transform들을 points 배열에 다 넣는다
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        ZombieTimePreV = Time.time;   //현재 시간 할당
        MonsterTimePreV = Time.time;   //현재 시간 할당
        SkeletonTimePreV = Time.time;   //현재 시간 할당
    }

    void Update()
    {
        SpawnZombie();
        SpawnMonster();
        SpawnSkeleton();
    }

    private void SpawnZombie()
    {
        // 현재 시간 - 과거 시간 을 계산한 값이 spawnTime보다 크거나 같을경우
        if (Time.time - ZombieTimePreV >= ZombieSpawnTime)
        {
            // Hierarchy에서 ZOMBIE 태그가 붙은 개수를 카운트해서 넘김
            int zombieCount = GameObject.FindGameObjectsWithTag("ZOMBIE").Length;
            if (zombieCount < ZombieMaxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(zombiePrefab, Points[randPos].position, Points[randPos].rotation);
                ZombieTimePreV = Time.time;   //다시 현재 시간 할당
            }

        }
    }
    private void SpawnMonster()
    {
        // 현재 시간 - 과거 시간 을 계산한 값이 spawnTime보다 크거나 같을경우
        if (Time.time - MonsterTimePreV >= MonsterSpawnTime)
        {
            int MonsterCount = GameObject.FindGameObjectsWithTag("MONSTER").Length;
            if (MonsterCount < MonsterMaxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(monsterPrefab, Points[randPos].position, Points[randPos].rotation);
                MonsterTimePreV = Time.time;   //다시 현재 시간 할당
            }

        }
    }
    private void SpawnSkeleton()
    {
        // 현재 시간 - 과거 시간 을 계산한 값이 spawnTime보다 크거나 같을경우
        if (Time.time - SkeletonTimePreV >= SkeletonSpawnTime)
        {
            int skeletonCount = GameObject.FindGameObjectsWithTag("SKELETON").Length;
            if (skeletonCount < SkeletonMaxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(skeletonPrefab, Points[randPos].position, Points[randPos].rotation);
                SkeletonTimePreV = Time.time;   //다시 현재 시간 할당
            }

        }
    }
}


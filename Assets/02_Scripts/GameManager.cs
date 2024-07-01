using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy�� �����Ǵ� ����, ���� ��ü�� �����ϴ� Ŭ����

// 1. Enemy Prefabs     2. ������ġ    3. ��������    4. ��� �������� 

public class GameManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject monsterPrefab;
    public GameObject skeletonPrefab;
    public Transform[] Points;
    #region private_Vars
    private float ZombieTimePreV; //�ð��� ��� ������, ������ ������ ����
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
        //Hierarchy���� SpawnPoints ���� �̸��� ã�´�
        // �ڱ� �ڽ� ����, ���� �������� transform���� points �迭�� �� �ִ´�
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        ZombieTimePreV = Time.time;   //���� �ð� �Ҵ�
        MonsterTimePreV = Time.time;   //���� �ð� �Ҵ�
        SkeletonTimePreV = Time.time;   //���� �ð� �Ҵ�
    }

    void Update()
    {
        SpawnZombie();
        SpawnMonster();
        SpawnSkeleton();
    }

    private void SpawnZombie()
    {
        // ���� �ð� - ���� �ð� �� ����� ���� spawnTime���� ũ�ų� �������
        if (Time.time - ZombieTimePreV >= ZombieSpawnTime)
        {
            // Hierarchy���� ZOMBIE �±װ� ���� ������ ī��Ʈ�ؼ� �ѱ�
            int zombieCount = GameObject.FindGameObjectsWithTag("ZOMBIE").Length;
            if (zombieCount < ZombieMaxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(zombiePrefab, Points[randPos].position, Points[randPos].rotation);
                ZombieTimePreV = Time.time;   //�ٽ� ���� �ð� �Ҵ�
            }

        }
    }
    private void SpawnMonster()
    {
        // ���� �ð� - ���� �ð� �� ����� ���� spawnTime���� ũ�ų� �������
        if (Time.time - MonsterTimePreV >= MonsterSpawnTime)
        {
            int MonsterCount = GameObject.FindGameObjectsWithTag("MONSTER").Length;
            if (MonsterCount < MonsterMaxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(monsterPrefab, Points[randPos].position, Points[randPos].rotation);
                MonsterTimePreV = Time.time;   //�ٽ� ���� �ð� �Ҵ�
            }

        }
    }
    private void SpawnSkeleton()
    {
        // ���� �ð� - ���� �ð� �� ����� ���� spawnTime���� ũ�ų� �������
        if (Time.time - SkeletonTimePreV >= SkeletonSpawnTime)
        {
            int skeletonCount = GameObject.FindGameObjectsWithTag("SKELETON").Length;
            if (skeletonCount < SkeletonMaxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(skeletonPrefab, Points[randPos].position, Points[randPos].rotation);
                SkeletonTimePreV = Time.time;   //�ٽ� ���� �ð� �Ҵ�
            }

        }
    }
}


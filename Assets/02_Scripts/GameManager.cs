using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy�� �����Ǵ� ����, ���� ��ü�� �����ϴ� Ŭ����

// 1. Enemy Prefabs     2. ������ġ    3. ��������    4. ��� �������� 

public class GameManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] Points;
    private float timePreV;
    private int maxCount = 10;
    private float spawnTime = 3.0f;
    void Start()
    {
        //Hierarchy���� SpawnPoints ���� �̸��� ã�´�
        // �ڱ� �ڽ� ����, ���� �������� transform���� points �迭�� �� �ִ´�
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        timePreV = Time.time;   //���� �ð� �Ҵ�
    }

    void Update()
    {
        // ���� �ð� - ���� �ð� �� ����� ���� spawnTime���� ũ�ų� �������
        if (Time.time - timePreV >= spawnTime)
        {
            // Hierarchy���� ZOMBIE �±װ� ���� ������ ī��Ʈ�ؼ� �ѱ�
            int zombieCount = GameObject.FindGameObjectsWithTag("ZOMBIE").Length;
            if (zombieCount < maxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(zombiePrefab, Points[randPos].position, Points[randPos].rotation);
                timePreV = Time.time;   //�ٽ� ���� �ð� �Ҵ�
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy가 생성되는 로직, 게임 전체를 관리하는 클래스

// 1. Enemy Prefabs     2. 스폰위치    3. 스폰간격    4. 몇마리 스폰할지 

public class GameManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] Points;
    private float timePreV;
    private int maxCount = 10;
    private float spawnTime = 3.0f;
    void Start()
    {
        //Hierarchy에서 SpawnPoints 옵젝 이름을 찾는다
        // 자기 자신 포함, 하위 옵젝들의 transform들을 points 배열에 다 넣는다
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        timePreV = Time.time;   //현재 시간 할당
    }

    void Update()
    {
        // 현재 시간 - 과거 시간 을 계산한 값이 spawnTime보다 크거나 같을경우
        if (Time.time - timePreV >= spawnTime)
        {
            // Hierarchy에서 ZOMBIE 태그가 붙은 개수를 카운트해서 넘김
            int zombieCount = GameObject.FindGameObjectsWithTag("ZOMBIE").Length;
            if (zombieCount < maxCount)
            {
                int randPos = Random.Range(1, Points.Length);
                Instantiate(zombiePrefab, Points[randPos].position, Points[randPos].rotation);
                timePreV = Time.time;   //다시 현재 시간 할당
            }
            
        }
    }
}

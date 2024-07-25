using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//싱글톤 기법
//게임매니저는 게임 전체를 컨트롤 해야 하므로 접근이 쉬워야 한다.
//static 변수를 만든 후 이 변수가 대표해서 게임매니저에 접근하게 한다.
//무분별한 객체 생성을 막고 하나만 생성되게 하는 기법.

//Enemy가 생성되는 로직, 게임 전체를 관리하는 클래스

// 1. Enemy Prefabs     2. 스폰위치    3. 스폰간격    4. 몇마리 스폰할지

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform[] Points;
    public CanvasGroup canvasGroup;
    public Text killText;
    private float timePreV;
    public static int killCount = 0;
    public static bool isOpened = false;
    //public GameObject[] EnemyPrefabs;
    //public int maxCount = 10;
    //string enemyTag = "ENEMY";

    void Start()
    {
        Instance = this;
        //객체 생성. 게임매니저의 public이라고 선언된 변수나 메서드는 다른 스크립트에서 접근 가능
        //Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        canvasGroup = GameObject.Find("Inventory").GetComponent<CanvasGroup>();
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        EnemySpawn();

        if (Input.GetKeyDown(KeyCode.Escape))
            GamePause();

        if (Input.GetKeyDown(KeyCode.I))
            InventoryOnOff();
    }

    public bool isPaused = false;
    public void GamePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0.0f : 1.0f;

        var playerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = playerObj.GetComponents<MonoBehaviour>();

        foreach (var script in scripts)
            script.enabled = !isPaused;
    }

    public void InventoryOnOff()
    {
        isOpened = !isOpened;
        InventoryUpdate();
    }

    public void InventoryUpdate()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = playerObj.GetComponents<MonoBehaviour>();         // Player에게 있는 모든 스크립트들을 Get.

        foreach (var script in scripts)
            script.enabled = !isOpened;

        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;

        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible ? true : false;

        canvasGroup.alpha = isOpened ? 1.0f : 0.0f;
        canvasGroup.interactable = isOpened ? true : false;
        canvasGroup.blocksRaycasts = isOpened ? true : false;
    }

    void EnemySpawn()
    {
        timePreV += Time.deltaTime;

        if (timePreV >= 3.0f)
        {
            var enemy = ObjPooling_Manager.instance.GetEnemyPool();

            if (enemy != null)
            {
                int pos = Random.Range(1, Points.Length);
                enemy.transform.position = Points[pos].position;
                enemy.SetActive(true);
            }

            timePreV = Time.deltaTime;
        }

    }

    public void KillScore(int score)
    {
        killCount += score;
        killText.text = $"Kill : <color=#FFAAAA>{killCount.ToString()}</color>";
    }

}
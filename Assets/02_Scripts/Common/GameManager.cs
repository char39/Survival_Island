using System.Collections;
using System.Collections.Generic;
using DataInfo;
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
    private Camera mainCamera;
    private Transform mainCam_tr;
    private float timePreV;
    public static int killCount = 0;
    public static bool isOpened = false;
    //public GameObject[] EnemyPrefabs;
    //public int maxCount = 10;
    //string enemyTag = "ENEMY";
    [Header("DataManager")]
    [SerializeField] private DataManager dataManager;
    public GameData gameData;

    // 인벤토리 아이템이 변경 되었을 때 발생시킬 이벤트 정의
    public delegate void ItemChangeDelegate();
    public static event ItemChangeDelegate OnItemChange;
    [SerializeField] private GameObject slotList;
    public GameObject[] itemObjects;

    void Awake()
    {
        Instance = this;

        dataManager = GetComponent<DataManager>();
        dataManager.Initialized();

        canvasGroup = GameObject.Find("Inventory").GetComponent<CanvasGroup>();
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        mainCam_tr = mainCamera.transform;
        isOpened = false;
        LoadGameData();
        InventoryUpdate();
    }

    void Update()
    {
        EnemySpawn();

        if (Input.GetKeyDown(KeyCode.P))
            GamePause();

        if (Input.GetKeyDown(KeyCode.I))
            InventoryOnOff();
    }

    private void OnApplicationQuit()        // OnDisable() 이벤트 함수보다 우선순위가 낮고, 자동 호출된다.
    {
        SaveGameData();
    }

    void LoadGameData()
    {
        GameData data = dataManager.Load();
        gameData.hp = data.hp;
        gameData.damage = data.damage;
        gameData.speed = data.speed;
        gameData.killCounts = data.killCounts;
        gameData.equipItem = data.equipItem;
        if (gameData.equipItem.Count > 0)
            InventorySetUp();
        killText.text = $"Kill : <color=#FFAAAA>{gameData.killCounts.ToString().PadLeft(2)}</color>";
    }
    void InventorySetUp()       // 인벤토리 UI에 저장된 장착아이템을 갱신
    {
        var slots = slotList.GetComponentsInChildren<Transform>();
        for (int i = 0; i < gameData.equipItem.Count; i++)      // 인벤에 장착된 아이템 개수 만큼 체크
        {
            for (int j = 1; j < slots.Length; j++)              // 인벤토리 슬롯 전부를 체크
            {
                if (slots[j].childCount > 0)            // 인벤 슬롯에 자식 옵젝이 1개 이상이라면
                    continue;                           // 현재 조건의 for문 통과
                int itemIndex = (int)gameData.equipItem[i].itemType;    // GameData.cs의 Item 클래스의 itemType의 열거형 상수값 : 아이템 index 번호를 불러온다라는 뜻
                itemObjects[itemIndex].GetComponent<Transform>().SetParent(slots[j].transform);     // 인벤 슬롯에 아이템을 추가
                itemObjects[itemIndex].GetComponent<ItemInfo>().itemData = gameData.equipItem[i];   // 아이템 정보를 저장
                break;
            }
        }
    }
    void SaveGameData()
    {
        dataManager.Save(gameData);
    }
    public void AddItem(Item item)              // 인벤토리 UI에서 특정 아이템 선택시 효과 적용
    {
        if (gameData.equipItem.Contains(item))      // 아이템이 이미 존재한다면
            return;                                 // 같은 아이템을 추가하지 않고 함수 종료
        gameData.equipItem.Add(item);
        switch (item.itemType)                          // 아이템 종류에 따라서 값 적용
        {
            case Item.ItemType.HP:                          // HP 아이템이라면
                if (item.itemCalc == Item.ItemCalc.VALUE)       // 아이템 계산 방식이 VALUE라면
                    gameData.hp += item.value;                  // 아이템의 값을 더한다.
                else
                    gameData.hp += gameData.hp * item.value;    // 아이템 계산 방식이 PERSENT라면, 최대 HP의 아이템 값을 곱한 값을 더한다.
                break;
            case Item.ItemType.SPEED:
                if (item.itemCalc == Item.ItemCalc.VALUE)       // 아이템 계산 방식이 VALUE라면
                    gameData.speed += item.value;                  // 아이템의 값을 더한다.
                else
                    gameData.speed += gameData.speed * item.value;    // 아이템 계산 방식이 PERSENT라면, 최대 HP의 아이템 값을 곱한 값을 더한다.
                break;
            case Item.ItemType.GRENADE:

                break;
            case Item.ItemType.DAMAGE:
                if (item.itemCalc == Item.ItemCalc.VALUE)       // 아이템 계산 방식이 VALUE라면
                    gameData.damage += item.value;                  // 아이템의 값을 더한다.
                else
                    gameData.damage += gameData.damage * item.value;    // 아이템 계산 방식이 PERSENT라면, 최대 HP의 아이템 값을 곱한 값을 더한다.
                break;
        }
        OnItemChange();     // 델리게이트 이벤트. 인벤토리 아이템이 변경 되었을 때 발생.
    }
    public void RemoveItem(Item item)           // 인벤토리 UI에서 특정 아이템을 제거하면 아이템의 효과를 제거
    {
        if (!gameData.equipItem.Contains(item))      // 아이템이 없다면
            return;                                  // 값 변경 없이 종료
        gameData.equipItem.Remove(item);
        switch (item.itemType)                          // 아이템 종류에 따라서 값 적용
        {
            case Item.ItemType.HP:                          // HP 아이템이라면
                if (item.itemCalc == Item.ItemCalc.VALUE)       // 아이템 계산 방식이 VALUE라면
                    gameData.hp -= item.value;                  // 아이템의 값을 더한다.
                else
                    gameData.hp /= 1.0f + item.value;    // 아이템 계산 방식이 PERSENT라면, 최대 HP의 아이템 값을 곱한 값을 더한다.
                break;
            case Item.ItemType.SPEED:
                if (item.itemCalc == Item.ItemCalc.VALUE)       // 아이템 계산 방식이 VALUE라면
                    gameData.speed += item.value;                  // 아이템의 값을 더한다.
                else
                    gameData.speed /= 1.0f + item.value;    // 아이템 계산 방식이 PERSENT라면, 최대 HP의 아이템 값을 곱한 값을 더한다.
                break;
            case Item.ItemType.GRENADE:

                break;
            case Item.ItemType.DAMAGE:
                if (item.itemCalc == Item.ItemCalc.VALUE)       // 아이템 계산 방식이 VALUE라면
                    gameData.damage += item.value;                  // 아이템의 값을 더한다.
                else
                    gameData.damage /= 1.0f + item.value;    // 아이템 계산 방식이 PERSENT라면, 최대 HP의 아이템 값을 곱한 값을 더한다.
                break;
        }
        OnItemChange();     // 델리게이트 이벤트. 인벤토리 아이템이 변경 되었을 때 발생.
    }

    public bool isPaused = false;
    public void GamePause()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = playerObj.GetComponents<MonoBehaviour>();

        if (!isOpened)
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0.0f : 1.0f;
            foreach (var script in scripts)
                script.enabled = !isPaused;
        }
        else if (isOpened)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void InventoryOnOff()
    {
        if (!isPaused)
        {
            isOpened = !isOpened;
            InventoryUpdate();
        }
    }

    public void InventoryUpdate()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = playerObj.GetComponents<MonoBehaviour>();         // Player에게 있는 모든 스크립트들을 Get.

        foreach (var script in scripts)
            script.enabled = !isOpened;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
        ++gameData.killCounts;
        killText.text = $"Kill : <color=#FFAAAA>{gameData.killCounts.ToString().PadLeft(2)}</color>";
    }

    public IEnumerator CameraShake()
    {
        mainCam_tr = mainCamera.transform;
        for (int i = 30; i > 0; i--)
        {
            float ran = Random.Range(-0.1f, 0.1f) * i * 0.1f;
            Vector3 changePos = new Vector3(ran, ran, ran);
            mainCamera.transform.position += changePos;

            float angle = Random.Range(-2f, 2f) * i * 0.1f;
            Quaternion changeRot = Quaternion.Euler(angle, angle, angle);
            mainCamera.transform.rotation *= changeRot;

            yield return new WaitForSeconds(0.002f);
        }
        mainCamera.transform.position = mainCam_tr.position;
        mainCamera.transform.rotation = mainCam_tr.rotation;
    }

}
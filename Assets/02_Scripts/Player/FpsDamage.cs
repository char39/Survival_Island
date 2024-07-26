using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;

public class FpsDamage : MonoBehaviour
{
    [Header("UI")]
    public Image HpBar;
    public Text HpText;
    public int hp = 0;
    public int maxHp = 100;
    public string attackTag = "ATTACK_HITBOX";
    public bool isPlayerDie = false;
    public GameObject ScreenImage;

    public delegate void PlayerDie_Dele();
    public static event PlayerDie_Dele PlayerDie_Event;

    void Start()
    {
        //ScreenImage = GameObject.Find("Image_Screen").gameObject; //게임오브젝트가 체크 해제되어있으면 못찾음
        ScreenImage = GameObject.Find("Canvas_UI").transform.GetChild(5).gameObject;
        maxHp = (int)GameManager.Instance.gameData.hp;
        hp = maxHp;
        HpBar.color = Color.green;
        HpInfo();
        UpdateSetup();
    }

    void OnEnable()
    {
        GameManager.OnItemChange += UpdateSetup;    // 함수를 이벤트로 등록
    }

    void UpdateSetup()
    {
        maxHp = (int)GameManager.Instance.gameData.hp;  // 게임매니저에서 최대hp가 바뀌면 실시간으로 최대hp를 변경해줌
        hp += (int)GameManager.Instance.gameData.hp - hp;   // 아이템으로 인한 최대hp에서 현재hp를 뺀 ?
        HpInfo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(attackTag))
        {
            hp -= 22;
            HpInfo();
            if (hp <= 0)
                PlayerDie_Ev();//PlayerDie();
        }
    }
    private void HpInfo()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);     //hp가 0보다 작으면 0, maxHp보다 크면 maxHp로 제한
        HpBar.fillAmount = (float)hp / (float)maxHp;
        if (HpBar.fillAmount <= 0.3f)
            HpBar.color = Color.red;
        else if (HpBar.fillAmount <= 0.5f)
            HpBar.color = Color.yellow;
        else if (HpBar.fillAmount <= 1f)
            HpBar.color = Color.green;
        HpText.text = $"HP : <color=#FFAAAA>{hp.ToString()}</color>";
    }

    void PlayerDie()
    {
        ScreenImage.SetActive(true);    //해당 오브젝트 활성화
        isPlayerDie = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        //런타임에서 ENEMY라는 태그를 가진 오브젝트를 찾아서 enemies 배열에 저장
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SendMessage("PlayerDeath", SendMessageOptions.DontRequireReceiver);
            //다른 게임오브젝트에 있는 함수를 호출할 때 사용하는 함수
        }

        Invoke("MoveNextScene", 3.0f);
        //3초 후 MoveNextScene 함수 호출
    }
    
    public void PlayerDie_Ev()
    {
        ScreenImage.SetActive(true);    //해당 오브젝트 활성화
        isPlayerDie = true;
        PlayerDie_Event();
        Invoke("MoveNextScene", 3.0f);
    }

    void MoveNextScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
//Scene 관련 기능을 사용하기 위해 추가

//MonoBehaviour를 상속받은 UI_Manager 클래스
//없으면 곤란함
public class UI_Manager : MonoBehaviour
{
    public Text finalKillScore;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (!GameObject.Find("Text_FinalkillText"))
            return;
        finalKillScore = GameObject.Find("Text_FinalKillText").GetComponent<Text>();
        finalKillScore.text = $"Kill Score : {GameManager.killCount.ToString()}";
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
        SceneManager.LoadScene("PlayerScene", LoadSceneMode.Additive);
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR    //전처리기 지시어 : [#들어간것] 컴파일전 미리 기능을 정의
        EditorApplication.isPlaying = false;
        #else               //UNITY_EDITOR가 정의되어 있지 않을 때
        Application.Quit();
        #endif              //전처리기 지시어 끝
    }
}

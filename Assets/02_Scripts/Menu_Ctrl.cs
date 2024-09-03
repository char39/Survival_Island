using UnityEngine;

public class Menu_Ctrl : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform Menu_Frame;
    public RectTransform Pause;
    public RectTransform Sound;
    public RectTransform Screen;
    public GameObject player;

    public bool IsMenuOpened = false;
    public bool IsPauseOpened = false;
    public bool IsSoundOpened = false;
    public bool IsScreenOpened = false;

    void Start()
    {
        canvas = GameObject.Find("Canvas_UI").GetComponent<Canvas>();
        Menu_Frame = canvas.transform.GetChild(8).GetComponent<RectTransform>();
        Pause = Menu_Frame.transform.GetChild(0).GetComponent<RectTransform>();
        Sound = Menu_Frame.transform.GetChild(1).GetComponent<RectTransform>();
        Screen = Menu_Frame.transform.GetChild(2).GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            MenuOpenToggle();
            MenuStateUpdate();
        }
    }

    private void MenuOpenToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsMenuOpened = true;
            IsPauseOpened = true;
            IsSoundOpened = false;
            IsScreenOpened = false;
        }
    }

    private void MenuStateUpdate()
    {
        Menu_Frame.gameObject.SetActive(IsMenuOpened);
        Pause.gameObject.SetActive(IsPauseOpened);
        Sound.gameObject.SetActive(IsSoundOpened);
        Screen.gameObject.SetActive(IsScreenOpened);
        Time.timeScale = IsMenuOpened ? 0 : 1;

        var scripts = player.GetComponents<MonoBehaviour>();         // Player에게 있는 모든 스크립트들을 Get.
        foreach (var script in scripts)
            script.enabled = !IsMenuOpened;
        if (IsMenuOpened)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void Resume_B()
    {
        IsMenuOpened = false;
    }

    public void Sound_B()
    {
        IsPauseOpened = false;
        IsSoundOpened = true;
        IsScreenOpened = false;
    }

    public void Screen_B()
    {
        IsPauseOpened = false;
        IsSoundOpened = false;
        IsScreenOpened = true;
    }

    public void Back_B()
    {
        IsPauseOpened = true;
        IsSoundOpened = false;
        IsScreenOpened = false;
    }
}

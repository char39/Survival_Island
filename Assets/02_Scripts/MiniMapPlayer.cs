using UnityEngine.UI;
using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    public Image minimapPlayer;
    public float time;

    void Start()
    {
        minimapPlayer = GetComponent<Image>();
        time = 0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time % 2 < 1)
            minimapPlayer.color = new Color(minimapPlayer.color.r, minimapPlayer.color.g, minimapPlayer.color.b, 0f);
        else
            minimapPlayer.color = new Color(minimapPlayer.color.r, minimapPlayer.color.g, minimapPlayer.color.b, 0.5f);
        
    }
}

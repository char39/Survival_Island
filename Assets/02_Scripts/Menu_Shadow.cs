using UnityEngine;
using UnityEngine.UI;

public class Menu_Shadow : MonoBehaviour
{
    public Dropdown dropdown;
    public Light light_;

    void Start()
    {
        dropdown.onValueChanged.AddListener(OnShadowChanged);    
    }

    public void OnShadowChanged(int value)
    {
        if (dropdown != null || light_ != null)
        {
            switch (value)
            {
                case 0:
                    light_.shadows = LightShadows.None;
                    break;
                case 1:
                    light_.shadows = LightShadows.Soft;
                    break;
                case 2:
                    light_.shadows = LightShadows.Hard;
                    break;
            }
        }
    }
}

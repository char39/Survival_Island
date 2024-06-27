using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCtrl : MonoBehaviour
{
    public Animation CombatSGAni;
    public Light FlashLightCtrl;
    public AudioClip FlashSound;    //����� ����
    public AudioSource AudioSource; //����� �ҽ�.
    public bool isRun = false;
    void Start()
    {
        
    }

    void Update()
    {
        GunCtrl();
        FlashCtrl();

    }

    private void FlashCtrl()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlashLightCtrl.enabled = !FlashLightCtrl.enabled;
            AudioSource.PlayOneShot(FlashSound, 1.0f);  //() ���� = ���������, �Ҹ� ����[1.0�� max]
        }
    }

    private void GunCtrl()
    {
        //GetKey() : Ű�ٿ� ���� ��� �Է�
        //GetKeyDown() : Ű�ٿ� ��� 1ȸ �Է�
        //GetKeyUp() : Ű�ٿ� �� ���� ��� 1ȸ �Է�
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        { 
            CombatSGAni.Play("running");
            isRun = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && (isRun == true))
        {
            CombatSGAni.Play("runStop");
            isRun = false;
        }
    }
}

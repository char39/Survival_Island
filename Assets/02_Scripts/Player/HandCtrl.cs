using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCtrl : MonoBehaviour
{
    public Animation CombatSGAni;
    public Light FlashLightCtrl;
    public AudioClip FlashSound;    //오디오 파일
    public AudioSource AudioSource; //오디오 소스.
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
            AudioSource.PlayOneShot(FlashSound, 1.0f);  //() 내부 = 오디오파일, 소리 볼륨[1.0이 max]
        }
    }

    private void GunCtrl()
    {
        //GetKey() : 키다운 동안 계속 입력
        //GetKeyDown() : 키다운 즉시 1회 입력
        //GetKeyUp() : 키다운 후 뗄시 즉시 1회 입력
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

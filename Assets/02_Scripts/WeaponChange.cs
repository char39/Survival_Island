using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public SkinnedMeshRenderer spas12;
    public MeshRenderer[] AK47;
    public MeshRenderer[] M4A1;
    public Animation ComBatSG;
    void Start()
    {

    }

    void Update()
    {
        // Alpha1은 일자 숫자키 1
        if (Input.GetKeyDown(KeyCode.Alpha1))        // AK47 활성화 나머지 비활성화
            WeaponChange1();
        else if (Input.GetKeyDown(KeyCode.Alpha2))  // M4A1 활성화 나머지 비활성화
            WeaponChange2();
        else if (Input.GetKeyDown(KeyCode.Alpha3))  // spas12 활성화 나머지 비활성화
            WeaponChange3();
    }

    private void WeaponChange1()
    {
        ComBatSG.Play("draw");
        for (int i = 0; i < AK47.Length; i++)
            AK47[i].enabled = true;
        spas12.enabled = false;
        for (int i = 0; i < M4A1.Length; i++)
            M4A1[i].enabled = false;
    }
    private void WeaponChange2()
    {
        ComBatSG.Play("draw");
        for (int i = 0; i < AK47.Length; i++)
            AK47[i].enabled = false;
        spas12.enabled = false;
        for (int i = 0; i < M4A1.Length; i++)
            M4A1[i].enabled = true;
    }
    private void WeaponChange3()
    {
        ComBatSG.Play("draw");
        foreach (var i in AK47)
            i.enabled = false;
        foreach (var i in M4A1)
            i.enabled = false;
        spas12.enabled = true;
    }
}
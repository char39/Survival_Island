using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.캔버스 UI가 카메라를 쳐다본다. [좀비hpUI]

public class LookAtCamera : MonoBehaviour
{
    public Transform mainCam;
    public Transform transf;

    void Start()
    {
        mainCam = Camera.main.transform;        //MainCamera의 위치값을 찾음
        transf = GetComponent<Transform>();         //자기 자신의 위치값을 찾음
    }

    void Update()
    {
        transf.LookAt(mainCam);
        // 캔버스가 메인카메라를 쳐다본다.
    }
}

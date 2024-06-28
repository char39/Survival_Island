using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.ĵ���� UI�� ī�޶� �Ĵٺ���. [����hpUI]

public class LookAtCamera : MonoBehaviour
{
    public Transform mainCam;
    public Transform transf;

    void Start()
    {
        mainCam = Camera.main.transform;        //MainCamera�� ��ġ���� ã��
        transf = GetComponent<Transform>();         //�ڱ� �ڽ��� ��ġ���� ã��
    }

    void Update()
    {
        transf.LookAt(mainCam);
        // ĵ������ ����ī�޶� �Ĵٺ���.
    }
}

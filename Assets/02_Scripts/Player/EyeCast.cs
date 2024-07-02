using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour
{
    private Transform tr;   //자기자신 transform
    private Ray ray;        //광선
    private RaycastHit hit; //충돌체 정보. 광선이 충돌한 대상체의 정보를 담고있는 구조체
    private float dist = 20.0f; //광선의 길이를 미리 할당
    public CrossHair c_Hair;    //CrossHair 스크립트를 사용하기 위한 변수
    void Start()
    {
        c_Hair = GameObject.Find("Canvas_UI").transform.GetChild(3).GetComponent<CrossHair>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        ray = new Ray(tr.position, tr.forward);
        // 광선의 방향은 자기자신의 앞쪽으로 설정
        // 방향과 거리 : Velocity
        // 정규화된 벡터의 길이는 1.0f
        // 동적 할당 하자마자 위치와 방향과 거리를 지정
        Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);
        // 광선을 Scene에서 시각적으로 확인하기 위해 디버그 레이를 그림

        // ray.origin, ray.direction, out hit, 1<<6|1<<7|1<<8
        // 위처럼 작성해도 무방
        if (Physics.Raycast(ray, out hit, dist, 1<<6|1<<7|1<<8))
        {
            // 광선이 충돌한 대상체의 정보를 hit에 저장
            // 충돌한 대상체의 레이어가 6, 7, 8일 경우에만 아래의 코드를 실행
            c_Hair.isGaze = true;
        }
        else
        {
            // 광선이 충돌하지 않았을 경우
            c_Hair.isGaze = false;
        }


    }
}

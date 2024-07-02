using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    Transform tr;
    Image crossHair;
    float startTime;    //crossHair가 커지기 시작한 시간을 저장
    float duration = 0.25f;  //crossHair가 커지는 시간(속도)
    float minSize = 0.7f;    //crossHair의 최소 크기
    float maxSize = 1.2f;    //crossHair의 최대 크기
    Color originColor = new Color(1f, 1f, 1f, 0.8f); //crossHair의 초기 색상
    Color gazeColor = Color.red;   //crossHair의 시선이 맞았을 때의 색상
    public bool isGaze = false;    //광선이 맞았는지 여부
    void Start()
    {
        tr = transform; // tr = GetComponent<Transform>();
        crossHair = GetComponent<Image>();
        startTime = Time.time;
        //          x, y, z 동일하게 크기를 가지기 위해 Vector3.one 사용
        tr.localScale = Vector3.one * minSize;
        //오브젝트의 부모와 관계없이 crossHair의 고유 크기를 최소로 설정
    }

    void Update()
    {
        if (isGaze)
        {
            float time = (Time.time - startTime) / duration;    //지난시간 / duration
            tr.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, time);
            //Lerp : 선형 보간법. 최소 크기부터 최대 크기까지 부드럽게
            //       바로 위 float time의 시간만큼 커지기 위함.
            crossHair.color = gazeColor;    //광선이 맞았을 때의 색상으로 변경
        }
        else
        {
            tr.localScale = Vector3.one * minSize;
            crossHair.color = originColor;  //광선이 맞지 않았을 때의 색상으로 변경
            startTime = Time.time;          //시간 초기화
        }


    }
}

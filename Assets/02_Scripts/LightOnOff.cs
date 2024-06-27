using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public Light StairLight;
    public AudioSource Source;
    public AudioClip Clip;

    void Start()
    {
        
    }

    // 유니티 Inspector의 Capsule Collider 컴포넌트에서 Is Trigger을 On 했을 때,
    // 통과 하면서 충돌 감지하는 함수를 콜백 함수라고 함. 스스로 호출하기 때문
    // 충돌 되는 순간 알아서 호출됨. 겜메에서 스텝 내부 이벤트가 아니라 스텝과 같은 충돌판정이라 생각하면 편함.
    // tip) Collider는 충돌을 감지하는 구조체
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StairLight.enabled = true; // Collider 구조체에 Player Tag를 가진 오브젝트가 충돌 판정시, 이 변수가 실행.
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StairLight.enabled = false; // Collider 구조체에 Player Tag를 가진 오브젝트가 충돌 판정이 사라질시, 이 변수가 실행.
            
        }
    }

    void Update()
    {
        
    }
    

}

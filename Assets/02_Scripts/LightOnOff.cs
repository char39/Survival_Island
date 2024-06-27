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

    // ����Ƽ Inspector�� Capsule Collider ������Ʈ���� Is Trigger�� On ���� ��,
    // ��� �ϸ鼭 �浹 �����ϴ� �Լ��� �ݹ� �Լ���� ��. ������ ȣ���ϱ� ����
    // �浹 �Ǵ� ���� �˾Ƽ� ȣ���. �׸޿��� ���� ���� �̺�Ʈ�� �ƴ϶� ���ܰ� ���� �浹�����̶� �����ϸ� ����.
    // tip) Collider�� �浹�� �����ϴ� ����ü
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StairLight.enabled = true; // Collider ����ü�� Player Tag�� ���� ������Ʈ�� �浹 ������, �� ������ ����.
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StairLight.enabled = false; // Collider ����ü�� Player Tag�� ���� ������Ʈ�� �浹 ������ �������, �� ������ ����.
            
        }
    }

    void Update()
    {
        
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody rb;
    void Start()
    {
        rb.AddForce(transform.forward * Speed); //Local 좌표로 Speed만큼 나감.
                                                //Vector3.forward = Global 좌표. vector 3차원좌표
        Destroy(this.gameObject, 3.0f);         //본인 오브젝트를 3초 후에 삭제한다.

    }

    //void Update()
    //{
        
    //}
}

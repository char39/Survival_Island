using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public TrailRenderer trail;
    public float Speed = 10f;
    public Rigidbody rb;
    public int damage = 20;
    void OnEnable()
    {
        trail = GetComponent<TrailRenderer>();
        trail.material = Resources.Load<Material>("Material_M");
        rb.AddForce(transform.forward * Speed); //Local 좌표로 Speed만큼 나감.
                                                //Vector3.forward = Global 좌표. vector 3차원좌표
        Invoke("Disable", 3.0f);                //3초 후에 OnDisable 함수를 호출한다.
        //Destroy(this.gameObject, 3.0f);         //본인 오브젝트를 3초 후에 삭제한다.
    }
    void Disable()
    {
        trail.Clear();
        rb.Sleep();
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    void OnDisable()
    {
        rb.Sleep();
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

}

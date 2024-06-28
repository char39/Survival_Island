using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSpark : MonoBehaviour
{
    [Header(".")]
    public GameObject SparkPrefab;
    public AudioSource source;
    public AudioClip SparkClip;
    FireCtrl fireCtrl;
    void Start()
    {
        fireCtrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>();
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "BULLET")
        {
            //Destroy(col.gameObject);
            //source.PlayOneShot(SparkClip, 1.0f);
            //var spark = Instantiate(SparkPrefab, col.transform.position, Quaternion.identity);
            ////                      무엇을         어디에[충돌위치]       회전없이 생성
            //Destroy(spark, 2.0f);

            Destroy(col.gameObject);
            source.PlayOneShot(SparkClip, 1.0f);

            Vector3 hitPos = col.transform.position;
            Vector3 firePosNormal = -(hitPos - fireCtrl.firePos.position).normalized;
            Quaternion hitRot = Quaternion.LookRotation(firePosNormal);
            var spark = Instantiate(SparkPrefab, hitPos, hitRot);
            Destroy(spark, 2.0f);
        }
    }




    //void Update()
    //{

    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 10f, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}

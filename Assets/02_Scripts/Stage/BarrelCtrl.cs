using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이시 배럴 색상을 랜덤하게 변경
// 배럴이 5번 이상 총알에 맞았을 때 폭발 구현

public class BarrelCtrl : MonoBehaviour
{
    [SerializeField]private GameObject exploEffect;
    [SerializeField]private Texture[] textures;
    [SerializeField]private MeshRenderer meshRenderer;
    [SerializeField]private Rigidbody rb;
    [SerializeField]private int hitCount = 0;
    [SerializeField]private bool isExplo = false;
    [SerializeField]private AudioClip clip_explo;
    private readonly string bulletTag = "BULLET";

    [SerializeField]private MeshFilter meshFilter;
    [SerializeField]private Mesh[] meshes;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        textures = Resources.LoadAll<Texture>("Texture");
        exploEffect = Resources.Load<GameObject>("Prefab/BigExplosionEffect");
        meshes = Resources.LoadAll<Mesh>("Mesh");
        meshRenderer.material.mainTexture = textures[Random.Range(0, textures.Length - 1)];
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(bulletTag))
        {
            hitCount++;
            if (hitCount == 5)
            {
                ExplosionBarrel();
                //StartCoroutine(GameManager.Instance.CameraShake());
            }
        }
    }
    

    private void ExplosionBarrel()
    {
        GameObject eff = Instantiate(exploEffect, transform.position, Quaternion.identity);
        Destroy(eff, 2.0f);

        Collider[] cols = Physics.OverlapSphere(transform.position, 20f, 1 << 6 | 1 << 7 | 1 << 8 | 1 << 9);
        foreach (Collider col in cols)
        {
            Rigidbody rb_col = col.GetComponent<Rigidbody>();
            if (rb_col != null)
            {
                //SoundManager.S_Instance.PlaySound(transform.position, clip_explo);
                rb_col.mass = 20.0f;
                rb_col.AddExplosionForce(1000, transform.position, 20f, 1200f);
                col.gameObject.SendMessage("ExplosionDie", SendMessageOptions.DontRequireReceiver);
            }
            Invoke("BarrelMassChange", 3.0f);
        }
        int index = Random.Range(1, meshes.Length);
        meshFilter.sharedMesh = meshes[index];
        GetComponent<MeshCollider>().sharedMesh = meshes[index];
    }
    void BarrelMassChange()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 20f, 1 << 9);
        foreach (Collider col in cols)
        {
            Rigidbody rb_col = col.GetComponent<Rigidbody>();
            if (rb_col != null)
            {
                rb_col.mass = 60.0f;
            }
        }
    }
    

}

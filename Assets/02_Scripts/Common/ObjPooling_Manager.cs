using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPooling_Manager : MonoBehaviour
{
    public static ObjPooling_Manager instance;
    public GameObject bulletPrefab;
    public int maxBulletPool = 10;
    public List<GameObject> bulletPoolList;

    void Awake()
    {
        instance = this;
        CreateBulletPool();
    }

    void CreateBulletPool()
    {
        GameObject BulletGroup = new GameObject("BulletGroup");
        for (int i = 0; i < 10; i++)
        {
            var bullets = Instantiate(bulletPrefab, BulletGroup.transform);
            bullets.name = $"bullet_{i+1}";
            bullets.SetActive(false);
            bulletPoolList.Add(bullets);
        }
    }
    public GameObject GetBulletPool()
    {
        for (int i = 0; i < bulletPoolList.Count; i++)
        {
            if (!bulletPoolList[i].activeSelf)
            {
                return bulletPoolList[i];
            }
        }
        return null;
    }
}

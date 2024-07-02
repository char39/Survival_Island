using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    [Header("Components")]
    public GameObject bulletPrefab;     //총알 옵젝
    public Transform firePos;           //발사 위치
    public Animation fireAni;           //총을 발사할때 애니메이션
    public AudioSource Source;
    public AudioClip fireClip;
    public ParticleSystem muzzleFlash;  //가져온 이펙트 이름을 변수 이름으로 한것
    [Header("Variations")]
    public float fireTime;
    public HandCtrl handCtrl;
    public int BulletCount = 0;
    public bool isReload = false;
    void Start()
    {
        handCtrl = this.gameObject.GetComponent<HandCtrl>();
        fireTime = Time.time;
        //현재 시간을 대입
        muzzleFlash.Stop();
    }

    void Update()
    {
        #region 단발
        ////마우스 왼쪽 버튼 눌림시 1회 실행.      0은 왼쪽클, 1은 우측클, 2는 휠버튼클
        //if (Input.GetMouseButtonDown(0))
        //    Fire(); //함수 호출
        #endregion
        #region 연발
        //현재 시간에서 과거 시간을 빼서 0.1초 이상일때, 발사 후 firetime을 현재 시간에 대입
        if ((Input.GetMouseButton(0)) && (Time.time - fireTime > 0.1f) && (!handCtrl.isRun) && (!isReload))
        {
            Fire();
            fireTime = Time.time;
        }
        #endregion
        if (Input.GetMouseButtonUp(0) || (handCtrl.isRun) || (isReload)) //대신 Invoke(,)를 써도됨 [일정 간격마다 실행]
        {
            muzzleFlash.Stop();
        }
    }

    void Fire()     //총알 발사 함수
    {
        BulletCount++;
        //오브젝트 생성 함수
        //          무엇을         어디에             어떤 rotation 으로
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        Source.PlayOneShot(fireClip, 1.0f);
        fireAni.Play("fire");
        muzzleFlash.Play();
        if(BulletCount >= 10)
        {
            // Start Co Routine : 게임 중 개발자가 원하는 프레임을
            //                    만드려고 할 때 사용
            StartCoroutine(Reload());   // Reload() 호출

        }
    }
    IEnumerator Reload()
    {
        isReload = true;
        yield return new WaitForSeconds(0.2f);  // 0.2초 대기
        fireAni.Play("pump3");                  // Reload 애니메이션 재생
        yield return new WaitForSeconds(0.7f);    // n초 대기
        BulletCount = 0;
        isReload = false;
    }
}

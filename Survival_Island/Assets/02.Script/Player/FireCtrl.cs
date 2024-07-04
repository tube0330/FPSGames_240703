using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    [Header("컴퍼넌트들")]
    //총알오브젝트
    public GameObject bulletPrefab; //총알 오브젝트
    public Transform firePos;   //발사 위치
    public Animation fireAni;   //애니메이션
    public AudioSource source;
    public AudioClip clip;
    public ParticleSystem muzzleFlash;  //이팩트

    [Header("각종변수들")]
    public float fireTime;
    public HandleCtrl hc;
    public int bulletCount = 0;
    public bool reload = false;
    void Start()
    {
        hc = this.gameObject.GetComponent<HandleCtrl>();
        fireTime = Time.time;   //현재 시간을 대입

        muzzleFlash.Stop();
    }


    void Update()
    {
        #region 마우스 왼쪽 버튼 클릭
        /*if (Input.GetMouseButtonDown(0) && hc.isRun == false)    //0은 왼쪽 1은 오른쪽
            Fire();*/
        #endregion

        #region 총알 발사 연사로 하는 로직
        if (Input.GetMouseButton(0))
        {
            if (Time.time - fireTime > 0.5f)
            {    //현재시간에서 과거시간을 빼면 흘러간 시간이 된다.
                if (!hc.isRun && !reload)
                    Fire();

                fireTime = Time.time;
            }
        }
        #endregion
        /*if(Input.GetMouseButtonUp(0))
            muzzleFlash.Stop();*/
    }

    void Fire() //총알 발사 함수
    {
        bulletCount++;

        //object 생성
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        //what          where          How rotation

        source.PlayOneShot(clip, 1.0f);
        fireAni.Play("fire");
        muzzleFlash.Play();
        Invoke("MuzzleFlashDisable", 0.03f);    //원하는 시간간격만큼 메서드를 호출
                                                //메서드명             //시간

        if (bulletCount == 10)
        {
            StartCoroutine(Reload());
        }

    }

    IEnumerator Reload()
    {
        reload = true;
        fireAni.Play("pump1");
        //0.5초 후에
        yield return new WaitForSeconds(0.5f);
        //리로드 애니메이션 재생

        bulletCount = 0;
        reload = false;
    }

    void MuzzleFlashDisable()
    {
        muzzleFlash.Stop();
    }

}
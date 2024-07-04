using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    [Header("���۳�Ʈ��")]
    //�Ѿ˿�����Ʈ
    public GameObject bulletPrefab; //�Ѿ� ������Ʈ
    public Transform firePos;   //�߻� ��ġ
    public Animation fireAni;   //�ִϸ��̼�
    public AudioSource source;
    public AudioClip clip;
    public ParticleSystem muzzleFlash;  //����Ʈ

    [Header("����������")]
    public float fireTime;
    public HandleCtrl hc;
    public int bulletCount = 0;
    public bool reload = false;
    void Start()
    {
        hc = this.gameObject.GetComponent<HandleCtrl>();
        fireTime = Time.time;   //���� �ð��� ����

        muzzleFlash.Stop();
    }


    void Update()
    {
        #region ���콺 ���� ��ư Ŭ��
        /*if (Input.GetMouseButtonDown(0) && hc.isRun == false)    //0�� ���� 1�� ������
            Fire();*/
        #endregion

        #region �Ѿ� �߻� ����� �ϴ� ����
        if (Input.GetMouseButton(0))
        {
            if (Time.time - fireTime > 0.5f)
            {    //����ð����� ���Žð��� ���� �귯�� �ð��� �ȴ�.
                if (!hc.isRun && !reload)
                    Fire();

                fireTime = Time.time;
            }
        }
        #endregion
        /*if(Input.GetMouseButtonUp(0))
            muzzleFlash.Stop();*/
    }

    void Fire() //�Ѿ� �߻� �Լ�
    {
        bulletCount++;

        //object ����
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        //what          where          How rotation

        source.PlayOneShot(clip, 1.0f);
        fireAni.Play("fire");
        muzzleFlash.Play();
        Invoke("MuzzleFlashDisable", 0.03f);    //���ϴ� �ð����ݸ�ŭ �޼��带 ȣ��
                                                //�޼����             //�ð�

        if (bulletCount == 10)
        {
            StartCoroutine(Reload());
        }

    }

    IEnumerator Reload()
    {
        reload = true;
        fireAni.Play("pump1");
        //0.5�� �Ŀ�
        yield return new WaitForSeconds(0.5f);
        //���ε� �ִϸ��̼� ���

        bulletCount = 0;
        reload = false;
    }

    void MuzzleFlashDisable()
    {
        muzzleFlash.Stop();
    }

}
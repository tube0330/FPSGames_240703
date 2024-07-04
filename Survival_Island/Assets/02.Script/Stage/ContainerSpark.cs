using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerSpark : MonoBehaviour
{

    public GameObject SparkPrefab;
    public AudioClip SparkClip;
    public AudioSource source;
    void Start()
    {
        
    }

    //is trigger üũ ���� ���� �� ���
    private void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "BULLET")
        {
            Destroy(col.gameObject);
            source.PlayOneShot(SparkClip, 1.0f);

            /*Vector3 hitpos = col.transform.position;
            Vector3 firePosnormal = hitpos - FireCtrl.firePos.position;
            firePosnormal = firePosnormal.normalized;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, firePosnormal);*/
            var spark = Instantiate(SparkPrefab,col.transform.position, Quaternion.identity);
            //                         what         where �浹�� ��ġ        ȸ������ �ʴ´�.

            Destroy(spark, 2.0f);
        }
    }
}

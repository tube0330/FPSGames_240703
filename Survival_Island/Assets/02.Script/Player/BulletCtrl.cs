using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float speed = 1500f;
    public Rigidbody rb;
    public int damage = 20;

    void Start()
    {
        rb.AddForce(transform.forward * speed); //���� ��ǥ�� ���ǵ常ŭ ������
        //Vector3.forward: �۷ι� ��ǥ�� ����. �̰� ���� �ȵ� ������� ��ǥ�� �ڲ� �ٲ�ϱ�

        Destroy(gameObject, 3.0f);  //�ڱ��ڽ��� ������Ʈ�� 3�� �Ŀ� �޸𸮿��� �����Ѵ�.
    }
}

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
        rb.AddForce(transform.forward * speed); //로컬 좌표로 스피드만큼 나간다
        //Vector3.forward: 글로벌 좌표로 나감. 이거 쓰면 안됨 사용자의 좌표가 자꾸 바뀌니까

        Destroy(gameObject, 3.0f);  //자기자신의 오브젝트를 3초 후에 메모리에서 삭제한다.
    }
}

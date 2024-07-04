using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDamage : MonoBehaviour
{
    [Header("컴포넌트")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;
    public GameObject bloodEffect;
    public BoxCollider boxCol;
    public MeshRenderer meshRend;

    [Header("관련 변수")]
    public string playerTag = "Player";
    public string bulletTag = "BULLET";
    public string HitStr = "HitTrigger";
    public string dieStr = "DieTrigger";
    public bool IsDie = false;

    [Header("UI")]
    public Image HPBar;
    public int maxHP = 100;
    public int HPInit = 0;
    public Text HPText;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        HPInit = maxHP;
        HPBar.color = Color.green;
    }

    private void OnCollisionEnter(Collision col)    //충돌 감지하는
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.mass = 800f;    //충돌할 때 무게 증가
            rb.isKinematic = false; //물리없앰 = false => 물리력 있게
            rb.freezeRotation = true;
        }

        else if (col.gameObject.CompareTag(bulletTag))
        {
            HitInfo(col);

            HPInit -= col.gameObject.GetComponent<BulletCtrl>().damage;
            HPBar.fillAmount = (float)HPInit / (float)maxHP;
            HPText.text = ($"HP: <color=#ff0000>{HPInit.ToString()}</color>");

            if (HPBar.fillAmount <= 0.3f) HPBar.color = Color.red;
            else if (HPBar.fillAmount <= 0.5f) HPBar.color = Color.yellow;

            if (HPInit <= 0)
            {
                MonsterDie();
                GameManager.Instance.KillScore(1);
            }
        }
    }

    private void HitInfo(Collision col)
    {
        Destroy(col.gameObject);
        animator.SetTrigger(HitStr);

        //Vector3 hitPos = col.transform.position;
        Vector3 hitPos = col.contacts[0].point;

        //Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, hitPos.normalized);
        Quaternion rot = Quaternion.LookRotation(-(col.contacts[0].normal));

        var blood = Instantiate(bloodEffect, hitPos, rot);
        Destroy(blood, Random.Range(0.8f, 1.2f));
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
            rb.mass = 75f;
    }

    void MonsterDie()
    {
        animator.SetTrigger(dieStr);
        capCol.enabled = false; //죽었을 때 Collider비활성화해서 못따라오게

        rb.isKinematic = true; //물리력 제거
        IsDie = true;

    }

    public void boxColEnable()
    {
        boxCol.enabled = true;  //enabled: 활성화
        meshRend.enabled = false;
    }

    public void boxColDisable()
    {
        boxCol.enabled = false;
        meshRend.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonDamage : MonoBehaviour
{
    [Header("컴포넌트")]
    public Rigidbody rb;
    public CapsuleCollider col;
    public Animator ani;
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
    public int HPInitiate = 0;
    public Text HPText;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        ani = GetComponent<Animator>();
        HPInitiate = maxHP;
        HPBar.color = Color.green;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.mass = 800f;
            rb.isKinematic = false;
            rb.freezeRotation = true;
        }

        else if (col.gameObject.CompareTag(bulletTag))
        {
            HitInfo(col);

            HPInitiate -= col.gameObject.GetComponent<BulletCtrl>().damage;
            HPBar.fillAmount = (float)HPInitiate / (float)maxHP;
            HPText.text = ($"HP: <color=#ff0000>{HPInitiate.ToString()}</color>");

            if (HPBar.fillAmount <= 0.3f) HPBar.color = Color.red;
            else if (HPBar.fillAmount <= 0.5f) HPBar.color = Color.yellow;

            if (HPInitiate <= 0)
            {
                SkeletonDie();
               
            }

        }
    }

    private void HitInfo(Collision col)
    {
        Destroy(col.gameObject);
        ani.SetTrigger(HitStr);

        Vector3 hitPos = col.transform.position;
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, hitPos.normalized);

        var blood = Instantiate(bloodEffect, hitPos, rot);
        Destroy(blood, Random.Range(0.8f, 1.2f));
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
            rb.mass = 75f;
    }

    void SkeletonDie()
    {
        ani.SetTrigger(dieStr);
        col.enabled = false;

        rb.isKinematic = true;
        IsDie = true;

        GameManager.Instance.KillScore(1);
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

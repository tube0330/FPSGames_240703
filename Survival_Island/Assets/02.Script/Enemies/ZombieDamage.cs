using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieDamage : MonoBehaviour
{
    [Header("������Ʈ")]
    public Rigidbody rb;
    public CapsuleCollider capCol;
    public Animator animator;
    public GameObject bloodEffect;
    public BoxCollider boxCol;
    public MeshRenderer meshRend;

    [Header("���� ����")]
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
    FireCtrl fireCtrl;  //�÷��̾��� �߻���ġ�� ������ �ִ� Ŭ����
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        fireCtrl = GameObject.FindWithTag("Player").GetComponent<FireCtrl>();
        HPInit = maxHP;
        HPBar.color = Color.green;
    }

    private void OnCollisionEnter(Collision col)    //�浹 �����ϴ�
    {
        if (col.gameObject.CompareTag(playerTag))
        {
            rb.mass = 800f;    //�浹�� �� ���� ����
            rb.isKinematic = false; //�������� = false => ������ �ְ�
            rb.freezeRotation = true;
        }

        else if (col.gameObject.CompareTag(bulletTag))
        {
            HitInfo(col);

            HPInit -= col.gameObject.GetComponent<BulletCtrl>().damage;
            HPBar.fillAmount = (float)HPInit / (float)maxHP;
            HPText.text = ($"HP: <color=#ff0000>{HPInit.ToString()}</color>");

            if (HPBar.fillAmount <= 0.3f)
                HPBar.color = Color.red;

            else if (HPBar.fillAmount <= 0.5f)
                HPBar.color = Color.yellow;

            if (HPInit <= 0)
            {
                ZombieDie();
                
            }
        }
    }

    private void HitInfo(Collision col)
    {
        Destroy(col.gameObject);
        animator.SetTrigger(HitStr);
        Vector3 fireNormal = (col.transform.position - fireCtrl.firePos.position);
        //   �Ÿ�           =    ������ġ              -         �߻���ġ
        fireNormal = fireNormal.normalized; //���ʹ���
        //�߻� ���� ����

        Quaternion rot = Quaternion.LookRotation(fireNormal);
        //LookRotation �Լ�: ���Ͱ��� �޾� ȸ������ �ٲپ� �ִ� ���

        var blood = Instantiate(bloodEffect, col.transform.position, Quaternion.identity);

        Destroy(blood, Random.Range(0.8f, 1.2f));   //0.8~1.2�� ���� �ð����� �ǳ�
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag(playerTag))
            rb.mass = 75f;
    }

    void ZombieDie()
    {
        animator.SetTrigger(dieStr);
        capCol.enabled = false; //�׾��� �� Collider��Ȱ��ȭ�ؼ� ���������

        rb.isKinematic = true; //������ ����
        IsDie = true;

        Destroy(gameObject, 5.0f);
        GameManager.Instance.KillScore(1);
    }

    public void boxColEnable()
    {
        boxCol.enabled = true;  //enabled: Ȱ��ȭ
        meshRend.enabled = false;
    }

    public void boxColDisable()
    {
        boxCol.enabled = false;
        meshRend.enabled = false;
    }
}

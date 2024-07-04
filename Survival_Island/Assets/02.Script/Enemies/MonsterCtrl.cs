using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    [Header("Component")]   //��Ʈ����Ʈ
    public NavMeshAgent find;  //���� ��� ã�� �׺� ������Ʈ
    public float attackDist = 3.0f; //���� ����
    public float traceDist = 20.0f; //���� ����
    public Animator animator;
    public MonsterDamage damage;

    [Header("���� ����")]
    public Transform Player;
    public Transform Monster;
    void Start()
    {
        find = this.gameObject.GetComponent<NavMeshAgent>();   //�ڱ��ڽ� ���ӿ�����Ʈ �ȿ� �ִ� NavMeshAgent ������Ʈ�� find�� ����
        //find = GetComponent<NavMeshAgent>();
        //C#: find = new NavMeshAgent();

        Monster = this.gameObject.GetComponent<Transform>(); //���� ��ġ ������(transform�� play�Ǵ� ���� �˾Ƽ� ��)
        //Monster = this.gameObject.Transform;
        //Monster = GetComponent<Transform>();

        Player = GameObject.FindWithTag("Player").transform;    //���̶�Ű �ȿ� �ִ� ���ӿ�����Ʈ�� �±׸� �о� ������
        animator = GetComponent<Animator>();
        damage = GetComponent<MonsterDamage>();
    }

    void Update()
    {
        if (damage.IsDie || Player.GetComponent<FPSDamage>().isPlayerDie)
            return;

        //�Ÿ� ���
        float distance = Vector3.Distance(Monster.position, Player.position);    //�˾Ƽ� �Ÿ��� ���ؼ� �Ѱ���

        #region �Ǵ��� �ȵǴ��� Ȯ��
        /*if (distance <= attackDist)
        {
            Debug.Log("����");
        }

        else if (distance <= traceDist)
        {
            Debug.Log("����");
        }

        else Debug.Log("���� ���� ���");*/
        #endregion

        if (distance <= attackDist)
        {
            find.isStopped = true; //���� ����

            animator.SetBool("isAttack", true);
            
            Vector3 playerPos = Player.position - transform.position;   //�÷��̾� ��ġ - ���� ��ġ �ϸ� ������ ����
            playerPos = playerPos.normalized;   //����ȭ ���� ���
            Quaternion rot = Quaternion.LookRotation(playerPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 3.0f);
        }

        else if (distance <= traceDist)
        {
            animator.SetBool("isAttack", false);
            animator.SetBool("isTrace", true);

            find.isStopped = false;    //���� ����

            //���� �����        �÷��̾�(��ġ)
            find.destination = Player.position;
        }

        else
        {
            animator.SetBool("isTrace", false);
            find.isStopped = false;
        }
    }

    public void PlayerDeath()
    {
        GetComponent<Animator>().SetTrigger("PlayerDie");
    }
}

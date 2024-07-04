using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCtrl : MonoBehaviour
{
    [Header("Component")]   //��Ʈ����Ʈ
    public NavMeshAgent agent;  //���� ��� ã�� �׺� ������Ʈ
    public float attackDist = 3.0f; //���� ����
    public float traceDist = 20.0f; //���� ����
    public Animator animator;
    public ZombieDamage damage;

    [Header("���� ����")]
    public Transform Player;
    public Transform thisZombie;
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();   //�ڱ��ڽ� ���ӿ�����Ʈ �ȿ� �ִ� NavMeshAgent ������Ʈ�� agent�� ����
        //C#: agent = new NavMeshAgent();

        thisZombie = this.gameObject.GetComponent<Transform>(); //���� ��ġ ������(transform�� play�Ǵ� ���� �˾Ƽ� ��)
        //thisZombie = this.gameObject.Transform;

        Player = GameObject.FindWithTag("Player").transform;    //���̶�Ű �ȿ� �ִ� ���ӿ�����Ʈ�� �±׸� �о� ������

        animator = GetComponent<Animator>();    //this.gameobject�����ص� ����
        damage = GetComponent<ZombieDamage>();
    }

    void Update()
    {
        if (damage.IsDie || Player.GetComponent<FPSDamage>().isPlayerDie)
            return;

        //�Ÿ� ���
        float distance = Vector3.Distance(thisZombie.position, Player.position);    //�˾Ƽ� �Ÿ��� ���ؼ� �Ѱ���

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
            agent.isStopped = true; //���� ����
            
            animator.SetBool("IsAttack", true);

            Quaternion rot = Quaternion.LookRotation(Player.position - thisZombie.position);
            thisZombie.rotation = Quaternion.Slerp(thisZombie.rotation, rot, Time.deltaTime * 3.0f);
        }

        else if (distance <= traceDist)
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsTrace", true);

            agent.isStopped = false;    //���� ����

            //���� �����        �÷��̾�(��ġ)
            agent.destination = Player.position;
        }

        else
        {
            animator.SetBool("IsTrace", false);
            agent.isStopped = false;
        }
    }

    public void PlayerDeath()
    {
        GetComponent<Animator>().SetTrigger("PlayerDie");
    }
}

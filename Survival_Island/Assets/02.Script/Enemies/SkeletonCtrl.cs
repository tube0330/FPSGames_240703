using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonCtrl : MonoBehaviour
{
    [Header("Component")]   //��Ʈ����Ʈ
    public NavMeshAgent find;  //���� ��� ã�� �׺� ������Ʈ
    public float attackDist = 3.0f; //���� ����
    public float traceDist = 20.0f; //���� ����
    public Animator animator;
    public SkeletonDamage damage;
    public AudioSource audioSource;
    public AudioClip swordclip;

    [Header("���� ����")]
    public Transform Player;
    public Transform Skeleton;
    void Start()
    {
        find = GetComponent<NavMeshAgent>();
        Skeleton = GetComponent<Transform>();
        Player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        damage = GetComponent<SkeletonDamage>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (damage.IsDie || Player.GetComponent<FPSDamage>().isPlayerDie)
            return;

        float distance = Vector3.Distance(Skeleton.position, Player.position);

        if (distance <= attackDist)
        {
            find.isStopped = false;

            animator.SetBool("isAttack", true);

            Vector3 playerPos = (Player.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(playerPos);
        }

        else if (distance <= traceDist)
        {
            animator.SetBool("isAttack", false);
            animator.SetBool("isTrace", true);

            find.isStopped = false;
            find.destination = Player.position;
        }

        else
        {
            animator.SetBool("isTrace", false);
            find.isStopped = false;
        }
    }

    public void SwordSfx()
    {
        audioSource.clip = swordclip; //���� Ŭ���� �޾Ƽ� 
        audioSource.PlayDelayed(0.25f);
    }

    public void PlayerDeath()
    {
        GetComponent<Animator>().SetTrigger("PlayerDie");
    }
}

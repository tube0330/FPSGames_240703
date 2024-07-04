using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonCtrl : MonoBehaviour
{
    [Header("Component")]   //어트리뷰트
    public NavMeshAgent find;  //추적 대상 찾는 네비 컴포넌트
    public float attackDist = 3.0f; //공격 범위
    public float traceDist = 20.0f; //추적 범위
    public Animator animator;
    public SkeletonDamage damage;
    public AudioSource audioSource;
    public AudioClip swordclip;

    [Header("관련 변수")]
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
        audioSource.clip = swordclip; //사운드 클립을 받아서 
        audioSource.PlayDelayed(0.25f);
    }

    public void PlayerDeath()
    {
        GetComponent<Animator>().SetTrigger("PlayerDie");
    }
}

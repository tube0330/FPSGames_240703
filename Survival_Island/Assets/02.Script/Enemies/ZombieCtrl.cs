using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCtrl : MonoBehaviour
{
    [Header("Component")]   //어트리뷰트
    public NavMeshAgent agent;  //추적 대상 찾는 네비 컴포넌트
    public float attackDist = 3.0f; //공격 범위
    public float traceDist = 20.0f; //추적 범위
    public Animator animator;
    public ZombieDamage damage;

    [Header("관련 변수")]
    public Transform Player;
    public Transform thisZombie;
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();   //자기자신 게임오브젝트 안에 있는 NavMeshAgent 컴포넌트를 agent에 대입
        //C#: agent = new NavMeshAgent();

        thisZombie = this.gameObject.GetComponent<Transform>(); //좀비 위치 가져옴(transform에 play되는 순간 알아서 들어감)
        //thisZombie = this.gameObject.Transform;

        Player = GameObject.FindWithTag("Player").transform;    //하이라키 안에 있는 게임오브젝트의 태그를 읽어 가져옴

        animator = GetComponent<Animator>();    //this.gameobject생략해도 ㄱㅊ
        damage = GetComponent<ZombieDamage>();
    }

    void Update()
    {
        if (damage.IsDie || Player.GetComponent<FPSDamage>().isPlayerDie)
            return;

        //거리 재기
        float distance = Vector3.Distance(thisZombie.position, Player.position);    //알아서 거리를 구해서 넘겨줌

        #region 되는지 안되는지 확인
        /*if (distance <= attackDist)
        {
            Debug.Log("공격");
        }

        else if (distance <= traceDist)
        {
            Debug.Log("추적");
        }

        else Debug.Log("추적 범위 벗어남");*/
        #endregion

        if (distance <= attackDist)
        {
            agent.isStopped = true; //추적 중지
            
            animator.SetBool("IsAttack", true);

            Quaternion rot = Quaternion.LookRotation(Player.position - thisZombie.position);
            thisZombie.rotation = Quaternion.Slerp(thisZombie.rotation, rot, Time.deltaTime * 3.0f);
        }

        else if (distance <= traceDist)
        {
            animator.SetBool("IsAttack", false);
            animator.SetBool("IsTrace", true);

            agent.isStopped = false;    //추적 시작

            //추적 대상은        플레이어(위치)
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

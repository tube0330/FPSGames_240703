using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    [Header("Component")]   //어트리뷰트
    public NavMeshAgent find;  //추적 대상 찾는 네비 컴포넌트
    public float attackDist = 3.0f; //공격 범위
    public float traceDist = 20.0f; //추적 범위
    public Animator animator;
    public MonsterDamage damage;

    [Header("관련 변수")]
    public Transform Player;
    public Transform Monster;
    void Start()
    {
        find = this.gameObject.GetComponent<NavMeshAgent>();   //자기자신 게임오브젝트 안에 있는 NavMeshAgent 컴포넌트를 find에 대입
        //find = GetComponent<NavMeshAgent>();
        //C#: find = new NavMeshAgent();

        Monster = this.gameObject.GetComponent<Transform>(); //좀비 위치 가져옴(transform에 play되는 순간 알아서 들어감)
        //Monster = this.gameObject.Transform;
        //Monster = GetComponent<Transform>();

        Player = GameObject.FindWithTag("Player").transform;    //하이라키 안에 있는 게임오브젝트의 태그를 읽어 가져옴
        animator = GetComponent<Animator>();
        damage = GetComponent<MonsterDamage>();
    }

    void Update()
    {
        if (damage.IsDie || Player.GetComponent<FPSDamage>().isPlayerDie)
            return;

        //거리 재기
        float distance = Vector3.Distance(Monster.position, Player.position);    //알아서 거리를 구해서 넘겨줌

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
            find.isStopped = true; //추적 중지

            animator.SetBool("isAttack", true);
            
            Vector3 playerPos = Player.position - transform.position;   //플레이어 위치 - 좀비 위치 하면 방향이 나옴
            playerPos = playerPos.normalized;   //정규화 방향 등록
            Quaternion rot = Quaternion.LookRotation(playerPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 3.0f);
        }

        else if (distance <= traceDist)
        {
            animator.SetBool("isAttack", false);
            animator.SetBool("isTrace", true);

            find.isStopped = false;    //추적 시작

            //추적 대상은        플레이어(위치)
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

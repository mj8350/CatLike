using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState
{
    ToMove = 0,
    Attack1,
    Attack2,
    Attack3,
}

public class BossAi : MonoBehaviour
{
    private BossState State;
    private MonsterMove movement;
    private MonsterAttack attack;

    private int ran;

    [SerializeField]
    private float moveS;
    [SerializeField]
    private float time;

    private void Awake()
    {
        if (!TryGetComponent<MonsterMove>(out movement))
            Debug.Log("BossAi.cs - Awake() - movement 참조 실패");
        if (!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("BossAi.cs - Awake() - attack 참조 실패");
    }
    private void Start()
    {
        Creat();
    }

    public void Creat()
    {
        StartCoroutine("BossCreat");
    }
    public IEnumerator BossCreat()
    {
        
        yield return YieldInstructionCache.WaitForSeconds(1f);
        ChangeState(BossState.ToMove);
    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(State.ToString());
        State = newState;
        StartCoroutine(State.ToString());
    }
    private IEnumerator ToMove()
    {
        movement.moveSpeed = moveS;
        yield return YieldInstructionCache.WaitForSeconds(time);
        movement.moveSpeed = 0f;
        Rand();
    }

    [SerializeField]
    private AttackType type1;//어떤 행동
    [SerializeField]
    private float time1;//몇 초 이후 다음 행동
    [SerializeField]
    private AttackType type2;
    [SerializeField]
    private float time2;
    [SerializeField]
    private AttackType type3;
    [SerializeField]
    private float time3;


    private IEnumerator Attack1()
    {
        attack.AttackActive(type1);
        yield return YieldInstructionCache.WaitForSeconds(time1);
        ChangeState(BossState.ToMove);
    }

    private IEnumerator Attack2()
    {
        attack.AttackActive(type2);
        yield return YieldInstructionCache.WaitForSeconds(time2);
        ChangeState(BossState.ToMove);
    }
    private IEnumerator Attack3()
    {
        attack.AttackActive(type3);
        yield return YieldInstructionCache.WaitForSeconds(time3);
        ChangeState(BossState.ToMove);
    }

    private void Rand()
    {
        ran = Random.Range(1, 4);
        switch (ran)
        {
            case 1:
                ChangeState(BossState.Attack1);
                break;
            case 2:
                ChangeState(BossState.Attack2);
                break;
            case 3:
                ChangeState(BossState.Attack3);
                break;
        }
    }
}

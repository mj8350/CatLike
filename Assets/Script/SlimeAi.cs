using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SlimeState
{
    ToMove = 0,
    Attack1,
    Attack2,
}

public class SlimeAi : MonoBehaviour
{
    private SlimeState State;
    private MonsterMove movement;
    private MonsterAttack attack;

    private int ran;

    private void Awake()
    {
        if (!TryGetComponent<MonsterMove>(out movement))
            Debug.Log("SlimeAi.cs - Awake() - movement 참조 실패");
        if(!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("SlimeAi.cs - Awake() - attack 참조 실패");

        SlimeCreat(new Vector3(3f, 3f, 0f));
    }

    public void SlimeCreat(Vector3 vec)
    {
        transform.position = vec;
        ChangeState(SlimeState.ToMove);
    }

    public void ChangeState(SlimeState newState)
    {
        StopCoroutine(State.ToString());
        State = newState;
        StartCoroutine(State.ToString());
    }
    private IEnumerator ToMove()
    {
        movement.moveSpeed = 2f;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        movement.moveSpeed = 0f;
        //ChangeState(SlimeState.Attack2);
        Rand();
    }

    private IEnumerator Attack1()
    {
        attack.AttackActive(AttackType.CircleFire01);
        yield return YieldInstructionCache.WaitForSeconds(3f);
        ChangeState(SlimeState.ToMove);
    }

    private IEnumerator Attack2()
    {
        attack.AttackActive(AttackType.ThreeFire01);
        yield return YieldInstructionCache.WaitForSeconds(3f);
        ChangeState(SlimeState.ToMove);
    }

    private void Rand()
    {
        ran = Random.Range(1, 3);
        switch (ran)
        {
            case 1:
                ChangeState(SlimeState.Attack1);
                break;
            case 2:
                ChangeState(SlimeState.Attack2);
                break;
        }
    }

}

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

    private void Awake()
    {
        if (!TryGetComponent<MonsterMove>(out movement))
            Debug.Log("SlimeAi.cs - Awake() - movement 참조 실패");
        if(!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("SlimeAi.cs - Awake() - attack 참조 실패");

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
        movement.moveSpeed = 3f;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        movement.moveSpeed = 0f;
    }

}

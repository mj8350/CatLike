using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OneState
{
    ToMove = 0,
    Attack
}

public class OneAi : MonoBehaviour
{
    private OneState State;
    private MonsterMove movement;
    private MonsterAttack attack;

    [SerializeField]
    private float moveS;
    [SerializeField]
    private float time;

    private void Awake()
    {
        if (!TryGetComponent<MonsterMove>(out movement))
            Debug.Log("OneAi.cs - Awake() - movement ���� ����");
        if (!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("OneAi.cs - Awake() - attack ���� ����");

        StartCoroutine(OneCreat(new Vector3(3f, 0f, 0f)));
    }

    

    public IEnumerator OneCreat(Vector3 vec)
    {
        transform.position = vec;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        ChangeState(OneState.ToMove);
    }

    public void ChangeState(OneState newState)
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
        ChangeState(OneState.Attack);
    }

    [SerializeField]
    private AttackType type1;//� �ൿ
    [SerializeField]
    private float time1;//�� �� ���� ���� �ൿ

    private IEnumerator Attack()
    {
        attack.AttackActive(type1);
        yield return YieldInstructionCache.WaitForSeconds(time1);
        ChangeState(OneState.ToMove);
    }

}

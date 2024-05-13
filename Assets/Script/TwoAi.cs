using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TwoState
{
    ToMove = 0,
    Attack,
}

public class TwoAi : MonoBehaviour,AI
{
    private TwoState State;
    private MonsterMove movement;
    private MonsterAttack attack;

    public int ran = 0;

    [SerializeField]
    private float moveS;
    [SerializeField]
    private float timeS;

    private void Awake()
    {
        if (!TryGetComponent<MonsterMove>(out movement))
            Debug.Log("TwoAi.cs - Awake() - movement 참조 실패");
        if (!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("TwoAi.cs - Awake() - attack 참조 실패");
    }

	public void Creat()
	{
        StartCoroutine("TwoCreat");
    }
	public IEnumerator TwoCreat()
    {
        //transform.position = vec;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        ChangeState(TwoState.ToMove);
    }

    public void ChangeState(TwoState newState)
    {
        StopCoroutine(State.ToString());
        State = newState;
        StartCoroutine(State.ToString());
    }
    private IEnumerator ToMove()
    {
        movement.moveSpeed = moveS;
        yield return YieldInstructionCache.WaitForSeconds(timeS);
        movement.moveSpeed = 0f;
        ChangeState(TwoState.Attack);
        //Rand();
    }
    
    
    [SerializeField]
    private AttackType[] type;
    [SerializeField]
    private float[] time;

    private IEnumerator Attack()
    {
        ran = Random.Range(0, type.Length);
        attack.AttackActive(type[ran]);
        yield return YieldInstructionCache.WaitForSeconds(time[ran]);
        ChangeState(TwoState.ToMove);
    }

}

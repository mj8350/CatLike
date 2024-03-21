using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TwoState
{
    ToMove = 0,
    Attack1,
    Attack2,
}

public class TwoAi : MonoBehaviour
{
    private TwoState State;
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
            Debug.Log("TwoAi.cs - Awake() - movement 참조 실패");
        if (!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("TwoAi.cs - Awake() - attack 참조 실패");
    }
	private void Start()
	{
        //StartCoroutine(TwoCreat(new Vector3(-3f, 0f, 0f)));
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
        yield return YieldInstructionCache.WaitForSeconds(time);
        movement.moveSpeed = 0f;
        //ChangeState(SlimeState.Attack2);
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

    private IEnumerator Attack1()
    {
        attack.AttackActive(type1);
        yield return YieldInstructionCache.WaitForSeconds(time1);
        ChangeState(TwoState.ToMove);
    }

    private IEnumerator Attack2()
    {
        attack.AttackActive(type2);
        yield return YieldInstructionCache.WaitForSeconds(time2);
        ChangeState(TwoState.ToMove);
    }

    private void Rand()
    {
        ran = Random.Range(1, 3);
        switch (ran)
        {
            case 1:
                ChangeState(TwoState.Attack1);
                break;
            case 2:
                ChangeState(TwoState.Attack2);
                break;
        }
    }
}

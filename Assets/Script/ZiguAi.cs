using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ZiguState
{
    Stand = 0,
    Attack,
    Attack1,
    Attack2,
}
public class ZiguAi : MonoBehaviour,AI
{
    private ZiguState State;
    private MonsterMove movement;
    private MonsterAttack attack;
    private MonsterChar mstChar;

    private int ran = 0;
    private int count =0;

    [SerializeField]
    private float timeS;

    private void Awake()
    {
        if (!TryGetComponent<MonsterMove>(out movement))
            Debug.Log("ZiguAi.cs - Awake() - movement 참조 실패");
        if (!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("ZiguAi.cs - Awake() - attack 참조 실패");
        if (!TryGetComponent<MonsterChar>(out mstChar))
            Debug.Log("ZiguAi.cs - Awake() - mstChar 참조 실패");
    }
    

    public void Creat()
    {
        StartCoroutine("ZiguCreat");
    }
    public IEnumerator ZiguCreat()
    {
        yield return YieldInstructionCache.WaitForSeconds(1f);
        ChangeState(ZiguState.Stand);
    }

    public void ChangeState(ZiguState newState)
    {
        StopCoroutine(State.ToString());
        State = newState;
        StartCoroutine(State.ToString());
    }
    private IEnumerator Stand()
    {
        movement.moveSpeed = 3f;
        yield return YieldInstructionCache.WaitForSeconds(timeS);
        movement.moveSpeed = 0f;
        if (mstChar.phase == 0)
            mstChar.ChangePhase(70, 1);
        else if (mstChar.phase == 1)
            mstChar.ChangePhase(30, 2);
        switch (mstChar.phase)
        {
            case 0:
                ChangeState(ZiguState.Attack);
                break;
            case 1:
                ChangeState(ZiguState.Attack1);
                break;
            case 2:
                ChangeState(ZiguState.Attack2);
                break;
        }


        
    }

    [SerializeField]
    private AttackType[] type;
    [SerializeField]
    private float[] time;

    [SerializeField]
    private AttackType[] type1;
    [SerializeField]
    private float[] time1;

    [SerializeField]
    private AttackType type2;



    private IEnumerator Attack()
    {
        attack.AttackActive(type[count]);
        yield return YieldInstructionCache.WaitForSeconds(time[count]);
        ChangeState(ZiguState.Stand);
        if (count < 3)
            count++;
        else
            count = 0;
    }

    private IEnumerator Attack1()
    {
        attack.AttackActive(type1[count]);
        yield return YieldInstructionCache.WaitForSeconds(time1[count]);
        ChangeState(ZiguState.Stand);
        if (count < 3)
            count++;
        else
            count = 0;
    }

    private IEnumerator Attack2()
    {
        attack.AttackActive(type2);
        yield return null;
    }
}

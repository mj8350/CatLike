using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossState
{
    Stand = 0,
    Attack,
    Attack1,
}

public class BossAi : MonoBehaviour
{
    private BossState State;
    private MonsterAttack attack;
    private MonsterChar mstChar;

    private int ran = 0;

    [SerializeField]
    private float timeS;

    private void Awake()
    {
        if (!TryGetComponent<MonsterAttack>(out attack))
            Debug.Log("BossAi.cs - Awake() - attack 참조 실패");
        if (!TryGetComponent<MonsterChar>(out mstChar))
            Debug.Log("BossAi.cs - Awake() - mstChar 참조 실패");
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
        ChangeState(BossState.Stand);
    }

    public void ChangeState(BossState newState)
    {
        StopCoroutine(State.ToString());
        State = newState;
        StartCoroutine(State.ToString());
    }
    private IEnumerator Stand()
    {
        yield return YieldInstructionCache.WaitForSeconds(timeS);
        mstChar.ChangePhase(30,1);
        switch (mstChar.phase)
        {
            case 0:
                ChangeState(BossState.Attack);
                break;
            case 1:
                ChangeState(BossState.Attack1);
                break;
        }
            

        //Rand();
    }

    [SerializeField]
    private AttackType[] type;
    [SerializeField]
    private float[] time;

    [SerializeField]
    private AttackType[] type1;
    [SerializeField]
    private float[] time1;



    private IEnumerator Attack()
    {
        ran = Random.Range(0, type.Length);
        attack.AttackActive(type[ran]);
        yield return YieldInstructionCache.WaitForSeconds(time[ran]);
        ChangeState(BossState.Stand);
    }

    private IEnumerator Attack1()
    {
        ran = Random.Range(0, type.Length);
        attack.AttackActive(type1[ran]);
        yield return YieldInstructionCache.WaitForSeconds(time1[ran]);
        ChangeState(BossState.Stand);
    }

}

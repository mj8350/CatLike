using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    CircleFire01, // 전방향 10발사
    ThreeFire01, //플레이어방향 3발
    Fire323, //플레이어 방향으로 323돌진
    Baby01, //애기초코 소환
    FollowFire01, //플레이어 추적 발사
    SpwanThorn01, //가시 소환하는 warning생성
    Front323, //플레이어 방향으로 3 2 3 발사
    FrontWing01, //플레이어 방향으로 돌진하면서 날개발사
    Sniping01, //저격
    Fire5, //플레이어 방향 5개 빠르게 날리기
    Bogle01, //플레이어 방향 16개 보글보글
    CircleFire02, //전방향 큰거 6개 느리게발사
    FrontWing02, //상어 돌진
    SlimeBubble, //슬라임 큰 버블
}

public class MonsterAttack : MonoBehaviour
{
    private Animator anim;

    private float attackRate; //공격 주기.
    private float attackCount; // 투사체 갯수.
    private float intervalAngle; // 투사체발사 방향과 옆 투사체 발사 방향 사이의 각도 
    private float weightAngle;      // 가중치 각도. 
    private AttackType currentAttack;

    private GameObject shoot;//발사 위치

    private MonsterMove movement;
    private Transform player;
    private GameObject obj;
    private float angle;
    private Vector2 dir;
    private Vector3 Mdir;

    //private Vector3 center = new Vector3(0f, 0.5f, 0f);

    private Vector3 dftScale = new Vector3(0.6f, 0.6f, 0.6f);
    private Vector3 midScale = new Vector3(0.8f, 0.8f, 0.8f);
    private Vector3 oriScale = new Vector3(1f, 1f, 1f);
    private Vector3 bigScale = new Vector3(1.5f, 1.5f, 1.5f);

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log("MonsterAttack.cs - Awake() - anim 참조 실패");
        if (!TryGetComponent<MonsterMove>(out movement))
            Debug.Log("MonsterAttack.cs - Awake() - movement 참조 실패");
        shoot = transform.Find("ShootPos").gameObject;

        player = GameObject.Find("Player").transform;
    }

    public void AttackActive(AttackType newAttack)
    {
        StopCoroutine(currentAttack.ToString());
        currentAttack = newAttack;
        StartCoroutine(currentAttack.ToString());
    }

    public void SetFire()
    {
        switch (currentAttack)
        {
            case AttackType.CircleFire01:
                Circle01();
                break;
            case AttackType.ThreeFire01:
                Three01();
                break;
            case AttackType.Fire323:
                fire323();
                break;
            case AttackType.Baby01:
                baby01();
                break;
            case AttackType.FollowFire01:
                followFire01();
                break;
            case AttackType.SpwanThorn01:
                spwanThorn();
                break;
            case AttackType.Front323:
                StartCoroutine("front323");
                break;
            case AttackType.FrontWing01:
                StartCoroutine("frontWing01");
                break;
            case AttackType.Sniping01:
                sniping();
                break;
            case AttackType.Fire5:
                StartCoroutine("fire5");
                break;
            case AttackType.Bogle01:
                StartCoroutine("bogle01");
                break;
            case AttackType.CircleFire02:
                Circle02();
                break;
            case AttackType.FrontWing02:
                StartCoroutine("frontWing01");
                StartCoroutine("spwanIce");
                break;
            case AttackType.SlimeBubble:
                slimeBubble();
                break;

        }
    }

    #region Slime
    private IEnumerator CircleFire01()
    {
        attackCount = 10;
        intervalAngle = 360f / attackCount;
        anim.SetTrigger("Attack");
        yield return null;
    }
    private void Circle01()
    {
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position; //+ center;
            angle = intervalAngle * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 5f, dftScale);
            }
        }
    }

    private IEnumerator ThreeFire01()
    {
        attackCount = 3;
        intervalAngle = 45f;

        anim.SetTrigger("Attack");
        yield return null;
    }
    private void Three01()
    {
        dir = (player.position - shoot.transform.position).normalized;
        dir = Quaternion.AngleAxis(-intervalAngle * 2f, Vector3.forward) * dir;
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position; //+ center;

            dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 5f, midScale);
            }
        }
    }
    #endregion

    #region Snake
    private IEnumerator Fire323()
    {
        attackRate = 1f;
        attackCount = 3;
        intervalAngle = 30f;
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        Mdir = (player.position - shoot.transform.position).normalized;
        movement.MoveRushOn(2f, Mdir);
        for (int i = 0; i < 3; i++)
        {
            anim.SetTrigger("Attack");
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
            if (attackCount == 3)
                attackCount--;
            else
                attackCount++;
        }
        movement.MoveRushOff();

    }
    private void fire323()
    {
        if (attackCount == 3)
            dir = Quaternion.AngleAxis(-intervalAngle * 2f, Vector3.forward) * Mdir;
        else
            dir = Quaternion.AngleAxis(-(intervalAngle / 2f * 3f), Vector3.forward) * Mdir;

        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position; //+ center;

            dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 5f, midScale);
            }
        }
    }

    #endregion

    #region Choco
    private IEnumerator Baby01()
    {
        anim.SetTrigger("Attack");
        yield return null;
    }

    private void baby01()
    {
        obj = PoolManager.Inst.pools[(int)PoolState.miniChoco].Pop();
        obj.transform.position = transform.position;
        if (obj.TryGetComponent<MiniChoco>(out MiniChoco mini))
            mini.miniOn();
    }

    private IEnumerator FollowFire01()
    {
        anim.SetTrigger("Attack");
        yield return null;
    }

    private void followFire01()
    {
        obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
        obj.transform.position = shoot.transform.position;
        if (obj.TryGetComponent<Projectile>(out Projectile projectile))
        {
            projectile.MoveTo2(4f, dftScale);
        }
    }



    #endregion

    #region Grape
    private IEnumerator SpwanThorn01()
    {
        for (int i = 0; i < 3; i++)
        {
            anim.SetTrigger("Attack");
            yield return YieldInstructionCache.WaitForSeconds(1f);
        }
    }

    private void spwanThorn()
    {
        obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
        obj.transform.position = player.position;
        if(obj.TryGetComponent<Warning>(out Warning warning)){
            warning.SetType(PoolState.thorn);
        }
    }


    #endregion

    #region Bear_SP
    private IEnumerator Front323()
    {
        attackRate = 0.5f;
        attackCount = 3;
        intervalAngle = 30f;
        anim.SetTrigger("Attack");
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        Mdir = (player.position - shoot.transform.position).normalized;

    }
    private IEnumerator front323()
    {
        for (int j = 0; j < 3; j++)
        {
            if (attackCount == 3)
                dir = Quaternion.AngleAxis(-intervalAngle * 2f, Vector3.forward) * Mdir;
            else
                dir = Quaternion.AngleAxis(-(intervalAngle / 2f * 3f), Vector3.forward) * Mdir;

            for (int i = 0; i < attackCount; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position; //+ center;

                dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, midScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
            if (attackCount == 3)
                attackCount--;
            else
                attackCount++;

        }
    }

    private IEnumerator FrontWing01()
    {
        attackRate = 0.1f;
        attackCount = 2;
        intervalAngle = 180f;
        Mdir = (player.position - shoot.transform.position).normalized;
        anim.SetTrigger("Attack");
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        movement.MoveRushOn(3f, Mdir);
        dir = Quaternion.AngleAxis(-90, Vector3.forward) * Mdir;
        yield return YieldInstructionCache.WaitForSeconds(2);
        
        movement.MoveRushOff();
    }
    private IEnumerator frontWing01()
    {
        for (int j = 0; j < 16; j++)
        {
            yield return YieldInstructionCache.WaitForSeconds(attackRate);

            for (int i = 0; i < attackCount; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position; //+ center;

                dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }
            }
        }
    }
    #endregion

    #region Bear_CB

    private IEnumerator Sniping01()
    {
        anim.SetTrigger("Attack");
        yield return null;
    }
    private void sniping()
    {
        obj = PoolManager.Inst.pools[(int)PoolState.alertLine].Pop();
        obj.transform.position = shoot.transform.position;
    }

    private IEnumerator Fire5()
    {
        attackCount = 5;
        attackRate = 0.1f;
        anim.SetTrigger("Attack");
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        Mdir = (player.position - shoot.transform.position).normalized;
    }
    private IEnumerator fire5()
    {
        for(int i = 0; i < attackCount; i++)
        {
            yield return YieldInstructionCache.WaitForSeconds(attackRate);

            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position; //+ center;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(Mdir, 20f, dftScale);
            }
            
        }
    }


    #endregion

    #region Pudding
    private IEnumerator Bogle01()
	{
        attackCount = 16;
        intervalAngle = 20f;

        anim.SetTrigger("Attack");
        //yield return null;
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        Mdir = (player.position - shoot.transform.position).normalized;

    }
    private IEnumerator bogle01()
    {
        for (int i = 0; i < attackCount; i++)
        {
            float ran = Random.Range(-intervalAngle, intervalAngle);
            dir = Quaternion.AngleAxis(ran, Vector3.forward) * Mdir;
            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position;

            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 5f, dftScale);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.03f);
        }
    }

    private IEnumerator CircleFire02()
    {
        attackCount = 6;
        intervalAngle = 360f / attackCount;
        anim.SetTrigger("Attack");
        yield return null;
    }
    private void Circle02()
    {
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position;
            angle = intervalAngle * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 2f, bigScale);
            }
        }
    }
	#endregion

	#region Shark
    private IEnumerator FrontWing02()
	{
        attackRate = 0.1f;
        attackCount = 2;
        intervalAngle = 180f;
        Mdir = (player.position - shoot.transform.position).normalized;
        anim.SetTrigger("Attack");
        yield return YieldInstructionCache.WaitForSeconds(0.5f);
        movement.MoveRushOn(5f, Mdir);
        dir = Quaternion.AngleAxis(-90, Vector3.forward) * Mdir;
        yield return YieldInstructionCache.WaitForSeconds(2);

        movement.MoveRushOff();
    }
    private IEnumerator frontWing02()
    {
        for (int j = 0; j < 16; j++)
        {
            yield return YieldInstructionCache.WaitForSeconds(attackRate);

            for (int i = 0; i < attackCount; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position; //+ center;

                dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }
            }
        }
    }

    private IEnumerator spwanIce()
	{
        for (int i = 0; i < 4; i++)
		{
            yield return YieldInstructionCache.WaitForSeconds(0.3f);
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            obj.transform.position = transform.position;
            if (obj.TryGetComponent<Warning>(out Warning warning))
            {
                warning.SetType(PoolState.ice);
                //warning.iceBorn = true;
            }
        }
	}

	#endregion

	#region Boss_Slime
    private IEnumerator SlimeBubble()
	{
        attackCount = 8;
        intervalAngle = 360f / attackCount;
        anim.SetTrigger("Attack");
        yield return null;
    }
    private void slimeBubble()
    {
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.bubble].Pop();
            obj.transform.position = shoot.transform.position; //+ center;
            angle = intervalAngle * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 2.5f, oriScale);
                projectile.BubblePop();
            }
        }
    }



    #endregion
}

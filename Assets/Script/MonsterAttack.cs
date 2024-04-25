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
    GrapeCircleFire, //포도 전방향20개 3번
    GrapeCircleThorn, //원형 나선 가시
    GrapeThorn, // 플레이어 추적 가시
    FrontWing03, //상어 보스 돌진
    Sniping02, //곰 보스 시간차 저격
    ThreeFire02, //젤리 플레이어방향 3개씩 30개발사
    JellyBubble, //젤리 120도 버블 발사
    JellyCircleFire, //전방향 8발사
    JellyPhase2Bubble, //젤리 2페이즈 양옆에서 버블
    Zigu01,
    Zigu02,
    Zigu03,
    Zigu04,
    ZiguCircle01,
    ZiguCircle02,
    ZiguCircle03,
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
    private Vector3 dir = Vector3.zero;
    private Vector3 Mdir;

    //private int count;
    //private bool countbool;

    private bool grapebool;
    private bool jellybool;
    private bool zigubool1;
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
        grapebool = true;
        jellybool = true;
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
            case AttackType.GrapeCircleFire:
                StartCoroutine("grapeCircleFire");
                break;
            case AttackType.GrapeCircleThorn:
                StartCoroutine("grapeCircleThorn");
                break;
            case AttackType.GrapeThorn:
                switch (Thorntype)
                {
                    case 1:
                        grapeThorn1();
                        break;
                    case 2:
                        grapeThorn2();
                        break;
                    case 3:
                        grapeThorn1();
                        grapeThorn3();
                        break;
                }
                break;
            case AttackType.FrontWing03:
                StartCoroutine("frontWing03");
                StartCoroutine("spwanIce3");
                break;
            case AttackType.Sniping02:
                StartCoroutine("sniping3");
                break;
            case AttackType.ThreeFire02:
                StartCoroutine("Three02");
                break;
            case AttackType.JellyBubble:
                if (jellybool)
                    StartCoroutine("jellyBubble01");
                else
                    StartCoroutine("jellyBubble02");
                StartCoroutine("jellyFire");
                break;
            case AttackType.JellyCircleFire:
                StartCoroutine("jellyCircle");
                StartCoroutine("jellyThorn");
                break;
            case AttackType.JellyPhase2Bubble:
                StartCoroutine("jellyBubble01");
                StartCoroutine("jellyBubble02");
                StartCoroutine("jellyFire");
                StartCoroutine("jellyThorn");
                break;
            case AttackType.Zigu01:
                StartCoroutine("zigu01");
                break;
            case AttackType.Zigu02:
                StartCoroutine("zigu02");
                break;
            case AttackType.Zigu03:
                StartCoroutine("zigu03");
                StartCoroutine("zigu3");
                break;
            case AttackType.Zigu04:
                StartCoroutine("zigu04");
                break;
            case AttackType.ZiguCircle01:
                StartCoroutine("ziguCircle01");
                break;
            case AttackType.ZiguCircle02:
                StartCoroutine("ziguCircle02");
                StartCoroutine("ziguCircle2");
                break;
            case AttackType.ZiguCircle03:
                StartCoroutine("ziguCircle03");
                StartCoroutine("ziguCircle3");
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
            obj.transform.position = shoot.transform.position;
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
        if (obj.TryGetComponent<Warning>(out Warning warning))
        {
            warning.SetType(PoolState.thorn, 3f);
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
        for (int i = 0; i < attackCount; i++)
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
                warning.SetType(PoolState.ice, 0f);
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

    #region Boss_Greap
    private IEnumerator GrapeCircleFire()
    {
        attackCount = 20;
        intervalAngle = 360f / attackCount;
        weightAngle = intervalAngle / 2;
        anim.SetTrigger("Attack");
        yield return null;
    }
    private IEnumerator grapeCircleFire()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < attackCount; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position; //+ center;
                angle = (intervalAngle * i) + (weightAngle * j);
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }



            }
            yield return YieldInstructionCache.WaitForSeconds(0.3f);
        }
    }
    private bool oneThorn;
    private IEnumerator GrapeCircleThorn()
    {
        oneThorn = true;
        attackCount = 6;
        intervalAngle = 360f / attackCount;
        if (grapebool)
        {
            weightAngle = 5f;
            grapebool = false;
        }
        else
        {
            weightAngle = -5f;
            grapebool = true;
        }

        anim.SetTrigger("Attack");
        yield return null;

    }

    private IEnumerator grapeCircleThorn()
    {
        for (int j = 1; j < 9; j++)
        {
            for (int i = 0; i < attackCount; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
                angle = (intervalAngle * i) + (weightAngle * j);
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                obj.transform.position = shoot.transform.position + (dir.normalized * j);
                StartCoroutine(ThornFire(obj.transform.position, dir));
                if (obj.TryGetComponent<Warning>(out Warning warning))
                {
                    if (oneThorn)
                        warning.SetType(PoolState.thorn, 2f);
                    else
                        warning.SetType(PoolState.thorn, 10f);
                }
            }
            oneThorn = false;
            yield return YieldInstructionCache.WaitForSeconds(0.2f);
        }
    }
    private GameObject obj1;
    private IEnumerator ThornFire(Vector3 pos, Vector3 vec)
    {
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        for (int n = 1; n < 3; n++)
        {
            obj1 = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj1.transform.position = pos;
            vec = Quaternion.AngleAxis(70, Vector3.forward) * vec;
            if (obj1.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(vec, 5f, dftScale);
            }
        }
    }

    private int Thorntype;
    private IEnumerator GrapeThorn()
    {
        attackCount = 20;
        intervalAngle = 360f / attackCount;
        Thorntype = 1;
        for (int i = 0; i < 3; i++)
        {
            anim.SetTrigger("Attack");
            yield return YieldInstructionCache.WaitForSeconds(3f);
        }
    }
    private void grapeThorn1()
    {
        for (int i = 0; i < attackCount + 1; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            angle = intervalAngle * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            if (i > 0)
                obj.transform.position = player.transform.position + (dir.normalized * 2.5f);
            else
                obj.transform.position = player.position;
            if (obj.TryGetComponent<Warning>(out Warning warning))
            {
                warning.SetType(PoolState.thorn, 2f);
            }
        }
        Thorntype = 2;
    }
    private void grapeThorn2()
    {
        for (int i = 0; i < 10 + 1; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            angle = intervalAngle * 2 * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            obj.transform.position = player.transform.position + (dir.normalized * 1.5f);
            if (obj.TryGetComponent<Warning>(out Warning warning))
            {
                warning.SetType(PoolState.thorn, 2f);
            }
        }
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            angle = intervalAngle * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            obj.transform.position = player.transform.position + (dir.normalized * 4f);
            if (obj.TryGetComponent<Warning>(out Warning warning))
            {
                warning.SetType(PoolState.thorn, 2f);
            }
        }
        Thorntype = 3;
    }
    private void grapeThorn3()
    {
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            angle = intervalAngle * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            obj.transform.position = player.transform.position + (dir.normalized * 5f);
            if (obj.TryGetComponent<Warning>(out Warning warning))
            {
                warning.SetType(PoolState.thorn, 2f);
            }
        }
        Thorntype = 1;
    }


    #endregion

    #region Boss_Shark
    private IEnumerator FrontWing03()
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
    private IEnumerator frontWing03()
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
    private IEnumerator spwanIce3()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.3f);
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            obj.transform.position = transform.position;
            if (obj.TryGetComponent<Warning>(out Warning warning))
            {
                warning.SetType(PoolState.ice, 0f);
                warning.iceBorn = true;
            }
        }
    }

    #endregion

    #region Boss_Bear
    private IEnumerator Sniping02()
    {
        anim.SetTrigger("Attack");
        yield return null;
    }

    private IEnumerator sniping3()
    {
        for (int i = 0; i < 3; i++)
        {
            sniping();
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
    }
    #endregion

    #region Jelly
    private IEnumerator ThreeFire02()
    {
        attackCount = 3;
        intervalAngle = 20f;
        weightAngle = 1f;

        Mdir = (player.position - shoot.transform.position).normalized;

        anim.SetTrigger("Attack");
        yield return null;
    }
    private IEnumerator Three02()
    {
        int count = 0;
        bool countbool = true;
        for (int j = 0; j < 30; j++)
        {
            if (countbool)
                Mdir = Quaternion.AngleAxis(-weightAngle * (j / 10), Vector3.forward) * Mdir;
            else
                Mdir = Quaternion.AngleAxis(weightAngle * (j / 10), Vector3.forward) * Mdir;

            dir = Quaternion.AngleAxis(-intervalAngle * 2f, Vector3.forward) * Mdir;
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
            count++;
            if (count % 5 == 0)
            {
                if (countbool)
                    countbool = false;
                else
                    countbool = true;
            }
            yield return YieldInstructionCache.WaitForSeconds(0.2f);
        }
    }
    private IEnumerator JellyBubble()
    {
        attackCount = 20;
        anim.SetTrigger("Attack");
        yield return null;
        if (jellybool)
            jellybool = false;
        else
            jellybool = true;
    }
    private IEnumerator jellyBubble01()
    {
        Vector3 dir = Vector3.down;
        dir = Quaternion.AngleAxis(+60, Vector3.forward) * dir;
        int intervalAngle = -6;
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.bubble].Pop();
            obj.transform.position = shoot.transform.position;
            dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 2.5f, dftScale);
                //projectile.BubblePop();
            }
            yield return YieldInstructionCache.WaitForSeconds(0.2f);
        }
    }
    private IEnumerator jellyBubble02()
    {
        Vector3 dir = Vector3.down;
        dir = Quaternion.AngleAxis(-60, Vector3.forward) * dir;
        int intervalAngle = 6;
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.bubble].Pop();
            obj.transform.position = shoot.transform.position;
            dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 2.5f, dftScale);
                //projectile.BubblePop();
            }
            yield return YieldInstructionCache.WaitForSeconds(0.2f);
        }

    }

    private IEnumerator jellyFire()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);

        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position;
            //dir = player.position;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo2(5f, dftScale);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.2f);
        }
    }
    private IEnumerator JellyCircleFire()
    {
        attackCount = 8;
        intervalAngle = 360f / attackCount;

        if (jellybool)
        {
            weightAngle = 3;
            jellybool = false;
        }
        else
        {
            weightAngle = -3;
            jellybool = true;
        }

        anim.SetTrigger("Attack");
        yield return null;
    }
    private IEnumerator jellyCircle()
    {
        for (int j = 0; j < 30; j++)
        {
            for (int i = 0; i < attackCount; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                angle = (intervalAngle * i) + (weightAngle * j);
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, midScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }
    }

    private IEnumerator jellyThorn()
    {
        yield return YieldInstructionCache.WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            obj.transform.position = player.position;
            if (obj.TryGetComponent<Warning>(out Warning warning))
            {
                warning.SetType(PoolState.thorn, 2f);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
    }

    private IEnumerator JellyPhase2Bubble()
    {
        attackCount = 20;
        anim.SetTrigger("Attack");
        yield return null;
    }


    #endregion

    #region Zigu
    private IEnumerator Zigu01()
    {
        attackRate = 0.2f;
        attackCount = 8;
        intervalAngle = 30f;
        zigubool1 = true;
        anim.SetTrigger("Attack");
        yield return null;

    }
    private IEnumerator zigu01()
    {
        for (int i = 0; i < attackCount; i++)
        {
            Mdir = (player.position - shoot.transform.position).normalized;
            if (zigubool1)
            {
                Mdir = Quaternion.AngleAxis(-intervalAngle, Vector3.forward) * Mdir;
                zigubool1 = false;
            }
            else
            {
                //Mdir = Quaternion.AngleAxis(intervalAngle, Vector3.forward) * Mdir;
                zigubool1 = true;
            }
            for (int j = 0; j < 2; j++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                dir = Quaternion.AngleAxis(intervalAngle * j, Vector3.forward) * Mdir;
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, midScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
    }

    private IEnumerator Zigu02()
    {
        transform.position = new Vector3(0, 100f, 0);
        anim.SetTrigger("Attack");
        yield return null;
    }

    private IEnumerator zigu02()
    {
        obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
        obj.transform.position = player.position;
        Vector3 pos = obj.transform.position;
        if (obj.TryGetComponent<Warning>(out Warning warning))
        {
            warning.ziguScale();
            warning.zigu = true;
        }
        yield return YieldInstructionCache.WaitForSeconds(1f);
        transform.position = pos;
    }

    private IEnumerator ZiguCircle01()
    {
        attackCount = 2;
        attackRate = 0.1f;
        intervalAngle = 180f;
        weightAngle = 16f;
        transform.position = Vector3.zero;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        anim.SetTrigger("Attack");
    }

    private IEnumerator ziguCircle01()
    {
        for (int j = 0; j < 60; j++)
        {
            for (int i = 0; i < attackCount; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                angle = (intervalAngle * i) - (weightAngle * j);
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                angle = 45 * i;
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(0.3f);
        }
    }
    private IEnumerator Zigu03()
    {
        attackRate = 0.2f;
        attackCount = 12;
        intervalAngle = 30f;
        zigubool1 = true;
        anim.SetTrigger("Attack");
        yield return null;

    }
    private IEnumerator zigu03()
    {
        for (int i = 0; i < attackCount; i++)
        {
            Mdir = (player.position - shoot.transform.position).normalized;
            if (zigubool1)
            {
                Mdir = Quaternion.AngleAxis(-(intervalAngle * 2), Vector3.forward) * Mdir;
                zigubool1 = false;
            }
            else
            {
                //Mdir = Quaternion.AngleAxis(intervalAngle, Vector3.forward) * Mdir;
                zigubool1 = true;
            }
            for (int j = 0; j < 3; j++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                dir = Quaternion.AngleAxis(intervalAngle * j, Vector3.forward) * Mdir;
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, midScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
    }
    private IEnumerator zigu3()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return YieldInstructionCache.WaitForSeconds(0.6f);
            GameObject obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo2(5f, dftScale);
            }
        }
    }

    private IEnumerator Zigu04()
    {
        anim.SetTrigger("Attack");
        yield return null;
    }

    private IEnumerator zigu04()
    {
        for (int i = 0; i < 4; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
            obj.transform.position = player.position;
            if (obj.TryGetComponent<Warning>(out Warning warning1))
            {
                warning1.SetType(PoolState.ice, 0f);
                warning1.iceBorn = true;
            }
            yield return YieldInstructionCache.WaitForSeconds(0.5f);
        }
        obj = PoolManager.Inst.pools[(int)PoolState.warning].Pop();
        obj.transform.position = player.position;
        Vector3 pos = obj.transform.position;
        if (obj.TryGetComponent<Warning>(out Warning warning))
        {
            warning.ziguScale();
            warning.zigu = true;
        }
        yield return YieldInstructionCache.WaitForSeconds(1f);
        transform.position = pos;

    }

    private IEnumerator ZiguCircle02()
    {
        attackCount = 4;
        attackRate = 0.18f;
        intervalAngle = 90f;
        weightAngle = 14f;
        transform.position = Vector3.zero;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        anim.SetTrigger("Attack");
    }

    private IEnumerator ziguCircle02()
    {
        for (int j = 0; j < 30; j++)
        {
            for (int i = 0; i < attackCount; i++)
            {
                GameObject obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                angle = (intervalAngle * i) + (weightAngle * j);
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 4f, oriScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
    }

    private IEnumerator ziguCircle2()
    {

        for (int i = 0; i < 50; i++)
        {
            obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
            obj.transform.position = shoot.transform.position;
            angle = -80 * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 8f, dftScale);
            }
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                angle = (intervalAngle * i) - (weightAngle * j);
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 8f, dftScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(0.1f);
        }

    }

    private IEnumerator ZiguCircle03()
    {
        attackCount = 7;
        attackRate = 0.15f;
        intervalAngle = 360 / attackCount;
        weightAngle = intervalAngle / attackCount;
        transform.position = Vector3.zero;
        yield return YieldInstructionCache.WaitForSeconds(1f);
        anim.SetTrigger("Attack");
    }

    private IEnumerator ziguCircle03()
    {
        int count = 0;
        while (true)
        {
            for(int i = 0;i < attackCount;i++)
            {
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                angle = (intervalAngle * i) - (weightAngle * count);
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }
            }
            count++;
            yield return YieldInstructionCache.WaitForSeconds(attackRate);
        }
    }

    private IEnumerator ziguCircle3()
    {
        while (true)
        {
            for(int i = 0; i < 30; i++)
            {
                GameObject obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = shoot.transform.position;
                angle = 12 * i;
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(1.35f);
        }
    }


    #endregion


}

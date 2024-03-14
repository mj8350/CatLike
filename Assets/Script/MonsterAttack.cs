using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    CircleFire01, // ������ �߻�
    ThreeFire01, //�÷��̾���� 3��
}

public class MonsterAttack : MonoBehaviour
{
    private Animator anim;

    private float attackRate; //���� �ֱ�.
    private float attackCount; // ����ü ����.
    private float intervalAngle; // ����ü�߻� ����� �� ����ü �߻� ���� ������ ���� 
    private float weightAngle;      // ����ġ ����. 
    private AttackType currentAttack;


    private Transform player;
    private GameObject obj;
    private float angle;
    private Vector2 dir;

    //private Vector3 center = new Vector3(0f, 0.5f, 0f);

    private Vector3 dftScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 midScale = new Vector3(1f, 1f, 1f);
    private Vector3 bigScale = new Vector3(1.5f, 1.5f, 1.5f);

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log("MonsterAttack.cs - Awake() - anim ���� ����");
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
        }
    }

    private IEnumerator CircleFire01()
    {
        attackCount = 10;
        intervalAngle = 360f / attackCount;
        anim.SetTrigger("Attack");
        yield return null;
    }

    private void Circle01()
    {
        for(int i = 0; i<attackCount; i++)
        {
            obj = PoolManager.Inst.pools[1].Pop();
            obj.transform.position = transform.position; //+ center;
            angle = intervalAngle * i;
            dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
            dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
            if(obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 5f,dftScale);
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
        dir = (player.position - transform.position).normalized;
        dir = Quaternion.AngleAxis(-intervalAngle * 2f, Vector3.forward) * dir;
        for (int i = 0; i < attackCount; i++)
        {
            obj = PoolManager.Inst.pools[1].Pop();
            obj.transform.position = transform.position; //+ center;

            dir = Quaternion.AngleAxis(+intervalAngle, Vector3.forward) * dir;
            if (obj.TryGetComponent<Projectile>(out Projectile projectile))
            {
                projectile.MoveTo1(dir, 5f, midScale);
            }
        }
    }

}

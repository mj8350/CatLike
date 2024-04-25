using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : PoolLabel, IDamage
{

    private GameObject obj;
    private float angle;
    private Vector3 dir;
    private Vector3 dftScale = new Vector3(0.6f, 0.6f, 0.6f);

    public float HP;


    private void Awake()
    {
        HP = 10f;
    }

    private void Update()
    {
        if (HP <= 0)
        {
            ReturnPool();
            HP = 10f;
        }
    }

    public void IcePop()
    {
        StartCoroutine("icePop");
    }

    private IEnumerator icePop()
    {
		while (true)
		{
            yield return YieldInstructionCache.WaitForSeconds(5f);
            for(int i = 0; i < 6; i++)
			{
                obj = PoolManager.Inst.pools[(int)PoolState.projectile].Pop();
                obj.transform.position = transform.position;
                angle = 60 * i;
                dir.x = Mathf.Cos(angle * Mathf.Deg2Rad);
                dir.y = Mathf.Sin(angle * Mathf.Deg2Rad);
                if (obj.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.MoveTo1(dir, 5f, dftScale);
                }
            }
        }
        
    }

    public void TakeDamage(float damage)
	{
        Debug.Log($"아이스가 {damage}만큼의 데미지를 입음");
        HP -= damage;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player) && player.miss)
        {
            if (collision.gameObject.TryGetComponent<IDamage>(out IDamage damage))
            {
                damage.TakeDamage(5f);
            }
        }
    }
}

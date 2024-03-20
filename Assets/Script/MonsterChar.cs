using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : PoolLabel, IDamage
{
    public float HP;

	private void Awake()
	{
		HP = 30f;
	}

	private void Update()
	{
		if (HP <= 0)
		{
			//ReturnPool();
			Destroy(gameObject);
			HP = 30f;
		}
	}

	public void TakeDamage(float damage)
    {
        Debug.Log($"���Ͱ� {damage}�� �������� ����");
		HP -= damage;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.TryGetComponent<IDamage>(out IDamage damage))
		{
			damage.TakeDamage(5f);
		}
	}
}

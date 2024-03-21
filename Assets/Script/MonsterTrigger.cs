using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour, IDamage
{
	private MonsterChar monster;
	private void Awake()
	{
		monster = transform.GetComponentInParent<MonsterChar>();
	}

	public void TakeDamage(float damage)
	{
		Debug.Log($"���Ͱ� {damage}�� �������� ����");
		monster.HP -= damage;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<IDamage>(out IDamage damage))
		{
			damage.TakeDamage(5f);
		}
	}

}

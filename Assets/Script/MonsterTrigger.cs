using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour, IDamage
{
	private MonsterChar monster;
	private Vector3 vec;
	private void Awake()
	{
		monster = transform.GetComponentInParent<MonsterChar>();
	}

    private void Update()
    {
		vec = monster.transform.position;
		vec.y += 0.5f;
		transform.position = vec;
    }

    public void TakeDamage(float damage)
	{
		Debug.Log($"���Ͱ� {damage}�� �������� ����");
		monster.HP -= damage;
	}

	/*private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<IDamage>(out IDamage damage))
		{
			damage.TakeDamage(5f);
		}
	}*/

}

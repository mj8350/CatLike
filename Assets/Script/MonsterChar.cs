using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : PoolLabel
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
			StartCoroutine("Die");
			//Destroy(gameObject);
			
		}
	}
	private void FixedUpdate()
	{
		
	}

	private IEnumerator Die()
	{
		yield return YieldInstructionCache.WaitForSeconds(0.1f);
		ReturnPool();
		HP = 30f;
	}


}

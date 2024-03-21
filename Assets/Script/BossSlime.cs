using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : MonoBehaviour
{
	private GameObject obj;
	private MonsterChar monster;
	private Vector3 SpwanPos;
	public bool die;

	private void Awake()
	{
		if (!TryGetComponent<MonsterChar>(out monster))
			Debug.Log("BossSlime.cs - Awake() - monster 참조 오류");
		die = true;
	}

	private void Update()
	{
		if (monster.HP <= 0&&die)
		{
			SpwanSlime();
			die = false;
		}
	}
	private void FixedUpdate()
	{
		
	}
	[SerializeField]
	private PoolState PS;
	private void SpwanSlime()
	{
		SpwanPos = transform.position;
		SpwanPos.x -= 1;
		for(int i = 0; i < 2; i++)
		{
			obj = PoolManager.Inst.pools[(int)PS].Pop();
			obj.transform.position = SpwanPos;
			if (obj.TryGetComponent<TwoAi>(out TwoAi ai))
				ai.Creat();
			SpwanPos.x += 2;
		}

	}

	


}

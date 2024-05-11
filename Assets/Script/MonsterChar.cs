using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChar : PoolLabel
{
	[SerializeField]
	private float MaxHP;

    public float HP;

	public int phase = 0;

	private GameObject obj;

	private void Awake()
	{
		HP = MaxHP;
	}

	private void Update()
	{
		if (HP <= 0)
		{
			
			StartCoroutine("Die");
		}
	}

	private IEnumerator Die()
	{
		obj = PoolManager.Inst.pools[(int)PoolState.smoke].Pop();
		obj.transform.position = transform.GetChild(0).position;
		obj.transform.localScale = transform.localScale / 2;
		yield return YieldInstructionCache.WaitForSeconds(0.1f);
		GameManager.Instance.PlayerData.EXP+=5 ;
		GameManager.Instance.PlayerData.Gold++;
		ReturnPool();
		HP = MaxHP;
	}

	public void ChangePhase(int PH,int ps)
    {
		if (HP <= MaxHP / 100 * PH)
			phase=ps;
		
    }


}

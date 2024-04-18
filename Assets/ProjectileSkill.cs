using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : SkillComponent
{
    public override void ActiveSkill()
    {
        base.ActiveSkill();

        Debug.Log("프로젝타일 발사");
    }
}

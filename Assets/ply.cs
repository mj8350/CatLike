using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ply : MonoBehaviour
{
    private List<SkillComponent> skills;


    private void Update()
    {
         skills.Add(gameObject.AddComponent<ProjectileSkill>());
    }
}

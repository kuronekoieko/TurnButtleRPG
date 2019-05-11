using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] Skill skill;
    [SerializeField] UserData _userData;

    public static List<SkillInfo> skillList;

    public static UserData userData;

    public void Init()
    {
        skillList = skill.list;
        userData = _userData;
    }
}

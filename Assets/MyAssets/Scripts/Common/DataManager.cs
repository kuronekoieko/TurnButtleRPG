using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] CharactorStatus charactorStatus;
    [SerializeField] Skill skill;

    public static List<Parametor> charactorStatusList;
    public static List<SkillInfo> skillList;

    public void Init()
    {
        charactorStatusList = charactorStatus.list;
        skillList = skill.list;

    }
}

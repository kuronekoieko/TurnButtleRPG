using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create Skill", fileName = "Skill")]
public class Skill : ScriptableObject
{
    public List<SkillInfo> list = new List<SkillInfo>();
}

[System.Serializable]
public class SkillInfo
{
    public int skillID;
    public string name;
    public int ATK;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バトル時の情報を格納
/// </summary>
public class BattleStatus
{
    static BattleStatus _i = new BattleStatus();
    public static BattleStatus i
    {
        get { return _i; }
    }

    //ターンごとの攻撃情報を格納する
    public List<BattleInfo> players = new List<BattleInfo>();
    public SkillButton[] skillButtons;
    public OwnedCharactor[] partyMembers;
    public PlayerUI[] playerUIs;
}

public class BattleInfo
{
    public int partyMemberIndex;
    public int skillID;

    public BattleInfo(int skillID)
    {
        this.skillID = skillID;
    }
}
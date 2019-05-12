using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ターンごとの攻撃情報を格納する
/// </summary>
public class ButtleStatus
{
    static ButtleStatus _i = new ButtleStatus();
    public static ButtleStatus i
    {
        get { return _i; }
    }

    //キャラクターごとの選択したスキルを格納するリスト
    public List<ButtleInfo> players = new List<ButtleInfo>();
    public SkillButton[] skillButtons;
    public OwnedCharactor[] partyMembers;
    public PlayerUI[] playerUIs;
}

public class ButtleInfo
{
    public int skillID;

    public ButtleInfo(int skillID)
    {
        this.skillID = skillID;
    }
}



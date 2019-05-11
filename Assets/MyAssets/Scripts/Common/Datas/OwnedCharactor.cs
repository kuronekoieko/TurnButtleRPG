using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OwnedCharactor
{
    public CharactorSpec charactorSpec;
    public Status status;
}

[System.Serializable]
public class Status
{
    public int level;
    public int exp;
    public int hp;
    public int mp;
    public int speed;

    public int[] buttleSkills = new int[4];
    public List<int> ownedSkills = new List<int>();

}
